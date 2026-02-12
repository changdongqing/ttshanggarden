// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dtos.Constraints;

namespace Gardener.Core.Client.Authorization
{
    public class LoginUserInfo
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserDto? User { get; set; }
        /// <summary>
        /// 页面资源key集合
        /// </summary>
        public List<string>? UiResourceKeys { get; set; }
        /// <summary>
        /// 菜单资源
        /// </summary>
        public List<ResourceDto>? MenuResources { get; set; }

        /// <summary>
        /// 超级管理员
        /// </summary>
        public bool CurrentUserIsSuperAdmin
        {
            get
            {
                return User?.IsSuperAdministrator ?? false;
            }
        }
        /// <summary>
        /// 是否是租户
        /// </summary>
        public bool CurrentUserIsTenant
        {
            get
            {
                return User == null ? true : ((IModelTenantId)User).IsTenant;
            }
        }
    }
}
