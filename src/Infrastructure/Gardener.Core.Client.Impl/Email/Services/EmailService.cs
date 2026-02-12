// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Email.Dtos;
using Gardener.Core.Email.Services;

namespace Gardener.Core.Client.Impl.Email.Services
{
    [ScopedService]
    public class EmailService :ClientServiceCaller, IEmailService
    {
        public EmailService(IApiCaller apiCaller):base(apiCaller, "email")
        {
        }

        public async Task<bool> Send(SendEmailInputDto input)
        {
            return await apiCaller.PostAsync<SendEmailInputDto, bool>($"{this.baseUrl}/send", input);
        }
    }
}
