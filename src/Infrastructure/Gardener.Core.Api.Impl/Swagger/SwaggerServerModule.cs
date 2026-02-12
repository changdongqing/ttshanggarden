// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.Core.Api.Impl.Swagger.Services;
using Gardener.Core.Module;
using Gardener.Core.Swagger;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.Swagger
{
    /// <summary>
    /// 模块
    /// </summary>
    public class SwaggerServerModule : SwaggerModule, IServerModule
    {
        
        /// <summary>
        /// 
        /// </summary>
        public string ApiGroupName => Constant.InfrastructureService;

        /// <summary>
        /// 限定ApiTag
        /// </summary>
        public Type[]? IncludeApiControlTypes
        {
            get
            {
                return [typeof(SwaggerService)];
            }
        }

        /// <summary>
        /// 启动时
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task OnStart(CancellationToken cancellationToken)
        {
            App.GetRequiredService<ApiEndpointService>().Init();
            return Task.CompletedTask;
        }
    }
}
