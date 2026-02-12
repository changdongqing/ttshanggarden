// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Dtos
{
    /// <summary>
    /// Json数据配置
    /// </summary>
    public class JsonDataConfig
    {
        /// <summary>
        /// 字段配置
        /// </summary>
        public List<JsonDataFieldConfig> FieldConfigs { get; set; } = new List<JsonDataFieldConfig>();
    }
}
