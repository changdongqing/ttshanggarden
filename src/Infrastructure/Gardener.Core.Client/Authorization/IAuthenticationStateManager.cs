// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Authorization
{
    /// <summary>
    /// 身份状态管理
    /// </summary>
    public interface IAuthenticationStateManager
    {
        /// <summary>
        /// 用户Token来自于登录
        /// </summary>
        bool UserTokenFromLogin {  get; }
        /// <summary>
        /// 载入本地token
        /// </summary>
        /// <returns></returns>
        Task<TokenOutput?> LoadLocalToken();
        /// <summary>
        /// 获取当前token
        /// </summary>
        /// <returns></returns>
        TokenOutput? GetCurrentToken();
        /// <summary>
        /// 判断当前用户是否有该资源权限
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool CheckCurrentUserHaveResource(object key);
        /// <summary>
        /// 判断当前用户是否有该资源权限
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> CheckCurrentUserHaveResourceAsync(object key);
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        UserDto? GetCurrentUser();

        /// <summary>
        /// 获取当前用户的菜单
        /// </summary>
        /// <returns></returns>
        List<ResourceDto>? GetCurrentUserMenus();
        /// <summary>
        /// 重新加载用户相关信息
        /// </summary>
        /// <returns></returns>
        Task<LoginUserInfo?> ReloadCurrentUserInfos();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInput"></param>
        /// <param name="isAutoLogin"></param>
        /// <returns></returns>
        Task<bool> Login(LoginInput loginInput, bool isAutoLogin = true);
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        Task Logout(bool tokenIsAvailable);
        /// <summary>
        /// 设置一个身份验证状态变化的回调
        /// </summary>
        /// <param name="c"></param>
        void SetNotifyAuthenticationStateChangedAction(Action<bool, UserDto?> c);
        /// <summary>
        /// 获取当前身份的token头，可以添加于自定义的httpclient中验证使用
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, string>?> GetCurrentTokenHeaders();
        /// <summary>
        /// 同步刷新token
        /// </summary>
        /// <param name="force">强制刷新</param>
        /// <returns></returns>
        Task<TokenOutput?> RefreshToken(bool force = false);
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
        Task<bool> TestToken(string? flag = null);

        /// <summary>
        /// 是否是租户
        /// </summary>
        /// <remarks>
        /// <para>如果用户租户编号不为null或空认为是租户</para>
        /// </remarks>
        /// <returns></returns>
        bool CurrentUserIsTenant();

        /// <summary>
        /// 是否是租户管理员
        /// </summary>
        /// <remarks>
        /// 是否分配资源 <see cref="CommonResourceKeys.SystemTenantAdministratorKey"/>
        /// </remarks>
        /// <returns></returns>
        bool CurrentUserIsTenantAdministrator();

        /// <summary>
        /// 获取第一个可现实的菜单地址
        /// </summary>
        /// <returns></returns>
        string? GetCurrentUserFirstMenu();
    }
}
