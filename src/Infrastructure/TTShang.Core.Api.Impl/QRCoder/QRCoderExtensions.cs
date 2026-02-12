// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.QRCoder.Services;
using TTShang.Core.Module;
using TTShang.Core.QRCoder.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Api.Impl.QRCoder
{
    /// <summary>
    /// 二维码服务
    /// </summary>
    public static class QRCoderExtensions
    {
        /// <summary>
        /// 添加二维码服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddQRCoder(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, QRCoderServerModule>();
            services.AddScoped<IQRCoderService, QRCoderService>();
            services.AddRestController<QRCoderService>();
            return services;
        }
    }
}
