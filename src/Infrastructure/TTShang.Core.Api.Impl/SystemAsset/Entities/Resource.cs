// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.UserCenter.Entities;
using TTShang.Core.SystemAsset.Resources;
using System.ComponentModel.DataAnnotations;

namespace TTShang.Core.Api.Impl.SystemAsset.Entities
{
    /// <summary>
    /// 资源表
    /// </summary>
    public class Resource : ResourceDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Resource, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<Resource>
    {
        /// <summary>
        /// 父级
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Parent), ResourceType = typeof(SystemAssetResource))]
        public Resource? Parent { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Children), ResourceType = typeof(SystemAssetResource))]
        public new ICollection<Resource>? Children { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public new List<ResourceFunction> ResourceFunctions { get; set; } = new List<ResourceFunction>();

        /// <summary>
        /// 租户资源关系
        /// </summary>
        public List<SystemTenantResource> TenantResources { get; set; } = new List<SystemTenantResource>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<Resource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => x.Key).IsUnique();
            entityBuilder
               .HasMany(x => x.Children)
               .WithOne(x => x.Parent)
               .HasForeignKey(x => x.ParentId)
               .OnDelete(DeleteBehavior.Cascade);
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return [];
        }
    }
}