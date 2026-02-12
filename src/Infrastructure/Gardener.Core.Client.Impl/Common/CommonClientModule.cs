// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Module;
using Gardener.Core.Common;

namespace Gardener.Core.Client.Impl.Common
{

    /// <summary>
    /// 公共模块
    /// </summary>
    [SingletonService]
    public class CommonClientModule : CommonModule, IClientModule
    {
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<SharedLocalResource>);
        }
    }
}
