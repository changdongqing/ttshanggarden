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
    /// 地磅设备配置
    /// </summary>
    public class WeighbridgeDeviceConfig : WeighbridgeDeviceConfigDto, IEntityBase<MasterDbContextLocator>, IEntityTypeBuilder<WeighbridgeDeviceConfig, MasterDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<WeighbridgeDeviceConfig> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(e => e.DeviceId).IsUnique();
        }
    }
}
