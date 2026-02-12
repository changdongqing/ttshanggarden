// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Attachment.Dtos;
using TTShang.Core.Attachment.Enums;
using TTShang.Core.Attachment.Services;
using Microsoft.AspNetCore.Http;

namespace TTShang.Core.Client.Impl.Attachment.Services
{
    [ScopedService]
    public class AttachmentService : ClientServiceBase<SystemAttachmentDto, Guid>, IAttachmentService
    {

        public AttachmentService(IApiCaller apiCaller) : base(apiCaller, "attachment")
        {
        }

        public Task<IEnumerable<SystemAttachmentDto>> GetMyAttachments(AttachmentBusinessType attachmentBusinessType)
        {
            return apiCaller.GetAsync<IEnumerable<SystemAttachmentDto>>($"{controller}/my-attachments/{attachmentBusinessType}");
        }

        public async Task<string> GetRemoteImage(string remoteFilePath)
        {
            return await apiCaller.PostAsync<string, string>
                ($"{controller}/remote-image", request: remoteFilePath);
        }

        public async Task<PageList<SystemAttachmentDto>> Search(int? businessType, int? fileType, string businessId, string order = "desc", int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object?> pramas = new Dictionary<string, object?>()
            {
                {"businessType",businessType },
                {"fileType",fileType },
                {"order",order }
            };
            return await apiCaller.GetAsync<PageList<SystemAttachmentDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }

        public Task<UploadAttachmentOutput> Upload(UploadAttachmentInput input, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
