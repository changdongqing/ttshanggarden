// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Dtos
{
    /// <summary>
    /// 设备标签
    /// </summary>
    [Display(Name = nameof(IotLocalResource.DeviceTag), ResourceType = typeof(IotLocalResource))]
    public class DeviceTagDto : TenantBaseDtoNoKey
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceId), ResourceType = typeof(IotLocalResource))]
        public Guid DeviceId { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Tag), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Tag { get; set; } = null!;
    }
}
