// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Attachment.Dtos;
using Gardener.Core.Printer.Enums;
using Gardener.Core.Printer.Resources;

namespace Gardener.Core.Printer.Dtos
{
    /// <summary>
    /// 打印模板
    /// </summary>
    [Display(Name = nameof(PrinterLocalResource.PrintTemplate), ResourceType = typeof(PrinterLocalResource))]
    public class PrintTemplateDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 模板key
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Display(Name = nameof(PrinterLocalResource.TemplateKey), ResourceType = typeof(PrinterLocalResource))]
        public string TemplateKey { get; set; } = default!;
        /// <summary>
        /// 模板类型
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Display(Name = nameof(PrinterLocalResource.TemplateType), ResourceType = typeof(PrinterLocalResource))]
        [CodeType("print_template_type")]
        public string TemplateType { get; set; } = default!;
        /// <summary>
        /// 模板名称
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Display(Name = nameof(PrinterLocalResource.TemplateName), ResourceType = typeof(PrinterLocalResource))]
        public string TemplateName { get; set; } = default!;

        /// <summary>
        /// 模板高
        /// </summary>
        [Display(Name = nameof(PrinterLocalResource.TemplateHeight), ResourceType = typeof(PrinterLocalResource))]
        public int TemplateHeight { get; set; }

        /// <summary>
        /// 模板宽
        /// </summary>
        [Display(Name = nameof(PrinterLocalResource.TemplateWidth), ResourceType = typeof(PrinterLocalResource))]
        public int TemplateWidth { get; set; }

        /// <summary>
        /// 模板预览图
        /// </summary>
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Display(Name = nameof(PrinterLocalResource.TemplatePreviewImage), ResourceType = typeof(PrinterLocalResource))]
        public string? TemplatePreviewImage {  get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Display(Name = nameof(PrinterLocalResource.TemplateContent), ResourceType = typeof(PrinterLocalResource))]
        public string TemplateContent { get; set; } = default!;

        /// <summary>
        /// 模板结果类型
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [Display(Name = nameof(PrinterLocalResource.TemplateResultType), ResourceType = typeof(PrinterLocalResource))]
        public TemplateResultType TemplateResultType { get; set; } = TemplateResultType.CommandJson;

        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <returns></returns>
        public UploadAttachmentOutput[]? GetTemplatePreviewImageInfos()
        {
            if (TemplatePreviewImage == null) { return null; }
            return System.Text.Json.JsonSerializer.Deserialize<UploadAttachmentOutput[]>(TemplatePreviewImage);
        }

    }
}
