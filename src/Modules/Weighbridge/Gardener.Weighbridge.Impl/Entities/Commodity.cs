using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gardener.Weighbridge.Impl.Entities
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public class Commodity : CommodityDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Commodity, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<Commodity> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => new { x.TenantId, x.CommodityCode }).IsUnique();
            entityBuilder.HasIndex(x => new { x.TenantId, x.CommodityName }).IsUnique();
        }
    }
}
