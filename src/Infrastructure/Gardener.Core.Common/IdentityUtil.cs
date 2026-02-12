// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.Core.Authorization.Services;

namespace Gardener.Core.Common
{
    /// <summary>
    /// 身份快捷操作
    /// </summary>
    public class IdentityUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Identity? GetIdentity()
        {
            IIdentityService identityService = App.GetService<IIdentityService>();

            return identityService.GetIdentity();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string? GetIdentityId()
        {
            return GetIdentity()?.Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IdentityType GetIdentityType()
        {
            Identity? identity = GetIdentity();
            return identity == null ? IdentityType.Unknown : identity.IdentityType;
        }
    }
}
