// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Schedule;
using TTShang.EasyJob.Impl.Entities;

namespace TTShang.EasyJob.Impl.Jobs
{
    /// <summary>
    /// 清理定时任务日志任务
    /// </summary>
    [JobDetail("job_CleanJobRunLog", Description = "清理定时任务日志任务,附加参数day可以指定保留天数，默认是保留7天。", GroupName = "System", Concurrent = false)]
    [PeriodMinutes(10, TriggerId = "trigger_CleanJobRunLogJob", Description = "清理定时任务日志任务触发器")]
    public class CleanJobRunLogJob : IJob
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        /// <summary>
        /// 清理定时任务日志任务
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public CleanJobRunLogJob(IServiceScopeFactory serviceScopeFactory)
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
            using var factory = serviceScopeFactory.CreateScope();
            IRepository<SysJobLog> repository = factory.ServiceProvider.GetRequiredService<IRepository<SysJobLog>>();
            int count = await repository
                .AsQueryable(false)
                .Where(x => x.CreatedTime.CompareTo(current) <= 0).ExecuteDeleteAsync();
            context.Result = $"执行完成，保留近{day}天记录，移除{count}条记录。";
        }
    }
}
