// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Humanizer.Localisation;

namespace Gardener.Core.Client.Impl.UserCenter.Services
{
    /// <summary>
    /// 租户服务
    /// </summary>
    [ScopedService]
    public class TenantService : ClientServiceBase<SystemTenantDto, Guid>, ITenantService
    {
        public TenantService(IApiCaller apiCaller) : base(apiCaller, "tenant")
        {
        }
        /// <summary>
        /// 为租户绑定资源
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="resourceIds"></param>
        /// <returns></returns>
        public Task<bool> AddResources(Guid tenantId, Guid[] resourceIds)
        {
            return apiCaller.PostAsync<Guid[], bool>($"{this.baseUrl}/{tenantId}/resources", resourceIds);
        }


        /// <summary>
        /// 获取租户下资源
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ResourceDto>> GetResources(Guid tenantId)
        {
            return apiCaller.GetAsync<IEnumerable<ResourceDto>>($"{this.baseUrl}/{tenantId}/resources");
        }

        /// <summary>
        /// 复制一个新租户
        /// </summary>
        /// <remarks>
        /// 从现有租户，复制出一个新租户，租户的资源权限将保持一致。
        /// </remarks>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<bool> CopyNewTenant(Guid tenantId)
        {
            return apiCaller.PostAsync<Guid[], bool>($"{this.baseUrl}/{tenantId}/copy-new-tenant");
        }
    }
}
