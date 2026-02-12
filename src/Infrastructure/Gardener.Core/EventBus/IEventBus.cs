// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.EventBus
{
    /// <summary>
    /// 事件服务
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        void Publish(EventBase e, CancellationToken? cancellationToken = null);

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        Task PublishAsync(EventBase e, CancellationToken? cancellationToken = null);

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="callBack"></param>
        /// <param name="eventKeys">事件Key</param>
        /// <returns></returns>
        ISubscriber Subscribe<TEvent>(string eventType, Func<TEvent, Task> callBack, params string[] eventKeys) where TEvent : EventBase
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="callBack"></param>
        /// <param name="eventKeys">事件Key</param>
        /// <returns></returns>
        ISubscriber Subscribe<TEvent>(Func<TEvent, Task> callBack, params string[] eventKeys) where TEvent : EventBase
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventGroup"></param>
        /// <param name="eventType"></param>
        /// <param name="callBack"></param>
        /// <param name="eventKeys">事件Key</param>
        /// <returns></returns>
        ISubscriber Subscribe<TEvent>(EventGroup eventGroup, string eventType, Func<TEvent, Task> callBack, params string[] eventKeys) where TEvent : EventBase
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="subscriber"></param>
        void UnSubscribe(ISubscriber subscriber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有订阅者
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        List<ISubscriber>? GetSubscribers(EventBase e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有订阅者
        /// </summary>
        /// <returns></returns>
        List<ISubscriber>? GetSubscribers()
        {
            throw new NotImplementedException();
        }
    }
}
