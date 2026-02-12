// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.SystemAsset.Services;
using System.Web;

namespace TTShang.Core.Client.Impl.SystemAsset.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class FunctionService : ClientServiceBase<FunctionDto, Guid>, IFunctionService
    {
        public FunctionService(IApiCaller apiCaller) : base(apiCaller, "function")
        {
        }

        public async Task<bool> EnableAudit(Guid id, bool enableAudit = true)
        {
            return await apiCaller.PutAsync<bool, bool>($"{baseUrl}/{id}/enable-audit/{enableAudit}");
        }

        public async Task<bool> Exists(HttpMethod method, string path)
        {
            path = HttpUtility.UrlEncode(path);
            return await apiCaller.GetAsync<bool>($"{baseUrl}/exists/{method}/{path}");
        }

        public Task<bool> Exists(ApiHttpMethod method, string path)
        {
            throw new NotImplementedException();
        }

        public async Task<FunctionDto?> GetByKey(string key)
        {
            return await apiCaller.GetAsync<FunctionDto>($"{baseUrl}/by-key/{key}");
        }
    }
}
