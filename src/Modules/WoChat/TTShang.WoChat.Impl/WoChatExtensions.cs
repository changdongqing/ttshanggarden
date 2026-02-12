// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;
using TTShang.WoChat.Impl.Services;

namespace TTShang.WoChat.Impl
{
    /// <summary>
    /// WoChat
    /// </summary>
    public static class WoChatExtensions
    {
        /// <summary>
        /// 添加WoChat
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification"></param>
        /// <returns></returns>
        public static IServiceCollection AddWoChat(this IServiceCollection services, bool enableAutoVerification = true)
        {
            services.AddSingleton<IServerModule, WoChatServerModule>();

            services.AddScoped<IWoChatImService, WoChatImService>();
            return services;
        }
    }
}
