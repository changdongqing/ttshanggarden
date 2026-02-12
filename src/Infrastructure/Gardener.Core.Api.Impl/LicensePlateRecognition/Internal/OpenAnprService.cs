// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.RemoteRequest;
using Furion.RemoteRequest.Extensions;
using Gardener.Core.LicensePlateRecognition.Dtos;
using Gardener.Core.LicensePlateRecognition.Enums;
using Gardener.Core.LicensePlateRecognition.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace Gardener.Core.Api.Impl.LicensePlateRecognition.Internal
{
    /// <summary>
    /// open anpr实现
    /// </summary>
    internal class OpenAnprService : ILPRService
    {
        private readonly ILogger<OpenAnprService> logger;
        /// <summary>
        /// 配置
        /// </summary>
        private readonly IOptions<OpenAnprSettings> options;

        public OpenAnprService(IOptions<OpenAnprSettings> options, ILogger<OpenAnprService> logger)
        {
            this.options = options;
            this.logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<List<LPRRecognitionInfo>?> Recognition(byte[] image, int limit = 5)
        {
            //图片转base64
            string base64 = Convert.ToBase64String(image);
            return Recognition(base64, limit);
        }


        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="base64Image"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<LPRRecognitionInfo>?> Recognition(string base64Image, int limit = 5)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "limit", 1 },
                { "image", base64Image }
            };
            // 处理 application/x-www-form-urlencoded 请求
            HttpRequestPart httpRequestPart = options.Value.ApiUrl.SetBody(data, "application/json", Encoding.UTF8);
            var result = await httpRequestPart.PostAsStringAsync();
            if (string.IsNullOrWhiteSpace(result))
            {
                logger.LogWarning("车牌检测异常,返回结果为空");
                return null;
            }
            dynamic resultObj = Clay.Parse(result);
            if (resultObj.code != 0)
            {
                logger.LogWarning("车牌检测异常 result=" + result);
                return null;
            }
            IEnumerable<dynamic> recognitionDatas = resultObj.data.AsEnumerator<dynamic>(); ;
            List<LPRRecognitionInfo> lPRRecognitionInfos = new List<LPRRecognitionInfo>();

            foreach (dynamic recognitionData in recognitionDatas)
            {
                dynamic location = recognitionData.location;
                dynamic recognition = recognitionData.recognition;
                LPRRecognitionInfo recognitionInfo = new LPRRecognitionInfo()
                {
                    Score = recognitionData.score,
                    PlateInfo = new PlateInfo(recognition.plateNo)
                    {
                        PlateColor = Enum.Parse<PlateColor>(recognition.plateColor),
                        PlateLayout = Enum.Parse<PlateLayout>(recognition.layout)
                    },
                    PlateLocation = new PlateLocation(
                        new LocationPoint(location.leftTop.x, location.leftTop.y),
                    new LocationPoint(location.rightTop.x, location.rightTop.y),
                    new LocationPoint(location.rightBottom.x, location.rightBottom.y),
                    new LocationPoint(location.leftBottom.x, location.leftBottom.y))
                };
                lPRRecognitionInfos.Add(recognitionInfo);
            }

            return lPRRecognitionInfos;
        }
    }
}
