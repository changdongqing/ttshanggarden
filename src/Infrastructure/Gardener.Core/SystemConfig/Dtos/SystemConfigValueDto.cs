// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.SystemConfig.Resources;

namespace Gardener.Core.SystemConfig.Dtos
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfigValueDto: BaseDto<string>
    {
        /// <summary>
        /// 配置值
        /// </summary>
        [Display(Name = nameof(SystemConfigResource.Value), ResourceType = typeof(SystemConfigResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Value { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = nameof(SystemConfigResource.Remark), ResourceType = typeof(SystemConfigResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Remark { get; set; } = null!;
        /// <summary>
        /// 模块名称
        /// </summary>
        [Display(Name = nameof(SystemConfigResource.ModuleName), ResourceType = typeof(SystemConfigResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ModuleName { get; set; }
    }
}
