// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Attributes;

namespace TTShang.Iot.Dtos
{
    /// <summary>
    /// 更新设备别名
    /// </summary>
    public class UpdateDeviceAliasInput
    {
        /// <summary>
        /// 更新设备别名
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="alias"></param>
        public UpdateDeviceAliasInput(Guid deviceId, string? alias)
        {
            DeviceId = deviceId;
            Alias = alias;
        }

        /// <summary>
        /// 设备编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceId), ResourceType = typeof(IotLocalResource))]
        [GuidRequired(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid DeviceId { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Alias), ResourceType = typeof(IotLocalResource))]
        [MaxLength(5, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Alias {  get; set; }
    }
}
