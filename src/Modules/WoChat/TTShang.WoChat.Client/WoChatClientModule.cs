// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------



namespace TTShang.WoChat.Client
{
    /// <summary>
    /// 注入此模块
    /// </summary>
    [SingletonService]
    internal class WoChatClientModule : WoChatModule, IClientModule
    {
        public IEnumerable<ModuleComponent> GetAutoRegisterComponents()
        {
            return [new ModuleComponent(typeof(WoChatBtn), "body::after")];
        }

        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<WoChatResource>);
        }
    }
}
