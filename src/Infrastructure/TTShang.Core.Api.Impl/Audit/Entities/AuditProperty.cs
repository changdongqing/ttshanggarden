// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Audit.Dtos;

namespace TTShang.Core.Api.Impl.Audit.Entities
{
    /// <summary>
    /// 实体属性审计信息
    /// </summary>
    [IgnoreAudit]
    public class AuditProperty : AuditPropertyDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator, GardenerIgnoreAuditDbContextLocator>
    {
        /// <summary>
        /// 审计实体
        /// </summary>
        public AuditEntity? AuditEntity { get; set; }
    }
}
