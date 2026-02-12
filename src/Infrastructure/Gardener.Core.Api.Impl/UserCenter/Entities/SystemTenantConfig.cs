namespace Gardener.Core.Api.Impl.UserCenter.Entities
{
    /// <summary>
    /// 租户配置
    /// </summary>
    public class SystemTenantConfig : SystemTenantConfigDto, IEntityBase, IEntityTypeBuilder<SystemTenantConfig, MasterDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<SystemTenantConfig> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex([nameof(SystemTenantConfig.TenantId), nameof(SystemTenantConfig.ConfigKey)]).IsUnique();
        }
    }
}
