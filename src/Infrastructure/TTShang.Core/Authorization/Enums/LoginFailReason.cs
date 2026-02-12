// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Authorization.Enums
{
    /// <summary>
    /// 登录失败原因
    /// </summary>
    public enum LoginFailReason
    {
        /// <summary>
        /// 用户不存在
        /// </summary>
        UserNotExist,
        /// <summary>
        /// 用户已锁定
        /// </summary>
        UserLocked,
        /// <summary>
        /// 密码不正确
        /// </summary>
        WrongPassword
    }
}
