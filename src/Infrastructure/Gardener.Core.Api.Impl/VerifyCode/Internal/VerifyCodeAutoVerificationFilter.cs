// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.VerifyCode;
using Gardener.Core.VerifyCode.Core;
using Gardener.Core.VerifyCode.Dtos;
using Gardener.Core.VerifyCode.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.VerifyCode.Internal
{
    /// <summary>
    /// 验证码自动验证过滤器
    /// </summary>
    internal class VerifyCodeAutoVerificationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public VerifyCodeAutoVerificationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            VerifyCodeAutoVerificationAttribute attribute = context.HttpContext.GetMetadata<VerifyCodeAutoVerificationAttribute>();
            if (attribute == null) { await next(); return; }

            IList<ParameterDescriptor> parameters = context.ActionDescriptor.Parameters;
            if (parameters == null || parameters.Count == 0)
            {
                await next(); return;
            }
            ParameterDescriptor? parameter = parameters.FirstOrDefault(x => x.ParameterType.IsSubclassOf(typeof(VerifyCodeCheckInput)));
            if (parameter == null) { await next(); return; }
            var inputTemp = context.ActionArguments[parameter.Name];
            if (inputTemp == null) { await next(); return; }
            var input = (VerifyCodeCheckInput)inputTemp;

            IVerifyCode _verifyCodeService = _serviceProvider.GetRequiredKeyedService<IVerifyCode>(nameof(VerifyCodeTypeEnum) + input.VerifyCodeType);
            if (await _verifyCodeService.Verify(input.VerifyCodeKey, input.VerifyCode))
            {
                await next(); return;
            }
            else
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Verify_Code_Verification_Failed);
            }
        }
    }
}
