// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.Localization
{
    /// <summary>
    /// 模块
    /// </summary>
    public class LocalizationModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "Localization";


        /// <summary>
        /// 
        /// </summary>
        public string? Description => "多语言模块";

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 210;
    }
}
