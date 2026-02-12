// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Enums
{
    /// <summary>
    /// 设备系统日志类型
    /// </summary>
    public enum DeviceSystemLogType : int
    {
        /// <summary>
        /// 设备连接中
        /// </summary>
        DeviceConnecting = 0,
        /// <summary>
        /// 设备连接成功
        /// </summary>
        DeviceConnectSucceed = 1,
        /// <summary>
        /// 连接失败
        /// </summary>
        DeviceConnectFailed = 2,
        /// <summary>
        /// 断开连接
        /// </summary>
        DeviceDisconnect=3
    }
}
