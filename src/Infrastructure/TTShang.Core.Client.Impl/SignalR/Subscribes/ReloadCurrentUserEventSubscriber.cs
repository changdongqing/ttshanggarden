// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Authorization.Events;
using TTShang.Core.Client.SignalR;

namespace TTShang.Core.Client.Impl.SignalR.Subscribes
{
    /// <summary>
    /// 用户信息刷新后，连接signalRClient
    /// </summary>
    [ScopedService]
    public class ReloadCurrentUserEventSubscriber : EventSubscriberBase<ReloadCurrentUserEvent>
    {
        private readonly ISignalRClientManager signalRClientManager;

        public ReloadCurrentUserEventSubscriber(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }

        public override Task CallBack(ReloadCurrentUserEvent e)
        {
            //无需等待
            signalRClientManager.ConnectionAndStartAll();
            return Task.CompletedTask;
        }
    }
}
