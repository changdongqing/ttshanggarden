// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Weighbridge.Impl.Entities
{
    /// <summary>
    /// 称重记录日志
    /// </summary>
    [Table("Wbg" + nameof(WeighingRecordLog))]
    public class WeighingRecordLog : WeighingRecordLogDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<WeighingRecordLog, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<WeighingRecordLog> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => x.WeighingRecordId);
            entityBuilder.HasIndex(x => x.WeighbridgeConfigId);
            entityBuilder.HasIndex(x => x.CreatedTime);
            entityBuilder.HasIndex(x => x.PlateNumber);
        }
    }
}
