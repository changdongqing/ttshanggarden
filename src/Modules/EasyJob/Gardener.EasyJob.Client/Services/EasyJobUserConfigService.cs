// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.EasyJob.Client.Services
{
    /// <summary>
    /// 定时任务-用户配置服务
    /// </summary>
    [ScopedService]
    public class EasyJobUserConfigService : ClientServiceCaller, ISysJobUserConfigService
    {
        /// <summary>
        /// 定时任务-用户配置服务
        /// </summary>
        /// <param name="apiCaller"></param>
        public EasyJobUserConfigService(IApiCaller apiCaller) : base(apiCaller, "sys-job-user-config", "job")
        {
        }

        public Task<SysJobUserConfigDto?> GetMyConfig()
        {
            return apiCaller.GetAsync<SysJobUserConfigDto?>($"{this.baseUrl}/my-config");
        }

        public Task<SysJobUserConfigDto?> SaveMyConfig(SysJobUserConfigDto config)
        {
            return apiCaller.PostAsync<SysJobUserConfigDto, SysJobUserConfigDto?>($"{this.baseUrl}/save-my-config", config);
        }
    }
}
