// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Module;
using TTShang.Core.UserCenter;

namespace TTShang.Core.Client.Impl.UserCenter
{
    /// <summary>
    /// 用户中心客户端模块
    /// </summary>
    [SingletonService]
    public class UserCenterClientModule : UserCenterModule, IClientModule
    {
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<UserCenterResource>);
        }
    }
}
