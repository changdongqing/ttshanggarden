// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Module;
using TTShang.Core.Common;

namespace TTShang.Core.Client.Impl.Common
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
