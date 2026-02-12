// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Enums;

namespace TTShang.Core.Printer.Dtos
{
    /// <summary>
    /// 打印初始化
    /// </summary>
    public class PrintInitCommand : PrintCommand
    {
        /// <summary>
        /// 打印初始化
        /// </summary>
        public PrintInitCommand() : base(PrintCommandType.Init)
        {
        }
    }
}
