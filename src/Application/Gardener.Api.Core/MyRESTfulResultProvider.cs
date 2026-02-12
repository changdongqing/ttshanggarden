// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.UnifyResult;
using Gardener.Core.Enums;
using Gardener.Core.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Gardener.Api.Core
{
    /// <summary>
    /// RESTful 风格返回值
    /// </summary>
    [SuppressSniffer, UnifyModel(typeof(RESTfulResult<>))]
    public class MyRESTfulResultProvider : IUnifyResultProvider
    {

        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ILogger<MyRESTfulResultProvider> _logger;
        private readonly IOptions<JsonOptions> _jsonOptions;
        /// <summary>
        /// RESTful 风格返回值
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="jsonOptions"></param>
        public MyRESTfulResultProvider(ILogger<MyRESTfulResultProvider> logger, IOptions<JsonOptions> jsonOptions)
        {
            _logger = logger;
            _jsonOptions = jsonOptions;
        }

        /// <summary>
        /// 异常返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata)
        {
            
            _logger.LogError(context.Exception, metadata.Errors?.ToString());
            if (context.Exception is DbUpdateException)
            {
                // 检查内部异常是否包含外键约束失败的信息
                if (context.Exception.InnerException != null && context.Exception.InnerException.Message.Contains("foreign key constraint fails"))
                {
                    return new JsonResult(RESTfulResult(500, errorCode: nameof(Gardener.Core.Resources.SharedLocalResource.FOREIGN_KEY_CONSTRAINT_FAILS), errors: Gardener.Core.Resources.SharedLocalResource.FOREIGN_KEY_CONSTRAINT_FAILS), _jsonOptions.Value.JsonSerializerOptions);
                }
            }
            if (metadata.ErrorCode == null)
            {
                //未知异常
                return new JsonResult(RESTfulResult(metadata.StatusCode, errorCode: "Unknown", errors: metadata.Errors), _jsonOptions.Value.JsonSerializerOptions);
            }
            else
            {
                //主动抛出异常
                return new JsonResult(RESTfulResult(metadata.StatusCode, errorCode: metadata.ErrorCode, errors: context.Exception.Message), _jsonOptions.Value.JsonSerializerOptions);
            }

        }

        /// <summary>
        /// 成功返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public IActionResult OnSucceeded(ActionExecutedContext context, object data)
        {
            return new JsonResult(RESTfulResult(StatusCodes.Status200OK, true, data), _jsonOptions.Value.JsonSerializerOptions);
        }

        /// <summary>
        /// 验证失败返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public IActionResult OnValidateFailed(ActionExecutingContext context, ValidationMetadata metadata)
        {
            object errorCode = metadata.ErrorCode;

            if (errorCode == null && metadata.ModelState != null && metadata.ModelState.IsValid == false)
            {
                //字段验证失败
                errorCode = ExceptionCode.ModelValidateFailed;
            }
            return new JsonResult(RESTfulResult(StatusCodes.Status400BadRequest, errorCode: errorCode, errors: metadata.ValidationResult), _jsonOptions.Value.JsonSerializerOptions);
        }

        /// <summary>
        /// 特定状态码返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="unifyResultSettings"></param>
        /// <returns></returns>
        public async Task OnResponseStatusCodes(HttpContext context, int statusCode, UnifyResultSettingsOptions unifyResultSettings)
        {
            // 设置响应状态码
            UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);

            switch (statusCode)
            {
                // 处理 401 状态码
                case StatusCodes.Status401Unauthorized:
                    await context.Response.WriteAsJsonAsync(RESTfulResult(statusCode, errors: EnumHelper.GetEnumDescription(ExceptionCode.Unauthorized), errorCode: ExceptionCode.Unauthorized)
                        , _jsonOptions.Value.JsonSerializerOptions);
                    break;
                // 处理 403 状态码
                case StatusCodes.Status403Forbidden:
                    await context.Response.WriteAsJsonAsync(RESTfulResult(statusCode, errors: EnumHelper.GetEnumDescription(ExceptionCode.Forbidden), errorCode: ExceptionCode.Forbidden)
                        , _jsonOptions.Value.JsonSerializerOptions);
                    break;
                default: break;
            }
        }

        /// <summary>
        /// 返回 RESTful 风格结果集
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="succeeded"></param>
        /// <param name="data"></param>
        /// <param name="errors"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        static private object RESTfulResult(int statusCode, bool succeeded = default, object? data = default, object? errors = default, object? errorCode = default)
        {

            if (succeeded)
            {
                return new
                {
                    Data = data,
                    StatusCode = statusCode,
                    Succeeded = succeeded,
                    ErrorCode = errorCode?.ToString(),
                    Errors = errors,
                    Extras = UnifyContext.Take(),
                    Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
                };
            }

            return new
            {
                StatusCode = statusCode,
                Succeeded = succeeded,
                ErrorCode = errorCode?.ToString(),
                Errors = errors,
                Extras = UnifyContext.Take(),
                Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };
        }
        /// <summary>
        /// JWT 授权异常返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IActionResult OnAuthorizeException(DefaultHttpContext context, ExceptionMetadata metadata)
        {
            return new JsonResult(RESTfulResult(metadata.StatusCode, data: metadata.Data, errors: metadata.Errors)
              , _jsonOptions.Value.JsonSerializerOptions);
        }
    }
}
