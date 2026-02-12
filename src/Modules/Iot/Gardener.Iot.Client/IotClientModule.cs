// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Iot.Client
{
    /// <summary>
    /// Iot客户端模块
    /// </summary>
    [SingletonService]
    public class IotClientModule : IotModule, IClientModule
    {
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<IotLocalResource>);
        }
    }
}
