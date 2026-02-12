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
    /// 登录日志服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class LoginLogService : ServiceBase<LoginLog, LoginLogDto, Int64, GardenerMultiTenantDbContextLocator>, ILoginLogService
    {
        private readonly IAuthService _authorizationManager;
        /// <summary>
        /// 登录日志服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="authorizationManager"></param>
        public LoginLogService(IRepository<LoginLog, GardenerMultiTenantDbContextLocator> repository, IAuthService authorizationManager) : base(repository)
        {
            _authorizationManager = authorizationManager;
        }

        /// <summary>
        /// 获取用户上一次登录信息
        /// </summary>
        /// <remarks>
        /// 获取当前用户上一次登录信息
        /// </remarks>
        /// <returns></returns>
        public async Task<LoginLogDto?> GetUserLastLoginLog()
        {
            Identity? identity = _authorizationManager.GetIdentity();
            if (identity == null)
            {
                return null;
            }

            LoginLogDto? loginLog = await _repository.AsQueryable(false).Where(x => identity.Id.Equals(x.IdentityId) && x.IdentityType.Equals(identity.IdentityType) && !identity.LoginId.Equals(x.LoginId)).OrderByDescending(x => x.LoginTime).Select(x => x.Adapt<LoginLogDto>()).FirstOrDefaultAsync();

            return loginLog;

        }
    }
}
