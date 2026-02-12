// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dict.Resources;
using TTShang.Core.Dtos.Constraints;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.Core.Dict.Dtos
{
    /// <summary>
    /// 字典信息
    /// </summary>
    [Display(Name = nameof(DictResource.Code), ResourceType = typeof(DictResource))]
    public class CodeDto : BaseDto<int>, IModelModule
    {
        /// <summary>
        /// 字段类型编号
        /// </summary>
        [Display(Name = nameof(DictResource.CodeTypeId), ResourceType = typeof(DictResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public int CodeTypeId { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        [Display(Name = nameof(DictResource.CodeValue), ResourceType = typeof(DictResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string CodeValue { get; set; } = null!;
        /// <summary>
        /// 字典名称
        /// </summary>
        [Display(Name = nameof(DictResource.CodeName), ResourceType = typeof(DictResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string CodeName { get; set; } = null!;
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = nameof(DictResource.Order), ResourceType = typeof(DictResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public int Order { get; set; }
        /// <summary>
        /// 扩展参数
        /// </summary>
        [Display(Name = nameof(DictResource.ExtendParams), ResourceType = typeof(DictResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ExtendParams { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [Display(Name = nameof(DictResource.Color), ResourceType = typeof(DictResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Color { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        [Display(Name = nameof(DictResource.CodeType), ResourceType = typeof(DictResource))]
        [NotMapped]
        public CodeTypeDto CodeType { get; set; } = null!;
        /// <summary>
        /// 模块名称
        /// </summary>
        [Display(Name = nameof(DictResource.ModuleName), ResourceType = typeof(DictResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ModuleName { get; set; }
    }
}
