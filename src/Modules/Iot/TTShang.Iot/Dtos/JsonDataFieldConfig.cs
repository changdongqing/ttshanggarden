// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Dtos
{
    /// <summary>
    /// json数据字段配置
    /// </summary>
    public class JsonDataFieldConfig
    {
        /// <summary>
        /// json数据字段配置
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <param name="remarks"></param>
        public JsonDataFieldConfig(string fieldName, JsonDataFieldType fieldType, string? remarks=null)
        {
            FieldName = fieldName;
            FieldType = fieldType;
            Remarks = remarks;
        }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public JsonDataFieldType FieldType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
        /// <summary>
        /// 嵌套字段配置（数组、对象字段可以配置）
        /// </summary>
        public JsonDataFieldConfig? NestFieldConfig { get; set; }   
    }
}
