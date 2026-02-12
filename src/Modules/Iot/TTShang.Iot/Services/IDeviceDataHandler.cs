// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Buffers;

namespace TTShang.Iot.Services
{
    /// <summary>
    /// 设备数据处理器
    /// </summary>
    public interface IDeviceDataHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="clientId">设备客户端编号</param>
        /// <param name="deviceConnectionType">设备连接类型</param>
        /// <param name="contentType">数据内容类型</param>
        /// <param name="content">数据内容</param>
        /// <param name="receivedTime">接收到的时间</param>
        /// <param name="userProperties">用户数据</param>
        /// <param name="deviceConnection">设备连接信息</param>
        /// <param name="device">设备信息</param>
        /// <param name="extendData">扩展数据</param>
        /// <returns></returns>
        Task Handler(string clientId, DeviceConnectionType deviceConnectionType, DeviceDataContentType? contentType, ReadOnlySequence<byte>? content, DateTimeOffset receivedTime, IEnumerable<KeyValuePair<string, string>>? userProperties = null, DeviceConnectionDto? deviceConnection = null, DeviceDto? device = null, IDictionary<string, object>? extendData = null);
    }
}
