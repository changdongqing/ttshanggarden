// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Authorization.Services
{
    /// <summary>
    /// 接口查询服务
    /// </summary>
    public interface IApiQueryService
    {
        /// <summary>
        /// 根据key获取功能点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<FunctionDto?> Query(string key);

        /// <summary>
        /// 是否启用审计
        /// </summary>
        /// <remarks>
        /// 判断Api是否启用审计
        /// </remarks>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool?> IsEnableAudit(string key);
    }
}
