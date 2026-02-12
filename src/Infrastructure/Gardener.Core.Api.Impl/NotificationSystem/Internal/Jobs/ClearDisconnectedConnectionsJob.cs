// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Schedule;
using Gardener.Core.NotificationSystem;

namespace Gardener.Core.Api.Impl.NotificationSystem.Internal.Jobs
{
    /// <summary>
    /// 清零已断开SignalR连接
    /// </summary>
    [JobDetail("job_clearDisconnectedConnections", Description = "清零已断开SignalR连接", GroupName = "System", Concurrent = false)]
    [PeriodMinutes(5, TriggerId = "trigger_clearDisconnectedConnections", Description = "清零已断开SignalR连接作业触发器")]	//	毫秒周期（间隔）作业触发器特性
    public class ClearDisconnectedConnectionsJob : IJob
    {
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemNotificationService"></param>
        public ClearDisconnectedConnectionsJob(ISystemNotificationService systemNotificationService)
        {
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {
           await systemNotificationService.ClearDisconnectedConnections();
            context.Result = "执行完成";
        }
    }
}
