// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Common.DbContextLocators;
using System.ComponentModel.DataAnnotations;

namespace TTShang.EasyJob.Impl.Entities
{
    /// <summary>
    /// 任务详情
    /// </summary>
    public class SysJobDetail : SysJobDetailDto, IEntityBase<MasterDbContextLocator, GardenerIgnoreAuditDbContextLocator>
    {
        /// <summary>
        /// 触发器集合
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.JobTriggers), ResourceType = typeof(EasyJobLocalResource))]
        public new IEnumerable<SysJobDetail>? JobTriggers { get; set; }
    }
}
