// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Email.Entities;
using Gardener.Core.Email.Dtos;
using Gardener.Core.Email.Services;

namespace Gardener.Core.Api.Impl.Email.Services
{

    /// <summary>
    /// 邮件服务器配置服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class EmailServerConfigService : ServiceBase<EmailServerConfig, EmailServerConfigDto, Guid>, IEmailServerConfigService
    {
        /// <summary>
        /// 邮件服务器配置服务
        /// </summary>
        /// <param name="repository"></param>
        public EmailServerConfigService(IRepository<EmailServerConfig> repository) : base(repository)
        {
        }
    }
}
