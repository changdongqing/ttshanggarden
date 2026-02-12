// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Module;
using Gardener.Core.Email;
using Gardener.Core.Email.Resources;

namespace Gardener.Core.Client.Impl.Email
{
    /// <summary>
    /// 邮件客户端模块
    /// </summary>
    [SingletonService]
    public class EmailClientModule : EmailModule, IClientModule
    {
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<EmailLocalResource>);
        }
    }
}
