// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Iot.Enums
{
    /// <summary>
    /// 设备断开连接原因
    /// </summary>
    public enum DeviceDisconnectReason
    {
        /// <summary>
        /// 正常中断
        /// </summary>
        NormalDisconnection = 0,
        /// <summary>
        /// 管理操作
        /// </summary>
        AdministrativeAction = 10,
        /// <summary>
        /// 连接超过速率
        /// </summary>
        ConnectionRateExceeded = 20,
        /// <summary>
        /// 服务端繁忙
        /// </summary>
        ServerBusy=30,
        /// <summary>
        /// 连接保持超时
        /// </summary>
        KeepAliveTimeout=40,
        /// <summary>
        /// 未授权
        /// </summary>
        NotAuthorized = 50,
        /// <summary>
        /// 其他
        /// </summary>
        Other=999
    }
}
