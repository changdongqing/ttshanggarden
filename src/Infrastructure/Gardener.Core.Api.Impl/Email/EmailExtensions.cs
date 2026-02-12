// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;
using Microsoft.Extensions.DependencyInjection;
using Gardener.Core.Email.Services;
using Gardener.Core.Api.Impl.Email.Services;

namespace Gardener.Core.Api.Impl.Email
{
    /// <summary>
    /// 邮件
    /// </summary>
    public static class EmailExtensions
    {
        /// <summary>
        /// 启用邮件模块
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEmail(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, EmailServerModule>();
            services.AddScoped<IEmailService, EmailService>();
            services
                .AddRestController<EmailServerConfigService>()
                .AddRestController<EmailService>()
                .AddRestController<EmailTemplateService>();
            return services;
        }
    }
}
