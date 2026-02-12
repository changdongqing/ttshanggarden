// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.SystemConfig.Services;
using Gardener.Core.SystemConfig.Dtos;
using Microsoft.AspNetCore.Authorization;
using Gardener.Core.Api.Impl.SystemConfig.Entities;

namespace Gardener.Core.Api.Impl.SystemConfig.Services
{
    /// <summary>
    /// 系统配置服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class SystemConfigValueService : ServiceBase<SystemConfigValue, SystemConfigValueDto, string>, ISystemConfigValueService
    {
        /// <summary>
        /// 系统配置服务
        /// </summary>
        /// <param name="repository"></param>
        public SystemConfigValueService(IRepository<SystemConfigValue, MasterDbContextLocator> repository) : base(repository)
        {
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        /// <remarks>
        /// 查询系统配置
        /// </remarks>
        /// <returns></returns>
        [AllowAnonymous]
        public Task<SystemConfigDto> GetSystemConfig()
        {
            return Task.FromResult(new SystemConfigDto
            {

                Copyright = DateTime.Now.Year + " 园丁",
                SystemName = "园丁",
                SystemDescription = "园丁,是个很简单的管理系统"

            });
        }
    }
}
