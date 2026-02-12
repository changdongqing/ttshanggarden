// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.LicensePlateRecognition.Dtos;
using Microsoft.AspNetCore.Http;

namespace Gardener.Core.LicensePlateRecognition.Services
{
    /// <summary>
    /// 车牌服务
    /// </summary>
    public interface ILicencePlateService
    {
        /// <summary>
        /// 识别车牌
        /// </summary>
        /// <param name="input"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<List<LPRRecognitionInfo>?> Recognition(LPRRecognitionInput input, IFormFile file);

        /// <summary>
        /// 识别车牌-从base64识别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<LPRRecognitionInfo>?> RecognitionFromBase64(LPRRecognitionInput input);
    }
}
