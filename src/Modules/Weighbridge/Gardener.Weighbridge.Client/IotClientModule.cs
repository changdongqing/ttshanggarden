// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Module;
using Gardener.Core.Localization;

namespace Gardener.Weighbridge.Client
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
