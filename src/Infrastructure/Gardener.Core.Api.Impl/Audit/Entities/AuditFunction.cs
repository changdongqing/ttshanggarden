// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Audit.Dtos;
using Gardener.Core.Audit.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Api.Impl.Audit.Entities
{
    /// <summary>
    /// 功能审计信息
    /// </summary>
    [IgnoreAudit]
    public class AuditFunction : AuditFunctionDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator, GardenerIgnoreAuditDbContextLocator>
    {

        /// <summary>
        /// 审计数据信息集合
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.AuditEntities), ResourceType = typeof(AuditLocalResource))]
        public new ICollection<AuditEntity>? AuditEntities { get; set; }
    }
}
