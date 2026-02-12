// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Iot.Services;
using Microsoft.Extensions.Logging;
using NetCoreServer;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace TTShang.Iot.Server.Tcp
{
    internal class IotTcpServer : TcpServer
    {
        protected readonly ConcurrentDictionary<string, Guid> ConnectionSuccessSessionIdMaps = new ConcurrentDictionary<string, Guid>();

        private readonly ILogger logger;
        private readonly IDeviceCommunicationCableSplicer communicationCableSplicer;
        private readonly Func<IotTcpSession, Task> OnConnectionVerifySuccessfully;
        public IotTcpServer(IPAddress address, int port, ILogger logger, IDeviceCommunicationCableSplicer communicationCableSplicer) : base(address, port)
        {
            this.logger = logger;
            this.communicationCableSplicer = communicationCableSplicer;
            OnConnectionVerifySuccessfully = (s) => OnConnectionVerifySuccessfullyCallback(s);
        }
        protected override TcpSession CreateSession()
        {
            return new IotTcpSession(this, logger, communicationCableSplicer, OnConnectionVerifySuccessfully);
        }

        protected override void OnError(SocketError error)
        {
            logger.LogError($"Chat TCP server caught an error with code {error}");
        }
        /// <summary>
        /// 连接中
        /// </summary>
        /// <param name="session"></param>
        protected override void OnConnecting(TcpSession session)
        {
            base.OnConnecting(session);
        }
        /// <summary>
        /// 连接成功
        /// </summary>
        /// <param name="session"></param>
        protected override void OnConnected(TcpSession session)
        {
            base.OnConnected(session);
        }
        /// <summary>
        /// 断开连接中
        /// </summary>
        /// <param name="session"></param>
        protected override void OnDisconnecting(TcpSession session)
        {
            base.OnDisconnecting(session);
        }
        /// <summary>
        /// 断开连接成功
        /// </summary>
        /// <param name="session"></param>
        protected override void OnDisconnected(TcpSession session)
        {
            string? clientId = ((IotTcpSession)session).ClientId;
            if (String.IsNullOrEmpty(clientId))
            {
                return;
            }
            ConnectionSuccessSessionIdMaps.TryRemove(clientId, out var _);
            base.OnDisconnected(session);
        }

        private Task OnConnectionVerifySuccessfullyCallback(IotTcpSession session)
        {
            if (session.Authorized && !string.IsNullOrEmpty(session.ClientId))
            {
                ConnectionSuccessSessionIdMaps.AddOrUpdate(session.ClientId, _ => session.Id, (_, _) => session.Id);
            }
            return Task.CompletedTask;
        }

        internal IotTcpSession? GetSession(string clientId)
        {
            Guid sessionId;
            if (ConnectionSuccessSessionIdMaps.TryGetValue(clientId, out sessionId))
            {
                if (Sessions.TryGetValue(sessionId, out var session))
                {
                    return (IotTcpSession)session;
                }
            }
            return null;
        }
    }
}
