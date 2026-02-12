namespace Gardener.Iot.Dtos
{
    /// <summary>
    /// 设备组
    /// </summary>
    [Display(Name = nameof(IotLocalResource.DeviceGroup), ResourceType = typeof(IotLocalResource))]
    public class DeviceGroupDto : TenantBaseDto<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Name), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(30, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Description), ResourceType = typeof(IotLocalResource))]
        [MaxLength(300, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Description { get; set; }

        /// <summary>
        /// 资源排序
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Order), ResourceType = typeof(IotLocalResource))]
        public int Order { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [Display(Name = nameof(IotLocalResource.ParentId), ResourceType = typeof(IotLocalResource))]
        public int? ParentId { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<DeviceGroupDto>? Children { get; set; }
    }
}
