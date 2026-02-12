// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.AppManager.Enums;
using TTShang.Core.AppManager.Resources;

namespace TTShang.Core.AppManager.Dtos
{
    /// <summary>
    /// 应用版本
    /// </summary>
    [Display(Name = nameof(AppManagerResource.AppVersion), ResourceType = typeof(AppManagerResource))]
    public class AppVersionDto : BaseDto<long>
    {
        /// <summary>
        /// 应用编号
        /// </summary>
        [Display(Name = nameof(AppManagerResource.AppId), ResourceType = typeof(AppManagerResource))]
        [GuidRequired(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid AppId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = nameof(AppManagerResource.VersionDescription), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string VersionDescription { get; set; } = default!;

        /// <summary>
        /// 版本号
        /// </summary>
        [Display(Name = nameof(AppManagerResource.VersionNumber), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [Range(1, long.MaxValue, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.NumberRangeValidationError))]
        public long VersionNumber { get; set; } = 1;

        /// <summary>
        /// 版本名
        /// </summary>
        [Display(Name = nameof(AppManagerResource.VersionName), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public string VersionName { get; set; } = default!;

        /// <summary>
        /// 应用本地地址
        /// </summary>
        /// <remarks>
        /// 用于初始手动下载地址
        /// </remarks>
        [Display(Name = nameof(AppManagerResource.AppLocalUrl), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]

        public string AppLocalUrl { get; set; } = default!;
        /// <summary>
        /// 应用地址
        /// </summary>
        /// <remarks>
        /// 用于app自动更新地址
        /// </remarks>
        [Display(Name = nameof(AppManagerResource.AppUrl), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string AppUrl { get; set; } = default!;

        /// <summary>
        /// 强制更新
        /// </summary>
        [Display(Name = nameof(AppManagerResource.ForcedUpdating), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool ForcedUpdating {  get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        [Display(Name = nameof(AppManagerResource.FileName), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string FileName {  get; set; }= default!;

        /// <summary>
        /// 应用环境
        /// </summary>
        [Display(Name = nameof(AppManagerResource.Environment), ResourceType = typeof(AppManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public AppEnvironments Environment { get; set; }

        /// <summary>
        /// 应用
        /// </summary>
        public AppDto? App { get; set; }
    }
}
