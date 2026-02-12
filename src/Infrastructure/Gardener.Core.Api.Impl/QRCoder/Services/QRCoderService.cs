// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DynamicApiController;
using Gardener.Core.QRCoder.Dtos;
using Gardener.Core.QRCoder.Enums;
using Gardener.Core.QRCoder.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using System.Net.Http.Headers;
using ZXing;
using ZXing.Common;
using ZXing.ImageSharp;
using ZXing.ImageSharp.Rendering;
using ZXing.QrCode;

namespace Gardener.Core.Api.Impl.QRCoder.Services
{
    /// <summary>
    /// 二维码服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class QRCoderService : IQRCoderService
    {
        /// <summary>
        /// 读取二维码
        /// </summary>
        /// <remarks>
        /// 从图片Base64读取二维码
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<string?> DecodeFromBase64(QRCodeDecodeInput input)
        {
            if (input.ImageBase64.StartsWith("data:"))
            {
                input.ImageBase64 = input.ImageBase64.Substring(input.ImageBase64.IndexOf(",") + 1);
            }
            byte[] bytes = Convert.FromBase64String(input.ImageBase64);
            string? resultText = null;
            using (var ms = new MemoryStream(bytes.ToArray()))
            {
                using (Image<Rgba32> bmp = Image.Load<Rgba32>(ms))
                {// 因有些图片较模糊，故放大比较容易识别。可不放大 Bitmap bmp = new Bitmap(image);
                    // 该类名称为BarcodeReader,可以读二维码和条形码
                    var isp = new ZXing.ImageSharp.BarcodeReader<Rgba32>();
                    isp.Options = new DecodingOptions
                    {
                        CharacterSet = "UTF-8"
                    };
                    Result result = isp.Decode(bmp);

                    if (result != null)
                    {
                        resultText = result.Text;
                    }
                }
            }
            return Task.FromResult(resultText);
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="contents">内容</param>
        /// <param name="color">Black、Red等</param>
        /// <param name="width">二维码宽，默认500</param>
        /// <param name="height">二维码高，默认500</param>
        /// <returns></returns>
        [NonAction]
        public Stream GetDimensionalCode(string contents, string color = "Black", int width = 500, int height = 500)
        {
            var w = new QRCodeWriter();
            BitMatrix b = w.encode(contents, BarcodeFormat.QR_CODE, width, height);
            var zzb = new ZXing.ImageSharp.BarcodeWriter<Rgba32>();
            zzb.Options = new EncodingOptions()
            {
                Margin = 0,
            };
            #region 设置颜色
            Color fore_color;
            if (!Color.TryParse(color, out fore_color))//没有的话默认黑色
            {
                fore_color = Color.Black;
            }
            zzb.Renderer = new ImageSharpRenderer<Rgba32>() { Foreground = fore_color, Background = Color.White };
            #endregion
            var ms = new MemoryStream();
            using (var image = zzb.Write(b))
            {

                image.SaveAsJpeg(ms);//保存于流
            }
            ms.Position = 0;//设置位置
            return ms;
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [NonUnify]
        [QueryParameters]
        public IActionResult GetDimensionalCodeImage(string contents, string color = "Black", int width = 500, int height = 500)
        {
            // 假设这里有一个方法来获取图片的字节数据
            Stream stream = GetDimensionalCode(contents, color, width, height);
            return new FileStreamResult(stream, "image/jpeg");
        }
        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns>返回条形码图片二进制串</returns>
        [NonAction]
        public Stream GetStripCode(string contents, int width = 800, int height = 400)
        {
            var margin = 0;
            var barcodeWriter = new ZXing.ImageSharp.BarcodeWriter<Rgba32>
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                    PureBarcode = true,
                    Margin = margin
                }
            };
            var ms = new MemoryStream();
            using (var image = barcodeWriter.Write(contents))
            {
                image.SaveAsJpeg(ms);//保存于流
            }
            ms.Position = 0;//设置位置
            return ms;
        }

        /// <summary>
        /// 生成条形码
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [NonUnify]
        [QueryParameters]
        public IActionResult GetStripCodeImage(string contents, int width = 800, int height = 400)
        {
            // 假设这里有一个方法来获取图片的字节数据
            Stream stream = GetStripCode(contents, width, height);
            return new FileStreamResult(stream, "image/jpeg");
        }

        /// <summary>
        /// 创建二维码图片-以Base64返回
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<string?> CreateQrCodeBase64(QRCodeEncodeInput input)
        {
            Stream? stream = null;
            if (input.QRCodeType.Equals(QRCodeType.QrCode))
            {
                stream = GetDimensionalCode(input.Contents, input.Color, input.Width, input.Height);
            }
            else if (input.QRCodeType == QRCodeType.Code128)
            {
                stream = GetStripCode(input.Contents, input.Width, input.Height);
            }
            string? result = null;
            if (stream != null)
            {
                using var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                result = Convert.ToBase64String(memoryStream.ToArray());
            }
            return Task.FromResult<string?>(result);
        }
    }
}
