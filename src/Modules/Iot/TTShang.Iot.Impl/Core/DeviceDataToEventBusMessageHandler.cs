// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Buffers;

namespace TTShang.Iot.Impl.Core
{
    /// <summary>
    /// 将设备数据发送到事件中心
    /// </summary>
    /// <remarks>
    /// 可以实现自己的处理方式。例如：发送到kafaka
    /// </remarks>
    public class DeviceDataToEventBusMessageHandler : IDeviceDataHandler
    {

        private readonly IEventBus eventBus;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventBus"></param>
        public DeviceDataToEventBusMessageHandler(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="deviceConnectionType"></param>
        /// <param name="contentType"></param>
        /// <param name="content"></param>
        /// <param name="receivedTime"></param>
        /// <param name="userProperties"></param>
        /// <param name="deviceConnection"></param>
        /// <param name="device"></param>
        /// <param name="extendData"></param>
        /// <returns></returns>
        public Task Handler(string clientId, DeviceConnectionType deviceConnectionType, DeviceDataContentType? contentType, ReadOnlySequence<byte>? content, DateTimeOffset receivedTime, IEnumerable<KeyValuePair<string, string>>? userProperties = null, DeviceConnectionDto? deviceConnection = null, DeviceDto? device = null, IDictionary<string, object>? extendData = null)
        {
            return eventBus.PublishAsync(new DeviceDataHandleEvent(clientId, deviceConnectionType, receivedTime)
            {
                UserProperties = userProperties,
                DeviceConnection = deviceConnection,
                Device = device,
                Content = content?.ToArray(),
                ExtendData = extendData,
                ContentType = contentType
            });
        }
    }
}
