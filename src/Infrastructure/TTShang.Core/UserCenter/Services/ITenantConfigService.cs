// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.UserCenter.Services
{
    /// <summary>
    ///  租户配置服务
    /// </summary>
    public interface ITenantConfigService : IServiceBase<SystemTenantConfigDto, Int32>
    {
        /// <summary>
        /// 根据配置key获取租户配置
        /// </summary>
        /// <remarks>
        /// 根据配置key获取租户配置
        /// </remarks>
        /// <param name="tenantId"></param>
        /// <param name="configKey"></param>
        /// <returns></returns>
        Task<SystemTenantConfigDto?> GetTenantConfigByConfigKey(Guid tenantId, string configKey);

        /// <summary>
        /// 获取我的租户配置
        /// </summary>
        /// <remarks>
        /// 获取当前登录用户的租户配置
        /// </remarks>
        /// <param name="configKey"></param>
        /// <returns></returns>
        Task<SystemTenantConfigDto?> GetMyTenantConfig(string configKey);
    }
}
