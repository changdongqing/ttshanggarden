using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Iot.Impl.Entities
{
    /// <summary>
    /// 设备组
    /// </summary>
    [Table("Iot" + nameof(DeviceGroup))]
    public class DeviceGroup : DeviceGroupDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<DeviceGroup, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 父级
        /// </summary>
        public DeviceGroup? Parent { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public new ICollection<DeviceGroup>? Children { get; set; }

        /// <summary>
        /// 设备集
        /// </summary>
        public ICollection<Device>? Devices { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<DeviceGroup> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
              .HasMany(x => x.Children)
              .WithOne(x => x.Parent)
              .HasForeignKey(x => x.ParentId)
              .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
              .HasMany(x => x.Devices)
              .WithOne(x => x.DeviceGroup)
              .HasForeignKey(x => x.DeviceGroupId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
