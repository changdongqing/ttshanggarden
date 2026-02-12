// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.LicensePlateRecognition.Dtos;

namespace Gardener.Core.LicensePlateRecognition.Services
{
    /// <summary>
    /// 车牌识别服务
    /// </summary>
    public interface ILPRService
    {
        /// <summary>
        /// 识别
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="limit">最大搜索条数：默认5</param>
        /// <returns></returns>
        Task<List<LPRRecognitionInfo>?> Recognition(byte[] image,int limit=5);

        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="base64Image">base64图片</param>
        /// <param name="limit">最大搜索条数：默认5</param>
        /// <returns></returns>
        Task<List<LPRRecognitionInfo>?> Recognition(string base64Image, int limit = 5);
    }
}
