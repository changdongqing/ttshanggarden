// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.LicensePlateRecognition.Dtos;
using Gardener.Core.LicensePlateRecognition.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.LicensePlateRecognition.Services
{
    /// <summary>
    /// 车牌服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService),Module ="lpr")]
    public class LicencePlateService : ILicencePlateService
    {
        private readonly IServiceProvider serviceProvider;
        /// <summary>
        /// 车牌服务
        /// </summary>
        /// <param name="serviceProvider"></param>
        public LicencePlateService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 识别车牌
        /// </summary>
        /// <param name="input"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<List<LPRRecognitionInfo>?> Recognition([FromForm] LPRRecognitionInput input, IFormFile file)
        {
            ILPRService service = serviceProvider.GetRequiredKeyedService<ILPRService>(input.ServiceType);
            Stream stream = file.OpenReadStream();
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            return service.Recognition(data, input.Limit);
        }
        /// <summary>
        /// 识别车牌-从base64识别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<LPRRecognitionInfo>?> RecognitionFromBase64(LPRRecognitionInput input)
        {
            if(string.IsNullOrEmpty(input.Base64Image))
            {
                return null;
            }
            ILPRService service = serviceProvider.GetRequiredKeyedService<ILPRService>(input.ServiceType);
            return await service.Recognition(input.Base64Image, input.Limit);
        }
    }
}
