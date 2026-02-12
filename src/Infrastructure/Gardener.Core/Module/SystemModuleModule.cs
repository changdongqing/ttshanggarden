// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Module
{
    /// <summary>
    /// 模块
    /// </summary>
    public class SystemModuleModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "SystemModule";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "多模块管理模块";

        /// <summary>
        /// 模块放最后
        /// </summary>
        public int Order => 10000;
    }
}
