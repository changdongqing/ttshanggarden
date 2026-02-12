// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dtos.Constraints;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TTShang.Core.SystemAsset.Dtos
{
    /// <summary>
    /// 系统-功能（api）
    /// </summary>
    [Display(Name = nameof(SystemAssetResource.Function), ResourceType = typeof(SystemAssetResource))]
    public class FunctionDto : BaseDto<Guid>, IModelModule
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Key), ResourceType = typeof(SystemAssetResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Key { get; set; } = null!;

        /// <summary>
        /// 启用审计
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.EnableAudit), ResourceType = typeof(SystemAssetResource))]
        public bool EnableAudit { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.ModuleName), ResourceType = typeof(SystemAssetResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ModuleName { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Group), ResourceType = typeof(SystemAssetResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Group { get; set; } = null!;
        /// <summary>
        /// 分组
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.GroupTitle), ResourceType = typeof(SystemAssetResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? GroupTitle { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Tags), ResourceType = typeof(SystemAssetResource))]
        public string? Tags { get; set; }

        /// <summary>
        /// 标签标题
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.TagTitles), ResourceType = typeof(SystemAssetResource))]
        public string? TagTitles { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Summary), ResourceType = typeof(SystemAssetResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Summary { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Description), ResourceType = typeof(SystemAssetResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Description { get; set; }

        /// <summary>
        /// API路由地址
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Path), ResourceType = typeof(SystemAssetResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Path { get; set; } = null!;

        /// <summary>
        /// 接口请求方法
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Method), ResourceType = typeof(SystemAssetResource))]
        public ApiHttpMethod Method { get; set; } = ApiHttpMethod.GET;


        /// <summary>
        /// 填充api信息
        /// </summary>
        /// <param name="api"></param>
        public void FillApiInfo(ApiEndpoint api)
        {
            Key = api.Key;
            Group = api.Group;
            GroupTitle = api.GroupTitle ?? api.Group;
            Summary = api.Summary;
            Description = api.Description;
            Path = api.Path;
            Method = api.Method;
            if (api.Tags != null)
            {
                Tags = string.Join(',', api.Tags.Keys.ToList());
                TagTitles = string.Join(',', api.Tags.Values.ToList());
            }
        }
    }
}
