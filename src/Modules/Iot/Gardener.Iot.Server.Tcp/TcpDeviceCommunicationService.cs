// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Iot.Services;
using Microsoft.Extensions.Logging;
using System.Net;
using Gardener.Iot.Dtos;
using Microsoft.Extensions.Options;
using System.Text;

namespace Gardener.Iot.Server.Tcp
{
    /// <summary>
    /// TCP
    /// </summary>
    public class TcpDeviceCommunicationService : IDeviceCommunicationService, IDeviceCommunicationControlService
    {
        private readonly ILogger<TcpDeviceCommunicationService> logger;
        private readonly IOptions<TcpServerOptions> options;
        /// <summary>
        /// TCP
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public TcpDeviceCommunicationService(ILogger<TcpDeviceCommunicationService> logger, IOptions<TcpServerOptions> options)
        {
            this.logger = logger;
            this.options = options;
        }
        private IotTcpServer? tcpServer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <param name="communicationCableSplicer"></param>
        /// <returns></returns>
        public Task ExecuteAsync(CancellationToken stoppingToken, IDeviceCommunicationCableSplicer communicationCableSplicer)
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="communicationCableSplicer"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken, IDeviceCommunicationCableSplicer communicationCableSplicer)
        {
            TcpServerOptions tcpServerOptions = options.Value;
            // 获取本地 IP 地址
            tcpServer = new IotTcpServer(IPAddress.Any, tcpServerOptions.Port, logger, communicationCableSplicer);
            //若操作系统支持，该选项会启用 SO_KEEPALIVE 功能。启用此功能后，在连接处于空闲状态时，系统会定期发送保活探测包以确认连接是否仍然有效。
            //tcpServer.OptionKeepAlive = true;
            //该选项指定了在向远程主机发送 TCP 保活探测包之前，TCP 连接保持空闲状态的秒数。
            //tcpServer.OptionTcpKeepAliveTime = 30;
            //该选项指定了在发送下一个 TCP 保活探测包之前，TCP 连接等待保活响应的秒数。
            //tcpServer.OptionTcpKeepAliveInterval = 30;
            //该选项指定了在终止 TCP 连接之前发送的 TCP 保活探测包的数量。
            //tcpServer.OptionTcpKeepAliveRetryCount = 10;
            logger.LogInformation($"TCP 服务端已经启动，监听 IP 地址为 {IPAddress.Any}, 监听端口号为 {tcpServerOptions.Port}");
            // Start the server
            logger.LogInformation("Tcp server starting...");
            tcpServer.Start();
            logger.LogInformation("Tcp server start done!");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="communicationCableSplicer"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task StopAsync(CancellationToken cancellationToken, IDeviceCommunicationCableSplicer communicationCableSplicer)
        {
            if (tcpServer != null)
            {
                // Stop the server
                Console.Write("Tcp server stopping...");
                tcpServer.Stop();
                Console.WriteLine("Tcp server stop done!");
            }
            return Task.CompletedTask;
        }

        public Task<bool> DisconnectClient(string clientId)
        {
            return Task.FromResult(tcpServer?.GetSession(clientId)?.Disconnect() ?? false);
        }

        public Task<bool> SendMesaageToAllClient(string content, DeviceDataContentType? contentType = null)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return SendMesaageToAllClient(bytes, contentType);
        }

        public Task<bool> SendMesaageToAllClient(byte[] content, DeviceDataContentType? contentType = null)
        {
            if (tcpServer != null)
            {
                var result = tcpServer.Multicast(content);
                return Task.FromResult(result);
            }
            return Task.FromResult(false);
        }

        public Task<bool> SendMesaageToClient(string clientId, string content, DeviceDataContentType? contentType = null)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return SendMesaageToClient(clientId, bytes, contentType);
        }

        public Task<bool> SendMesaageToClient(string clientId, byte[] content, DeviceDataContentType? contentType = null)
        {
            if (tcpServer != null)
            {
                IotTcpSession? session = tcpServer.GetSession(clientId);
                if (session != null)
                {
                   return Task.FromResult(session.SendAsync(content));
                }
            }
            return Task.FromResult(false);
        }
    }
}
