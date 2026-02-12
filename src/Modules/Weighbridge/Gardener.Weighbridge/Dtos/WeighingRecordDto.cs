// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Attributes;
using Gardener.Core.Dtos;
using Gardener.Core.Resources;
using Gardener.Weighbridge.Enums;
using Gardener.Weighbridge.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Weighbridge.Dtos
{
    /// <summary>
    /// 称重记录
    /// </summary>
    [Display(Name = nameof(WeighbridgeLocalResource.WeighingRecord), ResourceType = typeof(WeighbridgeLocalResource))]
    public class WeighingRecordDto : TenantBaseDto<Guid>
    {

        /// <summary>
        /// 地磅配置编号
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.WeighbridgeConfigId), ResourceType = typeof(WeighbridgeLocalResource))]
        [GuidRequired(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid WeighbridgeConfigId { get; set; }

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
        public string? VehicleType { get; set; }

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
        public string? Driver { get; set; }

        /// <summary>
        /// 货物名称
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.CommodityName), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? CommodityName { get; set; }

        /// <summary>
        /// 货物代码
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.CommodityCode), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? CommodityCode { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        /// <remarks>
        /// 最后一次总重量
        /// </remarks>
        [Display(Name = nameof(WeighbridgeLocalResource.Weight), ResourceType = typeof(WeighbridgeLocalResource))]
        public double Weight { get; set; }

        /// <summary>
        /// 皮重
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.TareWeight), ResourceType = typeof(WeighbridgeLocalResource))]
        public double TareWeight { get; set; }

        /// <summary>
        /// 货重
        /// </summary>
        /// <remarks>
        /// 货重=总重-皮重
        /// </remarks>
        [Display(Name = nameof(WeighbridgeLocalResource.CommodityWeight), ResourceType = typeof(WeighbridgeLocalResource))]
        [NotMapped]
        public double CommodityWeight
        {
            get
            {
                return Weight - TareWeight;
            }
        }

        /// <summary>
        /// 称重次数
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.WeighingNumber), ResourceType = typeof(WeighbridgeLocalResource))]
        public int WeighingNumber { get; set; }

        /// <summary>
        /// 称重状态
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.WeighingStatus), ResourceType = typeof(WeighbridgeLocalResource))]
        public WeighingStatus WeighingStatus { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        [NotMapped]
        public List<WeighingRecordLogDto> WeighingRecordLogs { get; set; } = new();

        /// <summary>
        /// 地磅组合
        /// </summary>
        [NotMapped]
        [Display(Name = nameof(WeighbridgeLocalResource.WeighbridgeConfig), ResourceType = typeof(WeighbridgeLocalResource))]
        public WeighbridgeConfigDto? WeighbridgeConfig { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.OperatorName), ResourceType = typeof(WeighbridgeLocalResource))]
        public string OperatorName { get; set; } = default!;
    }
}
