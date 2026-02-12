// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.Module.Internal;
using TTShang.Core.Api.Impl.Module.Services;
using TTShang.Core.Module;
using TTShang.Core.Module.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Api.Impl.Module
{
    /// <summary>
    /// 模块
    /// </summary>
    public static class ModuleExtensions
    {
        /// <summary>
        /// 添加模块管理
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <remarks>
        /// 请在使用到ef的模块之前添加。
        /// </remarks>
        public static IServiceCollection AddModuleManager(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, SystemModuleServerModule>();
            services.AddSingleton<ServerModuleManager>();
            services.AddScoped<ISystemModuleService, SystemModuleService>();
            services.AddHostedService<ModuleBackgroundService>();
            services.AddRestController<SystemModuleService>();
            return services;
        }
    }
}
