// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.NotificationSystem;

namespace TTShang.Core.Client.Impl.NotificationSystem.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class UserOnlineChangeNotificationEventSubscriber : EventSubscriberBase<UserOnlineChangeNotificationData>
    {
        private readonly IClientNotifier clientNotifier;
        private readonly IAuthenticationStateManager authStateManager;

        public UserOnlineChangeNotificationEventSubscriber(IClientNotifier clientNotifier, IAuthenticationStateManager authStateManager)
        {
            this.clientNotifier = clientNotifier;
            this.authStateManager = authStateManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override Task CallBack(UserOnlineChangeNotificationData e)
        {
            UserOnlineChangeNotificationData notificationData = e;
            //无数据 或 无身份 或 非user身份
            if (notificationData == null || notificationData.Identity == null || !notificationData.Identity.IdentityType.Equals(IdentityType.User))
            {
                return Task.CompletedTask;
            }
            var user = authStateManager.GetCurrentUser();
            //未登录，或是自己
            if (user == null || user.Id.ToString() == notificationData.Identity.Id)
            {
                return Task.CompletedTask;
            }
            //不同租户下用户
            if (user.TenantId != null && user.TenantId != notificationData.Identity.TenantId)
            {
                return Task.CompletedTask;
            }
            if (notificationData.OnlineStatus.Equals(UserOnlineStatus.Online))
            {
                clientNotifier.Info($"{notificationData.Identity.NickName ?? notificationData.Identity.Name} 刚刚上线了<br/>IP:[{notificationData.Ip}]", "用户上线通知");
            }
            else if (notificationData.OnlineStatus.Equals(UserOnlineStatus.Offline))
            {
                clientNotifier.Info($"{notificationData.Identity.NickName ?? notificationData.Identity.Name} 刚刚离线了<br/>IP:[{notificationData.Ip}]", "用户离线通知");
            }
            return Task.CompletedTask;
        }
    }
}
