// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.QRCoder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Core.QRCoder.Dtos
{
    /// <summary>
    /// 二维码编码输入
    /// </summary>
    public class QRCodeEncodeInput
    {
        /// <summary>
        /// 二维码类型
        /// </summary>
        public QRCodeType QRCodeType { get; set; } = QRCodeType.QrCode;
        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }=string.Empty;
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; } = "Black";
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; } = 500;
        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; } = 50;

    }
}
