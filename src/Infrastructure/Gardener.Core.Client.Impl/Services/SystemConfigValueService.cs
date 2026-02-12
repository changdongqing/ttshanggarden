// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.SystemConfig.Dtos;
using Gardener.Core.SystemConfig.Services;

namespace Gardener.Core.Client.Impl.Services
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [ScopedService]
    public class SystemConfigValueService : ClientServiceBase<SystemConfigValueDto, string>, ISystemConfigValueService
    {
        private Task<SystemConfigDto>? systemConfigTask;
        public SystemConfigValueService(IApiCaller apiCaller) : base(apiCaller, "system-config-value")
        {
        }
        public Task<SystemConfigDto> GetSystemConfig()
        {
            if (systemConfigTask != null)
            {
                if (systemConfigTask.IsCompleted)
                {
                    return Task.FromResult(systemConfigTask.Result);

                }
                else
                {
                    return systemConfigTask;
                }
            }
            systemConfigTask = apiCaller.GetAsync<SystemConfigDto>($"{this.baseUrl}/system-config");
            return systemConfigTask;
        }
    }
}
