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
    /// 远程服务调用通知事件
    /// </summary>
    public class RemoteServiceCallNotificationDataEventSubscriber : IEventSubscriber
    {
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 远程服务调用通知事件
        /// </summary>
        /// <param name="systemNotificationService"></param>
        public RemoteServiceCallNotificationDataEventSubscriber(ISystemNotificationService systemNotificationService)
        {
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 远程服务调用
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventGroup.SystemNotify) + "Gardener.Core.NotificationSystem." + nameof(RemoteServiceCallNotificationData))]
        public async Task Call(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            RemoteServiceCallNotificationData notification = (RemoteServiceCallNotificationData)eventSource.Payload;
            //收到消息，转发给对应客户端
            await systemNotificationService.SendToClient(notification.ToConnectionId, notification);
        }
    }
}
