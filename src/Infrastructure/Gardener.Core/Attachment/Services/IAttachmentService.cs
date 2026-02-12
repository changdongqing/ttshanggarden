// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Attachment.Dtos;
using Gardener.Core.Attachment.Enums;
using Microsoft.AspNetCore.Http;

namespace Gardener.Core.Attachment.Services
{
    /// <summary>
    /// 附件服务接口
    /// </summary>
    public interface IAttachmentService : IServiceBase<SystemAttachmentDto, Guid>
    {
        /// <summary>
        /// 获取远程图片-转换为base64字符串
        /// </summary>
        /// <param name="remoteFilePath"></param>
        /// <returns></returns>
        Task<string> GetRemoteImage(string remoteFilePath);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="input"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<UploadAttachmentOutput> Upload(UploadAttachmentInput input, IFormFile file);

        /// <summary>
        /// 获取我的某一类型附件数据
        /// </summary>
        /// <param name="attachmentBusinessType"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemAttachmentDto>> GetMyAttachments(AttachmentBusinessType attachmentBusinessType);
    }
}