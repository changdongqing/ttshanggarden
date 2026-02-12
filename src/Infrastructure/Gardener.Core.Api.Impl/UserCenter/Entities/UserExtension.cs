// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Core.Api.Impl.UserCenter.Entities
{
    /// <summary>
    /// 用户扩展信息表
    /// </summary>
    [Description("用户扩展信息")]
    public class UserExtension : UserExtensionDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<UserExtension, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<UserExtension, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<UserExtension> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            //entityBuilder.HasKey(e => e.UserId).HasName("PRIMARY");
            entityBuilder
                .HasOne(x => x.User)
                .WithOne(x => x.UserExtension)
                .HasForeignKey<UserExtension>(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<UserExtension> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new UserExtension()
                    {
                        UserId=8,
                        QQ="123456"
                    }
            };
        }
    }

}
