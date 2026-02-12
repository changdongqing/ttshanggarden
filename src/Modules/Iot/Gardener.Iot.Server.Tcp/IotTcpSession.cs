// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Iot.Enums;
using Gardener.Iot.Services;
using Microsoft.Extensions.Logging;
using NetCoreServer;
using System.Buffers;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gardener.Iot.Server.Tcp
{
    /// <summary>
    /// 
    /// </summary>
    internal class IotTcpSession : TcpSession
    {
        private bool _authorized = false;
        internal bool Authorized => _authorized;
        private string? clientId;
        internal string? ClientId => clientId;
        private string? account;
        private string? secretKey;
        private readonly ILogger logger;
        private readonly IDeviceCommunicationCableSplicer communicationCableSplicer;

        private bool autoDisconnectOnTimeout = false;

        private readonly Func<IotTcpSession, Task> onConnectionVerifySuccessfully;
        public IotTcpSession(TcpServer server, ILogger logger, IDeviceCommunicationCableSplicer communicationCableSplicer, Func<IotTcpSession, Task> onConnectionVerifyed) : base(server)
        {
            this.logger = logger;
            this.communicationCableSplicer = communicationCableSplicer;
            this.onConnectionVerifySuccessfully = onConnectionVerifyed;
        }
        protected override void OnConnecting()
        {
            base.OnConnecting();
        }
        protected override async void OnConnected()
        {
            logger.LogInformation($"{clientId} Chat TCP session with Id {Id} connected!");

            //如果已经验证过了，就直接通过
            if (clientId != null && account != null && _authorized)
            {
                var remoteEndPoint = this.Socket.RemoteEndPoint?.ToString() ?? "0.0.0.0";
                //重新连接成功
                await communicationCableSplicer.OnClientConnected(clientId, DeviceConnectionType.Tcp, remoteEndPoint, account);
                await onConnectionVerifySuccessfully.Invoke(this);
            }

        }
        protected override void OnDisconnecting()
        {
            base.OnDisconnecting();
        }
        protected override async void OnDisconnected()
        {
            logger.LogInformation($"{clientId} Chat TCP session with Id {Id} disconnected!");
            if (_authorized && !string.IsNullOrEmpty(clientId))
            {
                //这里取this.Socket.RemoteEndPoint异常，不能取
                if (autoDisconnectOnTimeout)
                {
                    await communicationCableSplicer.OnClientDisconnected(clientId, DeviceConnectionType.Tcp, null, disconnectReason: DeviceDisconnectReason.KeepAliveTimeout, disconnectReasonDescription: "收到超时错误，主动中断");
                }
                else
                {

                    await communicationCableSplicer.OnClientDisconnected(clientId, DeviceConnectionType.Tcp, null);
                }
            }
            base.OnDisconnected();
        }
        protected override void OnSent(long sent, long pending)
        {
            base.OnSent(sent, pending);
        }
        protected override void OnEmpty()
        {
            base.OnEmpty();
        }
        protected override async void OnReceived(byte[] buffer, long offset, long size)
        {
            try
            {
                CmdType cmdType = GetType(buffer);
                var remoteEndPoint = this.Socket.RemoteEndPoint?.ToString() ?? "0.0.0.0";
                if (cmdType.Equals(CmdType.Login))
                {
                    string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
                    //登录
                    string[] values = message.Split(";");
                    this.clientId = values[1];
                    this.account = values[2];
                    this.secretKey = values[3];
                    ConnectionIdentityAuthenticationState state = await communicationCableSplicer.OnConnectionVerify(clientId, deviceConnectionType: Enums.DeviceConnectionType.Tcp, remoteEndPoint, account, secretKey);
                    if (ConnectionIdentityAuthenticationState.Succeed.Equals(state))
                    {
                        //连接成功
                        await communicationCableSplicer.OnClientConnected(clientId, DeviceConnectionType.Tcp, remoteEndPoint, account);
                        this._authorized = true;
                        await onConnectionVerifySuccessfully.Invoke(this);
                    }
                    else
                    {
                        //SendAsync($"Login fail. {state}");
                        //连接失败,断开连接
                        this.Disconnect();
                    }
                    return;
                }
                //未登录或没有编号
                if (!_authorized || string.IsNullOrEmpty(clientId))
                {
                    return;
                }
                if (cmdType.Equals(CmdType.Ping))
                {
                    //ping
                    await communicationCableSplicer.OnPingClient(clientId, DeviceConnectionType.Tcp, remoteEndPoint);
                }
                else
                {
                    byte[] bytes = new byte[size];
                    //ArraySegment<byte>
                    System.Buffer.BlockCopy(buffer, (int)offset, bytes, 0, (int)size);
                    //接收消息
                    await communicationCableSplicer.OnApplicationMessageReceived(clientId, DeviceConnectionType.Tcp, new ReadOnlySequence<byte>(bytes));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{clientId} OnReceived error {ex.Message}");
            }
        }

        protected override void OnError(SocketError error)
        {
            logger.LogError($"{clientId} Chat TCP session caught an error with code {error}");

            if (SocketError.TimedOut.Equals(error))
            {
                //autoDisconnectOnTimeout = true;
                //超时，主动断开
                //this.Disconnect();
            }
        }

        /// <summary>
        /// 消息判断
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private CmdType GetType(byte[] buffer)
        {
            if (buffer.Length > 5)
            {
                //Login
                if (buffer[0] == 76 && buffer[1] == 111 && buffer[2] == 103 && buffer[3] == 105 && buffer[4] == 110)
                {
                    return CmdType.Login;
                }
                if (buffer.Length > 9)
                {
                    //Heartbeat
                    if (buffer[0] == 72 && buffer[1] == 101 && buffer[2] == 97 && buffer[3] == 114 && buffer[4] == 116 && buffer[5] == 98 && buffer[6] == 101 && buffer[7] == 97 && buffer[8] == 116)
                    {
                        return CmdType.Ping;
                    }

                }
            }
            return CmdType.Other;
        }
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum CmdType
    {
        Login,
        Ping,
        Other

    }
}
