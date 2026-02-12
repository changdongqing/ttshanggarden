// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Printer.Enums;

namespace Gardener.Core.Printer.Dtos
{
    /// <summary>
    /// 对齐方式命令
    /// </summary>
    public class PrintAlignCommand : PrintCommand
    {
        /// <summary>
        /// 对齐方式命令
        /// </summary>
        /// <param name="alianType"></param>
        public PrintAlignCommand(PrintAlianType alianType) : base(PrintCommandType.Align)
        {
            AlianType = alianType;
        }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public PrintAlianType AlianType { get; set; } = PrintAlianType.Left;
    }
}
