// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Authorization.Services;
using Gardener.Core.NotificationSystem;
using Gardener.EasyJob.Impl.Core;
using Gardener.EasyJob.Impl.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Gardener.EasyJob.Impl.Services
{
    /// <summary>
    /// 定时任务-用户配置服务
    /// </summary>
    [ApiDescriptionSettings("EasyJob", Module = "job")]
    public class SysJobUserConfigService : ISysJobUserConfigService, IDynamicApiController
    {
        private readonly IIdentityService identityService;
        private readonly IRepository<SysJobUserConfig> userConfigRepository;
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 定时任务-用户配置服务
        /// </summary>
        /// <param name="identityService"></param>
        /// <param name="userConfigRepository"></param>
        /// <param name="systemNotificationService"></param>
        public SysJobUserConfigService(IIdentityService identityService, IRepository<SysJobUserConfig> userConfigRepository, ISystemNotificationService systemNotificationService)
        {
            this.identityService = identityService;
            this.userConfigRepository = userConfigRepository;
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 查询配置
        /// </summary>
        /// <remarks>
        /// 获取我的的配置
        /// </remarks>
        /// <returns></returns>
        public async Task<SysJobUserConfigDto?> GetMyConfig()
        {
            var identity = identityService.GetIdentity();
            if (identity == null)
            {
                return null;
            }
            SysJobUserConfig? userConfig = await userConfigRepository.Where(x => x.IdentityId.Equals(identity.Id) && x.IdentityType.Equals(identity.IdentityType) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
            if (userConfig == null)
            {
                userConfig = new SysJobUserConfig();
                userConfig.IdentityType = identity.IdentityType;
                userConfig.IdentityId = identity.Id;
                userConfig.EnableRealTimeMonitor = false;
            }
            return userConfig.Adapt<SysJobUserConfigDto>();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <remarks>
        /// 保存我的配置
        /// </remarks>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<SysJobUserConfigDto?> SaveMyConfig(SysJobUserConfigDto config)
        {
            var identity = identityService.GetIdentity();
            if (identity == null)
            {
                return null;
            }
            IEnumerable<string>? oldGroups = await systemNotificationService.GetGroups<EasyJobNotificationHubGrouper>(identity);
            SysJobUserConfigDto? result;
            SysJobUserConfig? userConfig = await userConfigRepository.Where(x => x.IdentityId.Equals(identity.Id) && x.IdentityType.Equals(identity.IdentityType) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
            if (userConfig != null)
            {
                userConfig.EnableRealTimeMonitor = config.EnableRealTimeMonitor;
                EntityEntry<SysJobUserConfig> entity = await userConfigRepository.UpdateNowAsync(userConfig);
                result = entity.Entity.Adapt<SysJobUserConfigDto>();
            }
            else
            {
                userConfig = new SysJobUserConfig();
                config.Adapt(userConfig);
                EntityEntry<SysJobUserConfig> entity = await userConfigRepository.UpdateNowAsync(userConfig);
                result = entity.Entity.Adapt<SysJobUserConfigDto>();
            }
            //上面需要立即入库，下面使用分组器才能查询得到
            //控制通知系统
            if (config.EnableRealTimeMonitor)
            {
                await systemNotificationService.UserGroupAdd<EasyJobNotificationHubGrouper>(identity);
            }
            else if(oldGroups != null && oldGroups.Any())
            {
                await systemNotificationService.UserGroupRemove(oldGroups,identity);
            }
            return result;
        }
    }
}
