// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Gardener.Iot.Dtos
{
    /// <summary>
    /// 设备数据
    /// </summary>
    [Display(Name = nameof(IotLocalResource.DeviceData), ResourceType = typeof(IotLocalResource))]
    public class DeviceDataDto : TenantBaseDto<long>
    {
        /// <summary>
        /// 设备连接编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceConnectionId), ResourceType = typeof(IotLocalResource))]
        public long? DeviceConnectionId { get; set; }

        /// <summary>
        /// 设备连接类型
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceConnectionType), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public DeviceConnectionType DeviceConnectionType { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceId), ResourceType = typeof(IotLocalResource))]
        public Guid? DeviceId { get; set; }

        /// <summary>
        /// 设备客户端编号
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DeviceClientId), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string DeviceClientId { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = nameof(IotLocalResource.ContentType), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]

        public string ContentType { get; set; } = null!;

        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Content), ResourceType = typeof(IotLocalResource))]
        [MaxLength(5000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Content { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        [Display(Name = nameof(IotLocalResource.ExtendData), ResourceType = typeof(IotLocalResource))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ExtendData { get; set; }

        /// <summary>
        /// content存储为base64
        /// </summary>
        /// <param name="isHex"></param>
        /// <returns></returns>
        public string? GetContentString(bool isHex = false)
        {
            if (string.IsNullOrEmpty(Content))
            {
                return null;
            }
            var bytes = Convert.FromBase64String(Content);
            return isHex ? Convert.ToHexString(bytes) : Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 原始内容
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public byte[]? OriginalContent { get; set; }
    }
}
