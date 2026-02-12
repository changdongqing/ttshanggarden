// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Schedule;
using TTShang.Iot.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Iot.Impl.Jobs
{
    /// <summary>
    /// 检查超时连接
    /// </summary>
    [JobDetail("job_IotCheckTimeoutConnection", Description = "检查设备超时连接。", GroupName = "iot", Concurrent = false)]
    [PeriodSeconds(10, TriggerId = "trigger_IotCheckTimeoutConnection", Description = "检查设备超时连接任务触发器")]
    public class IotCheckTimeoutConnectionJob : IJob
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        /// <summary>
        /// 检查超时连接
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public IotCheckTimeoutConnectionJob(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {

            using var scope = serviceScopeFactory.CreateScope();
            IDeviceConnectionTool service = scope.ServiceProvider.GetRequiredService<IDeviceConnectionTool>();
            long count = 0;
            List<DeviceConnectionDto> deviceConnections = await service.GetTimeoutConnections();
            if (!deviceConnections.Any())
            {
                context.Result = $"执行完成,处理{count}条超时连接";
                return;
            }
            foreach (var item in deviceConnections)
            {
                await service.DisconnectTimeoutConnection(item);
            }
            context.Result = $"执行完成,处理{deviceConnections.Count}条超时连接";
        }
    }
}
