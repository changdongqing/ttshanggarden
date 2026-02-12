// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Authorization.Services;
using Gardener.Core.Common;
using Gardener.Core.Dtos;
using Gardener.Core.Enums;
using Gardener.Weighbridge.Impl.Entities;
using Gardener.Weighbridge.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gardener.Weighbridge.Impl.Services
{

    /// <summary>
    /// 用户地磅配置服务
    /// </summary>
    [ApiDescriptionSettings("Weighbridge", Module = "weighbridge")]
    public class UserWeighbridgeConfigService : ServiceBase<UserWeighbridgeConfig, UserWeighbridgeConfigDto, int, GardenerMultiTenantDbContextLocator>, IUserWeighbridgeConfigService
    {
        private readonly IIdentityService _identityService;
        /// <summary>
        /// 用户地磅配置服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="identityService"></param>
        public UserWeighbridgeConfigService(IRepository<UserWeighbridgeConfig, GardenerMultiTenantDbContextLocator> repository, IIdentityService identityService) : base(repository)
        {
            _identityService = identityService;
        }
        /// <summary>
        /// 查询-获取自己的配置数据
        /// </summary>
        /// <remarks>
        /// 查询-获取自己的配置数据
        /// </remarks>
        /// <returns></returns>
        public Task<UserWeighbridgeConfigDto?> FindMyUserConfig()
        {
            Identity? identity = _identityService.GetIdentity();
            if (identity == null || !identity.IdentityType.Equals(IdentityType.User))
            {
                return Task.FromResult<UserWeighbridgeConfigDto?>(null);
            }
            int userId = int.Parse(identity.Id);
            return FindUserConfig(userId);
        }

        /// <summary>
        /// 查询-根据用户编号获取配置数据
        /// </summary>
        /// <remarks>
        /// 查询-根据用户编号获取配置数据
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<UserWeighbridgeConfigDto?> FindUserConfig(int userId)
        {
            return _repository.AsQueryable(false).Where(x => x.UserId.Equals(userId) && x.IsDeleted == false && x.IsLocked == false).Select(x => x.Adapt<UserWeighbridgeConfigDto>()).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 保存-添加或更新当前用户配置
        /// </summary>
        /// <remarks>
        /// 保存-添加或更新当前用户配置
        /// </remarks>
        /// <param name="userWeighbridgeConfig"></param>
        /// <returns></returns>
        public async Task<UserWeighbridgeConfigDto?> SaveMyUserWeighbridgeConfig(UserWeighbridgeConfigDto userWeighbridgeConfig)
        {
            Identity? identity = _identityService.GetIdentity();
            if (identity == null || !identity.IdentityType.Equals(IdentityType.User))
            {
                return null;
            }
            int userId = int.Parse(identity.Id);
            var userConfig = await _repository.Where(x => x.UserId.Equals(userId)).FirstOrDefaultAsync();
            if (userConfig == null)
            {
                userConfig = userWeighbridgeConfig.Adapt<UserWeighbridgeConfig>();
                userConfig.UserId = userId;
                userConfig.Id = 0;
                var entity = await _repository.InsertAsync(userConfig);
                userWeighbridgeConfig= entity.Entity.Adapt<UserWeighbridgeConfigDto>();
            }
            else
            {
                bool change = false;
                if(!string.IsNullOrEmpty(userWeighbridgeConfig.DefaultPrintTemplateKey))
                {
                    userConfig.DefaultPrintTemplateKey = userWeighbridgeConfig.DefaultPrintTemplateKey;
                    change= true;
                }
                if (userWeighbridgeConfig.DefaultWeighbridgeConfigId.HasValue)
                {
                    userConfig.DefaultWeighbridgeConfigId = userWeighbridgeConfig.DefaultWeighbridgeConfigId;
                    change = true;
                }
                if (change)
                {
                    userConfig.UserId = userId;
                    var entity = await _repository.UpdateAsync(userConfig);
                    userWeighbridgeConfig = entity.Entity.Adapt<UserWeighbridgeConfigDto>();
                }
            }
            return userWeighbridgeConfig;

        }
    }
}
