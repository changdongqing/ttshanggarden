// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Printer.Dtos;

namespace Gardener.Core.Printer.Services
{
    /// <summary>
    /// 打印指令转换器
    /// </summary>
    public interface IPrintCommandConvert
    {
        /// <summary>
        /// 将打印指令转换为byte[]
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        byte[] ConvertToByte(IReadOnlyList<PrintCommand> commands);
    }
}
