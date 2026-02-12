// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Module;
using Gardener.Core.Localization;

namespace Gardener.EasyJob.Client
{
    /// <summary>
    /// 注入此模块
    /// </summary>
    [SingletonService]
    internal class EasyJobClientModule : EasyJobModule, IClientModule
    {
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<EasyJobLocalResource>);
        }
    }
}
