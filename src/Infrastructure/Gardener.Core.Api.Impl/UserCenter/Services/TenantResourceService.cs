// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.UserCenter.Entities;
using Gardener.Core.UserCenter.Services;

namespace Gardener.Core.Api.Impl.UserCenter.Services
{
    /// <summary>
    /// 租户资源服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class TenantResourceService : ServiceBaseNoKey<SystemTenantResource, SystemTenantResourceDto>, ITenantResourceService
    {
        /// <summary>
        /// 租户资源服务
        /// </summary>
        /// <param name="repository"></param>
        public TenantResourceService(IRepository<SystemTenantResource, MasterDbContextLocator> repository) : base(repository)
        {
        }
    }
}
