// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Iot.Dtos
{
    /// <summary>
    /// 设备
    /// </summary>
    [Display(Name = nameof(IotLocalResource.Device), ResourceType = typeof(IotLocalResource))]
    public class DeviceDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.ProductId), ResourceType = typeof(IotLocalResource))]
        public Guid? ProductId { get; set; }
        /// <summary>
        /// 设备组编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceGroupId), ResourceType = typeof(IotLocalResource))]
        public int? DeviceGroupId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Name), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(30, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 别名
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Alias), ResourceType = typeof(IotLocalResource))]
        [MaxLength(5, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Alias {  get; set; }
        /// <summary>
        /// 客户端编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.ClientId), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(10, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        public string ClientId { get; set; } = null!;

        /// <summary>
        /// 账户
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Account), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(5, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        public string Account { get; set; } = null!;

        /// <summary>
        /// 密钥
        /// </summary>
        [Display(Name = nameof(IotLocalResource.SecretKey), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(128, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(10, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        public string SecretKey { get; set; }=null!;

        /// <summary>
        /// 保留历史数据
        /// </summary>
        [Display(Name = nameof(IotLocalResource.StorageHistoryData), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool StorageHistoryData { get; set; }

        /// <summary>
        /// 设备组信息
        /// </summary>
        public DeviceGroupDto? DeviceGroup { get; set; }
 
        /// <summary>
        /// 设备标签集
        /// </summary>
        public ICollection<DeviceTagDto>? DeviceTags { get; set; }

        /// <summary>
        /// 连接中设备连接
        /// </summary>
        [NotMapped]
        [Display(Name = nameof(IotLocalResource.ConnectingDeviceConnection), ResourceType = typeof(IotLocalResource))]
        public DeviceConnectionDto? ConnectingDeviceConnection {  get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public ProductDto? Product { get; set; }

        /// <summary>
        /// 输入内容类型
        /// </summary>
        [Display(Name = nameof(IotLocalResource.InputContentType), ResourceType = typeof(IotLocalResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? InputContentType { get; set; }

        /// <summary>
        /// 获取格式化名称
        /// </summary>
        /// <returns></returns>
        public string GetFormatName()
        {
            return (string.IsNullOrEmpty(Alias) ? "" : "(" + Alias + ")") + Name;
        }
    }
}
