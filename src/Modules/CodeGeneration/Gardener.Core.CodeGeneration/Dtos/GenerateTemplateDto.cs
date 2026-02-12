// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.CodeGeneration.Resources;
using Gardener.Core.Dtos;
using Gardener.Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.CodeGeneration.Dtos
{
    /// <summary>
    /// 生成模板
    /// </summary>
    [Display(Name = nameof(CodeGenLocalResource.GenerateTemplate), ResourceType = typeof(CodeGenLocalResource))]
    public class GenerateTemplateDto : BaseDto<int>
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.TemplateName), ResourceType = typeof(CodeGenLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string TemplateName { get; set; } = default!;
        /// <summary>
        /// 模板内容
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.TemplateContent), ResourceType = typeof(CodeGenLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [DataType(DataType.Text)]
        public string TemplateContent {  get; set; } = default!;
    }
}
