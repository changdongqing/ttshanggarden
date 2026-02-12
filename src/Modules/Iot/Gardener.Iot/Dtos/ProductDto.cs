using Gardener.Core.Attachment.Dtos;
using Gardener.Core.Attributes;

namespace Gardener.Iot.Dtos
{
    /// <summary>
    /// 产品
    /// </summary>
    [Display(Name = nameof(IotLocalResource.Product), ResourceType = typeof(IotLocalResource))]
    public class ProductDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = nameof(IotLocalResource.ProductName), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ProductName { get; set; } = default!;
        /// <summary>
        /// 产品类型
        /// </summary>
        [CodeType("iot-product-type")]
        [Display(Name = nameof(IotLocalResource.ProductType), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ProductType {  get; set; } = default!;
        /// <summary>
        /// 产品图片
        /// </summary>
        [Display(Name = nameof(IotLocalResource.ProductImages), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(5000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ProductImages {  get; set; }=default!;
        /// <summary>
        /// 产品版本
        /// </summary>
        [Display(Name = nameof(IotLocalResource.ProductVersion), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ProductVersion = default!;
        /// <summary>
        /// 产品描述
        /// </summary>
        [Display(Name = nameof(IotLocalResource.ProductDescription), ResourceType = typeof(IotLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ProductDescription { get; set; }
        /// <summary>
        /// 上行配置
        /// </summary>
        [Display(Name = nameof(IotLocalResource.UpstreamConfig), ResourceType = typeof(IotLocalResource))]
        [MaxLength(5000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? UpstreamConfig { get; set; }
        /// <summary>
        /// 下行配置
        /// </summary>
        [Display(Name = nameof(IotLocalResource.DownstreamConfig), ResourceType = typeof(IotLocalResource))]
        [MaxLength(5000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? DownstreamConfig { get; set; }
        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <returns></returns>
        public UploadAttachmentOutput[]? GetProductImageInfos()
        {
            return System.Text.Json.JsonSerializer.Deserialize<UploadAttachmentOutput[]>(ProductImages);
        }
    }
}
