// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.FriendlyException;
using TTShang.Core.Authorization.Services;
using TTShang.Core.Common;
using TTShang.Core.Dtos;
using TTShang.Weighbridge.Impl.Entities;
using TTShang.Weighbridge.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Weighbridge.Impl.Services
{
    /// <summary>
    /// 车辆称重配置服务
    /// </summary>
    [ApiDescriptionSettings("Weighbridge", Module = "weighbridge")]
    public class VehicleWeighingConfigService : ServiceBase<VehicleWeighingConfig, VehicleWeighingConfigDto, Int32, GardenerMultiTenantDbContextLocator>, IVehicleWeighingConfigService
    {
        private readonly IIdentityService _identityService;
        /// <summary>
        /// 车辆称重配置服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="identityService"></param>
        public VehicleWeighingConfigService(IRepository<VehicleWeighingConfig, GardenerMultiTenantDbContextLocator> repository, IIdentityService identityService) : base(repository)
        {
            _identityService = identityService;
        }
        /// <summary>
        /// 查询-根据车牌号查询配置
        /// </summary>
        /// <remarks>
        /// 查询-根据车牌号查询配置
        /// </remarks>
        /// <param name="plateNumber"></param>
        /// <returns></returns>
        public Task<VehicleWeighingConfigDto?> FindByPlateNumber([FromQuery]string plateNumber)
        {
            return _repository.AsQueryable(false).Where(x => x.PlateNumber.Equals(plateNumber)).Select(x => x.Adapt<VehicleWeighingConfigDto>()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 保存车辆配置
        /// </summary>
        /// <remarks>
        /// 保存车辆配置
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> SaveVehicleConfig(VehicleWeighingConfigDto input)
        {
            Identity? identity = _identityService.GetIdentity();
            if (identity == null)
            {
                return false;
            }
            if (identity.TenantId.HasValue)
            {
                input.TenantId = identity.TenantId.Value;
            }
            VehicleWeighingConfig? oldConfig = await _repository.AsQueryable(false).Where(x => x.PlateNumber.Equals(input.PlateNumber)).FirstOrDefaultAsync();
            if (oldConfig != null)
            {
                oldConfig.MaximumLoad=input.MaximumLoad;
                await _repository.UpdateIncludeAsync(oldConfig, [nameof(VehicleWeighingConfig.MaximumLoad)]);
                return true;
            }
            else
            {
                await _repository.InsertAsync(input.Adapt<VehicleWeighingConfig>());
                return true;
            }
        }
    }
}
