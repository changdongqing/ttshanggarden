// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Impl.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceDataHandleEventStoreSubscriber : IEventSubscriber
    {

        private readonly IDeviceDataStoreService deviceDataStoreService;
        private readonly IEventBus eventBus;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceDataStoreService"></param>
        /// <param name="eventBus"></param>
        public DeviceDataHandleEventStoreSubscriber(IDeviceDataStoreService deviceDataStoreService, IEventBus eventBus)
        {
            this.deviceDataStoreService = deviceDataStoreService;
            this.eventBus = eventBus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe($"{nameof(EventGroup.SystemEvent)}Gardener.Iot.Dtos.{nameof(DeviceDataHandleEvent)}")]
        public async Task InsertDb(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            DeviceDataHandleEvent deviceDataHandleEvent = (DeviceDataHandleEvent)eventSource.Payload;
            //入库
            DeviceDataDto deviceData = await deviceDataStoreService.SaveDeviceData(deviceDataHandleEvent.DeviceClientId, deviceDataHandleEvent.DeviceConnectionType, deviceDataHandleEvent.ContentType, deviceDataHandleEvent.Content, deviceDataHandleEvent.UserProperties, deviceDataHandleEvent.DeviceConnection, deviceDataHandleEvent.Device, deviceDataHandleEvent.ExtendData);
            //服务端事件
            await eventBus.PublishAsync(new DeviceDataSaveAfterNotificationData(deviceData));
        }
    }
}
