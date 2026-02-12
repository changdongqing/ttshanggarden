// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Iot.Impl.Entities
{
    /// <summary>
    /// 设备
    /// </summary>
    [Table("Iot" + nameof(Device))]
    public class Device : DeviceDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Device, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<Device, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 设备组信息
        /// </summary>
        public new DeviceGroup? DeviceGroup { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public new Product? Product { get; set; }

        /// <summary>
        /// 设备标签集
        /// </summary>
        public new ICollection<DeviceTag>? DeviceTags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<Device> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => x.ClientId).IsUnique();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Device> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return [
                new Device(){ 
                    Id=Guid.Parse("8d24834a-2933-4efa-a1aa-796d0151674e"),
                    Name="地磅T2001V4ZOX9KNN06XJ9",
                    Alias="L1",
                    ProductId=Guid.Parse("9cef1579-b9a3-4ea6-a936-ab26b701bcd5"),
                    ClientId="TRANSDUCER-XVQX-410048",
                    Account="wqeda",
                    SecretKey="UYsq5uveECGUw63J1vUihH",
                    CreatedTime=DateTimeOffset.Now
                },
                new Device(){
                    Id=Guid.Parse("c7577810-ade6-4365-afc7-f72fb8e11d6b"),
                    Name="地磅T200P1EXWP631OE9GKO",
                    Alias="R1",
                    ProductId=Guid.Parse("9cef1579-b9a3-4ea6-a936-ab26b701bcd5"),
                    ClientId="WT200-EMQA-1304690576750280704",
                    Account="wpmbv",
                    SecretKey="0GBW6NXuZaXmxWGC03k",
                    CreatedTime=DateTimeOffset.Now
                }
                ,
                new Device(){
                    Id=Guid.Parse("4a63d763-cae2-41bb-889d-e575ddcd2f07"),
                    Name="地磅T20020O1QXZ18ZEQW89",
                    Alias="L2",
                    ProductId=Guid.Parse("9cef1579-b9a3-4ea6-a936-ab26b701bcd5"),
                    ClientId="TRANSDUCER-ILJX-1133007872",
                    Account="msesz",
                    SecretKey="Jzvjd6KfW1WAuqkgj8ZMLf3k",
                    CreatedTime=DateTimeOffset.Now
                },
                new Device(){
                    Id=Guid.Parse("5eb42d9c-aff9-4ff8-a01d-89dcc9b23023"),
                    Name="地磅T200L1YLNJ9Z49VJVW6",
                    Alias="R2",
                    ProductId=Guid.Parse("9cef1579-b9a3-4ea6-a936-ab26b701bcd5"),
                    ClientId="WT200-TBPG-1289877091751297024",
                    Account="namaq",
                    SecretKey="2MykV6TEHEmyXZrcm06D",
                    CreatedTime=DateTimeOffset.Now
                }
                ];
        }
    }
}
