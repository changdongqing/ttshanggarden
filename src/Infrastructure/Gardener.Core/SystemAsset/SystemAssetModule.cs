// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.SystemAsset
{
    /// <summary>
    /// 模块
    /// </summary>
    public class SystemAssetModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "SystemAsset";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "系统资产（资源、api）模块";
        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 120;
    }
}
