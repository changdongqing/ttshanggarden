// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Module;
using TTShang.Core.Dict;
using TTShang.Core.Dict.Resources;

namespace TTShang.Core.Client.Impl.Dict
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
