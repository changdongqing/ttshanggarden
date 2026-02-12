// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Module;
using TTShang.Core.Localization;

namespace TTShang.Weighbridge.Client
{
    /// <summary>
    /// Weighbridge客户端模块
    /// </summary>
    [SingletonService]
    public class WeighbridgeClientModule : WeighbridgeModule, IClientModule
    {
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<WeighbridgeLocalResource>);
        }
    }
}
