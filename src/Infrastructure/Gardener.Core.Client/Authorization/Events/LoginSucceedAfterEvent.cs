// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Authorization.Events
{
    /// <summary>
    /// 登录成功事件
    /// </summary>
    public class LoginSucceedAfterEvent : EventBase
    {
        /// <summary>
        /// 登录成功事件
        /// </summary>
        /// <param name="token"></param>
        public LoginSucceedAfterEvent(TokenOutput token) : base()
        {
            Token = token;
        }
        /// <summary>
        /// 登录token
        /// </summary>
        public TokenOutput Token { get; set; }
    }
}