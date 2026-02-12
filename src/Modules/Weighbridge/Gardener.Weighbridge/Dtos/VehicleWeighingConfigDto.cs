// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dtos;
using Gardener.Core.Resources;
using Gardener.Weighbridge.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Weighbridge.Dtos
{
    /// <summary>
    /// 车辆称重配置
    /// </summary>
    [Display(Name = nameof(WeighbridgeLocalResource.VehicleWeighingConfig), ResourceType = typeof(WeighbridgeLocalResource))]
    public class VehicleWeighingConfigDto : TenantBaseDto<int>
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.PlateNumber), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public string PlateNumber { get; set; } = default!;

        /// <summary>
        /// 最大载重(Kg)
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.MaximumLoad), ResourceType = typeof(WeighbridgeLocalResource))]
        public double? MaximumLoad { get; set; }
    }
}
