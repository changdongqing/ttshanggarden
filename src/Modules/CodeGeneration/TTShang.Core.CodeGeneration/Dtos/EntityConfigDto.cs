// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.CodeGeneration.Resources;
using TTShang.Core.Dtos;
using TTShang.Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace TTShang.Core.CodeGeneration.Dtos
{
    /// <summary>
    /// 实体类配置
    /// </summary>
    [Display(Name = nameof(CodeGenLocalResource.EntityConfig), ResourceType = typeof(CodeGenLocalResource))]
    public class EntityConfigDto : BaseDto<string>
    {
        /// <summary>
        /// 资源key前缀
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.ResourceKeyPrefix), ResourceType = typeof(CodeGenLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ResourceKeyPrefix { get; set; } = default!;
        /// <summary>
        /// 基础命名空间
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.BaseNameSpace), ResourceType = typeof(CodeGenLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string BaseNameSpace { get; set; } = default!;
        /// <summary>
        /// 接口命名空间
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.ApiNameSpace), ResourceType = typeof(CodeGenLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ApiNameSpace { get; set; } = default!;
        /// <summary>
        /// 客户端命名空间
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.ClientNameSpace), ResourceType = typeof(CodeGenLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ClientNameSpace { get; set; } = default!;
        /// <summary>
        /// 菜单地址
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.MenuPath), ResourceType = typeof(CodeGenLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? MenuPath { get; set; }
        /// <summary>
        /// 菜单Icon
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.MenuIcon), ResourceType = typeof(CodeGenLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? MenuIcon { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.MenuName), ResourceType = typeof(CodeGenLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? MenuName { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.ModuleName), ResourceType = typeof(CodeGenLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ModuleName { get; set; } = default!;
        /// <summary>
        /// 接口标签名称
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.FunctionTagName), ResourceType = typeof(CodeGenLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? FunctionTagName { get; set; }
        /// <summary>
        /// 支持多租户
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.SupportMultiTenant), ResourceType = typeof(CodeGenLocalResource))]
        public bool SupportMultiTenant { get; set; }
        /// <summary>
        /// 启用锁定
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.EnableLock), ResourceType = typeof(CodeGenLocalResource))]
        public bool EnableLock { get; set; }
        /// <summary>
        /// 启用删除选中
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.EnableDeleteSelected), ResourceType = typeof(CodeGenLocalResource))]
        public bool EnableDeleteSelected { get; set; }
        /// <summary>
        /// 启用删除
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.EnableDelete), ResourceType = typeof(CodeGenLocalResource))]
        public bool EnableDelete { get; set; }
        /// <summary>
        /// 启用搜素
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.EnableSearch), ResourceType = typeof(CodeGenLocalResource))]
        public bool EnableSearch { get; set; }
        /// <summary>
        /// 启用刷新
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.EnableRefresh), ResourceType = typeof(CodeGenLocalResource))]
        public bool EnableRefresh { get; set; }
        /// <summary>
        /// 启用添加
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.EnableAdd), ResourceType = typeof(CodeGenLocalResource))]
        public bool EnableAdd { get; set; }
        /// <summary>
        /// 启用更新
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.EnableUpdate), ResourceType = typeof(CodeGenLocalResource))]
        public bool EnableUpdate { get; set; }
        /// <summary>
        /// 启用详情
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.EnableDetail), ResourceType = typeof(CodeGenLocalResource))]
        public bool EnableDetail { get; set; }
        /// <summary>
        /// 使用树形列表
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.UseTreeList), ResourceType = typeof(CodeGenLocalResource))]
        public bool UseTreeList { get; set; }
        /// <summary>
        /// 启用添加子级
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.EnableAddChildren), ResourceType = typeof(CodeGenLocalResource))]
        public bool EnableAddChildren { get; set; }

    }
}
