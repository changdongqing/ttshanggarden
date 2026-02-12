// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.NotificationSystem;
using TTShang.Core.EventBus.Events;

namespace TTShang.Core.Client.Impl.NotificationSystem.Subscribes
{
    /// <summary>
    /// 当取消订阅事件
    /// </summary>
    [ScopedService]
    public class EventBusUnSubscribeEventSubscriber : EventSubscriberBase<EventBusUnSubscribeEvent>
    {
        private readonly ISystemNotificationSender systemNotificationSender;

        public EventBusUnSubscribeEventSubscriber(ISystemNotificationSender systemNotificationSender)
        {
            this.systemNotificationSender = systemNotificationSender;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override Task CallBack(EventBusUnSubscribeEvent e)
        {
            ISubscriber subscriber = e.Subscriber;
            return systemNotificationSender.DynamicSubscribe(subscriber, false);
        }
    }
}
