// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.QRCoder.Services;
using Gardener.Core.Module;
using Gardener.Core.QRCoder.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.QRCoder
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
