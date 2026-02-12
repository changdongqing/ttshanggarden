// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using TTShang.Core.EventBus;
using TTShang.Core.NotificationSystem;

namespace TTShang.Core.Api.Impl.NotificationSystem.Internal.Subscribes
{
    /// <summary>
    /// 动态订阅通知事件
    /// </summary>
    public class DynamicSubscribeNotificationDataEventSubscriber : IEventSubscriber
    {
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 动态订阅通知事件
        /// </summary>
        /// <param name="systemNotificationService"></param>
        public DynamicSubscribeNotificationDataEventSubscriber(ISystemNotificationService systemNotificationService)
        {
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 动态订阅通知事件
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventGroup.SystemNotify) + "Gardener.Core.NotificationSystem." + nameof(DynamicSubscribeNotificationData))]
        public async Task DynamicSubscribe(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            DynamicSubscribeNotificationData data = (DynamicSubscribeNotificationData)eventSource.Payload;
            //收到消息，转发给所有客户端
            await systemNotificationService.DynamicSubscribe(data.SubscribeEventGroup, data.SubscribeEventType, data.Subscribe, data.ConnectionId, data.SubscribeEventKeys);
        }
    }
}
