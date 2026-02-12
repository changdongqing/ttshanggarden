// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Attributes;
using TTShang.Core.Dtos;
using TTShang.Core.Resources;
using TTShang.Weighbridge.Enums;
using TTShang.Weighbridge.Resources;
using System.ComponentModel.DataAnnotations;

namespace TTShang.Weighbridge.Dtos
{
    /// <summary>
    /// 称重记录日志
    /// </summary>
    [Display(Name = nameof(WeighbridgeLocalResource.WeighingRecordLog), ResourceType = typeof(WeighbridgeLocalResource))]
    public class WeighingRecordLogDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 称重记录编号
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.WeighingRecordId), ResourceType = typeof(WeighbridgeLocalResource))]
        [GuidRequired(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid WeighingRecordId {  get; set; }

        /// <summary>
        /// 地磅配置编号
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.WeighbridgeConfigId), ResourceType = typeof(WeighbridgeLocalResource))]
        [GuidRequired(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid WeighbridgeConfigId {  get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.PlateNumber), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public string PlateNumber { get; set; } = default!;

        /// <summary>
        /// 车辆类型
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.VehicleType), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? VehicleType { get; set; } = default!;

        /// <summary>
        /// 最大载重(吨)
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.MaximumLoad), ResourceType = typeof(WeighbridgeLocalResource))]
        public double? MaximumLoad { get; set; }

        /// <summary>
        /// 司机
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Driver), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(10, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Driver {  get; set; }

        /// <summary>
        /// 货物名称
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.CommodityName), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(10, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? CommodityName { get; set; }

        /// <summary>
        /// 货物代码
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.CommodityCode), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(10, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? CommodityCode { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Weight), ResourceType = typeof(WeighbridgeLocalResource))]
        public double Weight { get; set; }

        /// <summary>
        /// 重量变化
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.WeightChange), ResourceType = typeof(WeighbridgeLocalResource))]
        public double WeightChange { get; set; }

        /// <summary>
        /// 称重状态
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.WeighingStatus), ResourceType = typeof(WeighbridgeLocalResource))]
        public WeighingStatus WeighingStatus { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.OperatorName), ResourceType = typeof(WeighbridgeLocalResource))]
        public string OperatorName { get; set; } = default!;
    }
}
