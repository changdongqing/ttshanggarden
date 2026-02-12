// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Enums;

namespace TTShang.Core.Printer.Dtos
{
    /// <summary>
    /// 打印二维码
    /// </summary>
    public class PrintQrCodeCommand : PrintCommand
    {
        /// <summary>
        /// 打印二维码
        /// </summary>
        /// <param name="content">内容</param>
        public PrintQrCodeCommand(string content) : base(PrintCommandType.QrCode)
        {
            this.Content = content;
        }
        /// <summary>
        /// 像素点大小
        /// </summary>
        /// <remarks>
        /// 设置像素点大小 1≤n ≤24
        /// </remarks>
        public byte QrPixelSize { get; set; } = 1;
        /// <summary>
        /// 单元大小
        /// </summary>
        /// <remarks>
        /// 设置QR单元大小, 1≤n ≤16
        /// </remarks>
        public byte QrSize { get; set; } = 8;
        /// <summary>
        /// 纠错等级
        /// </summary>
        /// <remarks>
        /// 设置QR错误纠错等级, 48-51
        /// </remarks>
        public byte ErrorCorrectionLevel { get; set; } = 48;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 编码类型
        /// </summary>
        public string CharacterSet { get; set; } = "UTF-8";
    }
}
