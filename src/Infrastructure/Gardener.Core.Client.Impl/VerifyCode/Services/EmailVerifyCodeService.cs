// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.VerifyCode.Dtos;
using Gardener.Core.VerifyCode.Services;

namespace Gardener.Core.Client.Impl.VerifyCode.Services
{
    [ScopedService]
    public class EmailVerifyCodeService :ClientServiceCaller, IEmailVerifyCodeService
    {
        public EmailVerifyCodeService(IApiCaller apiCaller):base(apiCaller, "email-verify-code")
        {
        }

        public async Task<EmailVerifyCodeOutput> Create(EmailVerifyCodeInput input)
        {
            return await apiCaller.PostAsync<VerifyCodeInput, EmailVerifyCodeOutput>($"{this.baseUrl}", input);
        }

        public async Task<bool> Remove(string key)
        {
            return await apiCaller.DeleteAsync<bool>($"{this.baseUrl}/{key}");
        }

        public async Task<bool> Verify(EmailVerifyCodeCheckInput verifyCodeInput)
        {
            return await apiCaller.PostAsync<EmailVerifyCodeCheckInput, bool>($"{this.baseUrl}/verify", verifyCodeInput);
        }
    }
}
