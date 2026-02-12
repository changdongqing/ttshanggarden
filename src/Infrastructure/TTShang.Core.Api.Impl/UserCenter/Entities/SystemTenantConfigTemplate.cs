namespace TTShang.Core.Api.Impl.UserCenter.Entities
{
    /// <summary>
    /// 租户配置模板
    /// </summary>
    public class SystemTenantConfigTemplate : SystemTenantConfigTemplateDto, IEntityBase, IEntityTypeBuilder<SystemTenantConfigTemplate, MasterDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<SystemTenantConfigTemplate> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x=>x.ConfigKey).IsUnique();
        }
    }
}
