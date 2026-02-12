// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using TTShang.Core.EventBus;

namespace TTShang.Core.Api.Impl.EventBus.Internal
{
    /// <summary>
    /// 事件发送服务
    /// </summary>
    internal class EventBusService : IEventBus
    {
        private readonly IEventPublisher eventPublisher;

        public EventBusService(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task PublishAsync(EventBase e, CancellationToken? cancellationToken = null)
        {
            EventSource<EventBase> eventSource = new EventSource<EventBase>(e.EventGroup.ToString() + e.EventType, e.IsConsumOnce);
            eventSource.Body = e;
            if (cancellationToken.HasValue)
            {
                eventSource.CancellationToken = cancellationToken.Value;
            }
            return eventPublisher.PublishAsync(eventSource);
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public void Publish(EventBase e, CancellationToken? cancellationToken = null)
        {
            PublishAsync(e, cancellationToken);
        }
    }
}
