// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using Gardener.Core.Api.Impl.UserCenter.Services;
using Gardener.Core.Api.Impl.UserCenter.Subscribes;
using Gardener.Core.Module;
using Gardener.Core.UserCenter.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.UserCenter
{
    /// <summary>
    /// 用户中心
    /// </summary>
    public static class UserCenterExtensions
    {
        /// <summary>
        /// 用户中心
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUserCenter(this IServiceCollection services)
        {
            //module
            services.AddSingleton<IServerModule, UserCenterServerModule>();
            //service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITenantConfigService, TenantConfigService>();
            services.AddScoped<ITenantConfigTemplateService, TenantConfigTemplateService>();
            //subscriber
            services.AddSingleton<IEventSubscriber, TenantEventSubscriber>();
            //api
            services.AddRestController<AccountService>();
            services.AddRestController<ClientFunctionService>();
            services.AddRestController<ClientService>();
            services.AddRestController<DeptService>();
            services.AddRestController<PositionService>();
            services.AddRestController<RoleService>();
            services.AddRestController<TenantResourceService>();
            services.AddRestController<TenantService>();
            services.AddRestController<UserService>();
            services.AddRestController<TenantConfigService>();
            services.AddRestController<TenantConfigTemplateService>();

            return services;
        }
    }
}
