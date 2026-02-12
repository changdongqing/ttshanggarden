// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Impl.NotificationSystem.Constants;
using Gardener.Core.Client.NotificationSystem;
using Gardener.Core.Client.SignalR;
using Gardener.Core.NotificationSystem;

namespace Gardener.Core.Client.Impl.NotificationSystem
{
    [ScopedService]
    public class SystemNotificationSender : ISystemNotificationSender
    {
        private readonly ISignalRClientManager signalRClientManager;

        private readonly IEventBus _eventBus;
        Timer? timer = null;

        public SystemNotificationSender(ISignalRClientManager signalRClientManager, IEventBus eventBus)
        {
            this.signalRClientManager = signalRClientManager;
            _eventBus = eventBus;
            timer = new Timer(DynamicSubscribe, true, 0, 5000);
        }
        /// <summary>
        /// 动态通知订阅
        /// </summary>
        /// <param name="x"></param>
        private async void DynamicSubscribe(object? x)
        {
            var list = _eventBus.GetSubscribers();
            if (list == null)
            {
                return;
            }
            foreach (ISubscriber subscriber in list)
            {
                await DynamicSubscribe(subscriber, true);
            }
        }

        /// <summary>
        /// 动态订阅操作
        /// </summary>
        /// <param name="subscriber">订阅者</param>
        /// <param name="subscribe">订阅/取消订阅</param>
        /// <returns></returns>
        public Task DynamicSubscribe(ISubscriber subscriber, bool subscribe)
        {
            //判断是否是动态通知订阅者
            Type type = subscriber.GetType();

            if (type.IsGenericType && type.GetGenericArguments()[0].IsAssignableTo(typeof(DynamicNotificationData)))
            {
                return SendDynamicSubscribe(subscriber.EventGroup, subscriber.EventType, subscribe, subscriber.EventKeys);
            }
            return Task.CompletedTask;
        }

        public ISignalRClient GetSignalRClient()
        {
            ISignalRClient signalRClient = signalRClientManager.Get(NotificationSystemSignalRClientNames.SystemNotificationSignalRClientName);
            return signalRClient;
        }

        public Task Send(NotificationData notificationData)
        {
            return GetSignalRClient().SendAsync("Send", notificationData);
        }

        public Task SendDynamicSubscribe(EventGroup subscribeEventGroup, string subscribeEventType, bool subscribe, params string[]? subscribeEventKeys)
        {
            var client = GetSignalRClient();
            var connectionId = client.GetConnectionId();

            if (connectionId == null)
            {
                return Task.CompletedTask;
            }
            return client.SendAsync("Send", new DynamicSubscribeNotificationData(subscribeEventGroup, subscribeEventType, connectionId, subscribeEventKeys) { Subscribe = subscribe });
        }

        public string? GetConnectionId()
        {
            return GetSignalRClient().GetConnectionId();
        }

        public async Task<Guid> DiscoverRemoteService(string connectionId)
        {
            var data = RemoteServiceCallNotificationData.CreateDiscoverRemoteServiceNotification(connectionId);
            await Send(data);
            return data.CallId;
        }

        public Task CallRemoteService(string connectionId, string serviceKey, string actionKey, string? data)
        {
            return Send(new RemoteServiceCallNotificationData(serviceKey, actionKey, connectionId)
            {
                Data = data
            });
        }
    }
}
