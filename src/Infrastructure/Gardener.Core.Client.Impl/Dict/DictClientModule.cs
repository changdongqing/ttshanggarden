// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Module;
using Gardener.Core.Dict;
using Gardener.Core.Dict.Resources;

namespace Gardener.Core.Client.Impl.Dict
{
    /// <summary>
    /// 字典客户端模块
    /// </summary>
    [SingletonService]
    public class DictClientModule : DictModule, IClientModule
    {
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<DictResource>);
        }
    }
}
