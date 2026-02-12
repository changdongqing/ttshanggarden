namespace Gardener.Iot.Impl.Services
{
    /// <summary>
    /// 设备数据服务
    /// </summary>
    [ApiDescriptionSettings("Iot", Module = "iot")]
    public class DeviceDataService : IDeviceDataService
    {
        private readonly IDeviceDataStoreService deviceDataStoreService;
        /// <summary>
        /// 设备数据服务
        /// </summary>
        /// <param name="deviceDataStoreService"></param>
        public DeviceDataService(IDeviceDataStoreService deviceDataStoreService)
        {
            this.deviceDataStoreService = deviceDataStoreService;
        }
        /// <summary>
        /// 分页获取设备数据
        /// </summary>
        /// <remarks>
        /// 分页获取设备数据
        /// </remarks>
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
        public Task<PageList<DeviceDataDto>> GetDeviceDatas(string clientId, int pageIndex = 1, int pageSize = 10, DeviceConnectionType? deviceConnectionType = null, string? contentType = null, Guid? deviceId = null, long? deviceConnectionId = null, DateTimeOffset? createTimeStart = null, DateTimeOffset? createTimeEnd = null)
        {
            return deviceDataStoreService.GetDeviceDatas(clientId, pageIndex, pageSize, deviceConnectionType, contentType, deviceId, deviceConnectionId, createTimeStart, createTimeEnd);
        }
        /// <summary>
        /// 获取最后一条设备数据
        /// </summary>
        /// <remarks>
        /// 根据设备的客户端编号获取最后一条数据
        /// </remarks>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Task<DeviceDataDto?> GetLastDeviceData(string clientId)
        {
            return deviceDataStoreService.GetLastDeviceData(clientId);
        }
        /// <summary>
        /// 获取最后一条设备数据
        /// </summary>
        /// <remarks>
        /// 根据设备编号和设备连接编号，获取最后一条设备或连接数据
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <param name="deviceConnectionId"></param>
        /// <returns></returns>
        public Task<DeviceDataDto?> GetLastDeviceData(Guid deviceId, long? deviceConnectionId = null)
        {
            return deviceDataStoreService.GetLastDeviceData(deviceId, deviceConnectionId);
        }
    }
}
