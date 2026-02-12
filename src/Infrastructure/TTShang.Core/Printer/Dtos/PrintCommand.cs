// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Enums;

namespace TTShang.Core.Printer.Dtos
{
    /// <summary>
    /// 打印指令
    /// </summary>
    public class PrintCommand
    {
        /// <summary>
        /// 打印指令类型
        /// </summary>
        public PrintCommandType CommandType { get; init; }
        /// <summary>
        /// 打印指令
        /// </summary>
        /// <param name="commandType"></param>
        public PrintCommand(PrintCommandType commandType)
        {
            CommandType = commandType;
            TypeAssemblyName = GetType().AssemblyQualifiedName;
        }

        /// <summary>
        /// 程序类型
        /// </summary>
        public string? TypeAssemblyName { get; set; }
    }
}
