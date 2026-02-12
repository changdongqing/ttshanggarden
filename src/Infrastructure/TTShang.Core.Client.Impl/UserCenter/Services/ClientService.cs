// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.UserCenter.Services
{
    [ScopedService]
    public class ClientService : ClientServiceBase<ClientDto, Guid>, IClientService
    {
        public ClientService(IApiCaller apiCaller) : base(apiCaller, "client")
        {
        }
        public async Task<List<FunctionDto>> GetFunctions(Guid id)
        {
            return await apiCaller.GetAsync<List<FunctionDto>>($"{this.baseUrl}/{id}/functions");
        }

        public async Task<TokenOutput> Login(ClientLoginInput input)
        {
            var result = await apiCaller.PostAsync<ClientLoginInput, TokenOutput>($"{this.baseUrl}/login", input);
            return result;
        }

        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            return await apiCaller.PostAsync<RefreshTokenInput, TokenOutput>($"{this.baseUrl}/refresh-token", input);
        }
    }
}
