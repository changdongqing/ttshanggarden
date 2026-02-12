using TTShang.Core.Authorization.Dtos;

namespace TTShang.Core.Api.Impl.Authorization.Entities
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLog : LoginLogDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
    }
}
