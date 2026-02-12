// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using Gardener.Core.Api.Impl.SystemAsset.Internal;
using Gardener.Core.Api.Impl.SystemAsset.Internal.Subscribes;
using Gardener.Core.Api.Impl.SystemAsset.Services;
using Gardener.Core.Authorization.Services;
using Gardener.Core.Module;
using Gardener.Core.SystemAsset.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.SystemAsset
{
    /// <summary>
    /// 系统资产（资源、api）
    /// </summary>
    public static class SystemAssetExtensions
    {
        /// <summary>
        /// 系统资产（资源、api）
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSystemAsset(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, SystemAssetServerModule>();

            services.AddScoped<IFunctionService, FunctionService>();
            services.AddScoped<IResourceFunctionService, ResourceFunctionService>();
            services.AddScoped<IResourceService, ResourceService>();

            //
            services.AddScoped<IApiQueryService, ApiQueryService>();

            services.AddRestController<FunctionService>();
            services.AddRestController<ResourceFunctionService>();
            services.AddRestController<ResourceService>();
            return services;
        }
    }
}
