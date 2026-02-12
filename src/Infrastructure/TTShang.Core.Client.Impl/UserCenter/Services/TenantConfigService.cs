// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Impl.UserCenter.Pages.TenantView;

namespace TTShang.Core.Client.Impl.UserCenter.Services
{
    /// <summary>
    ///  租户配置服务
    /// </summary>
    [ScopedService]
    public class TenantConfigService : ClientServiceBase<SystemTenantConfigDto, Int32>, ITenantConfigService
    {
        /// <summary>
        ///  租户配置服务
        /// </summary>
        public TenantConfigService(IApiCaller apiCaller) : base(apiCaller, "tenant-config")
        {
        }

        public Task<SystemTenantConfigDto?> GetMyTenantConfig(string configKey)
        {
            return apiCaller.GetAsync<SystemTenantConfigDto?>($"{baseUrl}/my-tenant-config/{configKey}");
        }

        public Task<SystemTenantConfigDto?> GetTenantConfigByConfigKey(Guid tenantId, string configKey)
        {
            return apiCaller.GetAsync<SystemTenantConfigDto?>($"{baseUrl}/tenant-config-by-config-key/{tenantId}/{configKey}");
        }
    }
}
