// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Common.DbContextLocators;

namespace TTShang.EasyJob.Impl.Entities
{
    /// <summary>
    /// 任务触发器
    /// </summary>
    public class SysJobTrigger : SysJobTriggerDto, IEntityBase<MasterDbContextLocator, GardenerIgnoreAuditDbContextLocator>
    {
    }
}
