// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.EventBus
{
    /// <summary>
    /// 订阅者
    /// </summary>
    /// <remarks>
    /// 目前仅client使用,client 是根据<typeparamref name="TEvent"/>名称定义唯一事件分类
    /// <code>EventTypeId=typeof(TEvent).FullName ?? typeof(TEvent).Name;</code>
    /// </remarks>
    public class Subscriber<TEvent> : EventSubscriberBase<TEvent> where TEvent : EventBase
    {
        /// <summary>
        /// 订阅者
        /// </summary>
        /// <param name="eventGroup"></param>
        /// <param name="eventType"></param>
        /// <param name="callBack"></param>
        public Subscriber(EventGroup eventGroup, string eventType, Func<TEvent, Task> callBack) : base(eventGroup, eventType)
        {
            _callBack = callBack;
        }
        /// <summary>
        /// 事件处理方法
        /// </summary>
        private Func<TEvent, Task> _callBack;

        /// <summary>
        /// 执行订阅者回调
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override Task CallBack(TEvent e)
        {
            return _callBack.Invoke(e);
        }
    }
}
