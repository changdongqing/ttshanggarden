// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TTShang.Core.Client.Impl.Authorization
{
    /// <summary>
    /// 自定义验证状态提供器
    /// </summary>
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthenticationStateManager authenticationStateManager;
        private readonly IClientLogger logger;
        private readonly ILocalizationLocalizer localizer;
        public CustomAuthenticationStateProvider(IAuthenticationStateManager authenticationStateManager, IClientLogger logger, ILocalizationLocalizer localizer)
        {
            this.authenticationStateManager = authenticationStateManager;
            authenticationStateManager.SetNotifyAuthenticationStateChangedAction(Refresh);
            this.logger = logger;
            this.localizer = localizer;
        }
        /// <summary>
        /// 刷新页面后会执行
        /// </summary>
        /// <returns></returns>
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            try
            {
                //取不到
                var user = authenticationStateManager.GetCurrentUser();
                if (user == null)
                {
                    //尝试刷新
                    var loginUser = await authenticationStateManager.ReloadCurrentUserInfos();
                    user = loginUser?.User;
                }
                //还是取不到
                if (user == null)
                {
                    logger.Error(localizer[nameof(SharedLocalResource.User_Info_Get_Error_Retry_Login)]);
                    return authenticationState;
                }
                else
                {
                    authenticationState = CreateAuthenticationState(user);
                    return authenticationState;
                }
            }
            catch (Exception ex)
            {
                logger.Error(localizer[nameof(SharedLocalResource.User_Info_Get_Error_Retry_Login)], ex: ex);
                return authenticationState;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isLogin">是否是登录</param>
        /// <param name="currentUser">当前登录后的用户</param>
        public void Refresh(bool isLogin, UserDto? currentUser)
        {
            if (!isLogin)
            {
                AuthenticationState authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
                return;
            }
            if (currentUser != null)
            {
                //有用户，直接使用
                NotifyAuthenticationStateChanged(Task.FromResult(CreateAuthenticationState(currentUser)));
            }
            else
            {
                //无用户，尝试去获取
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }
        /// <summary>
        /// 根据 userdto 创建 AuthenticationState
        /// </summary>
        /// <returns></returns>
        private AuthenticationState CreateAuthenticationState(UserDto currentUser)
        {
            if (currentUser == null) return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            Claim[] claims =
            [
                new Claim(ClaimTypes.Name, currentUser.NickName ?? currentUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString())
            ];
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth"));
            return new AuthenticationState(authenticatedUser);
        }
    }
}
