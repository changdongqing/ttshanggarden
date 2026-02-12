// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.AppManager.Resources;

namespace TTShang.Core.AppManager.Dtos
{
    /// <summary>
    /// 应用
    /// </summary>
    [Display(Name = nameof(AppManagerResource.App), ResourceType = typeof(AppManagerResource))]
    public class AppDto : BaseDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(AppManagerResource.AppName), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string AppName { get; set; } = default!;

        /// <summary>
        /// 应用包名
        /// </summary>
        [Display(Name = nameof(AppManagerResource.PackageName), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string PackageName { get; set; } = null!;

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = nameof(AppManagerResource.AppDescription), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string AppDescription { get; set; } = default!;

        /// <summary>
        /// 联系信息
        /// </summary>
        [Display(Name = nameof(AppManagerResource.ContactInformation), ResourceType = typeof(AppManagerResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ContactInformation {  get; set; }

        /// <summary>
        /// 隐私政策
        /// </summary>
        [Display(Name = nameof(AppManagerResource.PrivacyPolicy), ResourceType = typeof(AppManagerResource))]
        [MaxLength(2000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? PrivacyPolicy {  get; set; }

        /// <summary>
        /// 应用版本
        /// </summary>
        public ICollection<AppVersionDto> AppVersions { get; set; } = [];
    }
}
