// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dtos.Constraints;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Core.UserCenter.Dtos
{
    /// <summary>
    /// 系统租户配置
    /// </summary>
    [Display(Name = nameof(UserCenterResource.SystemTenantConfig), ResourceType = typeof(UserCenterResource))]
    public class SystemTenantConfigDto : BaseDto<int>
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [Display(Name = nameof(UserCenterResource.TenantId), ResourceType = typeof(UserCenterResource))]
        [GuidRequired(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid TenantId { get; set; }
        /// <summary>
        /// 配置键
        /// </summary>
        [Display(Name = nameof(UserCenterResource.ConfigKey), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ConfigKey { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Remark), ResourceType = typeof(UserCenterResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Remark { get; set; }
        /// <summary>
        /// 配置值
        /// </summary>
        [Display(Name = nameof(UserCenterResource.ConfigValue), ResourceType = typeof(UserCenterResource))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ConfigValue { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Tenant), ResourceType = typeof(SharedLocalResource))]
        [NotMapped]
        public ITenant? Tenant { get; set; }
    }
}
