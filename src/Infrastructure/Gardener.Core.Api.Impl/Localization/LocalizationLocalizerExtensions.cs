// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Impl.Localization;
using Microsoft.AspNetCore.Builder;

namespace Gardener.Core.Api.Impl.Localization
{
    /// <summary>
    /// 注入包装好的本地化内容处理器
    /// </summary>
    public static partial class LocalizationLocalizerExtensions
    {

        /// <summary>
        /// 多语言中间件拓展-配置多语言，必须在 路由注册之前(服务端)
        /// </summary>
        /// <param name="app"></param>
        /// <param name="customizeConfigure"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app, Action<RequestLocalizationOptions>? customizeConfigure = null)
        {
            app.UseAppLocalization(customizeConfigure);
            app.ApplicationServices.InitLocalizationLocalizerUtil();
            return app;
        }
    }
}
