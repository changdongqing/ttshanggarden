// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Email.Services;
using TTShang.Core.Email.Dtos;
using TTShang.Core.Api.Impl.Email.Entities;

namespace TTShang.Core.Api.Impl.Email.Services
{

    /// <summary>
    /// 邮件模板服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class EmailTemplateService : ServiceBase<EmailTemplate, EmailTemplateDto, Guid>, IEmailTemplateService
    {

        /// <summary>
        /// 邮件模板服务
        /// </summary>
        /// <param name="repository"></param>
        public EmailTemplateService(IRepository<EmailTemplate> repository) : base(repository)
        {
        }
    }
}
