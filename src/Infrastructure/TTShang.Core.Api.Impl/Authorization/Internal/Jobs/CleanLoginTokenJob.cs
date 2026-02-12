// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Schedule;
using TTShang.Core.Api.Impl.Authorization.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Api.Impl.Authorization.Internal.Jobs
{
    /// <summary>
    /// 清理登录token任务
    /// </summary>
    [JobDetail("job_CleanLoginToken", Description = "清理登录token任务", GroupName = "System", Concurrent = false)]
    [PeriodMinutes(5, TriggerId = "trigger_CleanLoginTokenJob", Description = "清理登录token任务触发器")]
    public class CleanLoginTokenJob : IJob
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        /// <summary>
        /// 清理登录token任务
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public CleanLoginTokenJob(IServiceScopeFactory serviceScopeFactory)
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
            DateTimeOffset current = DateTimeOffset.Now;
            using var factory = serviceScopeFactory.CreateScope();
            IRepository<LoginToken> loginTokenRepository = factory.ServiceProvider.GetRequiredService<IRepository<LoginToken>>();
            int count = await loginTokenRepository
                .AsQueryable(false)
                .Where(x => x.EndTime.CompareTo(current) <= 0 || x.IsDeleted == true).ExecuteDeleteAsync();
            context.Result = $"执行完成，移除{count}条记录。";
        }
    }
}
