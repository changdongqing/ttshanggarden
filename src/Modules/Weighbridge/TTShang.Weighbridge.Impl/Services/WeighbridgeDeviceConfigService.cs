// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Cache;
using TTShang.Core.Common;
using TTShang.Core.Dtos;
using TTShang.Iot.Services;
using TTShang.Weighbridge.Impl.Entities;
using TTShang.Weighbridge.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace TTShang.Weighbridge.Impl.Services
{
    /// <summary>
    /// 地磅设备配置服务
    /// </summary>
    [ApiDescriptionSettings("Weighbridge", Module = "weighbridge")]
    public class WeighbridgeDeviceConfigService : ServiceBase<WeighbridgeDeviceConfig, WeighbridgeDeviceConfigDto, Int32, MasterDbContextLocator>, IWeighbridgeDeviceConfigService
    {
        private readonly IDeviceService deviceService;
        private readonly ICache cache;
        /// <summary>
        /// 地磅设备配置服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="deviceService"></param>
        /// <param name="cache"></param>
        public WeighbridgeDeviceConfigService(IRepository<WeighbridgeDeviceConfig, MasterDbContextLocator> repository, IDeviceService deviceService, ICache cache) : base(repository)
        {
            this.deviceService = deviceService;
            this.cache = cache;
        }

        /// <summary>
        /// 查询-获取多个设备配置信息
        /// </summary>
        /// <param name="deviceIds"></param>
        /// <returns></returns>
        public async Task<Dictionary<Guid, WeighbridgeDeviceConfigDto>> FindDeviceConfig(Guid[] deviceIds)
        {
            if (deviceIds == null || !deviceIds.Any())
            {
                return new();
            }
            List<WeighbridgeDeviceConfigDto> list = await _repository.AsQueryable(false).Where(x => deviceIds.Contains(x.DeviceId)).Select(x => x.Adapt<WeighbridgeDeviceConfigDto>()).ToListAsync();
            return list.ToDictionary(x => x.DeviceId, x => x);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 高级查询，根据输入条件组合进行数据查询和排序
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async override Task<PageList<WeighbridgeDeviceConfigDto>> Search(PageRequest request)
        {
            PageList<WeighbridgeDeviceConfigDto> result = await base.Search(request);
            if (result.Items.Any())
            {
                foreach (var item in result.Items)
                {
                    item.Device = await deviceService.Find(item.DeviceId);
                }
            }
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除单条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> Delete(int id)
        {
            var entity = await base.Find(id);
            var result = await base.Delete(id);
            if (entity != null)
            {
                await ClearWeighbridgeDeviceConfigCache(entity.DeviceId);
            }
            return result;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "批量删除", Description = "根据多个主键批量删除")]
        public override async Task<bool> Deletes([FromBody] int[] ids)
        {
            List<Guid> list = await _repository
                .AsQueryable(false)
                .Where(x => ids.Contains(x.Id))
                .Select(x => x.DeviceId)
                .ToListAsync();
            var result = await base.Deletes(ids);
            foreach (var id in list)
            {
                await ClearWeighbridgeDeviceConfigCache(id);
            }
            return result;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <remarks>
        /// 更新单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(WeighbridgeDeviceConfigDto input)
        {
            var result = await base.Update(input);
            await ClearWeighbridgeDeviceConfigCache(deviceId: input.DeviceId);
            return result;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 添加单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<WeighbridgeDeviceConfigDto> Insert(WeighbridgeDeviceConfigDto input)
        {
            var result = await base.Insert(input);

            await ClearWeighbridgeDeviceConfigCache(deviceId: input.DeviceId);

            return result;
        }

        /// <summary>
        /// 查询-获取设备配置信息
        /// </summary>
        /// <remarks>
        /// 根据设备编号获取设备配置
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task<WeighbridgeDeviceConfigDto?> FindDeviceConfigByDeviceId(Guid deviceId)
        {
            WeighbridgeDeviceConfigDto? result = await cache.GetAsync("wbg:WeighbridgeDeviceConfig:" + deviceId, async () =>
            {
                WeighbridgeDeviceConfigDto? config = await _repository
                .AsQueryable(false)
                .Where(x => x.DeviceId.Equals(deviceId))
                .Select(x => x.Adapt<WeighbridgeDeviceConfigDto>())
                .FirstOrDefaultAsync();
                return config;
            }, TimeSpan.FromHours(1));

            return result;
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        private async Task ClearWeighbridgeDeviceConfigCache(Guid deviceId)
        {
            await cache.RemoveAsync("wbg:WeighbridgeDeviceConfig:" + deviceId);
        }
    }
}
