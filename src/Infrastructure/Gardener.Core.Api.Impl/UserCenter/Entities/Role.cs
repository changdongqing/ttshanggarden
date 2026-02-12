// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Api.Impl.UserCenter.Entities
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role : RoleDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<Role, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Role, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<RoleResource> RoleResources { get; set; } = new List<RoleResource>();


        /// <summary>
        /// 配置多对多关系
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Role> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex([nameof(Role.TenantId), nameof(Role.Name)]).IsUnique();
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Role> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
                new Role
                {
                    Id=1,Name="超级管理员",Remark="拥有所有权限",IsSuperAdministrator=true,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)
                },
                new Role
                {
                    Id=2,Name="浏览者",Remark="只能浏览",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)
                },
                new Role
                {
                    Id=3,Name="租户1_管理员",Remark="租户1_管理员",TenantId=Guid.Parse("710148b3-0c80-48a2-8f57-4b863be9859f"),CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)
                },
                new Role
                {
                    Id=4,Name="租户2_管理员",Remark="租户2_管理员",TenantId=Guid.Parse("f416b514-04c8-40ca-91a4-07c5bbf9c8c6"),CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)
                },
                new Role
                {
                    Id=5,Name="地磅租户1管理员",Remark="地磅租户1管理员",TenantId=Guid.Parse("5841a727-9b01-4782-ae49-c0b000e4cbe4"),CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)
                }
            };
        }
    }
}