// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Swagger.Dtos;

namespace Gardener.Core.Swagger.Services
{
    /// <summary>
    /// swagger 服务
    /// </summary>
    public interface ISwaggerService
    {
        /// <summary>
        /// 获取 swagger 配置
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup();

        /// <summary>
        /// 获取所有api信息
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<IEnumerable<ApiEndpoint>> GetApis(string? groupName = null, string[]? tags = null);
    }
}
