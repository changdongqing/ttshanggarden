// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Core.UserCenter.Dtos
{
    /// <summary>
    /// 角色资源关系
    /// </summary>
    [Display(Name = nameof(UserCenterResource.RoleResource), ResourceType = typeof(UserCenterResource))]
    public class RoleResourceDto : TenantBaseDtoNoKey
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Display(Name = nameof(UserCenterResource.RoleId), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public int RoleId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public RoleDto Role { get; set; } = null!;
        /// <summary>
        /// 权限Id
        /// </summary>
        [Display(Name = nameof(UserCenterResource.ResourceId), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [NotMapped]
        public ResourceDto Resource { get; set; } = null!;
    }
}
