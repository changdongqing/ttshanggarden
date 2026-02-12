namespace Gardener.Iot.Dtos
{
    /// <summary>
    /// 发送数据
    /// </summary>
    public class SendDataInput
    {
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="content"></param>
        public SendDataInput(string clientId, string content)
        {
            ClientId = clientId;
            Content = content;
        }

        /// <summary>
        /// 内容类型
        /// </summary>
        [Display(Name = nameof(IotLocalResource.ContentType), ResourceType = typeof(IotLocalResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ContentType {  get; set; }
        /// <summary>
        /// 客户端编号
        /// </summary>
        public string ClientId {  get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = nameof(IotLocalResource.Content), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(2000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Content { get; set; }

        /// <summary>
        /// 是否是16进制
        /// </summary>
        [Display(Name = nameof(IotLocalResource.IsHex), ResourceType = typeof(IotLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool IsHex { get; set; }=false;
    }
}
