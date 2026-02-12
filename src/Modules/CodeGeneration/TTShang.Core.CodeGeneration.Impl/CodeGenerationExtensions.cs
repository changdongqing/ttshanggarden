// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.CodeGeneration.Impl.Services;
using TTShang.Core.CodeGeneration.Services;
using TTShang.Core.Module;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.CodeGeneration.Impl
{
    /// <summary>
    /// 代码生成
    /// </summary>
    public static class CodeGenerationExtensions
    {
        /// <summary>
        /// 添加代码生成
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification"></param>
        /// <returns></returns>
        public static IServiceCollection AddCodeGeneration(this IServiceCollection services, bool enableAutoVerification = true)
        {
            services.AddSingleton<IServerModule, CodeGenerationServerModule>();
            services.AddScoped<IEntityConfigService, EntityConfigService>();
            services.AddScoped<IFieldConfigService, FieldConfigService>();

            services.AddRestController<CodeGenerationService>();
            services.AddRestController<EntityConfigService>();
            services.AddRestController<FieldConfigService>();
            services.AddRestController<GenerateTemplateService>();
            return services;
        }
    }
}
