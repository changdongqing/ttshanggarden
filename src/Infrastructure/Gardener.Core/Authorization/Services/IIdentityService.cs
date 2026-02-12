// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Authorization.Services
{
    /// <summary>
    /// 身份服务
    /// </summary>
    /// <remarks>
    /// 获取当前请求域的身份信息
    /// </remarks>
    public interface IIdentityService
    {
        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        Identity? GetIdentity();
    }
}
