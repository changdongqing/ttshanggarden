using Furion.EventBus;
using TTShang.Core.EventBus;
using TTShang.Iot.Dtos;
using TTShang.Weighbridge.Impl.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Weighbridge.Impl.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceDataSaveAfterNotificationSubscriber : IEventSubscriber
    {

        private readonly IServiceScopeFactory scopeFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scopeFactory"></param>
        public DeviceDataSaveAfterNotificationSubscriber(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe($"{nameof(EventGroup.SystemNotify)}Gardener.Iot.Dtos.{nameof(DeviceDataSaveAfterNotificationData)}")]
        public async Task Handle(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            DeviceDataSaveAfterNotificationData deviceData = (DeviceDataSaveAfterNotificationData)eventSource.Payload;
            using var scope= scopeFactory.CreateScope();
            WeighbridgeDeviceService service = scope.ServiceProvider.GetRequiredService<WeighbridgeDeviceService>();
            await service.NotifyToClient(deviceData);
        }
    }
}
