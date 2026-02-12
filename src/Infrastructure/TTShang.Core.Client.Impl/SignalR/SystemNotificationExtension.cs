// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Client.Impl.SignalR
{
    /// <summary>
    /// SignalRClientManager
    /// </summary>
    public static class SystemNotificationExtension
    {
        /// <summary>
        /// SignalRClientManager
        /// </summary>
        /// <param name="services"></param>
        public static void AddSignalRClientManager(this IServiceCollection services)
        {
            services.AddTransient<ISignalRClientBuilder, SignalRClientBuilder>();
            services.AddScoped<ISignalRClientManager, SignalRClientManager>();
        }

    }
}
