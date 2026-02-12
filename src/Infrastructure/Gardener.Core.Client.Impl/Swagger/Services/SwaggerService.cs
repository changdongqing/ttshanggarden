// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Swagger.Dtos;
using Gardener.Core.Swagger.Services;

namespace Gardener.Core.Client.Impl.Swagger.Services
{
    [ScopedService]
    public class SwaggerService : ClientServiceCaller, ISwaggerService
    {
        public SwaggerService(IApiCaller apiCaller) : base(apiCaller, "swagger")
        {
        }

        public async Task<IEnumerable<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup()
        {
            return await apiCaller.GetAsync<IEnumerable<SwaggerSpecificationOpenApiInfoDto>>($"{this.baseUrl}/api-group");
        }

        public async Task<IEnumerable<ApiEndpoint>> GetApis(string? groupName = null, string[]? tags = null)
        {
            List<KeyValuePair<string, object?>> ps = new List<KeyValuePair<string, object?>>();
            if (tags != null)
            {
                foreach (var item in tags)
                {
                    ps.Add(new KeyValuePair<string, object?>(nameof(tags), item));
                }
            }
            return await apiCaller.GetAsync<IEnumerable<ApiEndpoint>>($"{this.baseUrl}/apis/{groupName}", ps);
        }
    }
}
