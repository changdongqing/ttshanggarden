// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Common.DbContextLocators;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TTShang.EasyJob.Impl.Entities
{
    /// <summary>
    /// 任务运行日志
    /// </summary>
    public class SysJobLog : SysJobLogDto, IEntityBase<MasterDbContextLocator, GardenerIgnoreAuditDbContextLocator>, IEntityTypeBuilder<SysJobLog, MasterDbContextLocator, GardenerIgnoreAuditDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<SysJobLog> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => x.CreatedTime);
            entityBuilder.HasIndex(x => x.JobId);
            entityBuilder.HasIndex(x => x.TriggerId);
        }
    }
}
