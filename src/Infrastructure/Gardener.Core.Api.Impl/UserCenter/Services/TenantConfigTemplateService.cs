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
    /// 租户配置服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class TenantConfigTemplateService : ServiceBase<SystemTenantConfigTemplate, SystemTenantConfigTemplateDto, Int32>, ITenantConfigTemplateService
    {
        /// <summary>
        /// 租户配置服务
        /// </summary>
        /// <param name="repository"></param>
        public TenantConfigTemplateService(IRepository<SystemTenantConfigTemplate, MasterDbContextLocator> repository) : base(repository)
        {
        }
    }
}
