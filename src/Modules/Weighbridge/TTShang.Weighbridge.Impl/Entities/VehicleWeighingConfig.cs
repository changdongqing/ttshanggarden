// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TTShang.Weighbridge.Impl.Entities
{
    /// <summary>
    /// 车辆称重配置
    /// </summary>
    public class VehicleWeighingConfig : VehicleWeighingConfigDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<VehicleWeighingConfig, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<VehicleWeighingConfig> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => x.PlateNumber).IsUnique();
        }
    }
}
