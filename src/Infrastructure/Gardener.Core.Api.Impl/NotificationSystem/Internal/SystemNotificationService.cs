// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Cache;
using Gardener.Core.Enums;
using Gardener.Core.EventBus;
using Gardener.Core.NotificationSystem;
using IdGen;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Gardener.Core.Api.Impl.NotificationSystem.Internal
{
    /// <summary>
    /// 系统通知服务
    /// </summary>
    public class SystemNotificationService : ISystemNotificationService
    {
        private readonly string method = "ReceiveMessage";
        private readonly IHubContext<SystemNotificationHub> hubContext;
        private readonly ICache cache;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly static ConcurrentDictionary<string, (CancellationToken, Identity)> connectCancellationTokens = new ConcurrentDictionary<string, (CancellationToken, Identity)>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="cache"></param>
        /// <param name="serviceScopeFactory"></param>
        public SystemNotificationService(IHubContext<SystemNotificationHub> hubContext, ICache cache, IServiceScopeFactory serviceScopeFactory)
        {
            this.hubContext = hubContext;
            this.cache = cache;
            this.serviceScopeFactory = serviceScopeFactory;
        }
        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToAllClient(NotificationData notifyData)
        {
            return hubContext.Clients.All.SendAsync(method, notifyData);
        }
        /// <summary>
        /// 向指定用户组发送信息
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToGroup(string groupName, NotificationData notifyData)
        {
            return hubContext.Clients.Group(groupName).SendAsync(method, notifyData);
        }
        /// <summary>
        /// 向指定用户组发送信息
        /// </summary>
        /// <param name="groupNames"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToGroups(IEnumerable<string> groupNames, NotificationData notifyData)
        {
            return hubContext.Clients.Groups(groupNames).SendAsync(method, notifyData);
        }
        /// <summary>
        /// 向指定Client发送信息
        /// </summary>
        /// <param name="connectionId">接收连接编号</param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToClient(string connectionId, NotificationData notifyData)
        {
            return hubContext.Clients.Client(connectionId).SendAsync(method, notifyData);
        }
        /// <summary>
        /// 向指定Client发送信息
        /// </summary>
        /// <param name="connectionIds">接收连接编号集合</param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToClients(List<string> connectionIds, NotificationData notifyData)
        {
            return hubContext.Clients.Clients(connectionIds).SendAsync(method, notifyData);
        }
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="receiveUser">接收用户</param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToUser(Identity receiveUser, NotificationData notifyData)
        {
            return hubContext.Clients.User(GetUserId(receiveUser)).SendAsync(method, notifyData);
        }
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="receiveUsers">接收用户集合</param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToUsers(IEnumerable<Identity> receiveUsers, NotificationData notifyData)
        {
            return hubContext.Clients.Users(receiveUsers.Select(x => GetUserId(x))).SendAsync(method, notifyData);
        }
        /// <summary>
        /// 获取用户编号
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private string GetUserId(Identity identity)
        {
            return $"{identity.IdentityType}_{identity.Id}";
        }

        /// <summary>
        /// 设置在线状态
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="online"></param>
        /// <returns></returns>
        private Task SetUserOnlineState(Identity identity, bool online)
        {
            return cache.SetAsync(GetUserOnlineStateCacheKey(identity.IdentityType, identity.Id), online ? 1 : 0);
        }

        /// <summary>
        /// 获取用户链接
        /// </summary>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<UserConnectionInfo>?> GetUserConnectionInfos(IdentityType identityType, string id)
        {
            string key = GetUserConnectionInfoCacheKey(identityType, id);
            return cache.GetAsync<List<UserConnectionInfo>>(key);
        }
        /// <summary>
        /// 设置用户链接
        /// </summary>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <param name="connectionInfos"></param>
        /// <returns></returns>
        private Task SetUserConnectionInfos(IdentityType identityType, string id, List<UserConnectionInfo> connectionInfos)
        {
            string key = GetUserConnectionInfoCacheKey(identityType, id);
            return cache.SetAsync(key, connectionInfos);
        }
        /// <summary>
        /// 移除用户链接
        /// </summary>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private Task RemoveUserConnectionInfos(IdentityType identityType, string id)
        {
            string key = GetUserConnectionInfoCacheKey(identityType, id);
            return cache.RemoveAsync(key);
        }
        /// <summary>
        /// 获取用户链接缓存键
        /// </summary>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetUserConnectionInfoCacheKey(IdentityType identityType, string id)
        {
            return $"SystemNotification:Connection:{identityType}:{id}";
        }
        /// <summary>
        /// 获取用户在线状态
        /// </summary>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetUserOnlineStateCacheKey(IdentityType identityType, string id)
        {
            return $"SystemNotification:OnlineState:{identityType}:{id}";
        }
        /// <summary>
        /// 设置用户在线状态为在线
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="connectionId"></param>
        /// <param name="ip"></param>
        /// <param name="connectionAborted"></param>
        /// <returns></returns>
        public async Task SetUserOnline(Identity identity, string connectionId, string ip, CancellationToken connectionAborted)
        {
            connectCancellationTokens.AddOrUpdate(connectionId, _ => (connectionAborted, identity), (_, _) => (connectionAborted, identity));
            List<UserConnectionInfo>? connections = null;
            //已在线
            if (await CheckUserIsOnline(identity.IdentityType, identity.Id))
            {
                //已有连接
                connections = await GetUserConnectionInfos(identity.IdentityType, identity.Id);
            }
            if (connections != null)
            {
                //新连接加入
                connections.Add(new UserConnectionInfo(connectionId, identity, ip));
            }
            else
            {
                //只有本次连接
                connections = [new UserConnectionInfo(connectionId, identity, ip)];
            }
            await Task.WhenAll(SetUserOnlineState(identity, true), SetUserConnectionInfos(identity.IdentityType, identity.Id, connections));
        }

        /// <summary>
        /// 设置用户在线状态为离线
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>s
        public async Task SetUserOffline(Identity identity, string connectionId)
        {
            List<UserConnectionInfo>? connectionInfos = await GetUserConnectionInfos(identity.IdentityType, identity.Id);
            if (connectionInfos == null)
            {
                //没有连接
                await SetUserOnlineState(identity, false);
                return;
            }
            var currentUserCon = connectionInfos.Where(x => x.ConnectionId.Equals(connectionId)).FirstOrDefault();
            if(currentUserCon==null)
            {
                //该链接未找到

                return;
            }
            if (connectionInfos.Count == 1)
            {
                //只有自己一个连接
                await SetUserOnlineState(identity, false);
                await RemoveUserConnectionInfos(identity.IdentityType, identity.Id);

            }
            else
            {
                //多个连接
                connectionInfos.Remove(currentUserCon);
                await SetUserConnectionInfos(identity.IdentityType, identity.Id, connectionInfos);
            }
        }

        /// <summary>
        /// 判断用户是否在线
        /// </summary>
        /// <param name="identityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserIsOnline(IdentityType identityType, string id)
        {
            string key = GetUserOnlineStateCacheKey(identityType, id);
            return await cache.GetAsync(key, () => Task.FromResult(0)) == 1;
        }
        /// <summary>
        /// 设置用户到某个分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果不在线或连接信息不存在，无法设置
        /// </remarks>
        public Task<bool> UserGroupAdd(string groupName, Identity identity)
        {
            return UserGroupAdd(groupName, identity.IdentityType, identity.Id);
        }
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
        public async Task<bool> UserGroupAdd(string groupName, IdentityType identityType, string id)
        {
            if (!await CheckUserIsOnline(identityType, id))
            {
                return false;
            }
            List<UserConnectionInfo>? connectionInfos = await GetUserConnectionInfos(identityType, id);
            if (connectionInfos == null)
            {
                return false;
            }
            await connectionInfos.ForEachAsync(async connection =>
            {
                await hubContext.Groups.AddToGroupAsync(connection.ConnectionId, groupName);
            });
            return true;
        }

        /// <summary>
        /// 设置用户到某个分组
        /// </summary>
        /// <typeparam name="TSystemNotificationHubGrouper"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<bool> UserGroupAdd<TSystemNotificationHubGrouper>(Identity identity) where TSystemNotificationHubGrouper : ISystemNotificationHubGrouper
        {
            IEnumerable<string>? groups = await GetGroups<TSystemNotificationHubGrouper>(identity);
            if (groups == null || !groups.Any())
            {
                return true;
            }
            return await UserGroupAdd(groups, identity);
        }

        /// <summary>
        /// 设置用户到某些分组
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果不在线或连接信息不存在，无法设置
        /// </remarks>
        public async Task<bool> UserGroupAdd(IEnumerable<string> groups, Identity identity)
        {
            List<Task> tasks = new List<Task>(groups.Count());
            foreach (string group in groups)
            {
                tasks.Add(UserGroupAdd(group, identity));
            }
            await Task.WhenAll(tasks);
            return true;
        }

        /// <summary>
        /// 移除用户的某个分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果连接信息不存在，无法设置
        /// </remarks>
        public Task<bool> UserGroupRemove(string groupName, Identity identity)
        {
            return UserGroupRemove(groupName, identity.IdentityType, identity.Id);
        }

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
        public async Task<bool> UserGroupRemove(string groupName, IdentityType identityType, string id)
        {
            List<UserConnectionInfo>? connections = await GetUserConnectionInfos(identityType, id);
            if (connections == null || !connections.Any())
            {
                return false;
            }
            await connections.ForEachAsync(async connection =>
            {
                await hubContext.Groups.RemoveFromGroupAsync(connection.ConnectionId, groupName);
            });
            return true;
        }

        /// <summary>
        /// 移除用户的某个分组
        /// </summary>
        /// <typeparam name="TSystemNotificationHubGrouper"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果连接信息不存在，无法设置
        /// </remarks>
        public async Task<bool> UserGroupRemove<TSystemNotificationHubGrouper>(Identity identity) where TSystemNotificationHubGrouper : ISystemNotificationHubGrouper
        {
            IEnumerable<string>? groups = await GetGroups<TSystemNotificationHubGrouper>(identity);
            if (groups == null || !groups.Any())
            {
                return true;
            }
            return await UserGroupRemove(groups, identity);
        }

        /// <summary>
        /// 移除用户的某些分组
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果连接信息不存在，无法设置
        /// </remarks>
        public async Task<bool> UserGroupRemove(IEnumerable<string> groups, Identity identity)
        {
            List<Task> tasks = new List<Task>(groups.Count());
            foreach (string group in groups)
            {
                tasks.Add(UserGroupRemove(group, identity));
            }
            await Task.WhenAll(tasks);
            return true;
        }

        /// <summary>
        /// 根据类型获取分组器
        /// </summary>
        /// <typeparam name="TSystemNotificationHubGrouper"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<string>?> GetGroups<TSystemNotificationHubGrouper>(Identity identity) where TSystemNotificationHubGrouper : ISystemNotificationHubGrouper
        {
            using var scope = serviceScopeFactory.CreateScope();
            var services = scope.ServiceProvider;
            IEnumerable<ISystemNotificationHubGrouper> groupers = services.GetServices<ISystemNotificationHubGrouper>();
            var group = groupers.Where(x => x.GetType().Equals(typeof(TSystemNotificationHubGrouper))).FirstOrDefault();
            if (group == null)
            {
                return null;
            }
            var groups = await group.GetGroupName(identity);
            return groups;
        }

        /// <summary>
        /// 动态订阅信息
        /// </summary>
        private static ConcurrentDictionary<string, ConcurrentDictionary<string, DateTime>> dynamicSubscribers = new ConcurrentDictionary<string, ConcurrentDictionary<string, DateTime>>();

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
        public Task<bool> DynamicSubscribe(EventGroup subscribeEventGroup, string subscribeEventType, bool subscribe, string connectionId, params string[]? subscribeEventKeys)
        {
            return Task.Run(() =>
            {
                string eventTypeId = $"{subscribeEventGroup}_{subscribeEventType}";
                List<string> keys = new List<string>();
                if (subscribeEventKeys == null || !subscribeEventKeys.Any())
                {
                    keys.Add(eventTypeId);
                }
                else
                {
                    foreach (var key in subscribeEventKeys)
                    {
                        keys.Add(eventTypeId + "_" + key);
                    }
                }
                foreach (var eventTypeKey in keys)
                {
                    if (subscribe)
                    {
                        var queue = dynamicSubscribers.GetOrAdd(eventTypeKey, s =>
                        {
                            var queue = new ConcurrentDictionary<string, DateTime>();
                            return queue;
                        });
                        queue.AddOrUpdate(connectionId, DateTime.Now, (_, _) => DateTime.Now);
                    }
                    else
                    {
                        dynamicSubscribers.Remove(eventTypeKey, out _);
                    }
                }
                return true;
            });
        }

        /// <summary>
        /// 发送给动态订阅者
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        public Task<bool> SendToDynamicSubscriber(DynamicNotificationData notificationData)
        {
            return Task.Run(async () =>
            {
                List<string> sendConnectionIds = new List<string>();
                string[]? eventKeys = notificationData.EventKeys;

                if (eventKeys == null || !eventKeys.Any())
                {
                    return true;
                }

                foreach (string key in eventKeys)
                {
                    string eventKey = notificationData.EventTypeId + "_" + key;
                    if (dynamicSubscribers.TryGetValue(eventKey, out ConcurrentDictionary<string, DateTime>? queue))
                    {
                        if (queue != null)
                        {
                            foreach (var item in queue)
                            {
                                //心跳5秒一次，两次未收到强制过期
                                if ((DateTime.Now - item.Value).TotalMilliseconds > 11000)
                                {
                                    queue.Remove(item.Key, out _);
                                }
                                else
                                {
                                    sendConnectionIds.Add(item.Key);
                                }
                            }
                        }
                    }

                }
                if (sendConnectionIds.Any())
                {
                    await SendToClients(sendConnectionIds, notificationData);
                }
                return true;
            });
        }

        /// <summary>
        /// 判断是否有动态订阅者
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        public Task<bool> ExistsDynamicSubscriber(DynamicNotificationData notificationData)
        {
            return Task.Run<bool>(() =>
            {
                string[]? eventKeys = notificationData.EventKeys;

                if (eventKeys == null || !eventKeys.Any())
                {
                    return false;
                }
                foreach (string key in eventKeys)
                {
                    string eventKey = notificationData.EventTypeId + "_" + key;
                    if (dynamicSubscribers.TryGetValue(eventKey, out ConcurrentDictionary<string, DateTime>? queue))
                    {
                        if (queue != null)
                        {
                            foreach (var item in queue)
                            {
                                //
                                if ((DateTime.Now - item.Value).TotalMilliseconds <= 11000)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            });

        }

        /// <summary>
        /// 清零已断开连接
        /// </summary>
        /// <returns></returns>
        public async Task ClearDisconnectedConnections()
        {
            foreach (var key in connectCancellationTokens.Keys)
            {
                var item = connectCancellationTokens[key];
                if (item.Item1.IsCancellationRequested)
                {
                    //已断开
                    await SetUserOffline(item.Item2, key);
                    connectCancellationTokens.Remove(key, out _);
                }
            }
        }
    }
}
