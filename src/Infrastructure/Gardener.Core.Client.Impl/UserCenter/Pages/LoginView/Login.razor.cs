// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.VerifyCode;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Gardener.Core.Client.Impl.UserCenter.Pages.LoginView
{
    public partial class Login
    {
        private Form<LoginInput>? form;
        private bool loading = false;
        private bool autoLogin = true;
        private LoginInput loginInput = new LoginInput();
        private ImageVerifyCode? _imageVerifyCode;
        private string? returnUrl;

        [Inject]
        private IClientMessageService MessageService { get; set; } = null!;
        [Inject]
        private IAccountService AccountService { get; set; } = null!;
        [Inject]
        private NavigationManager Navigation { get; set; } = null!;
        [Inject]
        private IAuthenticationStateManager AuthenticationStateManager { get; set; } = null!;
        [Inject]
        private ILocalizationLocalizer<UserCenterResource> Localizer { get; set; } = null!;
        [Inject]
        private AppClientInfo appClientInfo {  get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            var url = new Uri(Navigation.Uri);
            var query = url.Query;
            if (QueryHelpers.ParseQuery(query).TryGetValue("returnUrl", out StringValues value))
            {
                if (!value.Equals(Navigation.Uri) && !value.Equals("/"))
                {
                    returnUrl = value;
                }
            }
            //已登录
            var user = AuthenticationStateManager.GetCurrentUser();
            if (user != null)
            {
                if (await AuthenticationStateManager.TestToken())
                {
                    var firstMenuPath = AuthenticationStateManager.GetCurrentUserFirstMenu() ?? "/";
                    Navigation.NavigateTo(returnUrl ?? firstMenuPath);
                }
            }
        }

        private void OnVerifyCodeInputed(string code)
        {
            if (_imageVerifyCode == null)
            {
                return;
            }
            var codeOutput = _imageVerifyCode.GetCodeOutput();
            if (codeOutput == null || codeOutput.CodeLength == null)
            {
                return;
            }
            if (loginInput.VerifyCode.Length == codeOutput.CodeLength)
            {
                form?.Submit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task OnLogin()
        {
            loading = true;
            if (loginInput.UserName != null)
            {
                loginInput.UserName = loginInput.UserName.Trim();
            }
            loginInput.ClientName = appClientInfo.AppName;
            loginInput.ClientVersion = appClientInfo.CurrentVersioName;
            var loginResult = await AuthenticationStateManager.Login(loginInput);
            if (loginResult)
            {
                var firstMenuPath = AuthenticationStateManager.GetCurrentUserFirstMenu() ?? "/";
                Navigation.NavigateTo(returnUrl ?? firstMenuPath);
                loading = false;
            }
            else
            {
                loading = false;
                MessageService.Error(Localizer.Combination(nameof(UserCenterResource.Login), nameof(SharedLocalResource.Fail)));
                if (_imageVerifyCode != null)
                {
                    await _imageVerifyCode.ReLoadVerifyCode();
                }
                //await InvokeAsync(StateHasChanged);
            }

        }
    }

}
