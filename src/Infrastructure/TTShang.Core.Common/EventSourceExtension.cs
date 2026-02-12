// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using TTShang.Core.EventBus;

namespace TTShang.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class EventSourceExtension
    {
        /// <summary>
        /// 获取 <see cref="IEventSource"/> 的 <see cref="IEventSource.Payload"/>,返回<typeparamref name="TData"/>
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="eventSource"></param>
        /// <returns></returns>
        public static TData GetPayload<TData>(this IEventSource eventSource)
        {
            return (TData)eventSource.Payload;
        }

        /// <summary>
        /// 获取 <see cref="IEventSource"/> 的 <see cref="IEventSource.Payload"/>,返回 <see cref="EventBase{TData}.Data"/>
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="eventSource"></param>
        /// <returns></returns>
        public static TData GetEventData<TData>(this IEventSource eventSource)
        {
            if (eventSource.Payload is EventBase<TData>)
            {
                return ((EventBase<TData>)eventSource.Payload).Data;
            }
            throw new InvalidOperationException($"{nameof(IEventSource.Payload)} not inherit {nameof(EventBase<TData>)}");
        }
    }
}
