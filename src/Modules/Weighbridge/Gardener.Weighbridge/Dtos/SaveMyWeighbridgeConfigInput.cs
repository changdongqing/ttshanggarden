using Gardener.Core.Resources;
using Gardener.Weighbridge.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Weighbridge.Dtos
{
    /// <summary>
    /// 保存我的地磅配置输入
    /// </summary>
    public class SaveMyWeighbridgeConfigInput
    {
        /// <summary>
        /// 地磅配置编号
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.WeighbridgeConfigId), ResourceType = typeof(WeighbridgeLocalResource))]
        public Guid? WeighbridgeConfigId {  get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(WeighbridgeLocalResource.Name), ResourceType = typeof(WeighbridgeLocalResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 设备编号组
        /// </summary>
        /// <remarks>
        /// 设备编号组
        /// </remarks>
        [Display(Name = nameof(WeighbridgeLocalResource.DeviceIds), ResourceType = typeof(WeighbridgeLocalResource))]
        public List<Guid> DeviceIds { get; set; } = new List<Guid>();

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
    }
}
