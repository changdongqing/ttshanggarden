using TTShang.Core.Attributes;

namespace TTShang.Iot.Dtos
{
    /// <summary>
    /// 设备绑定租户
    /// </summary>
    public class DeviceBindTenantInput
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceId), ResourceType = typeof(IotLocalResource))]
        [GuidRequired(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid DeviceId{ get; set; }
        /// <summary>
        /// 租户编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.TenantId), ResourceType = typeof(SharedLocalResource))]
        public Guid? TenantId { get; set; }
    }
}
