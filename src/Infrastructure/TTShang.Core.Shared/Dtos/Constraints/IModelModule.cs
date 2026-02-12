// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Dtos.Constraints
{
    /// <summary>
    /// 模块
    /// </summary>
    public interface IModelModule
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.ModuleName), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ModuleName { get; set; }
    }
}
