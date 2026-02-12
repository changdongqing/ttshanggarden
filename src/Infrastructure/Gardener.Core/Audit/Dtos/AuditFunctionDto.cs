// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Audit.Resources;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Core.Audit.Dtos
{
    /// <summary>
    /// Api审计信息
    /// </summary>
    [Display(Name = nameof(AuditLocalResource.AuditFunction), ResourceType = typeof(AuditLocalResource))]
    public class AuditFunctionDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 功能唯一键
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.FunctionKey), ResourceType = typeof(AuditLocalResource))]
        public string? FunctionKey { get; set; }
        /// <summary>
        /// 功能概要
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.FunctionSummary), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? FunctionSummary { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.OperaterId), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? OperaterId { get; set; }
        /// <summary>
        /// 操作者名称
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.OperaterName), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? OperaterName { get; set; }
        /// <summary>
        /// 操作者类型
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.OperaterType), ResourceType = typeof(AuditLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public IdentityType OperaterType { get; set; }
        /// <summary>
        /// 访问IP
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.Ip), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Ip { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.UserAgent), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? UserAgent { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.Path), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Path { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.Method), ResourceType = typeof(AuditLocalResource))]
        public ApiHttpMethod Method { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.Parameters), ResourceType = typeof(AuditLocalResource))]
        public string? Parameters { get; set; }
        /// <summary>
        /// 关联数据审计
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.AuditEntities), ResourceType = typeof(AuditLocalResource))]
        public ICollection<AuditEntityDto>? AuditEntities { get; set; }

        /// <summary>
        /// 接口信息
        /// </summary>
        [NotMapped]
        [Display(Name = nameof(AuditLocalResource.ApiEndpoint), ResourceType = typeof(AuditLocalResource))]
        public ApiEndpoint? ApiEndpoint { get; set; }


        /// <summary>
        /// 客户端类型
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.LoginClientType), ResourceType = typeof(AuditLocalResource))]
        public LoginClientType LoginClientType { get; set; }


        /// <summary>
        /// 登录Id(每次登录该Id自动生成)
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.LoginId), ResourceType = typeof(AuditLocalResource))]
        public string? LoginId { get; set; }
        /// <summary>
        /// 请求头
        /// </summary>
        /// <remarks>
        /// 存储关心的请求头
        /// </remarks>
        [Display(Name = nameof(AuditLocalResource.HttpHeaders), ResourceType = typeof(AuditLocalResource))]
        public string? HttpHeaders { get; set; }
    }
}
