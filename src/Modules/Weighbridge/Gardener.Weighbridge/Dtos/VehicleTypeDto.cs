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
    /// 车辆类型
    /// </summary>
    [Display(Name = nameof(WeighbridgeLocalResource.VehicleType), ResourceType = typeof(WeighbridgeLocalResource))]
    public class VehicleTypeDto : TenantBaseDto<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Name), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public string Name { get; set; } = default!;

        /// <summary>
        /// 最大载重（吨）
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.MaximumLoad), ResourceType = typeof(WeighbridgeLocalResource))]
        [Range(0, 1000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.NumberRangeValidationError))]
        public double? MaximumLoad {  get; set; }

        /// <summary>
        /// 轴数
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Axles), ResourceType = typeof(WeighbridgeLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [Range(0, 100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.NumberRangeValidationError))]
        public int Axles {  get; set; }

    }
}
