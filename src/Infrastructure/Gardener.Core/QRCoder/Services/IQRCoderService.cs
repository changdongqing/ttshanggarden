// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.QRCoder.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Core.QRCoder.Services
{
    /// <summary>
    /// 二维码服务
    /// </summary>
    public interface IQRCoderService
    {
        /// <summary>
        /// 读取二维码
        /// </summary>
        /// <remarks>
        /// 从图片Base64读取二维码
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string?> DecodeFromBase64(QRCodeDecodeInput input);

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="contents">内容</param>
        /// <param name="color">Black、Red等</param>
        /// <param name="width">二维码宽，默认500</param>
        /// <param name="height">二维码高，默认500</param>
        /// <returns></returns>
        Stream GetDimensionalCode(string contents, string color = "Black", int width = 500, int height = 500);

        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns>返回条形码图片二进制串</returns>
        Stream GetStripCode(string contents, int width = 800, int height = 400);

        /// <summary>
        /// 创建二维码图片-以Base64返回
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string?> CreateQrCodeBase64(QRCodeEncodeInput input);
    }
}
