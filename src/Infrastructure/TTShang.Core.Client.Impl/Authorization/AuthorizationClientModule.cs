// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Authorization;
using TTShang.Core.Authorization.Resources;
using TTShang.Core.Client.Module;

namespace TTShang.Core.Client.Impl.Authorization
{
    /// <summary>
    /// 身份认证客户端模块
    /// </summary>
    [SingletonService]
    public class AuthorizationClientModule: AuthorizationModule,IClientModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<AuthorizationLocalResource>);
        }
    }
}
