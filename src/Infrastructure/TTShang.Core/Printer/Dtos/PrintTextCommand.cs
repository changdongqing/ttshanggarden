// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Enums;

namespace TTShang.Core.Printer.Dtos
{
    /// <summary>
    /// 打印文本
    /// </summary>
    public class PrintTextCommand : PrintCommand
    {
        /// <summary>
        /// 打印文本
        /// </summary>
        /// <param name="text"></param>
        public PrintTextCommand(string text) : base(PrintCommandType.Text)
        {
            Text = text;
        }
        /// <summary>
        /// 编码类型
        /// </summary>
        public string CharacterSet { get; set; } = "gb2312";
        /// <summary>
        /// 文本内容
        /// </summary>
        public string Text {  get; set; }

    }
}
