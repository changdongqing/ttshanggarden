// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.EasyJob.Dtos;
using TTShang.EasyJob.Services;

namespace TTShang.EasyJob.Client.Services
{
    /// <summary>
    /// 定时任务-触发器服务
    /// </summary>
    [ScopedService]
    public class SysJobTriggerService : ClientServiceBase<SysJobTriggerDto, int>, ISysJobTriggerService
    {
        public SysJobTriggerService(IApiCaller apiCaller) : base(apiCaller, "sys-job-trigger")
        {
        }

        public Task<bool> Pause(int id)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/{id}/pause");
        }

        public Task<bool> Start(int id)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/{id}/start");
        }
    }
}
