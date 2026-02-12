// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Tools
{
    /// <summary>
    /// 设备链接工具
    /// </summary>
    public interface IDeviceConnectionTool
    {
        /// <summary>
        /// 获取连接中的连接信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        Task<DeviceConnectionDto?> GetConnectingDeviceConnection(string clientId, bool useCache = true);

        /// <summary>
        /// 获取连接中的连接信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        Task<DeviceConnectionDto?> GetConnectingDeviceConnection(Guid deviceId, bool useCache = true);

        /// <summary>
        /// 更新指定字段
        /// </summary>
        /// <param name="input"></param>
        /// <param name="clearCache"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<bool> UpdateIncludeNow(DeviceConnectionDto input, bool clearCache, string[] propertyNames);

        /// <summary>
        /// 获取超时的连接
        /// </summary>
        /// <returns></returns>
        Task<List<DeviceConnectionDto>> GetTimeoutConnections();

        /// <summary>
        /// 添加系统日志
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="logType"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task AddSystemLog(DeviceConnectionDto connection, DeviceSystemLogType logType, string? content);

        /// <summary>
        /// 清空设备连接缓存
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        Task TryDeviceConnectionRemoveCache(DeviceConnectionDto? connection);

        /// <summary>
        /// 断开超时连接
        /// </summary>
        /// <param name="deviceConnection"></param>
        /// <returns></returns>
        Task DisconnectTimeoutConnection(DeviceConnectionDto deviceConnection);
    }
}
