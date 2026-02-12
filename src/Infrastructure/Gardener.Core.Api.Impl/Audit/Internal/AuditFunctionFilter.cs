// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Audit.Entities;
using Gardener.Core.Authorization.Dtos;
using Gardener.Core.Authorization.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace Gardener.Core.Api.Impl.Audit.Internal
{
    /// <summary>
    /// 审计功能过滤器
    /// </summary>
    internal class AuditFunctionFilter : IAsyncActionFilter
    {
        private readonly IAuthService authorizationManager;
        private readonly IAuditService auditService;
        private readonly IServiceProvider serviceProvider;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizationManager"></param>
        /// <param name="auditService"></param>
        /// <param name="serviceProvider"></param>
        public AuditFunctionFilter(IAuthService authorizationManager,
            IAuditService auditService,
            IServiceProvider serviceProvider)
        {
            this.authorizationManager = authorizationManager;
            this.auditService = auditService;
            this.serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await Audit(context);
            await next();
        }

        #region private
        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Encoding GetRequestEncoding(HttpRequest request)
        {
            var requestContentType = request.ContentType;
            var requestMediaType = requestContentType == null ? default : new MediaType(requestContentType);
            var requestEncoding = requestMediaType.Encoding;
            if (requestEncoding == null)
            {
                requestEncoding = Encoding.UTF8;
            }
            return requestEncoding;
        }
        /// <summary>
        /// 读取body
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string?> ReadBodyAsync(HttpRequest request)
        {
            if (request.ContentType != null && request.ContentType.StartsWith("multipart/form-data;"))
            {
                //form 表单数据
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in request.Form)
                {
                    stringBuilder.AppendLine(item.ParseToString());
                }
                foreach (var item in request.Form.Files)
                {
                    stringBuilder.AppendLine($" {item.Name} {item.ContentType} {item.FileName} {item.Length}");
                }
                return stringBuilder.ToString();
            }
            string? result = null;
            request.Body.Position = 0;
            var stream = request.Body;
            long length = stream.Length;
            if (length > 0)
            {
                StreamReader streamReader = new StreamReader(stream, GetRequestEncoding(request));
                result = await streamReader.ReadToEndAsync();
            }
            request.Body.Position = 0;
            return result;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task Audit(ActionExecutingContext context)
        {
            IgnoreAuditAttribute ignoreAudit = context.HttpContext.GetMetadata<IgnoreAuditAttribute>();
            if (ignoreAudit != null) { return; }

            if (context.HttpContext.User.Identity == null || context.HttpContext.User.Identity.IsAuthenticated == false) { return; }

            ApiEndpoint? api = null;
            Identity? identity = null;
            if (authorizationManager != null)
            {
                api = await authorizationManager.GetApiEndpoint();
                if (api != null)
                {
                    IApiQueryService? apiSettingsQueryService = serviceProvider.GetService<IApiQueryService>();
                    //未启用审计
                    if (apiSettingsQueryService != null && Equals(false, await apiSettingsQueryService.IsEnableAudit(api.Key)))
                    {
                        return;
                    }
                }
                identity = authorizationManager.GetIdentity();
            }
            HttpContext httpContext = context.HttpContext;
            StringValues ua = string.Empty;
            StringBuilder headers = new StringBuilder();
            httpContext.Request.Headers.TryGetValue("User-Agent", out ua);
            if (httpContext.Request.Headers.TryGetValue("Client-AppName", out StringValues clientAppName))
            {
                headers.AppendLine($"Client-AppName:{clientAppName}");
            }
            if (httpContext.Request.Headers.TryGetValue("Client-CurrentVersion", out StringValues clientCurrentVersion))
            {
                headers.AppendLine($"Client-CurrentVersion:{clientCurrentVersion},");
            }
            if (httpContext.Request.Headers.TryGetValue("Client-CurrentVersioName", out StringValues clientCurrentVersioName))
            {
                headers.AppendLine($"Client-CurrentVersioName:{clientCurrentVersioName},");
            }
            ApiHttpMethod method = (ApiHttpMethod)Enum.Parse(typeof(ApiHttpMethod), httpContext.Request.Method.ToUpper());
            string path = $"{httpContext.Request.Path.Value}{httpContext.Request.QueryString.Value}";
            string? parameters = null;
            if (method.Equals(ApiHttpMethod.GET) || method.Equals(ApiHttpMethod.DELETE))
            {
                if (httpContext.Request.QueryString.Value != null)
                {
                    parameters = httpContext.Request.QueryString.Value;
                }
            }
            else if (method.Equals(ApiHttpMethod.POST) || method.Equals(ApiHttpMethod.PUT) || method.Equals(ApiHttpMethod.PATCH))
            {
                parameters = await ReadBodyAsync(httpContext.Request);
            }
            //功能唯一键-api唯一键
            string? functionKey = api != null ? api.Key : null;
            //功能概述-api概述+api描述
            string? functionSummary = api != null ? $"{api.Summary}-{api.Description}" : null;

            AuditFunction auditOperation = new AuditFunction()
            {
                CreatedTime = DateTimeOffset.Now,
                Id = Guid.NewGuid(),
                Ip = httpContext.GetRemoteIpAddressToIPv4(true),
                Path = path,
                Method = method,
                Parameters = parameters,
                UserAgent = ua.ToString(),
                FunctionKey = functionKey,
                FunctionSummary = functionSummary,

                OperaterId = identity != null ? identity.Id.ToString() : null,
                OperaterName = identity?.NickName ,
                OperaterType = identity != null ? identity.IdentityType : IdentityType.Unknown,
                LoginClientType = identity != null ? identity.LoginClientType : LoginClientType.Unknown,
                LoginId = identity != null ? identity.LoginId : null,
                CreateBy = identity?.Id,
                CreateIdentityType = identity?.IdentityType,
                TenantId = identity?.TenantId,
                HttpHeaders = headers.ToString(),
            };
            await auditService.SaveAuditFunction(auditOperation);
        }

    }
}
