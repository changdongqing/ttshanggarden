// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Module;
using TTShang.Core.Email;
using TTShang.Core.Email.Resources;

namespace TTShang.Core.Client.Impl.Email
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
