// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Enums;

namespace TTShang.Core.Printer.Dtos
{
    /// <summary>
    /// 黑白反显
    /// </summary>
    public class PrintReverseDisplayCommand : PrintCommand
    {
        /// <summary>
        /// 黑白反显
        /// </summary>
        /// <param name="enable">true 黑白反显，false 取消反显</param>
        public PrintReverseDisplayCommand(bool enable) : base(PrintCommandType.ReverseDisplay)
        {
            Enable = enable;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Enable { get; set; }
    }
}
