// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// 添加Common模块
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommonModule(this IServiceCollection services)
        {
            //Common模块
            services.AddSingleton<IServerModule, CommonServerModule>();
            return services;
        }
    }
}
