// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.FriendlyException;
using TTShang.Core.Authorization.Services;
using TTShang.Core.Common;
using TTShang.Core.Dtos;
using TTShang.Core.Dtos.Constraints;
using TTShang.Core.Resources;
using TTShang.Iot.Dtos;
using TTShang.Iot.Services;
using TTShang.Weighbridge.Impl.Entities;
using TTShang.Weighbridge.Resources;
using TTShang.Weighbridge.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace TTShang.Weighbridge.Impl.Services
{
    /// <summary>
    /// 地磅配置服务
    /// </summary>
    [ApiDescriptionSettings("Weighbridge", Module = "weighbridge")]
    public class WeighbridgeConfigService : ServiceBase<WeighbridgeConfig, WeighbridgeConfigDto, Guid, GardenerMultiTenantDbContextLocator>, IWeighbridgeConfigService
    {
        private readonly IDeviceService deviceService;
        private readonly IProductService productService;
        private readonly IIdentityService identityService;
        /// <summary>
        /// 地磅配置服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="deviceService"></param>
        /// <param name="productService"></param>
        /// <param name="identityService"></param>
        public WeighbridgeConfigService(IRepository<WeighbridgeConfig, GardenerMultiTenantDbContextLocator> repository, IDeviceService deviceService, IProductService productService, IIdentityService identityService) : base(repository)
        {
            this.deviceService = deviceService;
            this.productService = productService;
            this.identityService = identityService;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="tenantId">租户编号</param>
        /// <param name="includLocked">是否包含锁定的</param>
        /// <remarks>
        /// 获取所有可用数据，(实现<see cref="IModelDeleted"/> <see cref="IModelLocked"/>时会自动过滤)
        /// </remarks>
        /// <returns></returns>
        public override async Task<List<WeighbridgeConfigDto>> GetAllUsable([FromQuery] Guid? tenantId = null, [FromQuery] bool includLocked = false)
        {
            List<WeighbridgeConfigDto> result = await base.GetAllUsable(tenantId, includLocked);
            if (result.Any())
            {
                foreach (var item in result)
                {
                    await FillDeviceInfo(item);
                }
            }
            return result;
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
        public override async Task<PageList<WeighbridgeConfigDto>> Search(PageRequest request)
        {
            PageList<WeighbridgeConfigDto> page = await base.Search(request);
            if (page.Items.Any())
            {
                foreach (var item in page.Items)
                {
                    await FillDeviceInfo(item);
                }
            }
            return page;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据主键查找单条数据，如果不存在，返回null
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("find/{id}")]
        public override async Task<WeighbridgeConfigDto?> Find(Guid id)
        {
            WeighbridgeConfigDto? dto = await base.Find(id);

            return await FillDeviceInfo(dto);
        }

        /// <summary>
        /// 填充设备产品信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task<WeighbridgeConfigDto?> FillDeviceInfo(WeighbridgeConfigDto? dto)
        {
            if (dto != null && !string.IsNullOrEmpty(dto.DeviceIds))
            {
                Dictionary<Guid, ProductDto?> products = new();

                foreach (var deviceId in dto.DeviceIds.Split(",").Select(x => Guid.Parse(x)))
                {
                    var device = await deviceService.Find(deviceId);
                    if (device != null)
                    {
                        if (device.ProductId.HasValue)
                        {
                            if (!products.ContainsKey(device.ProductId.Value))
                            {
                                var product = await productService.Find(device.ProductId.Value);
                                products.Add(device.ProductId.Value, product);
                            }
                            device.Product = products[device.ProductId.Value];
                        }
                        dto.Devices.Add(device);
                    }
                }
            }
            return dto;

        }
        /// <summary>
        /// 保存我的地磅配置
        /// </summary>
        /// <remarks>
        /// 保存我的地磅配置
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> SaveMyWeighbridgeConfig(SaveMyWeighbridgeConfigInput input)
        {
            //判断设备是否存在可用
            if (!input.DeviceIds.Any())
            {
                throw Oops.Bah(WeighbridgeLocalResource.DeviceIdCannotBeEmpty);
            }
            var identity = identityService.GetIdentity();
            if (input.WeighbridgeConfigId.HasValue)
            {
                //更新
                var config = await _repository.AsQueryable(false).FirstOrDefaultAsync(x => x.Id == input.WeighbridgeConfigId.Value);
                if (config == null)
                {
                    throw Oops.Bah(string.Format(SharedLocalResource.Item_Data_Not_Find, WeighbridgeLocalResource.WeighbridgeConfig));
                }
                if (!input.Name.Equals(config.Name))
                {
                    if (await _repository.AsQueryable(false).AnyAsync(x => x.Name.Equals(input.Name) && !x.Id.Equals(input.WeighbridgeConfigId)))
                    {
                        throw Oops.Bah(WeighbridgeLocalResource.ConfigNameRepeat);
                    }
                }
                List<(Guid, Guid)> notBoundIds = new List<(Guid, Guid)>();
                foreach (var deviceId in input.DeviceIds)
                {
                    var oldDeviceIds = config.DeviceIds.Split(",").Select(x => Guid.Parse(x));
                    if (oldDeviceIds.Any(x => x.Equals(deviceId)))
                    {
                        continue;
                    }

                    var device = await deviceService.FindGlobal(deviceId);

                    if (device == null)
                    {
                        throw Oops.Bah(WeighbridgeLocalResource.DeviceDoesNotExist, deviceId);
                    }
                    else if (device.TenantId != null)
                    {
                        //租户只能绑定自己的
                        if (identity != null && identity.TenantId.HasValue && identity is IModelTenantId tenantId && tenantId.IsTenant && !device.TenantId.Equals(tenantId.TenantId))
                        {
                            throw Oops.Bah(WeighbridgeLocalResource.DeviceDoesNotExist, deviceId);
                        }
                    }
                    else
                    {
                        if (identity != null && identity.TenantId.HasValue && identity is IModelTenantId tenantId && tenantId.IsTenant)
                        {
                            notBoundIds.Add((deviceId, identity.TenantId.Value));
                        }
                    }
                }
                foreach (var item in notBoundIds)
                {
                    //绑定设备到本租户下
                    await deviceService.BindTenant(new DeviceBindTenantInput()
                    {
                        DeviceId = item.Item1,
                        TenantId = item.Item2
                    });
                }
                config.Name = input.Name;
                config.DeviceIds = string.Join(",", input.DeviceIds.Select(x => x.ToString()));
                config.Contacts = input.Contacts;
                config.Tel = input.Tel;
                config.Address = input.Address;
                config.Description = input.Description;
                await _repository.UpdateAsync(config.Adapt<WeighbridgeConfig>());
            }
            else
            {
                //添加
                if (await _repository.AsQueryable(false).AnyAsync(x => x.Name.Equals(input.Name)))
                {
                    throw Oops.Bah(WeighbridgeLocalResource.ConfigNameRepeat);
                }

                List<(Guid, Guid)> notBoundIds = new List<(Guid, Guid)>();
                foreach (var deviceId in input.DeviceIds)
                {
                    var device = await deviceService.FindGlobal(deviceId);

                    if (device == null)
                    {
                        throw Oops.Bah(WeighbridgeLocalResource.DeviceDoesNotExist, deviceId);
                    }
                    else if (device.TenantId != null)
                    {
                        //租户只能绑定自己的
                        if (identity != null && identity.TenantId.HasValue && identity is IModelTenantId tenantId && tenantId.IsTenant && !device.TenantId.Equals(tenantId.TenantId))
                        {
                            throw Oops.Bah(WeighbridgeLocalResource.DeviceDoesNotExist, deviceId);
                        }
                    }
                    else
                    {
                        if (identity != null && identity.TenantId.HasValue && identity is IModelTenantId tenantId && tenantId.IsTenant)
                        {
                            notBoundIds.Add((deviceId, identity.TenantId.Value));
                        }
                    }
                }
                foreach (var item in notBoundIds)
                {
                    //绑定设备到本租户下
                    await deviceService.BindTenant(new DeviceBindTenantInput()
                    {
                        DeviceId = item.Item1,
                        TenantId = item.Item2
                    });
                }
                await _repository.InsertAsync(new WeighbridgeConfig()
                {
                    Name = input.Name,
                    DeviceIds = string.Join(",", input.DeviceIds.Select(x => x.ToString())),
                    Contacts = input.Contacts,
                    Tel = input.Tel,
                    Address = input.Address,
                    Description = input.Description
                });
            }

            return true;
        }

    }
}