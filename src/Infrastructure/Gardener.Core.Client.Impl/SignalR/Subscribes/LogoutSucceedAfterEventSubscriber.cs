// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Authorization.Events;
using Gardener.Core.Client.SignalR;

namespace Gardener.Core.Client.Impl.SignalR.Subscribes
{
    /// <summary>
    /// 登出后端口系统通知
    /// </summary>
    [ScopedService]
    public class LogoutSucceedAfterEventSubscriber : EventSubscriberBase<LogoutSucceedAfterEvent>
    {
        private readonly ISignalRClientManager signalRClientManager;

        public LogoutSucceedAfterEventSubscriber(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }

        public override Task CallBack(LogoutSucceedAfterEvent e)
        {
            //无需等待
            signalRClientManager.StopAll();
            return Task.CompletedTask;
        }
    }
}
