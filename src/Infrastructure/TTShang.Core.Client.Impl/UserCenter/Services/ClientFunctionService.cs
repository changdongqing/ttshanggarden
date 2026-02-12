// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.UserCenter.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class ClientFunctionService :ClientServiceCaller, IClientFunctionService
    {
        public ClientFunctionService(IApiCaller apiCaller):base(apiCaller, "client-function")
        {
        }
        public async Task<bool> Add(List<ClientFunctionDto> clientFunctionDtos)
        {
            return await apiCaller.PostAsync<List<ClientFunctionDto>, bool>($"{this.baseUrl}", clientFunctionDtos);
        }

        public async Task<bool> Delete(Guid clientId, Guid functionId)
        {
            return await apiCaller.DeleteAsync<bool>($"{this.baseUrl}/{clientId}/{functionId}");
        }
    }
}
