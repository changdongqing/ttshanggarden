// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dtos.Constraints;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.Core.SystemAsset.Dtos
{
    /// <summary>
    /// 系统-资源(菜单、按钮、视图)
    /// </summary>
    [Display(Name = nameof(SystemAssetResource.Resource), ResourceType = typeof(SystemAssetResource))]
    public class ResourceDto : BaseDto<Guid>, ITreeNode<ResourceDto>, IModelModule
    {
        /// <summary>
        /// 名称
        /// Locale Key
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Name), ResourceType = typeof(SystemAssetResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 唯一键
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Key), ResourceType = typeof(SystemAssetResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Key { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Remark), ResourceType = typeof(SystemAssetResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Remark { get; set; }
        /// <summary>
        /// 资源地址
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Path), ResourceType = typeof(SystemAssetResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Path { get; set; }
        /// <summary>
        /// 资源图标
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Icon), ResourceType = typeof(SystemAssetResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Icon { get; set; }
        /// <summary>
        /// 资源排序
        /// </summary>
        [DefaultValue(0)]
        [Display(Name = nameof(SystemAssetResource.Order), ResourceType = typeof(SystemAssetResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public int Order { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.ParentId), ResourceType = typeof(SystemAssetResource))]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Children), ResourceType = typeof(SystemAssetResource))]
        [NotMapped]
        public ICollection<ResourceDto>? Children { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DefaultValue(ResourceType.Menu)]
        [Display(Name = nameof(SystemAssetResource.Type), ResourceType = typeof(SystemAssetResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public ResourceType Type { get; set; }
        /// <summary>
        /// 是否支持多租户
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.SupportMultiTenant), ResourceType = typeof(SystemAssetResource))]
        public bool SupportMultiTenant { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        /// <remarks>
        /// 菜单类型：控制在界面中是否展示该菜单
        /// </remarks>
        [Display(Name = nameof(SystemAssetResource.Hide), ResourceType = typeof(SystemAssetResource))]
        public bool Hide { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.ModuleName), ResourceType = typeof(SystemAssetResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ModuleName { get; set; }
        /// <summary>
        /// 资源接口关系信息
        /// </summary>
        public ICollection<ResourceFunctionDto> ResourceFunctions { get; set; } = [];

    }
}
