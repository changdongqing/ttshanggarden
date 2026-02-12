// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Authorization.Services
{
    /// <summary>
    /// 登录日志服务
    /// </summary>
    public interface ILoginLogService : IServiceBase<LoginLogDto, Int64>
    {
        /// <summary>
        /// 获取用户上一次登录信息
        /// </summary>
        /// <remarks>
        /// 获取当前用户上一次登录信息
        /// </remarks>
        /// <returns></returns>
        Task<LoginLogDto?> GetUserLastLoginLog();
    }
}
