// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.EventBus
{
    /// <summary>
    /// 事件基类
    /// </summary>
    public abstract class EventBase
    {
        /// <summary>
        /// 事件基类
        /// </summary>
        /// <remarks>
        /// <para>事件类型 默认 <see cref="EventGroup.SystemEvent"/></para>
        /// <para>此类事件下分组 默认是 <code>GetType().FullName ?? GetType().Name</code></para>
        /// </remarks>
        public EventBase()
        {
            EventType = GetType().FullName ?? GetType().Name;
        }
        /// <summary>
        /// 事件基类
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <remarks>
        /// 事件分组 默认 <see cref="EventGroup.SystemEvent"/>
        /// </remarks>
        public EventBase(string eventType)
        {
            EventType = eventType;
        }
        /// <summary>
        /// 事件基类
        /// </summary>
        /// <param name="eventGroup">事件组</param>
        /// <remarks>
        /// <para>此类事件下分组 默认是 <code>GetType().FullName ?? GetType().Name</code></para>
        /// </remarks>
        public EventBase(EventGroup eventGroup)
        {
            EventGroup = eventGroup;
            EventType = GetType().FullName ?? GetType().Name;
        }
        /// <summary>
        /// 事件基类
        /// </summary>
        /// <param name="eventGroup">事件组</param>
        /// <param name="eventType">事件类型</param>
        public EventBase(EventGroup eventGroup, string eventType)
        {
            EventGroup = eventGroup;
            EventType = eventType;
        }

        /// <summary>
        /// 唯一编号
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// 事件组
        /// </summary>
        public EventGroup EventGroup { get; init; } = EventGroup.SystemEvent;

        /// <summary>
        /// 事件组
        /// </summary>
        public string EventType { get; init; } = string.Empty;

        /// <summary>
        /// 是否仅消费一次
        /// </summary>
        public bool IsConsumOnce { get; set; } = true;

        /// <summary>
        /// 事件类型唯一标记
        /// </summary>
        public string EventTypeId
        {
            get
            {
                return EventGroup + "_" + EventType;
            }
        }

        /// <summary>
        /// 根据业务细分组
        /// </summary>
        public virtual string[]? EventKeys { get; set; }

    }
}
