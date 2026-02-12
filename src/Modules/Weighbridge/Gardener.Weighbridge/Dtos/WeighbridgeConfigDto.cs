// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Attributes;
using Gardener.Core.Dtos;
using Gardener.Core.Resources;
using Gardener.Iot.Dtos;
using Gardener.Weighbridge.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Weighbridge.Dtos
{
    /// <summary>
    /// 地磅配置
    /// </summary>
    [Display(Name = nameof(WeighbridgeLocalResource.WeighbridgeConfig), ResourceType = typeof(WeighbridgeLocalResource))]
    public class WeighbridgeConfigDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 设备编号组
        /// </summary>
        /// <remarks>
        /// 多个以逗号隔开
        /// </remarks>
        [Display(Name = nameof(WeighbridgeLocalResource.DeviceIds), ResourceType = typeof(WeighbridgeLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string DeviceIds { get; set; } = null!;
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Name), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Contacts), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Tel), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Tel { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Address), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Address { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Description), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Description { get; set; }

        /// <summary>
        /// 通道数
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.ChannelNumber), ResourceType = typeof(WeighbridgeLocalResource))]
        [Range(1, 32, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.NumberRangeValidationError))]
        public int ChannelNumber { get; set; } = 1;


        /// <summary>
        /// 设备信息
        /// </summary>
        [NotMapped]
        [Display(Name = nameof(WeighbridgeLocalResource.Devices), ResourceType = typeof(WeighbridgeLocalResource))]
        public List<DeviceDto> Devices { get; set; } = new ();
    }
}
