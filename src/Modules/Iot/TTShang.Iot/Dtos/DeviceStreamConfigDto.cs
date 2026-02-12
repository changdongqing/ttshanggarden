// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Dtos
{
    /// <summary>
    /// 设备流配置
    /// </summary>
    public class DeviceStreamConfigDto
    {
        /// <summary>
        /// 内容类型
        /// </summary>
        public DeviceDataContentType ContentType { get; set; } = DeviceDataContentType.ApplicationJson;

        /// <summary>
        /// 配置内容
        /// </summary>
        public object? ConfigContent { get; set; }
    }
}
