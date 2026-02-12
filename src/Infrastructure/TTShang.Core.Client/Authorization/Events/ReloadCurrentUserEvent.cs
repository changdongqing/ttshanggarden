// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Authorization.Events
{
    /// <summary>
    /// 重载当前用户事件
    /// </summary>
    public class ReloadCurrentUserEvent : EventBase
    {
        /// <summary>
        /// 重载当前用户事件
        /// </summary>
        /// <param name="token"></param>
        /// <param name="loginUserInfo"></param>
        public ReloadCurrentUserEvent(TokenOutput token, LoginUserInfo loginUserInfo) : base()
        {
            Token = token;
            LoginUserInfo = loginUserInfo;
        }
        /// <summary>
        /// 登录token
        /// </summary>
        public TokenOutput Token { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public LoginUserInfo LoginUserInfo { get; set; }
    }
}
