// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.EntityFramwork;
using Gardener.Core.Api.Impl.SystemAsset.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Core.Api.Impl.UserCenter.Entities
{
    /// <summary>
    /// 租户资源信息
    /// </summary>
    [Display(Name = nameof(SharedLocalResource.SystemTenantResource), ResourceType = typeof(SharedLocalResource))]
    public class SystemTenantResource : BaseDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<SystemTenantResource, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<SystemTenantResource>
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.TenantId), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Tenant), ResourceType = typeof(SharedLocalResource))]
        [NotMapped]
        public SystemTenant Tenant { get; set; } = default!;

        /// <summary>
        /// 资源编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.ResourceId), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]

        public Guid ResourceId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Resource), ResourceType = typeof(SharedLocalResource))]
        public Resource Resource { get; set; } = default!;

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<SystemTenantResource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasKey(x => new { x.TenantId, x.ResourceId });
            entityBuilder
             .HasOne(pt => pt.Tenant)
             .WithMany(t => t.TenantResources)
             .HasForeignKey(pt => pt.TenantId)
             .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
               .HasOne(pt => pt.Resource)
               .WithMany(t => t.TenantResources)
               .HasForeignKey(pt => pt.ResourceId)
               .OnDelete(DeleteBehavior.Cascade);
        }
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<SystemTenantResource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return [];
        }
    }
}
