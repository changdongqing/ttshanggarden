// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Schedule;
using Gardener.Core.NotificationSystem;
using Gardener.Weighbridge.Dtos.Cmds;
using Gardener.Weighbridge.Impl.Entities;
using Gardener.Weighbridge.Impl.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Gardener.Weighbridge.Impl.Jobs
{
    /// <summary>
    /// 定时自动发送读取值任务
    /// </summary>
    [JobDetail("job_AutoReadValueJob", Description = "定时自动发送读取值任务", GroupName = "Weighbridge", Concurrent = false)]
    [Period(1000, TriggerId = "trigger_AutoReadValueJob", Description = "定时自动发送读取值任务")]
    public class AutoReadValueJob : IJob
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ISystemNotificationService systemNotificationService;
        private readonly ILogger<AutoReadValueJob> logger;
        /// <summary>
        /// 定时自动发送读取值任务
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="systemNotificationService"></param>
        /// <param name="logger"></param>
        public AutoReadValueJob(IServiceScopeFactory serviceScopeFactory, ISystemNotificationService systemNotificationService, ILogger<AutoReadValueJob> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.systemNotificationService = systemNotificationService;
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {

            using var factory = serviceScopeFactory.CreateScope();
            IRepository<WeighbridgeConfig, MasterDbContextLocator> repository = factory.ServiceProvider.GetRequiredService<IRepository<WeighbridgeConfig, MasterDbContextLocator>>();
            List<string> list = await repository.AsQueryable(false).Where(x => x.IsDeleted == false && x.IsLocked == false).Select(x => x.DeviceIds).ToListAsync();
            long count = 0, success = 0, error = 0, noSubscriber = 0;
            int maxDegreeOfParallelism = 3; // 允许的最大并发数
            var tasks = new List<Task>(maxDegreeOfParallelism);
            HashSet<Guid> deviceIds = new HashSet<Guid>();
            foreach (string deviceids in list)
            {
                var ids = deviceids.Split(",");
                foreach (var id in ids)
                {
                    Guid deviceId = Guid.Parse(id);
                    deviceIds.Add(deviceId);
                }
            }
            if (!deviceIds.Any())
            {
                return;
            }
            ConcurrentQueue<Guid> queue = new ConcurrentQueue<Guid>();
            foreach (var item in deviceIds)
            {
                queue.Enqueue(item);
            }

            for (int i = 0; i < maxDegreeOfParallelism; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var factory2 = serviceScopeFactory.CreateScope())
                    {
                        WeighbridgeDeviceService weighbridgeDeviceService = factory2.ServiceProvider.GetRequiredService<WeighbridgeDeviceService>();
                        while (queue.TryDequeue(out Guid deviceId))
                        {
                            Interlocked.Increment(ref count);
                            try
                            {
                                WeighbridgeNotificationData weighbridgeNotificationData = new WeighbridgeNotificationData(deviceId);
                                if (!await systemNotificationService.ExistsDynamicSubscriber(weighbridgeNotificationData))
                                {
                                    Interlocked.Increment(ref noSubscriber);
                                    continue;
                                }
                                bool result = await weighbridgeDeviceService.ReadValue(new DeviceCmdInput<ReadValueCmd>(deviceId, new ReadValueCmd(24)));
                                if (result)
                                {
                                    Interlocked.Increment(ref success);
                                }
                                else
                                {
                                    Interlocked.Increment(ref error);
                                }
                            }
                            catch (Exception ex)
                            {
                                Interlocked.Increment(ref error);
                                logger.LogError(ex, $"Weighbridge readValue error,deviceId:{deviceId}");
                            }
                        }
                    }

                }));
            }
            await Task.WhenAll(tasks);
            context.Result = $"执行完成，总数{count},未订阅{noSubscriber}，成功{success}，失败{error}。";
        }
    }
}