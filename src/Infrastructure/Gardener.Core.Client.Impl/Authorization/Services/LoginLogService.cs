// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Authorization.Services;

namespace Gardener.Core.Client.Impl.Authorization.Services
{
    /// <summary>
    ///  登录日志服务
    /// </summary>
    [ScopedService]
    public class LoginLogService : ClientServiceBase<LoginLogDto, Int64>, ILoginLogService
    {
        /// <summary>
        ///  登录日志服务
        /// </summary>
        public LoginLogService(IApiCaller apiCaller) : base(apiCaller, "login-log")
        {
        }

        public Task<LoginLogDto?> GetUserLastLoginLog()
        {
            return apiCaller.GetAsync<LoginLogDto?>($"{baseUrl}/user-last-login-log");
        }
    }
}
