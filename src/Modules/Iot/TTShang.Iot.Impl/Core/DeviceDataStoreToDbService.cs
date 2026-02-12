// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Iot.Impl.Core
{
    /// <summary>
    /// 设备数据存储服务-存储到数据库
    /// </summary>
    public class DeviceDataStoreToDbService : IDeviceDataStoreService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ICache cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="cache"></param>
        public DeviceDataStoreToDbService(IServiceProvider serviceProvider, ICache cache)
        {
            this.serviceProvider = serviceProvider;
            this.cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private string GetLastDeviceDataClientIdKey(string clientId)
        {
            return $"iot:LastDeviceDataFromClientId:{clientId}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="deviceConnectionId"></param>
        /// <returns></returns>
        private string GetLastDeviceDataDeviceIdKey(Guid deviceId, long? deviceConnectionId = null)
        {
            if (deviceConnectionId == null)
            {
                return $"iot:LastDeviceDataFromDeviceId:{deviceId}";
            }
            return $"iot:LastDeviceDataFromDeviceIdAndConnectionId:{deviceId}:{deviceConnectionId}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceDataLog"></param>
        /// <returns></returns>
        private Task SetLastDeviceDataCache(DeviceData deviceDataLog)
        {
            string clientIdKey = GetLastDeviceDataClientIdKey(deviceDataLog.DeviceClientId);
            List<Task> tasks = new List<Task> { cache.SetAsync(clientIdKey, deviceDataLog) };
            if (deviceDataLog.DeviceId.HasValue)
            {
                string deviceIdKey = GetLastDeviceDataDeviceIdKey(deviceDataLog.DeviceId.Value);
                tasks.Add(cache.SetAsync(deviceIdKey, deviceDataLog));
                if (deviceDataLog.DeviceConnectionId.HasValue)
                {
                    string deviceIdAndDeviceConnectionIdKey = GetLastDeviceDataDeviceIdKey(deviceDataLog.DeviceId.Value, deviceDataLog.DeviceConnectionId);
                    tasks.Add(cache.SetAsync(deviceIdAndDeviceConnectionIdKey, deviceDataLog));
                }
            }
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="contentType"></param>
        /// <param name="content"></param>
        /// <param name="userProperties"></param>
        /// <param name="deviceConnection"></param>
        /// <param name="device"></param>
        /// <param name="extendData"></param>
        /// <returns></returns>
        public async Task<DeviceDataDto> SaveDeviceData(string clientId, DeviceConnectionType deviceConnectionType, DeviceDataContentType? contentType, byte[]? content, IEnumerable<KeyValuePair<string, string>>? userProperties = null, DeviceConnectionDto? deviceConnection = null, DeviceDto? device = null, IDictionary<string, object>? extendData = null)
        {
            //设备数据未指定内容类型，使用设备中指定的
            if (contentType == null && device != null && device.InputContentType != null)
            {
                contentType=new DeviceDataContentType(device.InputContentType);
            }

            var data = new DeviceData()
            {
                DeviceClientId = clientId,
                DeviceId = device?.Id,
                DeviceConnectionId = deviceConnection?.Id,
                ContentType = contentType.ToString() ?? DeviceDataContentType.Unknown.ToString(),
                DeviceConnectionType = deviceConnectionType,
                TenantId = device?.TenantId,
                OriginalContent = content
            };
            if (extendData != null)
            {
                data.ExtendData = JsonSerializer.Serialize(extendData);
            }
            //base64 字符串
            if (content != null && content.Length > 0)
            {
                data.Content = Convert.ToBase64String(content);
            }
            //存储实时数据到缓存
            await SetLastDeviceDataCache(data);
            //存储历史
            if (device != null && device.StorageHistoryData == true)
            {
                using var scope = serviceProvider.CreateScope();
                IRepository<DeviceData, GardenerMultiTenantDbContextLocator> repository = scope.ServiceProvider.GetRequiredService<IRepository<DeviceData, GardenerMultiTenantDbContextLocator>>();
                var result = await repository.InsertNowAsync(data);
                data = result.Entity;
            }
            return data.Adapt<DeviceDataDto>();
        }

        /// <summary>
        /// 获取最后一条设备数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<DeviceDataDto?> GetLastDeviceData(string clientId)
        {
            var data = await cache.GetAsync<DeviceData>(GetLastDeviceDataClientIdKey(clientId));
            return data?.Adapt<DeviceDataDto>();
        }

        /// <summary>
        /// 获取最后一条设备数据
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="deviceConnectionId"></param>
        /// <returns></returns>
        public async Task<DeviceDataDto?> GetLastDeviceData(Guid deviceId, long? deviceConnectionId = null)
        {
            var data = await cache.GetAsync<DeviceData>(GetLastDeviceDataDeviceIdKey(deviceId, deviceConnectionId));
            return data?.Adapt<DeviceDataDto>();
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="contentType"></param>
        /// <param name="deviceId"></param>
        /// <param name="deviceConnectionId"></param>
        /// <param name="createTimeStart"></param>
        /// <param name="createTimeEnd"></param>
        /// <returns></returns>
        public async Task<PageList<DeviceDataDto>> GetDeviceDatas(string clientId,
            int pageIndex = 1,
            int pageSize = 10,
            DeviceConnectionType? deviceConnectionType = null,
            string? contentType = null,
            Guid? deviceId = null,
            long? deviceConnectionId = null,
            DateTimeOffset? createTimeStart = null,
            DateTimeOffset? createTimeEnd = null)
        {
            using var scope = serviceProvider.CreateScope();
            IRepository<DeviceData, GardenerMultiTenantDbContextLocator> repository = scope.ServiceProvider.GetRequiredService<IRepository<DeviceData, GardenerMultiTenantDbContextLocator>>();
            var queryable = repository.AsQueryable(false);
            queryable.Where(x => x.DeviceClientId.Equals(clientId));
            queryable.Where(deviceConnectionType.HasValue, x => x.DeviceConnectionType.Equals(deviceConnectionType));
            queryable.Where(!string.IsNullOrEmpty(contentType), x => x.ContentType.Equals(contentType));
            queryable.Where(deviceId.HasValue, x => x.DeviceId.Equals(deviceId));
            queryable.Where(deviceConnectionId.HasValue, x => x.DeviceConnectionId.Equals(deviceConnectionId));
            queryable.Where(createTimeStart.HasValue, x => x.CreatedTime >= createTimeStart);
            queryable.Where(createTimeEnd.HasValue, x => x.CreatedTime <= createTimeEnd);
            var result = await queryable.ToPageAsync(pageIndex, pageSize);
            return result.Adapt<PageList<DeviceDataDto>>();
        }
    }
}
