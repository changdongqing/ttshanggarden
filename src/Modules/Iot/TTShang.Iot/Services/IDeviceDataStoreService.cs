// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Services
{
    /// <summary>
    /// 设备数据存储服务
    /// </summary>
    public interface IDeviceDataStoreService : IDeviceDataService
    {
        /// <summary>
        /// 保存设备数据
        /// </summary>
        /// <param name="clientId">设备客户端编号</param>
        /// <param name="deviceConnectionType">设备连接类型</param>
        /// <param name="contentType">数据内容类型</param>
        /// <param name="content">数据内容</param>
        /// <param name="userProperties">用户数据</param>
        /// <param name="deviceConnection">设备连接</param>
        /// <param name="device">设备</param>
        /// <param name="extendData">扩展数据</param>
        /// <returns></returns>
        Task<DeviceDataDto> SaveDeviceData(string clientId, DeviceConnectionType deviceConnectionType, DeviceDataContentType? contentType, byte[]? content, IEnumerable<KeyValuePair<string, string>>? userProperties = null, DeviceConnectionDto? deviceConnection = null, DeviceDto? device = null, IDictionary<string, object>? extendData = null);
    }
}
