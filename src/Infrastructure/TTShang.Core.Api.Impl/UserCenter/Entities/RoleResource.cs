// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.SystemAsset.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.Core.Api.Impl.UserCenter.Entities
{
    /// <summary>
    /// 用户和角色关系表
    /// </summary>
    public class RoleResource : RoleResourceDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<RoleResource, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<RoleResource, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 角色
        /// </summary>
        public new Role Role { get; set; } = null!;

        /// <summary>
        /// 权限
        /// </summary>
        public new Resource Resource { get; set; } = null!;

        /// <summary>
        /// 配置多对多关系
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<RoleResource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(t => new { t.RoleId, t.ResourceId });
            entityBuilder
                 .HasOne(pt => pt.Role)
                 .WithMany(t => t.RoleResources)
                 .HasForeignKey(pt => pt.RoleId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<RoleResource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return [];
        }
    }
}