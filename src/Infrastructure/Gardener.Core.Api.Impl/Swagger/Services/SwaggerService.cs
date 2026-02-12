// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Authorization.Dtos;
using Gardener.Core.Swagger.Dtos;
using Gardener.Core.Swagger.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.Swagger.Services
{
    /// <summary>
    /// Swagger服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class SwaggerService : ISwaggerService
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public SwaggerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 获取 swagger 配置
        /// </summary>
        /// <remarks>
        /// 获取api分组设置
        /// </remarks>
        /// <returns></returns>
        public Task<IEnumerable<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup()
        {
            return Task.FromResult(_serviceProvider.GetRequiredService<IApiEndpointService>().GetApiGroup());
        }

        /// <summary>
        /// 获取所有api信息
        /// </summary>
        /// <remarks>
        /// 从swagger获取所有api信息
        /// </remarks>
        /// <param name="groupName"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public Task<IEnumerable<ApiEndpoint>> GetApis(string? groupName = null, string[]? tags = null)
        {
            return _serviceProvider.GetRequiredService<IApiEndpointService>().GetApis(groupName, tags);
        }
    }
}
