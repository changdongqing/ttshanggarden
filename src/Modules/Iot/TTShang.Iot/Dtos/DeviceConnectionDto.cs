// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Dtos
{
    /// <summary>
    /// 设备连接信息
    /// </summary>
    [Display(Name = nameof(IotLocalResource.DeviceConnection), ResourceType = typeof(IotLocalResource))]
    public class DeviceConnectionDto : TenantBaseDto<long>
    {
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
        /// 设备客户端编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceClientId), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string DeviceClientId { get; set; } = null!;

        /// <summary>
        /// 设备客户端连接地址
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceClientEndpoint), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string DeviceClientEndpoint { get; set; } = null!;

        /// <summary>
        /// 设备连接类型
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceConnectionType), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public DeviceConnectionType DeviceConnectionType { get; set; }

        /// <summary>
        /// 用户属性
        /// </summary>
        [Display(Name = nameof(IotLocalResource.UserProperties), ResourceType = typeof(IotLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? UserProperties { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceConnectionState), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public DeviceConnectionState DeviceConnectionState { get; set; }

        /// <summary>
        /// 设备断开连接时间
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceDisconnectTime), ResourceType = typeof(IotLocalResource))]
        public DateTimeOffset? DeviceDisconnectTime { get; set; }

        /// <summary>
        /// 设备断开连接原因
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceDisconnectReason), ResourceType = typeof(IotLocalResource))]
        public DeviceDisconnectReason? DeviceDisconnectReason { get; set; }

        /// <summary>
        /// 设备断开连接原因描述
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceDisconnectReasonDescription), ResourceType = typeof(IotLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? DeviceDisconnectReasonDescription { get; set; }

        /// <summary>
        /// 设备最后一次ping时间
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceLastPingTime), ResourceType = typeof(IotLocalResource))]
        public DateTimeOffset? DeviceLastPingTime { get; set; }

        /// <summary>
        /// 设备最后一次推送数据时间
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceLastPushDataTime), ResourceType = typeof(IotLocalResource))]
        public DateTimeOffset? DeviceLastPushDataTime { get; set; }
    }
}
