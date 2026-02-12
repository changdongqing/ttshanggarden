// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Printer.Enums;

namespace Gardener.Core.Printer.Dtos
{
    /// <summary>
    /// 字符倍宽
    /// </summary>
    public class PrintFontDoubleCommand : PrintCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="height">高度-增加倍数</param>
        /// <param name="width">宽度-增加倍数</param>
        public PrintFontDoubleCommand(int height = 0, int width = 0) : base(PrintCommandType.FontDouble)
        {
            Height = height;
            Width = width;
        }

        /// <summary>
        /// 高度-增加倍数
        /// </summary>
        /// <remarks>
        /// 增加范围0~7 倍
        /// </remarks>
        public int Height { get; set; }
        /// <summary>
        /// 宽度-增加倍数
        /// </summary>
        /// <remarks>
        /// 增加范围0~7 倍
        /// </remarks>
        public int Width { get; set; }
    }
}
