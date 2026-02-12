// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Printer.Enums
{
    /// <summary>
    /// 打印指令类型
    /// </summary>
    public enum PrintCommandType
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Init,
        /// <summary>
        /// 文本
        /// </summary>
        Text,
        /// <summary>
        /// 加粗/取消加粗
        /// </summary>
        Bold,
        /// <summary>
        /// 打印走纸
        /// </summary>
        PrintGo,
        /// <summary>
        /// 对齐方式
        /// </summary>
        Align,
        /// <summary>
        /// 字符倍宽
        /// </summary>
        FontDouble,
        /// <summary>
        /// 黑白反显
        /// </summary>
        ReverseDisplay,
        /// <summary>
        /// 二维码
        /// </summary>
        QrCode
    }
}
