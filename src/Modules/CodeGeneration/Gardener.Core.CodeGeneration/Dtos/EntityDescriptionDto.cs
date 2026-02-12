// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Gardener.Core.CodeGeneration.Dtos
{
    /// <summary>
    /// 实体描述
    /// </summary>
    public class EntityDescriptionDto
    {
        /// <summary>
        /// 实体类名称
        /// </summary>
        public string EntityTypeName { get; set; } = default!;
        /// <summary>
        /// 实体类完整名称-唯一标识
        /// </summary>
        public string EntityTypeFullName { get; set; } = default!;
        /// <summary>
        /// 实体类
        /// </summary>
        [JsonIgnore]
        public Type EntityType { get; set; } = default!;
        /// <summary>
        /// 实体类Dto名称
        /// </summary>
        public string EntityDtoName { get; set; } = default!;
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? DisplayName { get; set; }
        /// <summary>
        /// 显示名资源类名
        /// </summary>
        public string? DisplayNameResourceTypeName { get; set; }
        /// <summary>
        /// 显示名资源类
        /// </summary>
        [JsonIgnore]
        public Type? DisplayNameResourceType { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; } = default!;
        /// <summary>
        /// 
        /// </summary>
        public string AssemblyName { get; set; } = default!;
        /// <summary>
        /// 主键名称
        /// </summary>
        public string? PrimaryKeyName { get; set; }
        /// <summary>
        /// 主键类型名称
        /// </summary>
        public string? PrimaryKeyTypeName { get; set; }
        /// <summary>
        /// 主键类型
        /// </summary>
        [JsonIgnore]
        public Type? PrimaryKeyType { get; set; }

        /// <summary>
        /// 是否实现<see cref="Core.Dtos.Constraints.IModelId{TKey}"/>
        /// </summary>
        public bool ImplementIModelId {  get; set; }

        /// <summary>
        /// 是否实现<see cref="Core.Dtos.Constraints.IModelCreated"/>
        /// </summary>
        public bool ImplementIModelCreated {  get; set; }

        /// <summary>
        /// 是否实现<see cref="Core.Dtos.Constraints.IModelLocked"/>
        /// </summary>
        public bool ImplementIModelLocked {  get; set; }

        /// <summary>
        /// 是否实现<see cref="Core.Dtos.Constraints.IModelDeleted"/>
        /// </summary>
        public bool ImplementIModelDeleted {  get; set; }

        /// <summary>
        /// 是否实现<see cref="Core.Dtos.Constraints.IModelUpdated"/>
        /// </summary>
        public bool ImplementIModelUpdated {  get; set; }

        /// <summary>
        /// 是否实现<see cref="Core.Dtos.Constraints.IModelTenant"/>
        /// </summary>
        public bool ImplementIModelTenant {  get; set; }

        /// <summary>
        /// 是否实现<see cref="Core.Dtos.Constraints.IModelTenantId"/>
        /// </summary>
        public bool ImplementIModelTenantId {  get; set; }

        /// <summary>
        /// 是否实现<see cref="Core.Dtos.Constraints.IModelModule"/>
        /// </summary>
        public bool ImplementIModelModule {  get; set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public List<FieldDescriptionDto> Fields { get; set; }= new();
        /// <summary>
        /// 实体类配置
        /// </summary>
        public EntityConfigDto EntityConfig {  get; set; }= default!;
    }
}
