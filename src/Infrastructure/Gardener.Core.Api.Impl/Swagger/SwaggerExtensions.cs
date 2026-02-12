// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Swagger.Services;
using Gardener.Core.Module;
using Gardener.Core.Swagger.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.Swagger
{
    /// <summary>
    /// Swagger
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// 添加Swagger
        /// </summary>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, SwaggerServerModule>();
            //注册swagger
            services.AddSpecificationDocuments(conf =>
            {
                conf.EnableAnnotations();
            });

            services.AddScoped<ISwaggerService, SwaggerService>();

            services.AddSingleton<ApiEndpointService>();
            services.AddSingleton<IApiEndpointService>(sp =>
            {
                return sp.GetRequiredService<ApiEndpointService>();
            });
            services.AddRestController<SwaggerService>();
            return services;
        }

        /// <summary>
        /// 启用Swagger
        /// </summary>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            //启用Swagger
            app.UseSpecificationDocuments();
            return app;
        }
    }
}
