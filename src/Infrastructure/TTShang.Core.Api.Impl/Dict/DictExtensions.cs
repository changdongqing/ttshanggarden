// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.Dict.Services;
using TTShang.Core.Dict.Services;
using TTShang.Core.Module;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Api.Impl.Dict
{
    /// <summary>
    /// 字典
    /// </summary>
    public static class DictExtensions
    {
        /// <summary>
        /// 启用字典
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDict(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, DictServerModule>();
            services.AddScoped<ICodeService, CodeService>();
            services.AddScoped<ICodeTypeService, CodeTypeService>();

            services.AddRestController<CodeService>();
            services.AddRestController<CodeTypeService>();
            return services;
        }
    }
}
