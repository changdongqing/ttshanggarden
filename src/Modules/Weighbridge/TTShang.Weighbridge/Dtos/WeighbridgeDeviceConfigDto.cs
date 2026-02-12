// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Attributes;
using TTShang.Core.Dtos;
using TTShang.Core.Resources;
using TTShang.Iot.Dtos;
using TTShang.Weighbridge.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.Weighbridge.Dtos
{
    /// <summary>
    /// 地磅设备配置
    /// </summary>
    [Display(Name = nameof(WeighbridgeLocalResource.WeighbridgeDeviceConfig), ResourceType = typeof(WeighbridgeLocalResource))]
    public class WeighbridgeDeviceConfigDto : BaseDto<int>
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.DeviceId), ResourceType = typeof(WeighbridgeLocalResource))]
        [GuidRequired(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid DeviceId { get; set; }

        /// <summary>
        /// 最大载重(Kg)
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.MaximumLoad), ResourceType = typeof(WeighbridgeLocalResource))]
        public double? MaximumLoad { get; set; }

        /// <summary>
        /// 分度值
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.DivisionValue), ResourceType = typeof(WeighbridgeLocalResource))]
        [Range(0,1000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.NumberRangeValidationError))]
        public int DivisionValue {  get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Device), ResourceType = typeof(WeighbridgeLocalResource))]
        [NotMapped]
        public DeviceDto? Device { get; set; }

        /// <summary>
        /// 误差系数
        /// </summary>
        /// <remarks>
        /// 如果设置误差系数，重量将根据系数进行补偿（正负值，范围-1~1）
        /// </remarks>
        [Display(Name = nameof(WeighbridgeLocalResource.ErrorCoefficient), ResourceType = typeof(WeighbridgeLocalResource))]
        public double? ErrorCoefficient { get; set; }
    }
}
