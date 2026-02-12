// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Impl.UserCenter.Pages.UserView;

namespace TTShang.Core.Client.Impl.UserCenter.Pages.AccountView.SettingsView
{
    public partial class BaseView : OperationDialogBase<int, bool, UserCenterResource>
    {
        private UserDto? _currentUser;

        [Inject]
        protected IAuthenticationStateManager AuthenticationStateManager { get; set; } = null!;
        [Inject]
        protected IAccountService AccountService { get; set; } = null!;
        [Inject]
        protected IClientNotifier Notifier { get; set; } = null!;

        private bool _saveBtnLoading = false;

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationStateManager.ReloadCurrentUserInfos();
            _currentUser = AuthenticationStateManager.GetCurrentUser();
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// 点击头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task OnAvatarClick(UserDto user)
        {
            string avatarDrawerWidth = "300";
            await OpenOperationDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(
                Localizer[nameof(SharedLocalResource.UplaodAvatar)],
                new UserUploadAvatarParams(user, false),
                width: avatarDrawerWidth,
                onClose: (r) =>
                {
                    return base.RefreshPageDom();
                });
        }

        /// <summary>
        /// 点击保存用户基本信息
        /// </summary>
        /// <returns></returns>
        private async Task SaveUserBaseInfo()
        {
            if (_currentUser == null) return;

            _saveBtnLoading = true;

            bool result = await AccountService.UpdateCurrentUserBaseInfo(_currentUser);

            if (result)
            {
                Notifier.Success(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Success)));
            }
            else
            {
                Notifier.Error(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Error)));
            }
            _saveBtnLoading = false;
        }
    }
}