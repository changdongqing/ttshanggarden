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
    /// 当订阅事件
    /// </summary>
    [ScopedService]
    public class EventBusSubscribeEventSubscriber : EventSubscriberBase<EventBusSubscribeEvent>
    {
        private readonly ISystemNotificationSender systemNotificationSender;

        public EventBusSubscribeEventSubscriber(ISystemNotificationSender systemNotificationSender)
        {
            this.systemNotificationSender = systemNotificationSender;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override Task CallBack(EventBusSubscribeEvent e)
        {
            ISubscriber subscriber = e.Subscriber;
            return systemNotificationSender.DynamicSubscribe(subscriber, true);
        }
    }
}
