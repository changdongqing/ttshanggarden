// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Module;

namespace TTShang.ToolBox.Client
{
    /// <summary>
    /// 注入此模块
    /// </summary>
    [SingletonService]
    public class ToolBoxClientModule : ToolBoxModule, IClientModule
    {

        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<ToolBoxLocalResource>);
        }
    }
}
