// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.SystemAsset.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Api.Impl.SystemAsset.Entities
{
    /// <summary>
    /// 资源功能信息
    /// </summary>
    public class ResourceFunction : ResourceFunctionDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<ResourceFunction, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 权限
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Resource), ResourceType = typeof(SystemAssetResource))]
        public Resource Resource { get; set; } = default!;

        /// <summary>
        /// 功能
        /// </summary>
        [Display(Name = nameof(SystemAssetResource.Function), ResourceType = typeof(SystemAssetResource))]
        public new Function? Function { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<ResourceFunction> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasKey(x => new { x.ResourceId, x.FunctionId });

            entityBuilder
               .HasOne(pt => pt.Resource)
               .WithMany(t => t.ResourceFunctions)
               .HasForeignKey(pt => pt.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
               .HasOne(pt => pt.Function)
               .WithMany(t => t.ResourceFunctions)
               .HasForeignKey(pt => pt.FunctionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
