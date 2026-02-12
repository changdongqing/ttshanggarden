// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Dtos.Constraints
{
    /// <summary>
    /// 租户权限
    /// </summary>
    public interface IModelTenantPermission
    {
        /// <summary>
        /// 授予所有租户
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.EmpowerAllTenants), ResourceType = typeof(SharedLocalResource))]
        public bool EmpowerAllTenants { get; set; }
    }
}
