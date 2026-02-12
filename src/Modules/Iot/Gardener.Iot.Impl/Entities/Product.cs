using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Iot.Impl.Entities
{
    /// <summary>
    /// 产品
    /// </summary>
    [Table("Iot" + nameof(Product))]
    public class Product : ProductDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Product, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<Product, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
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
        public void Configure(EntityTypeBuilder<Product> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => x.ProductName).IsUnique();

            entityBuilder
           .HasMany(x => x.Devices)
           .WithOne(x => x.Product)
           .HasForeignKey(x => x.ProductId)
           .OnDelete(DeleteBehavior.NoAction);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Product> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return [
                
                new Product(){
                    Id=Guid.Parse("304168d3-bd81-4e97-ac6a-9095971a70ab"),
                    ProductName="地磅T100",
                    ProductType="weighbridge-t100",
                    ProductImages="[{\"Id\":\"67393fc2-1b32-4cc8-beff-5c0bf5c6b08c\",\"FileName\":\"\\u5FAE\\u4FE1\\u56FE\\u7247_20241009164033.jpg\",\"Url\":\"http://47.94.212.176:20248/upload/iotproduct/20241009/21f3a16c-8b1b-4525-bdae-894e66b79cb3.jpg\"},{\"Id\":\"c23a0afd-6083-4cad-b7ee-5a62d0bf4e30\",\"FileName\":\"\\u5FAE\\u4FE1\\u56FE\\u7247_20241009164045.jpg\",\"Url\":\"http://47.94.212.176:20248/upload/iotproduct/20241009/76dbce0e-43d0-4411-8d3b-d9f0b2a10774.jpg\"},{\"Id\":\"42e1514e-d563-400b-8ac8-da4a9a610804\",\"FileName\":\"\\u5FAE\\u4FE1\\u56FE\\u7247_20241009164050.jpg\",\"Url\":\"http://47.94.212.176:20248/upload/iotproduct/20241009/b253d09c-a71e-4e4d-8156-0126367171d7.jpg\"}]\r\n",
                    ProductVersion="1",
                    EmpowerAllTenants=true,
                    CreatedTime=DateTimeOffset.Now
                },
                 new Product(){
                    Id=Guid.Parse("9cef1579-b9a3-4ea6-a936-ab26b701bcd5"),
                    ProductName="地磅T200",
                    ProductType="weighbridge-t200",
                    ProductImages="[{\"Id\":\"8498d262-9b3f-4b9a-933d-f6f61b229829\",\"FileName\":\"\\u5FAE\\u4FE1\\u56FE\\u7247_20241009164033.jpg\",\"Url\":\"http://47.94.212.176:20248/upload/iotproduct/20241009/a087649a-2805-436c-8bd0-95ba73ebf3b2.jpg\"},{\"Id\":\"85551d8e-6a76-4585-acae-0fbfc3ba1dd5\",\"FileName\":\"\\u5FAE\\u4FE1\\u56FE\\u7247_20241009164045.jpg\",\"Url\":\"http://47.94.212.176:20248/upload/iotproduct/20241009/9b319789-fd46-499a-a1dc-f72c01ad92a7.jpg\"},{\"Id\":\"b2330bc4-ced3-4df6-927e-34779570327f\",\"FileName\":\"\\u5FAE\\u4FE1\\u56FE\\u7247_20241009164050.jpg\",\"Url\":\"http://47.94.212.176:20248/upload/iotproduct/20241009/4e02171e-09c3-438b-9068-28e6f2912109.jpg\"}]\r\n",
                    ProductVersion="1",
                    EmpowerAllTenants=true,
                    CreatedTime=DateTimeOffset.Now
                }

                ];
        }
    }
}
