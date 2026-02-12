// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Attachment.Enums;

namespace Gardener.Core.Attachment.Dtos
{
    /// <summary>
    /// 上传附件
    /// </summary>
    public class UploadAttachmentInput
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(64, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string BusinessId { get; set; } = null!;
        /// <summary>
        /// 附件业务类型
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public AttachmentBusinessType BusinessType { get; set; }
        /// <summary>
        /// 文件存储服务
        /// </summary>
        /// <remarks>
        /// 可以选择不同的存储服务
        /// </remarks>
        public string? FileStoreServiceId { get; set; }
        /// <summary>
        /// 保存为原始名称
        /// </summary>
        /// <remarks>
        /// 如果保存为原始名称原始名称，将增加一级随机值目录
        /// </remarks>
        public bool SaveOriginalName {  get; set; }
    }
}
