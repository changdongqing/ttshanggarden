// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.Audit.Internal;
using TTShang.Core.Api.Impl.Audit.Services;
using TTShang.Core.Module;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Api.Impl.Audit
{
    /// <summary>
    /// 审计
    /// </summary>
    public static class AuditExtensions
    {
        /// <summary>
        /// 添加审计服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAudit(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, AuditServerModule>();

            services.Configure<MvcOptions>(options =>
            {
                //审计过滤器
                options.Filters.Add<AuditFunctionFilter>();
            });
            //数据管理
            services.AddScoped<IAuditService, AuditService<GardenerIgnoreAuditDbContextLocator>>();

            services.AddRestController<AuditEntityService>();
            services.AddRestController<AuditFunctionService>();
            return services;
        }
    }
}
