// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Impl.UserCenter.Services
{
    [ScopedService]
    public class RoleService : ClientServiceBase<RoleDto>,IRoleService
    {
        public RoleService(IApiCaller apiCaller):base(apiCaller, "role")
        {
        }

        public async Task<bool> DeleteResource(int roleId)
        {
            return await apiCaller.DeleteAsync<bool>($"{this.baseUrl}/{roleId}/resource");
        }

        public async Task<List<ResourceDto>> GetResource(int roleId)
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{this.baseUrl}/{roleId}/resource");
        }

        public async Task<string> GetRoleResourceSeedData()
        {
            return await apiCaller.GetAsync<string>($"{this.baseUrl}/role-resource-seed-data");
        }

        public async Task<bool> Resource(int roleId, Guid[] resourceIds)
        {
            return await apiCaller.PostAsync<Guid[], bool>($"{this.baseUrl}/{roleId}/resource", resourceIds);
        }
        public async Task<PageList<RoleDto>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object?> pramas = new Dictionary<string, object?>() 
            {
                {"name",name }
            };
            return  await apiCaller.GetAsync<PageList<RoleDto>>($"{this.baseUrl}/search/{pageIndex}/{pageSize}", pramas);
        }

    }
}
