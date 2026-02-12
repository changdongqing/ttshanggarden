// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Services
{
    /// <summary>
    /// 设备通讯控制服务
    /// </summary>
    public interface IDeviceCommunicationControlService
    {
        /// <summary>
        /// 断开客户端连接
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<bool> DisconnectClient(string clientId);

        /// <summary>
        /// 向所有客户端发送消息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<bool> SendMesaageToAllClient(string content, DeviceDataContentType? contentType = null);
        /// <summary>
        /// 向所有客户端发送消息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<bool> SendMesaageToAllClient(byte[] content, DeviceDataContentType? contentType = null);
        /// <summary>
        /// 向指定客户端发送消息
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<bool> SendMesaageToClient(string clientId, string content, DeviceDataContentType? contentType = null);
        /// <summary>
        /// 向指定客户端发送消息
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<bool> SendMesaageToClient(string clientId, byte[] content, DeviceDataContentType? contentType = null);
    }
}
