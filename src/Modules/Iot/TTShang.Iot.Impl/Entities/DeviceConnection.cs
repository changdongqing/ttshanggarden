// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.Iot.Impl.Entities
{
    /// <summary>
    /// 设备连接信息
    /// </summary>
    [Table("Iot" + nameof(DeviceConnection))]
    public class DeviceConnection : DeviceConnectionDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator, GardenerIgnoreAuditDbContextLocator>, IEntityTypeBuilder<DeviceConnection, MasterDbContextLocator, GardenerMultiTenantDbContextLocator, GardenerIgnoreAuditDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<DeviceConnection> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => new { x.DeviceClientId, x.DeviceConnectionState });
            entityBuilder.HasIndex(x => new { x.DeviceId, x.DeviceConnectionState });
        }
    }
}
