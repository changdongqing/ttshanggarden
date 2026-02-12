// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.VerifyCode.Core;
using Gardener.Core.VerifyCode.Dtos;
using Gardener.Core.VerifyCode.Enums;
using Gardener.Core.VerifyCode.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.VerifyCode.Services
{
    /// <summary>
    /// 邮件验证码服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class EmailVerifyCodeService : IEmailVerifyCodeService
    {
        private readonly IVerifyCode verifyCodeService;
        /// <summary>
        /// 验证码服务
        /// </summary>
        /// <param name="verifyCodeService"></param>
        public EmailVerifyCodeService([FromKeyedServices(nameof(VerifyCodeTypeEnum) + nameof(VerifyCodeTypeEnum.Email))] IVerifyCode verifyCodeService)
        {
            this.verifyCodeService = verifyCodeService;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="input">类型</param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<EmailVerifyCodeOutput> Create(EmailVerifyCodeInput input)
        {
            EmailVerifyCodeOutput imageVerifyCode = (EmailVerifyCodeOutput)await verifyCodeService.Create(input);
            return imageVerifyCode;
        }
        /// <summary>
        /// 移除验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> Remove(string key)
        {
            return await verifyCodeService.Remove(key);
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> Verify(EmailVerifyCodeCheckInput input)
        {
            return await verifyCodeService.Verify(input.VerifyCode, input.VerifyCodeKey);
        }
    }
}
