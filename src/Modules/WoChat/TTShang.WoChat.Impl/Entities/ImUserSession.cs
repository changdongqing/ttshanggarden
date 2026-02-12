// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.WoChat.Impl.Entities
{
    /// <summary>
    /// 用户会话列表
    /// </summary>
    [Table("WoChat" + nameof(ImUserSession))]
    public class ImUserSession : ImUserSessionDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
    }
}
