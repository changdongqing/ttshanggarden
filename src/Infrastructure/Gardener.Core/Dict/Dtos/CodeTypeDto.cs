// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dict.Resources;
using Gardener.Core.Dtos.Constraints;

namespace Gardener.Core.Dict.Dtos
{
    /// <summary>
    /// 字典类型
    /// </summary>
    [Display(Name = nameof(DictResource.CodeType), ResourceType = typeof(DictResource))]
    public class CodeTypeDto : BaseDto<int>, IModelModule
    {
        /// <summary>
        /// 字典类型名称
        /// </summary>
        [Display(Name = nameof(DictResource.CodeTypeName), ResourceType = typeof(DictResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string CodeTypeName { get; set; } = null!;
        /// <summary>
        /// 字典类型值
        /// </summary>
        [Display(Name = nameof(DictResource.CodeTypeValue), ResourceType = typeof(DictResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string CodeTypeValue { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = nameof(DictResource.Remark), ResourceType = typeof(DictResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Remark { get; set; }

        /// <summary>
        /// 字典集合
        /// </summary>
        public IEnumerable<CodeDto>? Codes { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.ModuleName), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ModuleName { get; set; }
    }
}
