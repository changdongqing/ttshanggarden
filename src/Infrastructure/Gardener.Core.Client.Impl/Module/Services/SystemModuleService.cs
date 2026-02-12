// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;
using Gardener.Core.Module.Services;

namespace Gardener.Core.Client.Impl.Module.Services
{
    /// <summary>
    /// 系统模块服务
    /// </summary>
    [ScopedService]
    public class SystemModuleService : ClientServiceBase<SystemModuleDto>, ISystemModuleService
    {
        public SystemModuleService(IApiCaller apiCaller) : base(apiCaller, "system-module")
        {
        }
    }
}
