// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Authorization.Dtos;

namespace Gardener.Core.Api.Impl.Authorization.Entities
{
    /// <summary>
    /// 登录Token信息
    /// </summary>
    [IgnoreAudit]
    public class LoginToken : LoginTokenDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
    }
}
