// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.NotificationSystem;
using TTShang.Core.NotificationSystem;

namespace TTShang.Core.Client.Impl.NotificationSystem.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class RemoteServiceCallNotificationEventSubscriber : EventSubscriberBase<RemoteServiceCallNotificationData>
    {
        private readonly IAuthenticationStateManager authStateManager;
        private readonly ISystemNotificationSender systemNotificationSender;
        private readonly IClientNotifier clientNotifier;

        public RemoteServiceCallNotificationEventSubscriber(IAuthenticationStateManager authStateManager, ISystemNotificationSender systemNotificationSender, IClientNotifier clientNotifier)
        {
            this.authStateManager = authStateManager;
            this.systemNotificationSender = systemNotificationSender;
            this.clientNotifier = clientNotifier;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override Task CallBack(RemoteServiceCallNotificationData e)
        {
            if (e.IsRequest())
            {
                if (e.IsDiscoverRemoteService())
                {
                    e.RequestToResponse().SetSuperServices(SuperServices());
                    return systemNotificationSender.Send(e);
                }
                if (e.ServiceKey.Equals("account"))
                {
                    if (e.ActionKey.Equals("forceLogout"))
                    {
                        authStateManager.Logout(true);
                    }
                }
                else if (e.ServiceKey.Equals("notification"))
                {
                    if (e.ActionKey.Equals("info") && !string.IsNullOrEmpty(e.Data))
                    {
                        clientNotifier.Info(e.Data);
                    }
                }
            }
            return Task.CompletedTask;
        }

        private List<RemoteService> SuperServices()
        {
            return
                [
                 new RemoteService("account","账户服务","与当前账号相关服务",[
                    new RemoteServiceAction("forceLogout","强制退出","让该端退出登录",false)
                ]),
                 new RemoteService("notification","通知服务","向该端发送通知",[
                    new RemoteServiceAction("info","消息提醒","在该端弹出消息提醒",true){DataDescription="文本消息内容"}
                ])
                ];
        }
    }
}
