// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Logging;
using Furion.Schedule;
using Gardener.Iot.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Iot.Impl.Jobs
{
    /// <summary>
    /// 清理无效的IoT连接Job
    /// </summary>
    [JobDetail("job_CleanIotInvalidConnection", Description = "清理无效的IoT连接,附加参数day可以指定保留天数，默认是保留7天。", GroupName = "iot", Concurrent = false)]
    [Hourly(TriggerId = "trigger_CleanIotInvalidConnection", Description = "清理无效的IoT连接任务触发器")]
    public class CleanIotInvalidConnectionJob : IJob
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        /// <summary>
        /// 清理无效的IoT连接
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public CleanIotInvalidConnectionJob(IServiceScopeFactory serviceScopeFactory)
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
            int day = 7;
            if (!string.IsNullOrWhiteSpace(context.JobDetail.Properties))
            {
                Dictionary<string, object>? keyValues = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(context.JobDetail.Properties);
                if (keyValues != null && keyValues.ContainsKey("day"))
                {
                    object? days = keyValues["day"];

                    if (days != null)
                    {
                        day = int.Parse(days.ToString() ?? day + "");
                    }
                }
            }

            DateTimeOffset current = DateTimeOffset.Now.AddDays(-day);

            using var scope = serviceScopeFactory.CreateScope();
            IRepository<DeviceConnection, GardenerIgnoreAuditDbContextLocator> repository = scope.ServiceProvider.GetRequiredService<IRepository<DeviceConnection, GardenerIgnoreAuditDbContextLocator>>();
            var list = await repository
                .AsQueryable(false)
                .Where(x => x.CreatedTime.CompareTo(current) <= 0 && DeviceConnectionState.Disconnect.Equals(x.DeviceConnectionState)).Take(500).ToListAsync();
            int count1 = 0;
            if (list.Any())
            {
                IRepository<DeviceSystemLog, GardenerIgnoreAuditDbContextLocator> repository1 = scope.ServiceProvider.GetRequiredService<IRepository<DeviceSystemLog, GardenerIgnoreAuditDbContextLocator>>();
                count1 = await repository1.AsQueryable(false).Where(x => x.DeviceConnectionId == null || list.Select(x => x.Id).Contains(x.DeviceConnectionId.Value)).ExecuteDeleteAsync();
                repository.Context.RemoveRange(list);
                repository.Context.SaveChanges();
            }
            context.Result = $"执行完成，保留近{day}天记录，移除{list.Count}条无效链接，移除{count1}条设备日志记录。";
        }
    }
}
