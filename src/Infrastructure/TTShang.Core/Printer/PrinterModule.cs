// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;

namespace TTShang.Core.Printer
{
    /// <summary>
    /// 打印模块
    /// </summary>
    public class PrinterModule : IModule
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => "Printer";
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description => "实现打印相关服务";
        /// <summary>
        /// 版本
        /// </summary>
        public string Version => "8.0.3";
    }
}
