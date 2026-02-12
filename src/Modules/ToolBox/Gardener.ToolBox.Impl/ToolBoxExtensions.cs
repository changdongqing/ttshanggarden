// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.ToolBox.Impl
{
    /// <summary>
    /// 工具箱
    /// </summary>
    public static class ToolBoxExtensions
    {
        /// <summary>
        /// 添加工具箱
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification"></param>
        /// <returns></returns>
        public static IServiceCollection AddToolBox(this IServiceCollection services, bool enableAutoVerification = true)
        {
            services.AddSingleton<IServerModule, ToolBoxServerModule>();
            return services;
        }
    }
}
