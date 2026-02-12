// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace TTShang.Core.CodeGeneration.Dtos
{
    /// <summary>
    /// 字段描述
    /// </summary>
    public class FieldDescriptionDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityTypeFullName"></param>
        /// <param name="name"></param>
        /// <param name="typeName"></param>
        public FieldDescriptionDto(string entityTypeFullName, string name, string typeName)
        {
            EntityTypeFullName = entityTypeFullName;
            Name = name;
            TypeName = typeName;
            FieldConfig = new FieldConfigDto()
            {
                EntityTypeFullName = entityTypeFullName,
                FieldName = name
            };
        }

        /// <summary>
        /// 实体类完整名称-唯一标识
        /// </summary>
        public string EntityTypeFullName { get; set; }
        /// <summary>
        /// 主键名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 主键类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FieldConfigDto FieldConfig { get; set; }
        /// <summary>
        /// 主键类型
        /// </summary>
        [JsonIgnore]
        public Type Type { get; set; } = default!;
        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimaryKey {  get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int? Order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsNullableType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDictCodeField { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? DictCodeTypeValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? MaxLength {  get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? MinLength {  get; set; }
       
    }
}
