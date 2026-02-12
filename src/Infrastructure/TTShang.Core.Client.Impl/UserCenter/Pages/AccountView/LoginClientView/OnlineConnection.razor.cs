// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.NotificationSystem;
using TTShang.Core.NotificationSystem;
using TTShang.Core.NotificationSystem.Services;

namespace TTShang.Core.Client.Impl.UserCenter.Pages.AccountView.LoginClientView
{
    public partial class OnlineConnection : OperationDialogBase<int?, bool, UserCenterResource>
    {

        [Inject]
        private IUserConnectQueryService userConnectQueryService { get; set; } = null!;
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = null!;
        [Inject]
        private ISystemNotificationSender systemNotificationSender { get; set; } = null!;
        [Inject]
        private IEventBus eventBus { get; set; } = null!;
        /// <summary>
        /// 确认提示服务
        /// </summary>
        [Inject]
        protected IConfirmService ConfirmService { get; set; } = null!;

        private List<UserConnectionInfo>? userConnectionInfos;
        public ListGridType grid = new ListGridType { Gutter = 16, Column = 4 };
        private string? currentConnectionId;
        private ISubscriber? subscriber;
        private ISubscriber? userOnlineSub;
        ClientListBindValue<string, List<RemoteService>?> remoteServices = new ClientListBindValue<string, List<RemoteService>?>(null);

        private int? GetUserId()
        {
            int? userId = null;

            if (this.Options != null)
            {
                userId = this.Options.Value;
            }
            else
            {
                UserDto? user = authenticationStateManager.GetCurrentUser();
                userId = user?.Id;
            }
            return userId;
        }
        protected override async Task OnInitializedAsync()
        {
            int? userId = GetUserId();

            if (userId == null)
            {
                return;
            }
            userOnlineSub = eventBus.Subscribe<UserOnlineChangeNotificationData>(async x =>
            {
                if (x.FromConnectionId != null && x.Identity != null && IdentityType.User.Equals(x.Identity.IdentityType) && x.Identity.Id.Equals(userId.Value.ToString()))
                {
                    if (x.OnlineStatus.Equals(UserOnlineStatus.Online))
                    {
                        await LoadData();
                        await RefreshPageDom();
                    }
                    else if (userConnectionInfos != null)
                    {
                        await LoadData();
                        await RefreshPageDom();
                    }
                }
            }
            );
            subscriber = eventBus.Subscribe<RemoteServiceCallNotificationData>(x =>
            {
                if (x.IsResponse() && callId.HasValue && x.CallId.Equals(callId.Value) && !string.IsNullOrEmpty(x.FromConnectionId))
                {
                    remoteServices.SetValue(x.FromConnectionId, x.GetRemoteServices());

                    return RefreshPageDom();
                }
                return Task.CompletedTask;
            });
            await LoadData();
            await base.OnInitializedAsync();
        }

        public async Task LoadData()
        {
            int? userId = GetUserId();
            if (userId == null)
            { 
                return;
            }
            currentConnectionId = systemNotificationSender.GetConnectionId();
            userConnectionInfos = await userConnectQueryService.GetUserConnectionInfos(IdentityType.User, userId.Value.ToString());
        }

        private Guid? callId;
        private async Task DiscoverRemoteService(string connectionId)
        {
            callId = await systemNotificationSender.DiscoverRemoteService(connectionId);
        }

        protected override void Dispose(bool disposing)
        {
            if (subscriber != null)
            {
                eventBus.UnSubscribe(subscriber);
            }
            if (userOnlineSub != null)
            {
                eventBus.UnSubscribe(userOnlineSub);
            }
            base.Dispose(disposing);
        }
        string? connectionId = null;
        string? serviceKey = null;
        string? actionKey = null;
        string? data = null;
        string dataDescription = "数据内容";
        string actionDescription = string.Empty;
        private bool inputDataBox = false;
        private async Task OnClickCallRemoteService(string connectionId, string serviceKey, RemoteServiceAction action)
        {

            this.connectionId = connectionId;
            this.serviceKey = serviceKey;
            this.actionKey = action.ActionKey;
            this.actionDescription= action.ActionDescription;
            this.data = null;
            var f = await ConfirmService.YesNo("操作确认", $"此操作【{this.actionDescription}】，确认要执行码？");
            if (f == ConfirmResult.Yes)
            {
                if (action.SuperInputData)
                {
                    dataDescription = action.DataDescription ?? "数据内容";
                    inputDataBox = true;
                }
                else
                {
                    await CallRemoteService();
                }
            }
        }

        private async Task CallRemoteService()
        {
            if (string.IsNullOrEmpty(connectionId) || string.IsNullOrEmpty(serviceKey) || string.IsNullOrEmpty(actionKey))
            {
                return;
            }
            await systemNotificationSender.CallRemoteService(connectionId, serviceKey, actionKey, data);
        }
    }
}
