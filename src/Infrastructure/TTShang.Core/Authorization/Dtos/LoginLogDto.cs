// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Authorization.Enums;
using TTShang.Core.Authorization.Resources;

namespace TTShang.Core.Authorization.Dtos
{
    /// <summary>
    /// 登录日志
    /// </summary>
    [Display(Name = nameof(AuthorizationLocalResource.LoginLog), ResourceType = typeof(AuthorizationLocalResource))]
    public class LoginLogDto : TenantBaseDto<long>
    {
        /// <summary>
        /// 登录时间
        /// </summary>
        /// <summary>
        /// 登录时间
        /// </summary>
        [Display(Name = nameof(AuthorizationLocalResource.LoginTime), ResourceType = typeof(AuthorizationLocalResource))]
        public DateTimeOffset LoginTime { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        /// <summary>
        /// 访问IP
        /// </summary>
        [Display(Name = nameof(AuthorizationLocalResource.Ip), ResourceType = typeof(AuthorizationLocalResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Ip { get; set; }


        /// <summary>
        /// 身份编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IdentityId), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? IdentityId { get; set; }

        /// <summary>
        /// 身份唯一名称
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IdentityName), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string IdentityName { get; set; } = null!;

        /// <summary>
        /// 身份类型
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IdentityType), ResourceType = typeof(SharedLocalResource))]
        public IdentityType IdentityType { get; set; }

        /// <summary>
        /// 获取或设置 登录Id
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.LoginId), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? LoginId { get; set; }

        /// <summary>
        /// 登录的客户端类型
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.LoginClientType), ResourceType = typeof(SharedLocalResource))]
        public LoginClientType LoginClientType { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        [Display(Name = nameof(AuthorizationLocalResource.LoginStatus), ResourceType = typeof(AuthorizationLocalResource))]
        public LoginStatus LoginStatus { get; set; }
        /// <summary>
        /// 登录失败原因
        /// </summary>
        [Display(Name = nameof(AuthorizationLocalResource.LoginFailReason), ResourceType = typeof(AuthorizationLocalResource))]
        public LoginFailReason? LoginFailReason { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.ClientName), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ClientName { get; set; }
        /// <summary>
        /// 客户端版本
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.ClientVersion), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ClientVersion { get; set; }


    }
}
