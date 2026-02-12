// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.UserCenter.Services
{
    [ScopedService]
    public class TenantConfigTemplateService : ClientServiceBase<SystemTenantConfigTemplateDto, Int32>, ITenantConfigTemplateService
    {
        public TenantConfigTemplateService(IApiCaller apiCaller) : base(apiCaller, "tenant-config-template")
        {
        }
    }
}
