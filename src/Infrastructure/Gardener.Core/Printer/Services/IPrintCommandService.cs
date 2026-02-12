// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Printer.Dtos;
using Gardener.Core.Printer.Enums;

namespace Gardener.Core.Printer.Services
{
    /// <summary>
    /// 打印指令
    /// </summary>
    public interface IPrintCommandService
    {
        /// <summary>
        /// 将打印指令转换为目标协议指令
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        byte[] Convert(IReadOnlyList<PrintCommand> commands, PrintProtocolType targetType);
        /// <summary>
        /// 将打印指令转换为目标协议指令
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        string ConvertToBase64(IReadOnlyList<PrintCommand> commands, PrintProtocolType targetType);

    }
}
