// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.SystemConfig.Services;
using Gardener.Core.Module;
using Gardener.Core.SystemConfig.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.SystemConfig
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public static class SystemConfigExtensions
    {
        /// <summary>
        /// 系统配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSystemConfig(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, SystemConfigServerModule>();

            services.AddScoped<ISystemConfigValueService, SystemConfigValueService>();

            services.AddRestController<SystemConfigValueService>();
            return services;
        }
    }
}
