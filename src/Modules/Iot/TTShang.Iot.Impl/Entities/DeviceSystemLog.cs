// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.Iot.Impl.Entities
{
    /// <summary>
    /// 设备系统日志
    /// </summary>
    [Table("Iot" + nameof(DeviceSystemLog))]
    public class DeviceSystemLog : DeviceSystemLogDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator,GardenerIgnoreAuditDbContextLocator>, IEntityTypeBuilder<DeviceSystemLog, MasterDbContextLocator, GardenerMultiTenantDbContextLocator, GardenerIgnoreAuditDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<DeviceSystemLog> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
        }
    }
}
