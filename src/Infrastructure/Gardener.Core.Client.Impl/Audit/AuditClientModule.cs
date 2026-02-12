// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Audit;
using Gardener.Core.Audit.Resources;
using Gardener.Core.Client.Module;

namespace Gardener.Core.Client.Impl.Audit
{
    /// <summary>
    /// 审计客户端模块
    /// </summary>
    [SingletonService]
    public class AuditClientModule : AuditModule, IClientModule
    {
        public Type? GetLocalizationLocalizerType()
        {
            return typeof(ILocalizationLocalizer<AuditLocalResource>);
        }
    }
}
