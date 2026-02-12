// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Module;
using TTShang.Core.SystemAsset;
using TTShang.Core.SystemAsset.Resources;

namespace TTShang.Core.Client.Impl.SystemAsset
{
    /// <summary>
    /// 系统资产客户端模块
    /// </summary>
    [SingletonService]
    public class SystemAssetClientModule: SystemAssetModule, IClientModule
    {
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<SystemAssetResource>);
        }
    }
}
