// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;

namespace TTShang.Core.AppManager
{
    /// <summary>
    /// 
    /// </summary>
    public class AppManagerModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "AppManager";
        /// <summary>
        /// 
        /// </summary>
        public string Description => "应用管理：实现各客户端的版本发布，用于客户端更新";


        /// <summary>
        /// 版本
        /// </summary>
        public string Version => "8.0.2";
    }
}
