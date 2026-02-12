// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Iot.Dtos
{
    /// <summary>
    /// 设备系统日志
    /// </summary>
    [Display(Name = nameof(IotLocalResource.Device), ResourceType = typeof(IotLocalResource))]
    public class DeviceSystemLogDto : TenantBaseDto<long>
    {
        /// <summary>
        /// 设备客户端编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceClientId), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string DeviceClientId { get; set; } = null!;

        /// <summary>
        /// 设备连接编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceConnectionId), ResourceType = typeof(IotLocalResource))]
        public long? DeviceConnectionId {  get; set; }
 
        /// <summary>
        /// 设备编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceId), ResourceType = typeof(IotLocalResource))]
        public Guid? DeviceId { get; set; }

        /// <summary>
        /// 设备账户
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceAccount), ResourceType = typeof(IotLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? DeviceAccount { get; set; }

        /// <summary>
        /// 设备系统日志类型
        /// </summary>
        [Display(Name = nameof(IotLocalResource.SystemLogType), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public DeviceSystemLogType SystemLogType { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Content), ResourceType = typeof(IotLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Content { get; set; }

    }
}
