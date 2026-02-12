// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.AppManager.Services;
using Gardener.Core.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.AppManager
{
    /// <summary>
    /// 应用管理
    /// </summary>
    public static class AppManagerExtensions
    {
        /// <summary>
        /// 添加应用管理
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppManager(this IServiceCollection services)
        {
            //模块
            services.AddSingleton<IServerModule, AppManagerServerModule>();

            services.AddRestController<AppService>();
            services.AddRestController<AppVersionService>();
            return services;
        }
    }
}
