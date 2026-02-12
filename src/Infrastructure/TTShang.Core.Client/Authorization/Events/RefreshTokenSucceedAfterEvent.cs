// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Authorization.Events
{
    /// <summary>
    /// 刷新token成功后
    /// </summary>
    public class RefreshTokenSucceedAfterEvent : EventBase
    {
        /// <summary>
        /// 刷新token成功后
        /// </summary>
        /// <param name="token"></param>
        public RefreshTokenSucceedAfterEvent(TokenOutput token) : base()
        {
            Token = token;
        }
        /// <summary>
        /// 登录token
        /// </summary>
        public TokenOutput Token { get; set; }

    }
}
