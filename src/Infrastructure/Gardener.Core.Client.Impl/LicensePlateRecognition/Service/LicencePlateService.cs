// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.LicensePlateRecognition.Dtos;
using Gardener.Core.LicensePlateRecognition.Services;
using Microsoft.AspNetCore.Http;

namespace Gardener.Core.Client.Impl.LicensePlateRecognition.Service
{
    [ScopedService]
    public class LicencePlateService : ClientServiceCaller, ILicencePlateService
    {
        public LicencePlateService(IApiCaller apiCaller) : base(apiCaller, "licence-plate", "lpr")
        {
        }

        public Task<List<LPRRecognitionInfo>?> Recognition(LPRRecognitionInput input, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<List<LPRRecognitionInfo>?> RecognitionFromBase64(LPRRecognitionInput input)
        {
            return apiCaller.PostAsync<LPRRecognitionInput, List<LPRRecognitionInfo>?>($"{baseUrl}/recognition-from-base64", input);
        }
    }
}
