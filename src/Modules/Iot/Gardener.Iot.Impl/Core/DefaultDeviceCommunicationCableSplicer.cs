// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Localization;
using Gardener.Core.Resources;
using Gardener.Iot.Impl.Core.Options;
using Gardener.Iot.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Buffers;
using System.Collections.Concurrent;

namespace Gardener.Iot.Impl.Core
{
    /// <summary>
    /// 默认的设备通讯对接器
    /// </summary>
    /// <remarks>
    /// <para>连接新增或修改状态时，会清理缓存</para>
    /// <para>连接的 ping 和 数据最后更新时间，更新时，只更新数据库,不清理和更新缓存</para>
    /// </remarks>
    public class DefaultDeviceCommunicationCableSplicer : IDeviceCommunicationCableSplicer
    {
        private readonly ILogger<DefaultDeviceCommunicationCableSplicer> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IOptions<IotOptions> iotOptions;
        private readonly IDeviceDataHandler deviceDataHandler;
        private static ConcurrentDictionary<string, DateTimeOffset> deviceLastPushDataTimes = new ConcurrentDictionary<string, DateTimeOffset>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="iotOptions"></param>
        /// <param name="logger"></param>
        /// <param name="deviceDataHandler"></param>
        public DefaultDeviceCommunicationCableSplicer(IServiceScopeFactory serviceScopeFactory, IOptions<IotOptions> iotOptions, ILogger<DefaultDeviceCommunicationCableSplicer> logger, IDeviceDataHandler deviceDataHandler)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.iotOptions = iotOptions;
            this.logger = logger;
            this.deviceDataHandler = deviceDataHandler;
        }

        /// <summary>
        /// 收到设备数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="userProperties"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        public async Task OnApplicationMessageReceived(string clientId, DeviceConnectionType deviceConnectionType, ReadOnlySequence<byte>? content, DeviceDataContentType? contentType = null, IEnumerable<KeyValuePair<string, string>>? userProperties = null, string? topic = null)
        {
            if (topic!=null  && ("0".Equals(topic) || "temp1".Equals(topic) || "temp2".Equals(topic)))
            {
                logger.LogWarning($"OnClientDisconnected clientId {clientId} 过滤topic{topic}数据");
                return;
            }
            DateTimeOffset receivedTime = DateTimeOffset.Now;
            using var scope = serviceScopeFactory.CreateScope();
            IDeviceConnectionTool connectionTool = scope.ServiceProvider.GetRequiredService<IDeviceConnectionTool>();
            var connection = await connectionTool.GetConnectingDeviceConnection(clientId);
            DeviceDto? device = null;
            if (connection != null && connection.DeviceId != null)
            {
                var options = iotOptions.Value;
                DateTimeOffset deviceLastPushDataTime = deviceLastPushDataTimes.GetOrAdd(clientId, DateTimeOffset.Now);
                //可能频率较高,根据设置的最小间隔来更新
                if ((DateTimeOffset.Now - deviceLastPushDataTime).TotalMilliseconds > options.UpdateLastPushDataTimeMinIntervalMilliseconds)
                {
                    connection.DeviceLastPushDataTime = DateTimeOffset.Now;
                    await connectionTool.UpdateIncludeNow(connection, false, new string[] { nameof(DeviceConnection.DeviceLastPushDataTime) });
                    deviceLastPushDataTimes.AddOrUpdate(clientId, DateTimeOffset.Now, (_, _) => DateTimeOffset.Now);
                }
                var deviceService = scope.ServiceProvider.GetRequiredService<IDeviceService>();
                device = await deviceService.TryGetByClientId(clientId);
            }
            Dictionary<string, object> extendData = new Dictionary<string, object>()
            {
                {"topic",topic??string.Empty }
            };
            
            await deviceDataHandler.Handler(clientId, deviceConnectionType, contentType, content, receivedTime, userProperties, connection, device, extendData);
        }

        /// <summary>
        /// 设备已连接成功（注意是已验证通过）
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="clientEndpoint"></param>
        /// <param name="account"></param>
        /// <param name="userProperties"></param>
        /// <returns></returns>
        public async Task OnClientConnected(string clientId, DeviceConnectionType deviceConnectionType, string clientEndpoint, string? account = null, IEnumerable<KeyValuePair<string, string>>? userProperties = null)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var deviceService = scope.ServiceProvider.GetRequiredService<IDeviceService>();
            DeviceDto? device = await deviceService.TryGetByClientId(clientId);
            IDeviceConnectionService connectionService = scope.ServiceProvider.GetRequiredService<IDeviceConnectionService>();
            IDeviceConnectionTool connectionTool = scope.ServiceProvider.GetRequiredService<IDeviceConnectionTool>();
            var connection = await connectionTool.GetConnectingDeviceConnection(clientId, false);
            //未在数据库找到连接，或者连接类型不一致
            if (connection == null || !deviceConnectionType.Equals(connection.DeviceConnectionType))
            {
                connection = new DeviceConnection()
                {
                    DeviceClientId = clientId,
                    DeviceClientEndpoint = clientEndpoint,
                    DeviceConnectionType = deviceConnectionType,
                    DeviceId = device?.Id,
                    DeviceAccount = account,
                    UserProperties = userProperties == null ? null : System.Text.Json.JsonSerializer.Serialize(userProperties),
                    DeviceConnectionState = DeviceConnectionState.Connecting,
                    TenantId = device?.TenantId
                };
                connection = await connectionService.Insert(connection);
                await AddDeviceSystemLog(clientId, DeviceSystemLogType.DeviceConnectSucceed, "连接成功", connection.Id, connection.DeviceId, connection.TenantId, account);

            }
            else
            {
                connection.DeviceClientEndpoint = clientEndpoint;
                connection.DeviceId = device?.Id;
                connection.DeviceAccount = account;
                connection.UserProperties = userProperties == null ? null : System.Text.Json.JsonSerializer.Serialize(userProperties);
                connection.DeviceConnectionState = DeviceConnectionState.Connecting;
                connection.TenantId = device?.TenantId;
                await connectionTool.UpdateIncludeNow(connection, true, new string[] {
                        nameof(DeviceConnection.DeviceClientEndpoint),
                        nameof(DeviceConnection.DeviceId),
                        nameof(DeviceConnection.DeviceAccount),
                        nameof(DeviceConnection.UserProperties),
                        nameof(DeviceConnection.DeviceConnectionState),
                        nameof(DeviceConnection.TenantId)
                });
                await AddDeviceSystemLog(clientId, DeviceSystemLogType.DeviceConnectSucceed, "重新连接成功", connection.Id, connection.DeviceId, connection.TenantId, account);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="clientEndpoint"></param>
        /// <param name="userProperties"></param>
        /// <param name="disconnectReason"></param>
        /// <param name="disconnectReasonDescription"></param>
        /// <returns></returns>
        public async Task OnClientDisconnected(string clientId, DeviceConnectionType deviceConnectionType, string? clientEndpoint, IEnumerable<KeyValuePair<string, string>>? userProperties = null, DeviceDisconnectReason disconnectReason = DeviceDisconnectReason.Other, string? disconnectReasonDescription = null)
        {
            deviceLastPushDataTimes.TryRemove(clientId, out _);
            using var scope = serviceScopeFactory.CreateScope();
            IDeviceConnectionTool connectionTool = scope.ServiceProvider.GetRequiredService<IDeviceConnectionTool>();
            var connection = await connectionTool.GetConnectingDeviceConnection(clientId,false);
            if (connection != null && connection.DeviceConnectionType.Equals(deviceConnectionType))
            {
                connection.DeviceConnectionState = DeviceConnectionState.Disconnect;
                connection.DeviceDisconnectReason = disconnectReason;
                connection.DeviceDisconnectReasonDescription = disconnectReasonDescription;
                connection.DeviceDisconnectTime = DateTime.UtcNow;
                await connectionTool.UpdateIncludeNow(connection, true, new string[] {
                        nameof(DeviceConnection.DeviceConnectionState),
                        nameof(DeviceConnection.DeviceDisconnectReason),
                        nameof(DeviceConnection.DeviceDisconnectReasonDescription),
                        nameof(DeviceConnection.DeviceDisconnectTime)
                });
                await AddDeviceSystemLog(clientId, DeviceSystemLogType.DeviceDisconnect, "断开连接", connection.Id, connection.DeviceId, connection.TenantId);
            }
            else
            {
                logger.LogWarning($"OnClientDisconnected clientId {clientId} 在 {nameof(DeviceConnection)}中不存在{deviceConnectionType}类型连接");
                await AddDeviceSystemLog(clientId, DeviceSystemLogType.DeviceDisconnect, $"断开连接，但未找到当前{deviceConnectionType}类型连接");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="clientEndpoint"></param>
        /// <param name="account"></param>
        /// <param name="secretKey"></param>
        /// <param name="userProperties"></param>
        /// <returns></returns>
        public async Task<ConnectionIdentityAuthenticationState> OnConnectionVerify(string clientId, DeviceConnectionType deviceConnectionType, string clientEndpoint, string? account = null, string? secretKey = null, IEnumerable<KeyValuePair<string, string>>? userProperties = null)
        {

            logger.LogInformation("iot OnConnectionVerify {0} {1} {2} {3}", clientId, deviceConnectionType, account, secretKey);
            IotOptions iotConfig = iotOptions.Value;
            using var scope = serviceScopeFactory.CreateScope();
            var deviceService = scope.ServiceProvider.GetRequiredService<IDeviceService>();
            DeviceDto? device = await deviceService.TryGetByClientId(clientId);
            //允许通过
            bool allow = true;
            //拒绝原因
            string? rejectReasonDescription = null;
            //不允许匿名连接，进行验证
            if (!iotConfig.AllowAnonymousDeviceConnect)
            {
                if (device == null)
                {
                    //设备未找到
                    allow = false;
                    rejectReasonDescription = string.Format(Lo.GetValue<SharedLocalResource>(nameof(SharedLocalResource.Item_Data_Not_Find)), Lo.GetValue<IotLocalResource>(nameof(IotLocalResource.Device)));
                }
                else if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(secretKey))
                {
                    //id和密钥不能为空
                    allow = false;
                    rejectReasonDescription = Lo.GetValue<IotLocalResource>(nameof(IotLocalResource.DeviceIdentityIsNullOrEmpty));
                }
                else
                {
                    //验证不通过
                    var result = await deviceService.VerifySecretKey(device.Id, account, secretKey);
                    if (!result)
                    {
                        allow = false;
                        rejectReasonDescription = Lo.GetValue<IotLocalResource>(nameof(IotLocalResource.DeviceIdentityIsInvalid));
                    }
                }

            }
            //允许通过
            if (allow)
            {
                return ConnectionIdentityAuthenticationState.Succeed;
            }
            //不允许通过,验证失败，更新或记录一个已断开的连接
            IDeviceConnectionTool connectionTool = scope.ServiceProvider.GetRequiredService<IDeviceConnectionTool>();
            DeviceConnectionDto? connection = await connectionTool.GetConnectingDeviceConnection(clientId, false);
            if (connection != null && connection.DeviceConnectionType.Equals(deviceConnectionType))
            {
                connection.DeviceConnectionState = DeviceConnectionState.Disconnect;
                connection.DeviceDisconnectReason = DeviceDisconnectReason.NotAuthorized;
                connection.DeviceDisconnectReasonDescription = rejectReasonDescription;
                connection.DeviceDisconnectTime = DateTimeOffset.Now;
                await connectionTool.UpdateIncludeNow(connection, true,
                [
                        nameof(DeviceConnection.DeviceConnectionState),
                        nameof(DeviceConnection.DeviceDisconnectReason),
                        nameof(DeviceConnection.DeviceDisconnectReasonDescription),
                        nameof(DeviceConnection.DeviceDisconnectTime),
                ]);
            }
            else
            {
                IDeviceConnectionService connectionService = scope.ServiceProvider.GetRequiredService<IDeviceConnectionService>();

                connection = new DeviceConnection()
                {
                    DeviceClientId = clientId,
                    DeviceClientEndpoint = clientEndpoint,
                    DeviceConnectionType = deviceConnectionType,
                    UserProperties = userProperties == null ? null : System.Text.Json.JsonSerializer.Serialize(userProperties),
                    DeviceConnectionState = DeviceConnectionState.Disconnect,
                    DeviceDisconnectReason = DeviceDisconnectReason.NotAuthorized,
                    DeviceDisconnectTime = DateTimeOffset.Now,
                    DeviceAccount = account,
                    DeviceId = device?.Id,
                    TenantId = device?.TenantId,
                    DeviceDisconnectReasonDescription = rejectReasonDescription
                };
                connection = await connectionService.Insert(connection);
            }
            //记录日志
            await AddDeviceSystemLog(clientId, DeviceSystemLogType.DeviceConnectFailed, "连接验证失败", connection.Id, connection.DeviceId, connection.TenantId, account);
            return ConnectionIdentityAuthenticationState.BadDeviceIdOrSecretKey;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task OnPingClient(string clientId, DeviceConnectionType deviceConnectionType, string? endpoint = null)
        {
            logger.LogInformation("iot OnPingClient {0} {1} {2}", clientId, deviceConnectionType, endpoint);
            using var scope = serviceScopeFactory.CreateScope();
            IDeviceConnectionTool connectionTool = scope.ServiceProvider.GetRequiredService<IDeviceConnectionTool>();
            var connection = await connectionTool.GetConnectingDeviceConnection(clientId, false);
            if (connection != null && deviceConnectionType.Equals(connection.DeviceConnectionType))
            {
                connection.DeviceLastPingTime = DateTimeOffset.Now;
                await connectionTool.UpdateIncludeNow(connection, false, [nameof(DeviceConnection.DeviceLastPingTime)]);
            }
            else
            {
                logger.LogWarning($"OnPingClient clientId {clientId} 在 {nameof(DeviceConnection)}中不存在");
            }
        }

        /// <summary>
        /// 添加设备系统日志
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceSystemLogType"></param>
        /// <param name="content"></param>
        /// <param name="deviceConnectionId"></param>
        /// <param name="deviceId"></param>
        /// <param name="tenantId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        private async Task AddDeviceSystemLog(string clientId, DeviceSystemLogType deviceSystemLogType, string? content = null, long? deviceConnectionId = null, Guid? deviceId = null, Guid? tenantId = null, string? account = null)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                IDeviceSystemLogService systemLogService = scope.ServiceProvider.GetRequiredService<IDeviceSystemLogService>();

                await systemLogService.Insert(new DeviceSystemLogDto()
                {
                    DeviceClientId = clientId,
                    DeviceConnectionId = deviceConnectionId,
                    DeviceId = deviceId,
                    SystemLogType = deviceSystemLogType,
                    Content = content,
                    TenantId = tenantId,
                    DeviceAccount = account
                });
            }
        }
    }
}
