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
    /// 字段配置
    /// </summary>
    [Display(Name = nameof(CodeGenLocalResource.FieldConfig), ResourceType = typeof(CodeGenLocalResource))]
    public class FieldConfigDto : BaseDto<int>
    {
        /// <summary>
        /// 实体类完整名称-唯一标识
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.EntityTypeFullName), ResourceType = typeof(CodeGenLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string EntityTypeFullName { get; set; } = default!;
        /// <summary>
        /// 字段名
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.FieldName), ResourceType = typeof(CodeGenLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string FieldName { get; set; } = default!;
        /// <summary>
        /// 列表排序
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.ListOrder), ResourceType = typeof(CodeGenLocalResource))]
        public int ListOrder { get; set; }
        /// <summary>
        /// 详情排序
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.DetailOrder), ResourceType = typeof(CodeGenLocalResource))]
        public int DetailOrder { get; set; }
        /// <summary>
        /// 在列表显示
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.ShowInList), ResourceType = typeof(CodeGenLocalResource))]
        public bool ShowInList { get; set; } = true;
        /// <summary>
        /// 在详情显示
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.ShowInDetail), ResourceType = typeof(CodeGenLocalResource))]
        public bool ShowInDetail { get; set; } = true;
        /// <summary>
        /// 是否能修改
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.CanModity), ResourceType = typeof(CodeGenLocalResource))]
        public bool CanModity { get; set; } = false;
        /// <summary>
        /// 是否可排序
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.Sortable), ResourceType = typeof(CodeGenLocalResource))]
        public bool Sortable { get; set; } = true;
        /// <summary>
        /// 是否可筛选
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.Filterable), ResourceType = typeof(CodeGenLocalResource))]
        public bool Filterable { get; set; } = true;

        /// <summary>
        /// 是否唯一
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.IsUnique), ResourceType = typeof(CodeGenLocalResource))]
        public bool IsUnique { get; set; } = false;
        /// <summary>
        /// 默认排序方式
        /// <para>升序 asc</para>
        /// <para>降序 desc</para>
        /// </summary>
        [Display(Name = nameof(CodeGenLocalResource.DefaultSortOrders), ResourceType = typeof(CodeGenLocalResource))]
        [MaxLength(10, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? DefaultSortOrder { get; set; }
    }
}
