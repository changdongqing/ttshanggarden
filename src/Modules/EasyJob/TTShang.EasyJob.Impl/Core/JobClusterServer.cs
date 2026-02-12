// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Schedule;
using System.Threading;

namespace TTShang.EasyJob.Impl.Core
{
    /// <summary>
    /// 实现集群故障转移
    /// </summary>
    public class JobClusterServer : IJobClusterServer
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly CancellationTokenSource _cancellationTokenSource;
        /// <summary>
        /// 实现集群故障转移
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public JobClusterServer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _cancellationTokenSource = new CancellationTokenSource();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Crash(Furion.Schedule.JobClusterContext context)
        {
            //Disposed
            _cancellationTokenSource.Cancel();
        }
        /// <summary>
        /// 当前作业调度器启动通知
        /// </summary>
        /// <param name="context"></param>
        public void Start(Furion.Schedule.JobClusterContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            ISysJobClusterService? jobClusterService = scope.ServiceProvider.GetService<ISysJobClusterService>();
            if (jobClusterService != null)
            {
               jobClusterService.Start(new Dtos.JobClusterContext(context.ClusterId));
                
            }
        }
        /// <summary>
        /// 当前作业调度器停止通知
        /// </summary>
        /// <param name="context"></param>
        public void Stop(Furion.Schedule.JobClusterContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            ISysJobClusterService? jobClusterService = scope.ServiceProvider.GetService<ISysJobClusterService>();
            if (jobClusterService != null)
            {
               jobClusterService.Stop(new Dtos.JobClusterContext(context.ClusterId));
                _cancellationTokenSource.Cancel();
            }
        }
        /// <summary>
        /// 等待被唤醒
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task WaitingForAsync(Furion.Schedule.JobClusterContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            ISysJobClusterService? jobClusterService = scope.ServiceProvider.GetService<ISysJobClusterService>();
            if (jobClusterService != null)
            {
                await jobClusterService.Waiting(new Dtos.JobClusterContext(context.ClusterId), _cancellationTokenSource.Token);
            }
        }
    }
}
