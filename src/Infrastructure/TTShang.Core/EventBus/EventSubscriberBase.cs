// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.NotificationSystem;

namespace TTShang.Core.EventBus
{
    /// <summary>
    /// 泛型订阅者基类
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <remarks>
    /// 目前仅client使用
    /// </remarks>
    public abstract class EventSubscriberBase<TEvent> : ISubscriber where TEvent : EventBase
    {
        /// <summary>
        /// 泛型订阅者基类
        /// </summary>
        public EventSubscriberBase()
        {
            _id = Guid.NewGuid().ToString();
            _eventType = typeof(TEvent).FullName ?? typeof(TEvent).Name;
            
            //派生自通知基类的，默认认为是通知
            if (typeof(TEvent).IsAssignableTo(typeof(NotificationData)))
            {
                _eventGroup = EventGroup.SystemNotify;
            }
            else
            {
                _eventGroup = EventGroup.SystemEvent;
            }
        }
        /// <summary>
        /// 泛型订阅者基类
        /// </summary>
        /// <param name="eventGroup"></param>
        /// <param name="eventType"></param>
        public EventSubscriberBase(EventGroup eventGroup, string eventType)
        {
            _id = Guid.NewGuid().ToString();
            _eventType = eventType;
            _eventGroup = eventGroup;
        }
        /// <summary>
        /// 订阅者唯一编号
        /// </summary>
        private readonly string _id;

        /// <summary>
        /// 订阅者唯一编号
        /// </summary>
        public string Id => _id;
        /// <summary>
        /// 事件类型
        /// </summary>
        private readonly string _eventType;
        /// <summary>
        /// 事件类型
        /// </summary>
        public string EventType => _eventType;
        /// <summary>
        /// 事件分组
        /// </summary>
        private readonly EventGroup _eventGroup;
        /// <summary>
        /// 事件分组
        /// </summary>
        public EventGroup EventGroup => _eventGroup;

        /// <summary>
        /// 是否是动态订阅者
        /// </summary>
        public bool IsDynamicSubscriber { get; set; }

        /// <summary>
        /// 动态订阅事件key
        /// </summary>
         public string[]? EventKeys { get; set; }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        public abstract Task CallBack(TEvent e);

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Task CallBack(EventBase e)
        {
            return CallBack((TEvent)e);
        }
    }
}
