// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.EventBus.Events;
using Gardener.Core.NotificationSystem;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Gardener.Core.Client.Impl.EventBus
{
    /// <summary>
    /// 事件通知
    /// </summary>
    /// <remarks>
    /// 事件通知
    /// </remarks>
    /// <param name="serviceProvider"></param>
    /// <param name="logger"></param>
    [ScopedService]
    public class EventBusSimpleImpl(IServiceProvider serviceProvider, IClientLogger logger) : IEventBus
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly IClientLogger _logger = logger;
        /// <summary>
        /// 订阅者
        /// </summary>
        private readonly ConcurrentDictionary<string, List<ISubscriber>> _subscribers = new();

        /// <summary>
        /// 
        /// </summary>
        private bool inited = false;

        /// <summary>
        /// 初始化静态订阅者
        /// </summary>
        private void InitStaticSubscribers()
        {
            if (inited)
            {
                return;
            }
            lock (_subscribers)
            {
                if (inited)
                {
                    return;
                }
                //静态注册订阅者
                var staticSubscribers = _serviceProvider.GetServices<ISubscriber>();
                if (staticSubscribers != null)
                {
                    foreach (var subscriber in staticSubscribers)
                    {
                        _subscribers.AddOrUpdate(subscriber.EventTypeId, [subscriber], (key, subs) =>
                        {
                            subs.Add(subscriber);
                            return subs;
                        });
                    }
                }
                inited = true;
            }
        }


        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task PublishAsync(EventBase e, CancellationToken? cancellationToken)
        {
            string typeNameId = e.EventTypeId;
            InitStaticSubscribers();
            List<Task> tasks = [];
            //订阅者
            if (_subscribers.TryGetValue(typeNameId, out List<ISubscriber>? subscribers))
            {
                //循环订阅者
                foreach (var subscriber in subscribers)
                {
                    try
                    {
                        if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
                        {
                            break;
                        }
                        //细分key过滤
                        if (subscriber.EventKeys != null && e.EventKeys != null && !e.EventKeys.Any(x => subscriber.EventKeys.Contains(x)))
                        {
                            continue;
                        }
                        //_logger.Info($"eventBus publish event {typeNameId} to {subscriber.Id}  {System.Text.Json.JsonSerializer.Serialize(e)}");
                        tasks.Add(subscriber.CallBack(e));
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"eventBus subscriber error {e.GetType().FullName}  {System.Text.Json.JsonSerializer.Serialize(e)}", ex: ex);
                    }
                }
            }
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public void Publish(EventBase e, CancellationToken? cancellationToken)
        {
            PublishAsync(e, cancellationToken);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventGroup"></param>
        /// <param name="eventType"></param>
        /// <param name="callBack"></param>
        /// <param name="eventKeys"></param>
        /// <returns></returns>
        public ISubscriber Subscribe<TEvent>(EventGroup eventGroup, string eventType, Func<TEvent, Task> callBack, params string[] eventKeys) where TEvent : EventBase
        {
            ISubscriber subscriber = new Subscriber<TEvent>(eventGroup, eventType, callBack)
            {
                EventKeys = eventKeys
            };
            _subscribers.AddOrUpdate(subscriber.EventTypeId, [subscriber], (key, subs) =>
            {
                subs.Add(subscriber);
                return subs;
            });
            Publish(new EventBusSubscribeEvent(subscriber), null);
            return subscriber;
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="callBack"></param>
        /// <param name="eventKeys"></param>
        /// <returns></returns>
        public ISubscriber Subscribe<TEvent>(string eventType, Func<TEvent, Task> callBack, params string[] eventKeys) where TEvent : EventBase
        {
            EventGroup eventGroup = EventGroup.SystemEvent;
            //派生自通知基类的，默认认为是通知
            if (typeof(TEvent).IsAssignableTo(typeof(NotificationData)))
            {
                eventGroup = EventGroup.SystemNotify;
            }
            return Subscribe<TEvent>(eventGroup, eventType, callBack, eventKeys);
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="callBack"></param>
        /// <param name="eventKeys"></param>
        /// <returns></returns>
        public ISubscriber Subscribe<TEvent>(Func<TEvent, Task> callBack, params string[] eventKeys) where TEvent : EventBase
        {
            Type eventType = typeof(TEvent);
            return Subscribe<TEvent>(eventType.FullName ?? eventType.Name, callBack, eventKeys);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="subscriber"></param>
        public void UnSubscribe(ISubscriber subscriber)
        {
            Publish(new EventBusUnSubscribeEvent(subscriber), null);
            string typeName = subscriber.EventTypeId;
            if (_subscribers.TryGetValue(typeName, out List<ISubscriber>? subscribers))
            {
                subscribers.RemoveAll(x => x.Id.Equals(subscriber.Id));
            }
        }

        /// <summary>
        /// 获取所有订阅者
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public List<ISubscriber>? GetSubscribers(EventBase e)
        {
            InitStaticSubscribers();
            _subscribers.TryGetValue(e.EventTypeId, out List<ISubscriber>? subscribers);
            return subscribers;
        }
        /// <summary>
        /// 获取所有订阅者
        /// </summary>
        /// <returns></returns>
        public List<ISubscriber>? GetSubscribers()
        {
            InitStaticSubscribers();
            return _subscribers.SelectMany(x => x.Value).ToList();
        }
    }
}
