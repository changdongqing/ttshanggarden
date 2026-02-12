// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Swagger.Dtos;

namespace TTShang.Core.Swagger.Services
{
    /// <summary>
    /// 接口终结点服务
    /// </summary>
    public interface IApiEndpointService
    {
        /// <summary>
        /// 获取api分组
        /// </summary>
        /// <remarks>
        /// 获取api分组设置信息
        /// </remarks>
        /// <returns></returns>
        IEnumerable<SwaggerSpecificationOpenApiInfoDto> GetApiGroup();

        /// <summary>
        /// 获取api信息
        /// </summary>
        /// <remarks>
        /// 根据组名和服务标签，获取api信息
        /// </remarks>
        /// <param name="groupName">分组</param>
        /// <param name="tags">标签</param>
        /// <returns></returns>
        Task<IEnumerable<ApiEndpoint>> GetApis(string? groupName = null, string[]? tags = null);

        /// <summary>
        /// 获取api信息
        /// </summary>
        /// <remarks>
        /// 根据组名和控制器类，获取api信息
        /// </remarks>
        /// <param name="groupName">分组</param>
        /// <param name="controlTypes">控制器类</param>
        /// <returns></returns>
        Task<IEnumerable<ApiEndpoint>> GetApis(string? groupName = null, Type[]? controlTypes = null);

        /// <summary>
        /// 获取api信息
        /// </summary>
        /// <remarks>
        /// 根据api key 获取api信息
        /// </remarks>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<ApiEndpoint?> GetApi(string key);

        /// <summary>
        /// 获取api信息
        /// </summary>
        /// <remarks>
        /// 根据请求方法和请求路径，获取api信息
        /// </remarks>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<ApiEndpoint?> GetApi(ApiHttpMethod method,string path);

        /// <summary>
        /// 获取api信息
        /// </summary>
        /// <remarks>
        /// 从上下文中获取api信息
        /// </remarks>
        /// <returns></returns>
        Task<ApiEndpoint?> GetApi(object httpContext);
    }
}
