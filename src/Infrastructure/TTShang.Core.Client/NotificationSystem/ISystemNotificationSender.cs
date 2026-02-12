// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.SignalR;
using TTShang.Core.NotificationSystem;

namespace TTShang.Core.Client.NotificationSystem
{
    public interface ISystemNotificationSender
    {
        /// <summary>
        /// 发现远程服务
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        Task<Guid> DiscoverRemoteService(string connectionId);
        /// <summary>
        /// 调用远程服务
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="serviceKey"></param>
        /// <param name="actionKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task CallRemoteService(string connectionId, string serviceKey, string actionKey, string? data);
        /// <summary>
        /// 发送通知到服务端
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        Task Send(NotificationData notificationData);
        /// <summary>
        /// 发送动态订阅通知到服务端
        /// </summary>
        /// <param name="subscribeEventGroup"></param>
        /// <param name="subscribeEventType"></param>
        /// <param name="subscribe"></param>
        /// <param name="subscribeEventKeys"></param>
        /// <returns></returns>
        Task SendDynamicSubscribe(EventGroup subscribeEventGroup, string subscribeEventType, bool subscribe, params string[]? subscribeEventKeys);

        /// <summary>
        /// 获取通知系统的client
        /// </summary>
        /// <returns></returns>
        ISignalRClient GetSignalRClient();

        /// <summary>
        /// 获取连接编号
        /// </summary>
        /// <returns></returns>
        string? GetConnectionId();

        /// <summary>
        /// 动态订阅操作
        /// </summary>
        /// <param name="subscriber">订阅者</param>
        /// <param name="subscribe">订阅/取消订阅</param>
        /// <returns></returns>
        Task DynamicSubscribe(ISubscriber subscriber, bool subscribe);
    }
}