// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Authorization;
using Furion.FriendlyException;
using Gardener.Core.Authorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gardener.Core.Api.Impl.Authorization.Internal
{
    /// <summary>
    /// JWT 授权自定义处理程序
    /// </summary>
    internal class JwtHandler : AppAuthorizeHandler
    {

        /// <summary>
        /// 授权验证核心方法（可重写）
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override async Task HandleAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            // 判断是否授权
            var isAuthenticated = context.User.Identity?.IsAuthenticated;
            if (isAuthenticated.HasValue && isAuthenticated.Value)
            {
                var authorizationManager = httpContext.RequestServices.GetRequiredService<IAuthService>();
                IAuthorizationRequirement? requirement1= context.PendingRequirements.FirstOrDefault(x => x is Microsoft.AspNetCore.Authorization.Infrastructure.DenyAnonymousAuthorizationRequirement);
                if (requirement1!=null )
                {
                    if (!await authorizationManager.CheckIdentityUsability())
                    {
                        context.Fail(StatusCodes.Status401Unauthorized);
                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        byte[] bytes = [];
                        await httpContext.Response.Body.FlushAsync();
                        return;
                    }
                    else
                    {
                        context.Succeed(requirement1);
                    }
                }
                IAuthorizationRequirement? requirement2 = context.PendingRequirements.FirstOrDefault(x => x is AppAuthorizeRequirement);
                if (requirement2!=null)
                {
                    if (!await authorizationManager.ChecktContenxtApiEndpoint())
                    {
                        context.Fail(StatusCodes.Status403Forbidden);
                        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        byte[] bytes = [];
                        await httpContext.Response.Body.FlushAsync();
                    }
                    else
                    {
                        context.Succeed(requirement2);
                    }
                }
            }
            else
            {
                // 退出 Swagger 登录
                httpContext.SignoutToSwagger();
                context.Fail((int)HttpStatusCode.Unauthorized);
            }
        }
    }
}