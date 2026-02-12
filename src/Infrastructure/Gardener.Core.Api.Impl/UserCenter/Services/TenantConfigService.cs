// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.UserCenter.Entities;
using Gardener.Core.Authorization.Services;
using Gardener.Core.UserCenter.Services;

namespace Gardener.Core.Api.Impl.UserCenter.Services
{
    /// <summary>
    /// 租户配置服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class TenantConfigService : ServiceBase<SystemTenantConfig, SystemTenantConfigDto, Int32>, ITenantConfigService
    {
        private readonly IIdentityService identityService;
        /// <summary>
        /// 租户配置服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="identityService"></param>
        public TenantConfigService(IRepository<SystemTenantConfig> repository, IIdentityService identityService) : base(repository)
        {
            this.identityService = identityService;
        }

        /// <summary>
        /// 根据配置key获取租户配置
        /// </summary>
        /// <remarks>
        /// 根据配置key获取租户配置
        /// </remarks>
        /// <param name="tenantId"></param>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public Task<SystemTenantConfigDto?> GetTenantConfigByConfigKey(Guid tenantId, string configKey)
        {
            return _repository.AsQueryable(false).Where(x => x.TenantId.Equals(tenantId) && x.ConfigKey.Equals(configKey)).Select(x => x.Adapt<SystemTenantConfigDto>()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取我的租户配置
        /// </summary>
        /// <remarks>
        /// 获取当前登录用户的租户配置
        /// </remarks>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public async Task<SystemTenantConfigDto?> GetMyTenantConfig(string configKey)
        {
            var identity=identityService.GetIdentity();
            if(identity == null || !identity.TenantId.HasValue)
            {
                return null;
            }
            return await GetTenantConfigByConfigKey(identity.TenantId.Value, configKey);
        }
    }
}
