// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Iot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet.Diagnostics;
using MQTTnet.Server;
using MQTTnet;
using TTShang.Iot.Enums;
using MQTTnet.Protocol;
using MQTTnet.Packets;
using System.Text;
using TTShang.Iot.Dtos;
using MQTTnet.Diagnostics.Logger;
using System.Buffers;

namespace TTShang.Iot.Server.Mqtt
{
    /// <summary>
    /// mqtt通讯服务
    /// </summary>
    /// <remarks>
    /// <para>本服务，启动了一个mqtt服务器和一个mqtt客户端。</para>
    /// <para>服务器：用于所有设备进行连接和接收设备数据</para>
    /// <para>客户端：用于发送数据给服务器，从而转发给设备</para>
    /// </remarks>
    public class MqttDeviceCommunicationService : IDeviceCommunicationService, IDeviceCommunicationControlService
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private readonly ILogger<MqttDeviceCommunicationService> logger;
        /// <summary>
        /// 服务器配置
        /// </summary>
        private readonly IOptions<MqttServerOptions> serverOptions;
        /// <summary>
        /// 服务容器
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// mqtt 服务器
        /// </summary>
        private MqttServer? mqttServer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serverOPtions"></param>
        /// <param name="serviceProvider"></param>
        public MqttDeviceCommunicationService(ILogger<MqttDeviceCommunicationService> logger, IOptions<MqttServerOptions> serverOPtions, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serverOptions = serverOPtions;
            this.serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 服务启动时
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken, IDeviceCommunicationCableSplicer communicationCableSplicer)
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 服务执行
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(CancellationToken stoppingToken, IDeviceCommunicationCableSplicer communicationCableSplicer)
        {
            MqttServerOptions options = serverOptions.Value;

            IMqttNetLogger? mqttNetLogger = serviceProvider.GetService<IMqttNetLogger>() ?? new MqttNetLogger(logger, options.LoggerIsEnabled);

            var mqttFactory = new MqttServerFactory(mqttNetLogger);

            var mqttServerOptions = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
                .WithDefaultEndpointPort(options.Port)
                .Build();
            mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
            //验证连接
            mqttServer.ValidatingConnectionAsync += async e =>
            {
                var userProperties = e.UserProperties?.Select(x => new KeyValuePair<string, string>(x.Name, x.Value)).ToList();
                var state = await communicationCableSplicer.OnConnectionVerify(e.ClientId, DeviceConnectionType.Mqtt, e.Endpoint, e.UserName, e.Password, userProperties);
                //验证失败
                switch (state)
                {
                    case ConnectionIdentityAuthenticationState.BadDeviceIdOrSecretKey: e.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword; break;
                }
            };
            //连接成功
            mqttServer.ClientConnectedAsync += e =>
            {
                var userProperties = e.UserProperties?.Select(x => new KeyValuePair<string, string>(x.Name, x.Value)).ToList();

                return communicationCableSplicer.OnClientConnected(e.ClientId, DeviceConnectionType.Mqtt, e.Endpoint, e.UserName, userProperties);
            };
            //断开连接
            mqttServer.ClientDisconnectedAsync += e =>
            {
                var userProperties = e.UserProperties?.Select(x => new KeyValuePair<string, string>(x.Name, x.Value)).ToList();

                DeviceDisconnectReason reasonCode = DeviceDisconnectReason.NormalDisconnection;
                if (e.ReasonCode != null)
                {
                    switch (e.ReasonCode)
                    {
                        case MqttDisconnectReasonCode.NormalDisconnection: reasonCode = DeviceDisconnectReason.NormalDisconnection; break;
                        case MqttDisconnectReasonCode.AdministrativeAction: reasonCode = DeviceDisconnectReason.AdministrativeAction; break;
                        case MqttDisconnectReasonCode.ConnectionRateExceeded: reasonCode = DeviceDisconnectReason.ConnectionRateExceeded; break;
                        case MqttDisconnectReasonCode.ServerBusy: reasonCode = DeviceDisconnectReason.ServerBusy; break;
                        case MqttDisconnectReasonCode.KeepAliveTimeout: reasonCode = DeviceDisconnectReason.KeepAliveTimeout; break;
                        case MqttDisconnectReasonCode.NotAuthorized: reasonCode = DeviceDisconnectReason.NotAuthorized; break;
                        default: reasonCode = DeviceDisconnectReason.Other; break;

                    }
                }
                return communicationCableSplicer.OnClientDisconnected(e.ClientId, DeviceConnectionType.Mqtt, e.Endpoint, userProperties, reasonCode, e.ReasonCode?.ToString());
            };
            //收到消息
            mqttServer.InterceptingInboundPacketAsync += e =>
            {
                if (e.Packet is MqttPingReqPacket)
                {
                    //ping
                    return communicationCableSplicer.OnPingClient(e.ClientId, DeviceConnectionType.Mqtt, e.Endpoint);
                }
                return Task.CompletedTask;
            };
            //收到推送
            mqttServer.InterceptingPublishAsync += e =>
            {
                DeviceDataContentType? deviceDataContentType = null;
                IEnumerable<KeyValuePair<string, string>>? userProperties = null;
                //记录下所有客户端发送的消息
                if (e.ApplicationMessage.ContentType != null)
                {
                    deviceDataContentType = new DeviceDataContentType(e.ApplicationMessage.ContentType);
                }
                if (e.ApplicationMessage.UserProperties != null)
                {
                    userProperties = e.ApplicationMessage.UserProperties.Select(x => new KeyValuePair<string, string>(x.Name, x.Value)).ToList();
                }
                return communicationCableSplicer.OnApplicationMessageReceived(e.ClientId, DeviceConnectionType.Mqtt, e.ApplicationMessage.Payload, deviceDataContentType, userProperties, e.ApplicationMessage.Topic);
            };
            //客户端订阅topic
            mqttServer.ClientSubscribedTopicAsync += MqttServer_ClientSubscribedTopicAsync;
            mqttServer.ApplicationMessageNotConsumedAsync += MqttServer_ApplicationMessageNotConsumedAsync;
            await mqttServer.StartAsync();
        }

        private Task MqttServer_ApplicationMessageNotConsumedAsync(ApplicationMessageNotConsumedEventArgs arg)
        {
            logger.LogInformation($"mqtt conten {Convert.ToHexString(arg.ApplicationMessage.Payload.ToArray())}  topic {arg.ApplicationMessage.Topic} not consumed");
            return Task.CompletedTask;
        }

        private Task MqttServer_ClientSubscribedTopicAsync(ClientSubscribedTopicEventArgs arg)
        {
            logger.LogInformation($"mqtt {arg.ClientId} subscribed topic {arg.TopicFilter.ToString()}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 向所有客户端发送消息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public Task<bool> SendMesaageToAllClient(string content, DeviceDataContentType? contentType = null)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return SendMesaageToAllClient(bytes, contentType);
        }
        /// <summary>
        /// 向所有客户端发送消息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<bool> SendMesaageToAllClient(byte[] content, DeviceDataContentType? contentType = null)
        {
            if (mqttServer != null)
            {
                MqttServerOptions options = serverOptions.Value;
                var applicationMessageBuider = new MqttApplicationMessageBuilder()
                           .WithTopic(options.ClientSubscribeAllDataTopic)
                           .WithPayload(content);
                if (contentType != null)
                {
                    applicationMessageBuider.WithContentType(contentType.ToString());
                }
                var applicationMessage = applicationMessageBuider.Build();
                await mqttServer.InjectApplicationMessage(new InjectedMqttApplicationMessage(applicationMessage)
                {
                    SenderClientId = options.SenderClientId
                });
                return true;
            }

            return false;
        }
        /// <summary>
        /// 向指定客户端发送消息
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public Task<bool> SendMesaageToClient(string clientId, string content, DeviceDataContentType? contentType = null)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);

            return SendMesaageToClient(clientId, bytes, contentType);
        }
        /// <summary>
        /// 向指定客户端发送消息
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<bool> SendMesaageToClient(string clientId, byte[] content, DeviceDataContentType? contentType = null)
        {
            if (mqttServer != null)
            {
                IList<MqttClientStatus> mqttClients = await mqttServer.GetClientsAsync();
                var client = mqttClients.FirstOrDefault(x => x.Id.Equals(clientId));
                //未连接
                if (client == null)
                {
                    logger.LogError("SendMesaageToClient 所有client {} 未找到mqtt client {}", string.Join(",", mqttClients.Select(x => x.Id + "_" + x.Session.Id)), clientId);
                    return false;
                }
                MqttServerOptions options = serverOptions.Value;
                string topic = options.ClientSubscribeSelfDataTopicPrefix + clientId;
                var applicationMessageBuider = new MqttApplicationMessageBuilder()
                           .WithTopic(topic)
                           .WithPayload(content);
                if (contentType != null)
                {
                    applicationMessageBuider.WithContentType(contentType.ToString());
                }
                var applicationMessage = applicationMessageBuider.Build();
                await mqttServer.InjectApplicationMessage(new InjectedMqttApplicationMessage(applicationMessage)
                {
                    SenderClientId = options.SenderClientId,
                    SenderUserName = options.SenderClientId,
                });
                logger.LogInformation("SendMesaageToClient client {} topic {} SenderClientId {}", clientId, topic, options.SenderClientId);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken, IDeviceCommunicationCableSplicer communicationCableSplicer)
        {
            if (mqttServer != null)
            {
                await mqttServer.StopAsync();
                mqttServer.Dispose();
            }
        }
        /// <summary>
        /// 断开客户端连接
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<bool> DisconnectClient(string clientId)
        {
            if (this.mqttServer != null)
            {
                await this.mqttServer.DisconnectClientAsync(clientId, MqttDisconnectReasonCode.NormalDisconnection);
                return true;
            }
            return false;
        }
    }
}
