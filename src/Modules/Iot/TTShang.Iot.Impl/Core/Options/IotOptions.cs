// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace TTShang.Iot.Impl.Core.Options
{
    /// <summary>
    /// Iot设置
    /// </summary>
    public class IotOptions : IConfigurableOptions
    {
        /// <summary>
        /// 允许匿名设备连接
        /// </summary>
        /// <remarks>
        /// 匿名设备
        /// </remarks>
        public bool AllowAnonymousDeviceConnect { get; set; } = true;

        /// <summary>
        /// 更新最后推送数据时间最小间隔毫秒数
        /// </summary>
        public long UpdateLastPushDataTimeMinIntervalMilliseconds { get; set; } = 5000;
    }
}
