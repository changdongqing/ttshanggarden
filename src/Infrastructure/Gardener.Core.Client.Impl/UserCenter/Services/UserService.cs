// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Impl.UserCenter.Services
{
    [ScopedService]
    public class UserService : ClientServiceBase<UserDto>,IUserService
    {
        public UserService(IApiCaller apiCaller):base(apiCaller, "user")
        {
        }

        public Task<List<ResourceDto>> GetResources(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoleDto>> GetRoles(int userId)
        {
            return await apiCaller.GetAsync<List<RoleDto>>($"{this.baseUrl}/{userId}/roles");
        }
       
        public async Task<PageList<UserDto>> Search(int[] deptIds, int pageIndex = 1, int pageSize = 10)
        {
            List<KeyValuePair<string, object?>> pramas = deptIds.ConvertToQueryParameters("deptIds");

            return await apiCaller.GetAsync<PageList<UserDto>>($"{this.baseUrl}/search/{pageIndex}/{pageSize}", pramas);
        }

        public async Task<bool> Role(int userId, int[] roleIds)
        {
            return await apiCaller.PostAsync<int[],bool>($"{this.baseUrl}/{userId}/role", roleIds);
        }

        public async Task<bool> UpdateAvatar(UpdateUserAvatarInput input)
        {
            return await apiCaller.PutAsync<UpdateUserAvatarInput, bool>($"{this.baseUrl}/avatar", input);
        }

        public async Task<string> GetCurrentUserId()
        {
            return await apiCaller.GetAsync<string>($"{this.baseUrl}/current-user-id");
        }

        public Task<List<UserDto>> GetUsers(IEnumerable<int> userIds)
        {
            List<KeyValuePair<string, object?>> values = new List<KeyValuePair<string, object?>>();
            foreach (int userId in userIds)
            {
                values.Add(new KeyValuePair<string, object?>("userIds", userId));
            }
            return apiCaller.GetAsync<List<UserDto>>($"{this.baseUrl}/users", values);
        }
    }
}
