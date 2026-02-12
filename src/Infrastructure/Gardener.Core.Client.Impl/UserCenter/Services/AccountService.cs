// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Impl.UserCenter.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class AccountService :ClientServiceCaller, IAccountService
    {
        public AccountService(IApiCaller apiCaller):base(apiCaller, "account")
        {
        }

        public Task<bool> ChangePassword(ChangePasswordInput changePasswordInput)
        {
            return apiCaller.PostAsync<ChangePasswordInput, bool>($"{this.baseUrl}/change-password", changePasswordInput);
        }

        public Task<UserDto> GetCurrentUser()
        {
            return apiCaller.GetAsync<UserDto>($"{this.baseUrl}/current-user");
        }

        public Task<List<ResourceDto>> GetCurrentUserMenus(string? rootKey = null)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>();
            queryString.Add(nameof(rootKey), rootKey);
            return apiCaller.GetAsync<List<ResourceDto>>($"{this.baseUrl}/current-user-menus", queryString);
        }

        public Task<List<string>> GetCurrentUserResourceKeys(params ResourceType[] resourceTypes)
        {
            List<KeyValuePair<string, object?>> paras = new List<KeyValuePair<string, object?>>();
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                paras.Add(new KeyValuePair<string, object?>("resourceTypes", resourceTypes[i]));
            }
            return apiCaller.GetAsync<List<string>>($"{this.baseUrl}/current-user-resource-keys", paras);
        }

        public Task<List<ResourceDto>> GetCurrentUserResources(params ResourceType[] resourceTypes)
        {
            List<KeyValuePair<string, object?>> paras = new List<KeyValuePair<string, object?>>();
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                paras.Add(new KeyValuePair<string, object?>("resourceTypes", resourceTypes[i]));
            }
            return apiCaller.GetAsync<List<ResourceDto>>($"{this.baseUrl}/current-user-resources", paras);
        }

        public Task<List<RoleDto>> GetCurrentUserRoles()
        {
            return apiCaller.GetAsync<List<RoleDto>>($"{this.baseUrl}/current-user-roles");
        }

        public Task<TokenOutput> Login(LoginInput input)
        {
            var result = apiCaller.PostAsync<LoginInput, TokenOutput>($"{this.baseUrl}/login", input);
            return result;
        }

        public Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            return apiCaller.PostAsync<RefreshTokenInput, TokenOutput>($"{this.baseUrl}/refresh-token", input);
        }

        public Task<bool> RemoveCurrentUserRefreshToken()
        {
            return apiCaller.DeleteAsync<bool>($"{this.baseUrl}/current-user-refresh-token");
        }

        public Task<bool> TestToken(string? flag = null)
        {
            return apiCaller.PostAsync<int, bool>($"{this.baseUrl}/test-token?flag={flag}", 0);
        }

        public Task<bool> UpdateCurrentUserBaseInfo(UserDto user)
        {
            return apiCaller.PutAsync<UserDto, bool>($"{this.baseUrl}/current-user-base-info", user);
        }
    }
}
