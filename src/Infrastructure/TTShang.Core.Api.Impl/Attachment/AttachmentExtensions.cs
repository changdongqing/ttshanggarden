// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.Attachment.Services;
using TTShang.Core.Module;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Api.Impl.Attachment
{
    /// <summary>
    /// 附件
    /// </summary>
    public static class AttachmentExtensions
    {
        /// <summary>
        /// 添加审计服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAttachment(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, AttachmentServerModule>();
            services.AddRestController<AttachmentService>();
            return services;
        }
    }
}
