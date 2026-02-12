// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Buffers;

namespace Gardener.Iot.Services
{
    /// <summary>
    /// 设备通讯对接器
    /// </summary>
    public interface IDeviceCommunicationCableSplicer
    {
        /// <summary>
        /// 当设备连接验证时
        /// </summary>
        /// <param name="clientId">客户端连接编号</param>
        /// <param name="deviceConnectionType">客户端连接类型</param>
        /// <param name="clientEndpoint">客户端Ip:Port</param>
        /// <param name="account">设备账号</param>
        /// <param name="secretKey">设备密钥</param>
        /// <param name="userProperties">用户数据</param>
        /// <returns></returns>
        Task<ConnectionIdentityAuthenticationState> OnConnectionVerify(string clientId, DeviceConnectionType deviceConnectionType, string clientEndpoint, string? account = null, string? secretKey = null, IEnumerable<KeyValuePair<string, string>>? userProperties = null);

        /// <summary>
        /// 设备已连接成功（注意是已验证通过）
        /// </summary>
        /// <param name="clientId">客户端连接编号</param>
        /// <param name="deviceConnectionType">客户端连接类型</param>
        /// <param name="clientEndpoint">客户端Ip:Port</param>
        /// <param name="account">设备账号（支持接入未注册的设备）</param>
        /// <param name="userProperties">用户数据</param>
        /// <returns></returns>
        Task OnClientConnected(string clientId, DeviceConnectionType deviceConnectionType, string clientEndpoint, string? account = null, IEnumerable<KeyValuePair<string, string>>? userProperties = null);

        /// <summary>
        /// 当设备断开连接时
        /// </summary>
        /// <param name="clientId">客户端连接编号</param>
        /// <param name="deviceConnectionType">客户端连接类型</param>
        /// <param name="clientEndpoint">客户端:Port</param>
        /// <param name="userProperties">用户数据</param>
        /// <param name="disconnectReason">中断原因code</param>
        /// <param name="disconnectReasonDescription">中断原因描述</param>
        /// <returns></returns>
        Task OnClientDisconnected(string clientId, DeviceConnectionType deviceConnectionType, string? clientEndpoint, IEnumerable<KeyValuePair<string, string>>? userProperties = null, DeviceDisconnectReason disconnectReason = DeviceDisconnectReason.Other, string? disconnectReasonDescription = null);

        /// <summary>
        /// 当设备发送ping检测时
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        Task OnPingClient(string clientId, DeviceConnectionType deviceConnectionType, string? endpoint = null);

        /// <summary>
        /// 当收到设备发送的数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="userProperties"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        Task OnApplicationMessageReceived(string clientId, DeviceConnectionType deviceConnectionType, ReadOnlySequence<byte>? content, DeviceDataContentType? contentType=null, IEnumerable<KeyValuePair<string, string>>? userProperties = null, string? topic=null);
    }
}
