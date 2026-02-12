// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Core.Printer.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class PrintBoldCommand : PrintCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bold"></param>
        public PrintBoldCommand(bool bold) : base(PrintCommandType.Bold)
        {
            Bold = bold;
        }
        /// <summary>
        /// 是否加粗
        /// </summary>
        public bool Bold { get; set; } = true;
    }
}
