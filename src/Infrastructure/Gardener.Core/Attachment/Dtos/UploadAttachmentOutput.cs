// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Attachment.Enums;

namespace Gardener.Core.Attachment.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadAttachmentOutput
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string Url { get; set; } = null!;


        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; } = null!;

        /// <summary>
        /// 文件原始名称
        /// </summary>
        public string OriginalName { get; set; } = null!;

        /// <summary>
        /// 文件大小（byte）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 附件上传状态
        /// </summary>
        public AttachmentUploadState UploadState { get; set; } = AttachmentUploadState.Success;
    }
}
