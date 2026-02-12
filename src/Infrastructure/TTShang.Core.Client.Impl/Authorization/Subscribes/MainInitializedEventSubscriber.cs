// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Authorization.Resources;
using TTShang.Core.Authorization.Services;
using TTShang.Core.Client.Shared;

namespace TTShang.Core.Client.Impl.Authorization.Subscribes
{
    /// <summary>
    /// 模板页初始化完成
    /// </summary>
    [ScopedService]
    public class MainInitializedEventSubscriber : EventSubscriberBase<MainInitializedEvent>
    {
        private readonly ILoginLogService loginLogService;
        private readonly INotificationService noticeService;
        private readonly IAuthenticationStateManager authenticationStateManager;
        private readonly ILocalizationLocalizer<AuthorizationLocalResource> localizer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginLogService"></param>
        /// <param name="noticeService"></param>
        /// <param name="authenticationStateManager"></param>
        /// <param name="localizer"></param>
        public MainInitializedEventSubscriber(ILoginLogService loginLogService, INotificationService noticeService, IAuthenticationStateManager authenticationStateManager, ILocalizationLocalizer<AuthorizationLocalResource> localizer)
        {
            this.loginLogService = loginLogService;
            this.noticeService = noticeService;
            this.authenticationStateManager = authenticationStateManager;
            this.localizer = localizer;
        }

        public override async Task CallBack(MainInitializedEvent e)
        {
            if (!authenticationStateManager.UserTokenFromLogin)
            {
                return;
            }
            LoginLogDto? loginLogDto = await loginLogService.GetUserLastLoginLog();
            if (loginLogDto == null)
            {
                return;
            }
            //RenderFragment render = b =>
            //{
            //    b.AddContent<LoginLogDto>(1, TTShang.Core.Client.Impl.Authorization.Pages.LoginLogView.LoginLogTips, loginLogDto);

            //};


#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            noticeService.Open(new NotificationConfig()
            {
                Message = localizer[nameof(AuthorizationLocalResource.LastLoginLog)],
                Description = LoginLogNotification.NotificationContent(loginLogDto),
                NotificationType = NotificationType.Info,
                Placement = NotificationPlacement.BottomRight,
                Duration = 15
            });
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
        }
    }
}
