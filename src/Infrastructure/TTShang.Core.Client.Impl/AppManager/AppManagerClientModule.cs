// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.AppManager;
using TTShang.Core.Client.Module;

namespace TTShang.Core.Client.Impl.AppManager
{
    /// <summary>
    /// app manager
    /// </summary>
    [SingletonService]
    public class AppManagerClientModule : AppManagerModule, IClientModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<AppManagerResource>);
        }
    }
}
