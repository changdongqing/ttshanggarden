// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Iot.Tools;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Gardener.Iot.Impl.Tools
{
    /// <summary>
    /// 设备链接工具
    /// </summary>
    internal class DeviceConnectionTool : IDeviceConnectionTool
    {

        private readonly ICache cache;
        private readonly IDeviceSystemLogService deviceSystemLogService;
        private readonly IRepository<DeviceConnection, GardenerIgnoreAuditDbContextLocator> ignoreAuditRepository;
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="deviceSystemLogService"></param>
        /// <param name="ignoreAuditRepository"></param>
        /// <param name="serviceProvider"></param>
        public DeviceConnectionTool(ICache cache, IDeviceSystemLogService deviceSystemLogService, IRepository<DeviceConnection, GardenerIgnoreAuditDbContextLocator> ignoreAuditRepository, IServiceProvider serviceProvider)
        {
            this.cache = cache;
            this.deviceSystemLogService = deviceSystemLogService;
            this.ignoreAuditRepository = ignoreAuditRepository;
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 更新指定字段
        /// </summary>
        /// <param name="input"></param>
        /// <param name="clearCache"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public async Task<bool> UpdateIncludeNow(DeviceConnectionDto input, bool clearCache, string[] propertyNames)
        {
            EntityEntry<DeviceConnection> entityEntry = await ignoreAuditRepository.UpdateIncludeNowAsync(input.Adapt<DeviceConnection>(), propertyNames);
            //发送通知
            await EntityEventNotityUtil.NotifyUpdateAsync(entityEntry.Entity);
            if (clearCache)
            {
                //移除缓存
                await TryDeviceConnectionRemoveCache(input);
            }
            return true;

        }

        /// <summary>
        /// 清空设备连接缓存
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async Task TryDeviceConnectionRemoveCache(DeviceConnectionDto? connection)
        {
            if (connection != null)
            {
                string key = IotCacheKeyConstants.GetConnectingDeviceConnectionCacheKey(connection.DeviceClientId);
                //移除缓存
                await cache.RemoveAsync(key);
                await cache.RemoveAsync(key + ":null_cache");
                if (connection.DeviceId.HasValue)
                {
                    string key1 = IotCacheKeyConstants.GetConnectingDeviceConnectionCacheKey(connection.DeviceId.Value);
                    //移除缓存
                    await cache.RemoveAsync(key1);
                    await cache.RemoveAsync(key1 + ":null_cache");
                }

            }
        }

        /// <summary>
        /// 获取连接中的连接信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private async Task<DeviceConnectionDto?> GetConnectingDeviceConnectionFromDb(string clientId)
        {
            var temp = await ignoreAuditRepository.AsQueryable(false)
                .Where(x => x.DeviceClientId.Equals(clientId) && x.DeviceConnectionState.Equals(DeviceConnectionState.Connecting) && x.IsDeleted == false && x.IsLocked == false)
                .Select(x => x.Adapt<DeviceConnectionDto>())
                .FirstOrDefaultAsync();

            return temp;
        }

        /// <summary>
        /// 获取连接中的连接信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        private async Task<DeviceConnectionDto?> GetConnectingDeviceConnectionFromDb(Guid deviceId)
        {
            var temp = await ignoreAuditRepository.AsQueryable(false)
                 .Where(x => x.DeviceId.HasValue && x.DeviceId.Equals(deviceId) && x.DeviceConnectionState.Equals(DeviceConnectionState.Connecting) && x.IsDeleted == false && x.IsLocked == false)
                 .Select(x => x.Adapt<DeviceConnectionDto>())
                 .FirstOrDefaultAsync();
            return temp;
        }

        /// <summary>
        /// 获取连接中的连接信息
        /// </summary>
        /// <remarks>
        /// <see cref="Core.DefaultDeviceCommunicationCableSplicer"/>中调用比较频繁，已缓存，获取到的DeviceLastPingTime和DeviceLastPushDataTime可能不准确
        /// </remarks>
        /// <param name="clientId"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public async Task<DeviceConnectionDto?> GetConnectingDeviceConnection(string clientId, bool useCache = true)
        {
            if (!useCache)
            {
                return await GetConnectingDeviceConnectionFromDb(clientId);
            }
            string key = IotCacheKeyConstants.GetConnectingDeviceConnectionCacheKey(clientId);
            DeviceConnectionDto? data = await cache.GetAsync<DeviceConnectionDto>(key);
            if (data != null)
            {
                return data;
            }
            bool noFind = await cache.GetAsync<bool>(key + ":null_cache", () =>
            {
                return Task.FromResult(false);
            });
            if (noFind)
            {
                return null;
            }
            data = await cache.GetAsync(key, async () =>
            {
                var temp = await GetConnectingDeviceConnectionFromDb(clientId);
                if (temp != null)
                {
                    return temp;
                }
                //未查到数据时，120秒内不再查询
                await cache.SetAsync(key + ":null_cache", true, TimeSpan.FromMinutes(2));
                return null;
            }, TimeSpan.FromMinutes(60));

            return data;
        }

        /// <summary>
        /// 获取连接中的连接信息
        /// </summary>
        /// <remarks>
        /// <see cref="Core.DefaultDeviceCommunicationCableSplicer"/>中调用比较频繁，已缓存，获取到的DeviceLastPingTime和DeviceLastPushDataTime可能不准确
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public async Task<DeviceConnectionDto?> GetConnectingDeviceConnection(Guid deviceId, bool useCache = true)
        {
            if (!useCache)
            {
                return await GetConnectingDeviceConnectionFromDb(deviceId);
            }
            string key = IotCacheKeyConstants.GetConnectingDeviceConnectionCacheKey(deviceId);

            DeviceConnectionDto? data = await cache.GetAsync<DeviceConnectionDto>(key);
            if (data != null)
            {
                return data;
            }
            bool noFind = await cache.GetAsync<bool>(key + ":null_cache", () =>
            {
                return Task.FromResult(false);
            });
            if (noFind)
            {
                return null;
            }
            data = await cache.GetAsync(key, async () =>
            {
                var temp = await GetConnectingDeviceConnectionFromDb(deviceId);
                if (temp != null)
                {
                    return temp;
                }
                //未查到数据时，120秒内不再查询
                await cache.SetAsync(key + ":null_cache", true, TimeSpan.FromMinutes(2));
                return null;
            }, TimeSpan.FromMinutes(60));

            return data;
        }

        /// <summary>
        /// 获取超时的连接
        /// </summary>
        /// <remarks>
        /// 5分钟未发起心跳或者未推送数据，认为是超时链接，将标记为已断开链接
        /// </remarks>
        /// <returns></returns>
        public Task<List<DeviceConnectionDto>> GetTimeoutConnections()
        {
            DateTimeOffset timeout = DateTimeOffset.Now.AddSeconds(-300);
            return ignoreAuditRepository.AsQueryable(false)
                .Where(x => x.DeviceConnectionState.Equals(DeviceConnectionState.Connecting)
                && (
                //都不为空时，必须都超时即认为超时
                (x.DeviceLastPingTime != null && x.DeviceLastPingTime < timeout && x.DeviceLastPushDataTime != null && x.DeviceLastPushDataTime < timeout) ||
                //只有推送数据时间
                (x.DeviceLastPingTime == null && x.DeviceLastPushDataTime != null && x.DeviceLastPushDataTime < timeout) ||
                //只有心跳时间
                (x.DeviceLastPingTime != null && x.DeviceLastPingTime < timeout && x.DeviceLastPushDataTime == null) ||
                //仅创建了
                (x.CreatedTime < timeout && x.DeviceLastPushDataTime == null && x.DeviceLastPingTime == null)
                )
                )
                .Select(x => x.Adapt<DeviceConnectionDto>()).ToListAsync();
        }

        /// <summary>
        /// 添加系统日志
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="logType"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task AddSystemLog(DeviceConnectionDto connection, DeviceSystemLogType logType, string? content)
        {
            return deviceSystemLogService.Insert(new DeviceSystemLogDto()
            {
                DeviceClientId = connection.DeviceClientId,
                DeviceConnectionId = connection.Id,
                DeviceId = connection.DeviceId,
                SystemLogType = DeviceSystemLogType.DeviceDisconnect,
                Content = content,
                TenantId = connection.TenantId
            });
        }

        public async Task DisconnectTimeoutConnection(DeviceConnectionDto deviceConnection)
        {
            string connectionFlag = $"{deviceConnection.DeviceConnectionType}连接不支持";
            IDeviceCommunicationControlService? communicationControlService = serviceProvider.GetKeyedService<IDeviceCommunicationControlService>(deviceConnection.DeviceConnectionType);
            if (communicationControlService != null)
            {

                var result = await communicationControlService.DisconnectClient(deviceConnection.DeviceClientId);
                connectionFlag = $"{deviceConnection.DeviceConnectionType}连接{(result ? "断开成功" : "断开失败")}";
            }

            deviceConnection.DeviceConnectionState = DeviceConnectionState.Disconnect;
            deviceConnection.DeviceDisconnectReason = DeviceDisconnectReason.KeepAliveTimeout;
            deviceConnection.DeviceDisconnectReasonDescription = "任务扫描到连接已经超时";
            deviceConnection.DeviceDisconnectTime = DateTimeOffset.Now;
            await UpdateIncludeNow(deviceConnection, true, [
                nameof(DeviceConnectionDto.DeviceConnectionState),
                    nameof(DeviceConnectionDto.DeviceDisconnectReason),
                    nameof(DeviceConnectionDto.DeviceDisconnectReasonDescription),
                    nameof(DeviceConnectionDto.DeviceDisconnectTime)]);
            await AddSystemLog(deviceConnection, DeviceSystemLogType.DeviceDisconnect, $"任务扫描到连接已经超时,主动更新为断开:{connectionFlag}");
        }
    }
}
