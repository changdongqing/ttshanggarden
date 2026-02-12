// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Authorization.Entities;
using Gardener.Core.Authorization.Dtos;
using Gardener.Core.Authorization.Services;

namespace Gardener.Core.Api.Impl.Authorization.Services
{
    /// <summary>
    /// 用户登录TOKEN服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class LoginTokenService : ServiceBase<LoginToken, LoginTokenDto, Guid, GardenerMultiTenantDbContextLocator>, ILoginTokenService
    {
        private readonly IRepository<LoginToken, GardenerMultiTenantDbContextLocator> _loginTokenRepository;
        /// <summary>
        /// 用户登录TOKEN服务
        /// </summary>
        /// <param name="repository"></param>
        public LoginTokenService(IRepository<LoginToken, GardenerMultiTenantDbContextLocator> repository) : base(repository)
        {
            _loginTokenRepository = repository;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<PageList<LoginTokenDto>> Search(PageRequest request)
        {
            IQueryable<LoginToken> queryable = base.GetSearchQueryable(request.FilterGroups)
                .Where(u => u.IsDeleted == false);
            return await queryable
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<LoginTokenDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }
    }
}
