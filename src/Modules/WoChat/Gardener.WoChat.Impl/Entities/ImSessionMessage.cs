// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.WoChat.Impl.Entities
{
    /// <summary>
    /// Im会话消息列表
    /// </summary>
    [Table("WoChat" + nameof(ImSessionMessage))]
    public class ImSessionMessage : ImSessionMessageDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
    }
}
