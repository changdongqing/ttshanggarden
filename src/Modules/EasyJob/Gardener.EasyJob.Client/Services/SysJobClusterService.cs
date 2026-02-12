// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.EasyJob.Client.Services
{
    /// <summary>
    /// 定时任务-集群服务
    /// </summary>
    [ScopedService]
    public class SysJobClusterService : ClientServiceBase<SysJobClusterDto,int>, ISysJobClusterService
    {
        public SysJobClusterService(IApiCaller apiCaller) : base(apiCaller, "sys-job-cluster", "job")
        {
        }

        public void Crash(JobClusterContext context)
        {
            throw new NotImplementedException();
        }

        public void Start(JobClusterContext context)
        {
            throw new NotImplementedException();
        }

        public void Stop(JobClusterContext context)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Waiting(JobClusterContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
