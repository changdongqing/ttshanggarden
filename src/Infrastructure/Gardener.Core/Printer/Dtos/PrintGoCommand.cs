// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Printer.Enums;

namespace Gardener.Core.Printer.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class PrintGoCommand : PrintCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        public PrintGoCommand(byte line = 1) : base(PrintCommandType.PrintGo)
        {
            Line = line;
        }

        /// <summary>
        /// 走纸行数
        /// </summary>
        public byte Line { get; set; } = 1;
    }
}
