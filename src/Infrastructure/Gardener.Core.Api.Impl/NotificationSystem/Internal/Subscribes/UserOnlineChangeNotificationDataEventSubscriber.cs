// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using Gardener.Core.EventBus;
using Gardener.Core.NotificationSystem;

namespace Gardener.Core.Api.Impl.NotificationSystem.Internal.Subscribes
{
    /// <summary>
    /// 用户在线状态变化通知事件
    /// </summary>
    public class UserOnlineChangeNotificationDataEventSubscriber : IEventSubscriber
    {
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 用户在线状态变化通知事件
        /// </summary>
        /// <param name="systemNotificationService"></param>
        public UserOnlineChangeNotificationDataEventSubscriber(ISystemNotificationService systemNotificationService)
        {
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 用户在线状态变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventGroup.SystemNotify) + "Gardener.Core.NotificationSystem." + nameof(UserOnlineChangeNotificationData))]
        public async Task Chat(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            UserOnlineChangeNotificationData chatNotification = (UserOnlineChangeNotificationData)eventSource.Payload;
            //收到消息，转发给所有客户端
            await systemNotificationService.SendToAllClient(chatNotification);
        }
    }
}
