// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.SystemAsset.Services;

namespace TTShang.Core.Client.Impl.SystemAsset.Services
{
    [ScopedService]
    public class ResourceService : ClientServiceBase<ResourceDto, Guid>, IResourceService
    {
        public ResourceService(IApiCaller apiCaller) : base(apiCaller, "resource")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetChildren(Guid id)
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{baseUrl}/{id}/children");
        }

        public async Task<List<FunctionDto>> GetFunctions(Guid id)
        {
            return await apiCaller.GetAsync<List<FunctionDto>>($"{baseUrl}/{id}/functions");
        }

        public async Task<List<ResourceDto>> GetRoot()
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{baseUrl}/root");
        }

        public async Task<List<ResourceDto>> GetTree(bool includLocked = true, string? rootKey = null, Guid? tenantId = null, bool? supportMultiTenant = null)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>();
            queryString.Add(nameof(rootKey), rootKey);
            queryString.Add(nameof(includLocked), includLocked);
            queryString.Add(nameof(tenantId), tenantId);
            queryString.Add(nameof(supportMultiTenant), supportMultiTenant);
            return await apiCaller.GetAsync<List<ResourceDto>>($"{baseUrl}/tree", queryString);
        }
    }
}
