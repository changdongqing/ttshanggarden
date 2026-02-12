// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;
using TTShang.Core.Module.Services;

namespace TTShang.Core.Client.Impl.Module.Services
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
