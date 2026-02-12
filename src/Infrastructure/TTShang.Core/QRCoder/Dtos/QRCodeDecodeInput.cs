// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.QRCoder.Dtos
{
    /// <summary>
    /// 二维码解码输入
    /// </summary>
    public class QRCodeDecodeInput
    {
        /// <summary>
        /// Base64图片
        /// </summary>
        public string ImageBase64 { get; set; }
        /// <summary>
        /// 二维码解码输入
        /// </summary>
        /// <param name="imageBase64"></param>
        public QRCodeDecodeInput(string imageBase64)
        {
            ImageBase64 = imageBase64;
        }
    }
}
