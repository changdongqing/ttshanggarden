// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.Iot.Impl.Entities
{
    /// <summary>
    /// 设备标签
    /// </summary>
    [Table("Iot" + nameof(DeviceTag))]
    public class DeviceTag : DeviceTagDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<DeviceTag, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 设备
        /// </summary>
        public Device Device { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<DeviceTag> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(e => new { e.DeviceId, e.Tag });
            entityBuilder
                .HasOne(e => e.Device)
                .WithMany(e => e.DeviceTags)
                .HasForeignKey(e => e.DeviceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
