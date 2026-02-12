using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.Weighbridge.Impl.Entities
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    [Table("Wbg" + nameof(VehicleType))]
    public class VehicleType : VehicleTypeDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<VehicleType, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<VehicleType> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return [
                new VehicleType(){
                    Id = 1,
                    Name="其他",
                    Axles=0,
                    MaximumLoad=0,
                    TenantId=Guid.Parse("5841a727-9b01-4782-ae49-c0b000e4cbe4")
                },
                new VehicleType(){
                    Id = 2,
                    Name="二轴",
                    Axles=2,
                    MaximumLoad=15,
                    TenantId=Guid.Parse("5841a727-9b01-4782-ae49-c0b000e4cbe4")
                }, new VehicleType(){
                    Id = 3,
                    Name="三轴",
                    Axles=3,
                    MaximumLoad=20,
                    TenantId=Guid.Parse("5841a727-9b01-4782-ae49-c0b000e4cbe4")
                }, new VehicleType(){
                    Id = 4,
                    Name="四轴",
                    Axles=4,
                    MaximumLoad=50,
                    TenantId=Guid.Parse("5841a727-9b01-4782-ae49-c0b000e4cbe4")
                }, new VehicleType(){
                    Id = 5,
                    Name="五轴",
                    Axles=5,
                    MaximumLoad=60,
                    TenantId=Guid.Parse("5841a727-9b01-4782-ae49-c0b000e4cbe4")
                }, new VehicleType(){
                    Id = 6,
                    Name="六轴",
                    Axles=6,
                    MaximumLoad=80,
                    TenantId=Guid.Parse("5841a727-9b01-4782-ae49-c0b000e4cbe4")
                }
                ];
        }
    }
}
