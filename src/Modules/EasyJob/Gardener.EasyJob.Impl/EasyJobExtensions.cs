// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;
using Gardener.EasyJob.Impl.Core;
using Gardener.Core.NotificationSystem;
using Gardener.EasyJob.Impl.Services;
using Furion;

namespace Gardener.EasyJob.Impl
{
    /// <summary>
    /// 任务调度
    /// </summary>
    public static class EasyJobExtensions
    {
        /// <summary>
        /// 启用任务调度
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEasyJob(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, EasyJobServerModule>();
            string? clusterId = App.Configuration["EasyJob:ClusterId"];
            // 任务调度
            services.AddSchedule(options =>
            {
                // 添加作业持久化器
                options.AddPersistence<DbJobPersistence>();
                // 添加监控器
                options.AddMonitor<EasyJobMonitor>();
                //添加集群（故障转移）
                options.AddClusterServer<JobClusterServer>();

                //作业集群 Id
                options.ClusterId = clusterId ?? "gardener-job";
            });

            services.AddSingleton<DynamicJobCompiler>();
            services.AddScoped<ISystemNotificationHubGrouper, EasyJobNotificationHubGrouper>();
            services.AddScoped<SchedulerLoader>();

            services.AddScoped<ISysJobDetailService, SysJobDetailService>();
            services.AddScoped<ISysJobClusterService, SysJobClusterService>();
            services.AddTransient<ISysJobTriggerService, SysJobTriggerService>();

            return services;
        }
    }
}
