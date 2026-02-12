// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using TTShang.Core.Client.Authorization.Events;

namespace TTShang.Core.Client.Impl.Authorization
{

    /// <summary>
    /// 身份状态管理
    /// </summary>
    public class AuthenticationStateManager : IAuthenticationStateManager
    {
        /// <summary>
        /// 用户Token来自于登录
        /// </summary>
        private bool userTokenFromLogin = false;
        /// <summary>
        /// 登录的时候选中记住我/自动登录时，refre token 记录到 localsession中
        /// </summary>
        private bool isAutoLogin = true;

        /// <summary>
        /// 登录用户信息
        /// </summary>
        private LoginUserInfo? loginUser;

        private AuthSettings authSettings;

        private readonly IAccountService accountService;
        private readonly IEventBus eventBus;
        private readonly ILoginDataAccessor loginDataAccessor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="logger"></param>
        /// <param name="authSettingsOpt"></param>
        /// <param name="navigationManager"></param>
        /// <param name="eventBus"></param>
        /// <param name="loginDataAccessor"></param>
        public AuthenticationStateManager(IAccountService accountService, IOptions<AuthSettings> authSettingsOpt, IEventBus eventBus, ILoginDataAccessor loginDataAccessor)
        {
            authSettings = authSettingsOpt.Value;
            this.accountService = accountService;
            this.eventBus = eventBus;
            this.loginDataAccessor = loginDataAccessor;
        }

        public bool UserTokenFromLogin => userTokenFromLogin;
        private object refreshTokenTaskLockObj = new object();
        private Task<TokenOutput?>? refreshTokenTask = null;
        /// <summary>
        /// 刷新token-防止并发
        /// </summary>
        /// <param name="force">强制刷新</param>
        /// <returns></returns>
        public Task<TokenOutput?> RefreshToken(bool force = false)
        {
            if (refreshTokenTask == null || refreshTokenTask.IsCompleted)
            {
                lock (refreshTokenTaskLockObj)
                {
                    if (refreshTokenTask == null || refreshTokenTask.IsCompleted)
                    {
                        refreshTokenTask = RefreshTokenTask(force);
                    }
                }
            }
            return refreshTokenTask;
        }
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="force">强制刷新</param>
        /// <returns></returns>
        private async Task<TokenOutput?> RefreshTokenTask(bool force = false)
        {
            TokenOutput? currentToken = await LoadLocalToken();
            //未登录
            if (currentToken == null) { await CleanAuthenticationState(); return null; }
            if (!force)
            {
                //RefreshToken已经过期了
                if (currentToken.RefreshTokenExpires < DateTimeOffset.Now.ToUnixTimeSeconds()) { await CleanAuthenticationState(); return null; }
                //AccessToken时间还很充裕
                if (currentToken.AccessTokenExpires - DateTimeOffset.Now.ToUnixTimeSeconds() > authSettings.RefreshTokenTimeThreshold) { return currentToken; }
            }
            //拿到新的token
            var tokenResult = await accountService.RefreshToken(new RefreshTokenInput() { RefreshToken = currentToken.RefreshToken });
            if (tokenResult != null)
            {
                //token 设置
                await SetToken(tokenResult);
                eventBus.Publish(new RefreshTokenSucceedAfterEvent(tokenResult));
                return tokenResult;
            }
            else
            {
                await CleanAuthenticationState();
                return null;
            }
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="token"></param>
        /// <param name="isAutoLogin">自动登录</param>
        /// <returns></returns>
        public async Task<bool> Login(LoginInput loginInput, bool isAutoLogin = true)
        {
            //自动加载
            this.isAutoLogin = isAutoLogin;
            this.userTokenFromLogin = true;
            var token = await accountService.Login(loginInput);
            if (token != null)
            {
                //设置token
                await SetToken(token);
                //加载当前用户信息
                await ReloadCurrentUserInfos();
                //通知状态变更
                notifyAuthenticationStateChangedAction(true,GetCurrentUser());
                //发送一个登录成功事件
                eventBus.Publish(new LoginSucceedAfterEvent(token));
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 注销
        /// </summary>
        public async Task Logout(bool tokenIsAvailable)
        {
            if(tokenIsAvailable)
            {
                await accountService.RemoveCurrentUserRefreshToken();
            }
            await CleanAuthenticationState();
            userTokenFromLogin = false;
            eventBus.Publish(new LogoutSucceedAfterEvent());
        }
        /// <summary>
        /// 清理身份状态信息
        /// </summary>
        private async Task CleanAuthenticationState()
        {
            //删除token
            await RemoveToken();
            //移除loginUser
            loginUser = null;
            notifyAuthenticationStateChangedAction(false,null);

        }
        #region 状态通知
        /// <summary>
        /// 用户身份变化需要调用此通知
        /// </summary>
        /// <remarks>
        /// <see cref="CustomAuthenticationStateProvider"/>
        /// 参数<bool>：是否是登录
        /// 参数<UserDto?>：如果是已登录，传递当前用户信息
        /// </remarks>
        Action<bool, UserDto?> notifyAuthenticationStateChangedAction = (b,user) => { };
        /// <summary>
        /// 设置状态通知回调
        /// </summary>
        /// <param name="c"></param>
        public void SetNotifyAuthenticationStateChangedAction(Action<bool, UserDto?> c)
        {
            notifyAuthenticationStateChangedAction = c;
        }
        #endregion

        #region userinfos
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        public UserDto? GetCurrentUser()
        {
            return loginUser?.User?.Adapt(new UserDto());
        }

        /// <summary>
        /// 重新加载用户相关信息
        /// </summary>
        /// <returns></returns>
        public async Task<LoginUserInfo?> ReloadCurrentUserInfos()
        {
            //刷新了，或者首次登录
            var token = await TryGetToken();
            if (token != null)
            {
                //重新请求user信息
                var task = accountService.GetCurrentUser();
                var task1 = accountService.GetCurrentUserResourceKeys(ResourceType.View, ResourceType.Menu, ResourceType.Action);
                var task2 = accountService.GetCurrentUserMenus(AuthConstant.ClientResourceRootKey);
                var userResult = await task;

                if (userResult != null)
                {
                    var uiResourceKeys = await task1;
                    var menuResources = await task2;
                    loginUser = new LoginUserInfo()
                    {
                        User = userResult,
                        UiResourceKeys = uiResourceKeys,
                        MenuResources = menuResources
                    };
                    await eventBus.PublishAsync(new ReloadCurrentUserEvent(token, loginUser));
                    return loginUser;
                }
            }
            return null;
        }

        /// <summary>
        /// 判断当前用户是否用于资源key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CheckCurrentUserHaveResource(object key)
        {
            if (loginUser == null)
            {
                return false;
            }
            //超级管理员
            if (true == loginUser.User?.IsSuperAdministrator)
            {
                return true;
            }
            if (loginUser.UiResourceKeys == null)
            {
                return false;
            }
            return loginUser.UiResourceKeys.Contains(key);
        }
        /// <summary>
        /// 判断当前用户是否用于资源key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> CheckCurrentUserHaveResourceAsync(object key)
        {
            return Task.FromResult(CheckCurrentUserHaveResource(key));
        }
        /// <summary>
        /// 获取当前用户菜单
        /// </summary>
        /// <returns></returns>
        public List<ResourceDto>? GetCurrentUserMenus()
        {
            return loginUser?.MenuResources;
        }
        #endregion

        #region local token

        /// <summary>
        /// 尝试获取token
        /// </summary>
        /// <remarks>
        /// 先从本地获取，本地没有或不可用，尝试刷新一下再获取
        /// </remarks>
        /// <returns></returns>
        private async Task<TokenOutput?> TryGetToken()
        {
            TokenOutput? token = await LoadLocalToken();
            //无效
            if (token == null) return null;
            //accessToken可用
            if (!string.IsNullOrEmpty(token.AccessToken) && token.AccessTokenExpires - DateTimeOffset.Now.ToUnixTimeSeconds() > authSettings.RefreshTokenTimeThreshold)
            {
                return token;
            }
            else
            {
                ///accessToken不可用，强制刷新后拿到
                return await RefreshToken(true);
            }

        }
        /// <summary>
        /// 当前token缓存
        /// </summary>
        private TokenOutput? currentToken;
        /// <summary>
        /// 获取当前token
        /// </summary>
        /// <returns></returns>
        public TokenOutput? GetCurrentToken()
        {
            return currentToken;
        }
        /// <summary>
        /// 载入本地token
        /// </summary>
        /// <returns></returns>
        public async Task<TokenOutput?> LoadLocalToken()
        {
            (bool, TokenOutput?) t = await loginDataAccessor.Get();
            //自动登录
            this.isAutoLogin = t.Item1;
            if (t.Item2 != null)
            {
                this.currentToken = t.Item2;
            }
            //token
            return t.Item2;
        }
        /// <summary>
        /// token 设置到浏览器缓存和httpclient
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        private Task SetToken(TokenOutput token)
        {
            currentToken = token;
            return loginDataAccessor.Set(isAutoLogin, token);
        }
        /// <summary>
        ///  移除浏览器token缓存
        /// </summary>
        private Task RemoveToken()
        {
            currentToken = null;
            //clear
            return loginDataAccessor.Remove(isAutoLogin);
        }
        #endregion
        /// <summary>
        /// 获取当前身份的token头，可以添加于自定义的httpclient中验证使用
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>?> GetCurrentTokenHeaders()
        {
            var tokenOutput = await LoadLocalToken();
            if (tokenOutput == null)
            {
                return null;
            }
            var hearder = new AuthenticationHeaderValue(GardenerAuthenticationSchemes.User, tokenOutput.AccessToken);
            return new Dictionary<string, string>
            {
                {"Authorization",hearder.ParseToString() ?? string.Empty }
            };
        }
        /// <summary>
        /// 测试token是否可用
        /// </summary>
        /// <param name="flag">标记</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>服务端不执行任何内容，token无效将返回响应401；</para>
        /// <para>在特殊位置，不通过容器中的HttpClient调用接口，无法实现token的被动刷新，就需要调用该方法去触发一下；</para>
        /// <para>当然通过调用的其他需校验token的接口也可以达到该效果；</para>
        /// </remarks>
        public async Task<bool> TestToken(string? flag = null)
        {
            if (currentToken == null) 
            {
                return false;
            }
            return await accountService.TestToken(flag);
        }

        /// <summary>
        /// 是否是租户
        /// </summary>
        /// <remarks>
        /// <para>如果用户租户编号不为null或空认为是租户</para>
        /// </remarks>
        /// <returns></returns>
        public bool CurrentUserIsTenant()
        {
            return loginUser?.CurrentUserIsTenant ?? true;
        }
        /// <summary>
        /// 是否是租户管理员
        /// </summary>
        /// <remarks>
        /// 是否分配资源 <see cref="CommonResourceKeys.SystemTenantAdministratorKey"/>
        /// </remarks>
        /// <returns></returns>
        public bool CurrentUserIsTenantAdministrator()
        {
            return !CurrentUserIsTenant() && CheckCurrentUserHaveResource(CommonResourceKeys.SystemTenantAdministratorKey);
        }

        /// <summary>
        /// 获取第一个可现实的菜单地址
        /// </summary>
        /// <returns></returns>
        public string? GetCurrentUserFirstMenu()
        {
            if (this.loginUser?.MenuResources == null || !this.loginUser.MenuResources.Any()) return null;

            string? GetMenu(ICollection<ResourceDto> resources)
            {
                foreach (var item in resources)
                {
                    if (!item.Hide && !string.IsNullOrEmpty(item.Path))
                    {
                        return item.Path;
                    }
                    if (item.Children != null && item.Children.Any())
                    {
                        var path = GetMenu(item.Children);
                        if (!string.IsNullOrEmpty(path))
                        {
                            return path;
                        }
                    }
                }
                return null;
            }
            return GetMenu(this.loginUser.MenuResources);
        }
    }
}
