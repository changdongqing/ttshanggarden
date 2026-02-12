// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.EventBus.Events
{
    /// <summary>
    /// 事件总线订阅事件
    /// </summary>
    public class EventBusSubscribeEvent : EventBase
    {
        /// <summary>
        /// 事件总线订阅事件
        /// </summary>
        /// <param name="subscriber"></param>
        public EventBusSubscribeEvent(ISubscriber subscriber) : base()
        {
            Subscriber = subscriber;
        }
        /// <summary>
        /// 订阅者
        /// </summary>
        public ISubscriber Subscriber { get; set; }
    }
}
