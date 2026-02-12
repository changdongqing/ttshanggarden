// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthSettings
    {
        /// <summary>
        /// token刷新过期时间阈值（单位：秒）
        /// </summary>
        public int RefreshTokenTimeThreshold { get; set; } = 70;
        /// <summary>
        /// 登陆页面地址
        /// </summary>
        public string LoginPagePath { get; set; } = "/auth/login";
    }
}
