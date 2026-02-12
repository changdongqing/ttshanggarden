// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.EasyJob.Client.Services
{

    /// <summary>
    /// 定时任务-任务服务
    /// </summary>
    [ScopedService]
    public class SysJobDetailService : ClientServiceBase<SysJobDetailDto, int>, ISysJobDetailService
    {
        public SysJobDetailService(IApiCaller apiCaller) : base(apiCaller, "sys-job-detail", "job")
        {
        }

        public Task<bool> CancelSleep()
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{this.baseUrl}/crash");
        }
        public Task<IEnumerable<SysJobTriggerDto>> GetTriggers(int id)
        {
            return apiCaller.GetAsync<IEnumerable<SysJobTriggerDto>>($"{this.baseUrl}/{id}/triggers");
        }

        public Task<bool> Pause(int id)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{this.baseUrl}/{id}/pause");
        }

        public Task<bool> PauseAll()
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{this.baseUrl}/pause-all");
        }

        public Task<bool> PersistAll()
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{this.baseUrl}/persist-all");
        }

        public Task<bool> Start(int id)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{this.baseUrl}/{id}/start");
        }
        public Task<bool> Run(int id)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{this.baseUrl}/{id}/run");
        }

        public Task<bool> StartAll()
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{this.baseUrl}/start-all");
        }
    }
}
