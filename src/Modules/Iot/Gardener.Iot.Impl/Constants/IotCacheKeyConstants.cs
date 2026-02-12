// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Iot.Impl.Constants
{
    /// <summary>
    /// iot缓存键
    /// </summary>
    public static class IotCacheKeyConstants
    {
        /// <summary>
        /// 获取设备缓存key
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static string GetDeviceCacheKey(string clientId)
        {
            return $"iot:Device:clientId:{clientId}";
        }
        /// <summary>
        /// 获取设备缓存key
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static string GetDeviceCacheKey(Guid deviceId)
        {
            return $"iot:Device:deviceId:{deviceId}";
        }
        /// <summary>
        /// 获取连接中客户端连接缓存key
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static string GetConnectingDeviceConnectionCacheKey(string clientId)
        {
            return $"iot:DeviceConnection:Connecting:ClientId:{clientId}";
        }
        /// <summary>
        /// 获取连接中客户端连接缓存key
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static string GetConnectingDeviceConnectionCacheKey(Guid deviceId)
        {
            return $"iot:DeviceConnection:Connecting:DeviceId:{deviceId}";
        }
    }
}
