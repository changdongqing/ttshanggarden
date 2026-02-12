// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.EventBus;
using Gardener.Core.NotificationSystem.Services;

namespace Gardener.Core.NotificationSystem
{
    /// <summary>
    /// 系统通知服务
    /// </summary>
    public interface ISystemNotificationService: IUserConnectQueryService
    {
        
        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToAllClient(NotificationData notifyData);
        /// <summary>
        /// 向指定Client发送信息
        /// </summary>
        /// <param name="connectionId">接收连接编号</param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToClient(string connectionId, NotificationData notifyData);
        /// <summary>
        /// 向指定Client发送信息
        /// </summary>
        /// <param name="connectionIds">接收连接编号集合</param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToClients(List<string> connectionIds, NotificationData notifyData);
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="receiveUser">接收用户</param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToUser(Identity receiveUser, NotificationData notifyData);

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="receiveUsers"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToUsers(IEnumerable<Identity> receiveUsers, NotificationData notifyData);

        /// <summary>
        /// 向指定用户组发送信息
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToGroup(string groupName, NotificationData notifyData);

        /// <summary>
        /// 向指定用户组发送信息
        /// </summary>
        /// <param name="groupNames"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToGroups(IEnumerable<string> groupNames, NotificationData notifyData);

        /// <summary>
        /// 设置用户在线状态为在线
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="connectionId"></param>
        /// <param name="ip"></param>
        /// <param name="connectionAborted"></param>
        /// <returns></returns>
        public Task SetUserOnline(Identity identity, string connectionId,string ip, CancellationToken connectionAborted);

        /// <summary>
        /// 设置用户在线状态为离线
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public Task SetUserOffline(Identity identity, string connectionId);

        /// <summary>
        /// 设置用户到某个分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果不在线或连接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupAdd(string groupName, Identity identity);
        /// <summary>
        /// 设置用户到某个分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果不在线或连接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupAdd(string groupName, IdentityType identityType, string id);

        /// <summary>
        /// 设置用户到某个分组
        /// </summary>
        /// <typeparam name="TSystemNotificationHubGrouper"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果不在线或连接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupAdd<TSystemNotificationHubGrouper>(Identity identity) where TSystemNotificationHubGrouper : ISystemNotificationHubGrouper;

        /// <summary>
        /// 设置用户到某些分组
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果不在线或连接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupAdd(IEnumerable<string> groups, Identity identity);

        /// <summary>
        /// 移除用户的某个分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果连接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupRemove(string groupName, Identity identity);
        /// <summary>
        /// 移除用户的某个分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果连接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupRemove(string groupName, IdentityType identityType, string id);

        /// <summary>
        /// 移除用户的某个分组
        /// </summary>
        /// <typeparam name="TSystemNotificationHubGrouper"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果连接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupRemove<TSystemNotificationHubGrouper>(Identity identity) where TSystemNotificationHubGrouper : ISystemNotificationHubGrouper;


        /// <summary>
        /// 移除用户的某些分组
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果连接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupRemove(IEnumerable<string> groups, Identity identity);

        /// <summary>
        /// 根据类型获取分组器
        /// </summary>
        /// <typeparam name="TSystemNotificationHubGrouper"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<string>?> GetGroups<TSystemNotificationHubGrouper>(Identity identity) where TSystemNotificationHubGrouper : ISystemNotificationHubGrouper;

        /// <summary>
        /// 动态订阅
        /// </summary>
        /// <remarks>
        /// <para>动态订阅是一个用于临时性的订阅机制，客户端需要主动发出动态订阅通知，服务端将记录该订阅信息，该记录有一个指定的过期时间。</para>
        /// <para>如果要一直保持订阅，需要在过期前持续发送订阅信息</para>
        /// </remarks>
        /// <param name="subscribeEventGroup"></param>
        /// <param name="subscribeEventType"></param>
        /// <param name="subscribe"></param>
        /// <param name="connectionId"></param>
        /// <param name="subscribeEventKeys"></param>
        /// <returns></returns>
        Task<bool> DynamicSubscribe(EventGroup subscribeEventGroup, string subscribeEventType, bool subscribe, string connectionId, params string[]? subscribeEventKeys);

        /// <summary>
        /// 发送给动态订阅者
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        Task<bool> SendToDynamicSubscriber(DynamicNotificationData notificationData);

        /// <summary>
        /// 判断是否有动态订阅者
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        Task<bool> ExistsDynamicSubscriber(DynamicNotificationData notificationData);

        /// <summary>
        /// 清零已断开连接
        /// </summary>
        /// <returns></returns>
        Task ClearDisconnectedConnections();
    }
}
