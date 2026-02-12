// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dtos.Constraints;

namespace TTShang.Core.SystemAsset.Dtos
{
    /// <summary>
    /// 资源功能关系信息
    /// </summary>
    [Display(Name = nameof(SystemAssetResource.ResourceFunction), ResourceType = typeof(SystemAssetResource))]
    public class ResourceFunctionDto: IModelModule
    {

        /// <summary>
        /// 权限Id
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.ResourceId), ResourceType = typeof(SystemAssetResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.FunctionId), ResourceType = typeof(SystemAssetResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid FunctionId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.CreatedTime), ResourceType = typeof(SystemAssetResource))]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// 模块名称
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.ModuleName), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ModuleName { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Function), ResourceType = typeof(SystemAssetResource))]
        public FunctionDto? Function { get; set; }
    }
}
