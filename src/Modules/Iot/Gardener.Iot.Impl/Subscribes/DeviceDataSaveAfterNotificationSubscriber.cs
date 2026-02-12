using Gardener.Core.NotificationSystem;

namespace Gardener.Iot.Impl.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceDataSaveAfterNotificationSubscriber : IEventSubscriber
    {

        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemNotificationService"></param>
        public DeviceDataSaveAfterNotificationSubscriber(ISystemNotificationService systemNotificationService)
        {
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe($"{nameof(EventGroup.SystemNotify)}Gardener.Iot.Dtos.{nameof(DeviceDataSaveAfterNotificationData)}")]
        public async Task Handle(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;

            DeviceDataSaveAfterNotificationData deviceDataHandleEvent = (DeviceDataSaveAfterNotificationData)eventSource.Payload;
            //入库
            //发送通知给动态订阅者
            await systemNotificationService.SendToDynamicSubscriber(deviceDataHandleEvent);

        }
    }
}
