// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using TTShang.Core.Api.Impl.Email.Services;
using TTShang.Core.Api.Impl.VerifyCode.Internal;
using TTShang.Core.Api.Impl.VerifyCode.Internal.CacheStore;
using TTShang.Core.Api.Impl.VerifyCode.Internal.DbStore;
using TTShang.Core.Api.Impl.VerifyCode.Internal.Settings;
using TTShang.Core.Api.Impl.VerifyCode.Services;
using TTShang.Core.Email.Services;
using TTShang.Core.Module;
using TTShang.Core.VerifyCode.Core;
using TTShang.Core.VerifyCode.Enums;
using TTShang.Core.VerifyCode.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TTShang.Core.Api.Impl.VerifyCode
{
    /// <summary>
    /// 验证码
    /// </summary>
    public static class VerifyCodeExtensions
    {
        /// <summary>
        /// 添加验证码服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification">是否启用自动验证</param>
        /// <returns></returns>
        public static IServiceCollection AddVerifyCode<TVerifyCodeStoreService>(this IServiceCollection services, bool enableAutoVerification = true) where TVerifyCodeStoreService : class, IVerifyCodeStoreService
        {
            services.AddSingleton<IServerModule, VerifyCodeServerModule>();

            if (enableAutoVerification)
            {
                services.Configure<MvcOptions>(options =>
                {
                    //自动验证
                    options.Filters.Add<VerifyCodeAutoVerificationFilter>();
                });
            }
            //图片验证码配置
            services.AddConfigurableOptions<ImageVerifyCodeOptions>();
            //图片验证码
            services.AddKeyedScoped<IVerifyCode, ImageVerifyCode>(nameof(VerifyCodeTypeEnum) + nameof(VerifyCodeTypeEnum.Image));
            services.AddScoped<IImageVerifyCodeService, ImageVerifyCodeService>();

            //邮件服务
            services.TryAddScoped<IEmailService, EmailService>();
            //邮件验证码配置
            services.AddConfigurableOptions<EmailVerifyCodeOptions>();
            //邮件验证码
            services.AddKeyedScoped<IVerifyCode, EmailVerifyCode>(nameof(VerifyCodeTypeEnum) + nameof(VerifyCodeTypeEnum.Email));
            services.AddScoped<IEmailVerifyCodeService, EmailVerifyCodeService>();

            //验证码存储实现
            services.AddScoped<IVerifyCodeStoreService, TVerifyCodeStoreService>();

            services.AddRestController<EmailVerifyCodeService>();
            services.AddRestController<ImageVerifyCodeService>();
            return services;
        }

        /// <summary>
        /// 添加验证码服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification">是否启用自动验证</param>
        /// <returns></returns>
        public static IServiceCollection AddVerifyCode(this IServiceCollection services, bool enableAutoVerification = true)
        {
            string? storeMode = App.Configuration["VerifyCodeStoreSetting"];
            storeMode = storeMode ?? "Cache";
            if ("Cache".Equals(storeMode))
            {
                services.AddVerifyCode<VerifyCodeCacheStoreService>();
            }
            else if ("DB".Equals(storeMode))
            {
                services.AddVerifyCode<VerifyCodeDbStoreService>();
            }
            return services;
        }
    }
}
