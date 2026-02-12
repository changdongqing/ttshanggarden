// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.SystemAsset.Services;

namespace Gardener.Core.Client.Impl.SystemAsset.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class ResourceFunctionService : ClientServiceBaseNoKey<ResourceFunctionDto>, IResourceFunctionService
    {
        public ResourceFunctionService(IApiCaller apiCaller) : base(apiCaller, "resource-function")
        {
        }

        public async Task<bool> Add(List<ResourceFunctionDto> resourceFunctionDtos)
        {
            return await apiCaller.PostAsync<List<ResourceFunctionDto>, bool>($"{baseUrl}", resourceFunctionDtos);
        }

        public async Task<bool> Delete(Guid resourceId, Guid functionId)
        {
            return await apiCaller.DeleteAsync<bool>($"{baseUrl}/{resourceId}/{functionId}");
        }

        public async Task<string> GetSeedData(List<Guid> resourceIds)
        {
            return await apiCaller.PostAsync<List<Guid>, string>($"{baseUrl}/seed-data", resourceIds);
        }
    }
}
