// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Gardener.Core.Module;

namespace Gardener.ToolBox
{
    /// <summary>
    /// 模块
    /// </summary>
    public class ToolBoxModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "ToolBox";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "工具箱";

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 200;
    }
}
