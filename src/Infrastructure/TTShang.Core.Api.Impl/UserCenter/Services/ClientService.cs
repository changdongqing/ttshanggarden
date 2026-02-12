// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DataEncryption;
using TTShang.Core.Api.Impl.UserCenter.Entities;
using TTShang.Core.Authorization.Dtos;
using TTShang.Core.Authorization.Services;
using TTShang.Core.UserCenter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace TTShang.Core.Api.Impl.UserCenter.Services
{
    /// <summary>
    /// 客户端服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class ClientService : ServiceBase<Client, ClientDto, Guid>, IClientService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtBearerService;
        private readonly IRepository<ClientFunction> _clientFunctionRespository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtBearerService"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="repository"></param>
        /// <param name="clientFunctionRespository"></param>
        public ClientService(IJwtService jwtBearerService, IHttpContextAccessor httpContextAccessor, IRepository<Client> repository, IRepository<ClientFunction> clientFunctionRespository) : base(repository)
        {
            _jwtBearerService = jwtBearerService;
            _httpContextAccessor = httpContextAccessor;
            _clientFunctionRespository = clientFunctionRespository;
        }
        /// <summary>
        /// 根据客户端编号获取绑定的接口列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<FunctionDto>> GetFunctions([ApiSeat(ApiSeats.ActionStart)] Guid id)
        {
            return await _clientFunctionRespository.AsQueryable(false)
                  .Include(x => x.Function)
                  .Where(x => x.ClientId.Equals(id))
                  .Select(x => x.Function)
                  .Where(x => x.IsDeleted == false && x.IsLocked == false)
                  .Select(x => x.Adapt<FunctionDto>())
                  .ToListAsync();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>登录接口</remarks>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<TokenOutput> Login(ClientLoginInput input)
        {
            long currentTimespan = DateTimeOffset.Now.ToUnixTimeSeconds();

            //校验时间戳
            if (input.Timespan <= 0 || input.Timespan > currentTimespan || input.Timespan < currentTimespan - 120)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Timespan_Is_Expired);
            }

            Client? client = _repository.AsQueryable(false).Where(x => x.Id.Equals(input.ClientId) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefault();
            if (client == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Client_No_Find);
            }

            //加密对比
            bool flag = MD5Encryption.Compare((input.ClientId + client.SecretKey + input.Timespan).ToUpper(), input.EncryptionValue.ToUpper(), true);
            if (!flag)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Client_Login_Fail);
            }

            Identity identity = new Identity(client.Id.ToString(),IdentityType.Client,LoginClientType.Server,Guid.NewGuid().ToString())
            {
                NickName = client.Name
            };

            var token = await _jwtBearerService.CreateToken(identity);
            return token.Adapt<TokenOutput>();
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <remarks>
        /// 通过刷新token获取新的token
        /// </remarks>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            var token = await _jwtBearerService.RefreshToken(input.RefreshToken);
            return token.Adapt<TokenOutput>();
        }
    }
}
