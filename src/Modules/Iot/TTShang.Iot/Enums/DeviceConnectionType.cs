// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Enums
{
    /// <summary>
    /// 设备连接类型
    /// </summary>
    public enum DeviceConnectionType : int
    {
        /// <summary>
        /// Mqtt
        /// </summary>
        Mqtt,
        /// <summary>
        /// Udp
        /// </summary>
        Udp,
        /// <summary>
        /// Tcp
        /// </summary>
        Tcp,
        /// <summary>
        /// Http
        /// </summary>
        Http
    }
}
