// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Furion.FriendlyException;
using TTShang.Core.Authorization.Services;
using TTShang.Core.Dtos.Constraints;
using TTShang.Core.Resources;
using TTShang.Core.Util.Extensions;
using TTShang.Iot.Tools;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;

namespace TTShang.Iot.Impl.Services
{
    /// <summary>
    /// 设备服务
    /// </summary>
    [ApiDescriptionSettings("Iot", Module = "iot")]
    public class DeviceService : ServiceBase<Device, DeviceDto, Guid, GardenerMultiTenantDbContextLocator>, IDeviceService
    {
        private readonly ICache cache;
        private readonly IDeviceConnectionTool deviceConnectionTool;
        private readonly IServiceProvider serviceProvider;
        private readonly IRepository<Device, MasterDbContextLocator> notMultiTenantRepository;
        private readonly IIdentityService identityService;
        private readonly IProductService productService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cache"></param>
        /// <param name="deviceConnectionTool"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="notMultiTenantRepository"></param>
        /// <param name="identityService"></param>
        /// <param name="productService"></param>
        public DeviceService(IRepository<Device, GardenerMultiTenantDbContextLocator> repository, ICache cache, IDeviceConnectionTool deviceConnectionTool, IServiceProvider serviceProvider, IRepository<Device, MasterDbContextLocator> notMultiTenantRepository, IIdentityService identityService, IProductService productService) : base(repository)
        {
            this.cache = cache;
            this.deviceConnectionTool = deviceConnectionTool;
            this.serviceProvider = serviceProvider;
            this.notMultiTenantRepository = notMultiTenantRepository;
            this.identityService = identityService;
            this.productService = productService;
        }
        /// <summary>
        /// 验证设备的密钥正确性
        /// </summary>
        /// <param name="deviceId">设备编号</param>
        /// <param name="account">账户</param>
        /// <param name="secretKey">密钥</param>
        /// <returns></returns>
        public async Task<bool> VerifySecretKey(Guid deviceId, string account, string secretKey)
        {
            var device = await _repository.FindAsync(deviceId);
            if (device == null)
            {
                return false;
            }
            return string.Equals(account, device.Account) && string.Equals(secretKey, device.SecretKey);
        }

        /// <summary>
        /// 根据ClientId查询设备，不存在返回null
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<DeviceDto?> TryGetByClientId(string clientId)
        {
            DeviceDto? deviceDto = await cache.GetAsync(IotCacheKeyConstants.GetDeviceCacheKey(clientId), async () =>
            {
                Device? entity = await _repository.Where(x => x.ClientId.Equals(clientId)).FirstOrDefaultAsync();
                return entity?.Adapt<DeviceDto>();
            }, TimeSpan.FromMinutes(30));

            return deviceDto;
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
        public override async Task<DeviceDto?> Find(Guid id)
        {
            DeviceDto? deviceDto = await FindOnlyMainData(id);
            if (deviceDto != null)
            {
                deviceDto.ConnectingDeviceConnection = await deviceConnectionTool.GetConnectingDeviceConnection(deviceDto.ClientId);
            }
            return deviceDto;
        }
        /// <summary>
        /// 仅仅获取主体数据
        /// </summary>
        /// <remarks>
        /// 这里仅获取Device主题数据并进行缓存，如果是租户查询且会过滤租户数据权限
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<DeviceDto?> FindOnlyMainData(Guid id)
        {
            DeviceDto? deviceDto = await cache.GetAsync(IotCacheKeyConstants.GetDeviceCacheKey(id), () =>
            {
                return base.Find(id);
            }, TimeSpan.FromMinutes(30));

            if(deviceDto==null)
            {
                return null;
            }
            var identity = identityService.GetIdentity();
            //根据当前身份租户信息过滤
            if (identity != null && identity.TenantId.HasValue && identity is IModelTenantId tenantId && tenantId.TenantId.HasValue && tenantId.IsTenant)
            {
                //租户编号与设备中租户编号不一致不能返回
                if(!tenantId.TenantId.Value.Equals(deviceDto.TenantId))
                {
                    return null;
                }
            }
            return deviceDto;
        }

        /// <summary>
        /// 获取设备的产品
        /// </summary>
        /// <remarks>
        /// 产品数据一般不会变化，这里缓存时间较久（2小时）
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<ProductDto?> FindDeviceProduct(Guid deviceId)
        {
            ProductDto? product = await cache.GetAsync("wbg:device:product:" + deviceId, async () =>
            {
                var device = await FindOnlyMainData(deviceId);
                if (device == null || !device.ProductId.HasValue)
                {
                    return null;
                }
                var product = await productService.Find(device.ProductId.Value);
                if (product == null)
                {
                    return null;
                }
                return product;
            }, TimeSpan.FromHours(2));
            return product;
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<PageList<DeviceDto>> Search(PageRequest request)
        {
            IQueryable<Device> queryable = GetSearchQueryable(request.FilterGroups);

            PageList<DeviceDto> devicePage = await queryable
                .Include(x => x.Product)
                .Include(x => x.DeviceGroup)
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<DeviceDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);

            if (devicePage.Items.Any())
            {
                foreach (var item in devicePage.Items)
                {
                    item.ConnectingDeviceConnection = await deviceConnectionTool.GetConnectingDeviceConnection(item.ClientId);
                }
            }
            return devicePage;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <remarks>
        /// 更新单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(DeviceDto input)
        {
            var result = await base.Update(input);
            if (result)
            {
                await TryRemoveDeviceCache(input.Id);
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
        public override async Task<bool> Delete(Guid id)
        {

            var result = await base.Delete(id);
            if (result)
            {
                await TryRemoveDeviceCache(id);
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
        public override async Task<bool> Deletes([FromBody] Guid[] ids)
        {
            var result = await base.Deletes(ids);
            foreach (var id in ids)
            {
                await TryRemoveDeviceCache(id);
            }
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键逻辑删除
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public override async Task<bool> FakeDelete(Guid id)
        {

            var result = await base.FakeDelete(id);
            if (result)
            {
                await TryRemoveDeviceCache(id);
            }
            return result;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量逻辑删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "批量删除", Description = "根据多个主键批量逻辑删除")]
        public override async Task<bool> FakeDeletes([FromBody] Guid[] ids)
        {
            var result = await base.FakeDeletes(ids);
            foreach (var id in ids)
            {
                await TryRemoveDeviceCache(id);
            }
            return result;
        }
        /// <summary>
        /// 锁定
        /// </summary>
        /// <remarks>
        /// 根据主键锁定或解锁数据（必须实现<see cref="IModelLocked"/>才能生效）
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="isLocked"></param>
        /// <returns></returns>
        [HttpPut]
        public override async Task<bool> Lock([ApiSeat(ApiSeats.ActionStart)] Guid id, bool isLocked = true)
        {
            var result = await base.Lock(id, isLocked);
            if (result)
            {
                await TryRemoveDeviceCache(id);
            }
            return result;
        }
        /// <summary>
        /// 尝试移除设备缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task TryRemoveDeviceCache(Guid id)
        {
            await cache.RemoveAsync(IotCacheKeyConstants.GetDeviceCacheKey(id));
            var entity = await _repository.AsQueryable(false).FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity != null)
            {
                await cache.RemoveAsync(IotCacheKeyConstants.GetDeviceCacheKey(entity.ClientId));
            }
        }
        /// <summary>
        /// 向设备发送消息
        /// </summary>
        /// <remarks>
        /// 向某个设备发送信息
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> SendMessage(SendDataInput input)
        {
            //检查链接
            var connection = await deviceConnectionTool.GetConnectingDeviceConnection(input.ClientId);
            if (connection == null)
            {
                return false;
            }
            IDeviceCommunicationControlService? communicationControlService = serviceProvider.GetKeyedService<IDeviceCommunicationControlService>(connection.DeviceConnectionType);
            if (communicationControlService == null)
            {
                return false;
            }
            DeviceDataContentType? contentType = input.ContentType == null ? null : new DeviceDataContentType(input.ContentType);
            byte[] bytes = input.IsHex ? input.Content.HexToByte() : Encoding.UTF8.GetBytes(input.Content);
            var result = await communicationControlService.SendMesaageToClient(connection.DeviceClientId, bytes, contentType);
            return result;
        }
        /// <summary>
        /// 向设备发送消息
        /// </summary>
        /// <remarks>
        /// 向某个设备发送信息
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<bool> SendMessage(Guid deviceId, byte[] content, DeviceDataContentType? contentType = null)
        {
            var device = await FindOnlyMainData(deviceId);
            if (device == null)
            {
                return false;
            }
            //检查链接
            var connection = await deviceConnectionTool.GetConnectingDeviceConnection(deviceId);
            if (connection == null)
            {
                return false;
            }
            IDeviceCommunicationControlService? communicationControlService = serviceProvider.GetKeyedService<IDeviceCommunicationControlService>(connection.DeviceConnectionType);
            if (communicationControlService == null)
            {
                return false;
            }
            var result = await communicationControlService.SendMesaageToClient(connection.DeviceClientId, content, contentType);
            return result;
        }
        /// <summary>
        /// 向设备发送消息
        /// </summary>
        /// <remarks>
        /// 向某个设备发送信息
        /// </remarks>
        /// <param name="clientId"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<bool> SendMessage(string clientId, byte[] content, DeviceDataContentType? contentType = null)
        {
            //检查链接
            var connection = await deviceConnectionTool.GetConnectingDeviceConnection(clientId);
            if (connection == null)
            {
                return false;
            }
            IDeviceCommunicationControlService? communicationControlService = serviceProvider.GetKeyedService<IDeviceCommunicationControlService>(connection.DeviceConnectionType);
            if (communicationControlService == null)
            {
                return false;
            }
            var result = await communicationControlService.SendMesaageToClient(connection.DeviceClientId, content, contentType);
            return result;
        }

        /// <summary>
        /// 向设备发送消息
        /// </summary>
        /// <remarks>
        /// 向某个设备发送信息
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <param name="contents"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<bool> SendMessage(Guid deviceId, IEnumerable<byte[]> contents, DeviceDataContentType? contentType = null)
        {
            var device = await FindOnlyMainData(deviceId);
            if (device == null)
            {
                return false;
            }
            foreach (var content in contents)
            {
                var result = await SendMessage(device.ClientId, content, contentType);
                if (!result)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断是否存在(判断全局)
        /// </summary>
        /// <remarks>
        /// 判断是否存在，根据输入条件组合进行数据查询判断是否存在
        /// </remarks>
        /// <param name="filterGroups"></param>
        /// <returns></returns>
        [HttpPost]
        public override Task<bool> Exists(List<FilterGroup> filterGroups)
        {
            IQueryable<Device> queryable = GetSearchQueryable(notMultiTenantRepository, filterGroups, true);
            return queryable.AnyAsync();
        }
        /// <summary>
        /// 查询-根据设备编号查找全局
        /// </summary>
        /// <remarks>
        /// 查询-根据设备编号查找全局,未找到返回null
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<DeviceDto?> FindGlobal(Guid deviceId)
        {
            var device = await notMultiTenantRepository.FindAsync(deviceId);
            return device?.Adapt<DeviceDto>();
        }
        /// <summary>
        /// 绑定租户
        /// </summary>
        /// <remarks>
        /// 将设备绑定给租户
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> BindTenant(DeviceBindTenantInput input)
        {
            var device = await notMultiTenantRepository.AsQueryable(false).Where(x => x.Id.Equals(input.DeviceId)).FirstOrDefaultAsync();
            if (device == null)
            {
                throw Oops.Bah(string.Format(SharedLocalResource.Item_Data_Not_Find, IotLocalResource.Device));
            }
            if (device.TenantId.HasValue)
            {
                throw Oops.Bah(string.Format(IotLocalResource.DeviceHasBeenBound, input.DeviceId));
            }
            var identity = identityService.GetIdentity();
            //租户只能绑定自己
            if (identity != null && identity.TenantId.HasValue && identity is IModelTenantId tenantId && tenantId.IsTenant)
            {
                input.TenantId = identity.TenantId.Value;
            }
            device.TenantId = input.TenantId;
            var result = await notMultiTenantRepository.UpdateIncludeAsync(device, [nameof(DeviceDto.TenantId)]);
            return true;
        }

        /// <summary>
        /// 更新设备别名
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 更新设备别名
        /// </remarks>
        /// <returns></returns>
        public async Task<bool> UpdateDeviceAlias(UpdateDeviceAliasInput input)
        {
            var device = await _repository.AsQueryable(false).Where(x => x.Id.Equals(input.DeviceId)).FirstOrDefaultAsync();
            if (device == null)
            {
                throw Oops.Bah(string.Format(SharedLocalResource.Item_Data_Not_Find, IotLocalResource.Device));
            }
            device.Alias = input.Alias;
            await _repository.UpdateIncludeAsync(device, [nameof(DeviceDto.Alias)]);
            await TryRemoveDeviceCache(input.DeviceId);
            return true;
        }
    }
}
