// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.LicensePlateRecognition.Internal;
using Gardener.Core.Api.Impl.LicensePlateRecognition.Services;
using Gardener.Core.LicensePlateRecognition.Enums;
using Gardener.Core.LicensePlateRecognition.Services;
using Gardener.Core.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.LicensePlateRecognition
{
    /// <summary>
    /// 车牌识别
    /// </summary>
    public static class LPRExtensions
    {
        /// <summary>
        /// 启用车牌识别
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLPR(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, LPRServerModule>();
            //配置
            services.AddConfigurableOptions<OpenAnprSettings>();

            //open anpr
            services.AddKeyedSingleton<ILPRService, OpenAnprService>(LPRServiceType.OPENANPR);

            //默认
            services.AddSingleton<ILPRService>(sp =>
            {
                return sp.GetRequiredKeyedService<ILPRService>(LPRServiceType.OPENANPR);
            });

            //api
            services.AddRestController<LicencePlateService>();

            return services;
        }
    }
}
