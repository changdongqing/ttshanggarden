// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Dtos.Constraints
{
    /// <summary>
    /// 租户
    /// </summary>
    public interface IModelTenant : IModelTenantId
    {
        /// <summary>
        /// 租户
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Tenant), ResourceType = typeof(SharedLocalResource))]
        public ITenant? Tenant { get; set; }
    }
}
