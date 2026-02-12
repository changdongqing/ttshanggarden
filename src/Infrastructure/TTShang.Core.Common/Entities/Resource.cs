//// -----------------------------------------------------------------------------
//// 园丁,是个很简单的管理系统
////  gitee:https://gitee.com/hgflydream/Gardener 
////  issues:https://gitee.com/hgflydream/Gardener/issues 
//// -----------------------------------------------------------------------------

//using Furion.DatabaseAccessor;
//using TTShang.Core.SystemAsset.Dtos;
//using TTShang.Core.SystemAsset.Enums;
//using TTShang.Core.SystemAsset.Resources;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System.ComponentModel.DataAnnotations;

//namespace TTShang.Core.Common.Entities
//{
//    /// <summary>
//    /// 资源表
//    /// </summary>
//    public class Resource : ResourceDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Resource, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<Resource>
//    {
//        /// <summary>
//        /// 父级
//        /// </summary>
//        [Display(Name = nameof(SystemAssetResource.Parent), ResourceType = typeof(SystemAssetResource))]
//        public Resource? Parent { get; set; }

//        /// <summary>
//        /// 子集
//        /// </summary>
//        [Display(Name = nameof(SystemAssetResource.Children), ResourceType = typeof(SystemAssetResource))]
//        public new ICollection<Resource>? Children { get; set; }

//        /// <summary>
//        /// 多对多中间表
//        /// </summary>
//        public new List<ResourceFunction> ResourceFunctions { get; set; } = new List<ResourceFunction>();

//        /// <summary>
//        /// 租户资源关系
//        /// </summary>
//        public List<SystemTenantResource> TenantResources { get; set; } = new List<SystemTenantResource>();
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="entityBuilder"></param>
//        /// <param name="dbContext"></param>
//        /// <param name="dbContextLocator"></param>
//        /// <exception cref="NotImplementedException"></exception>
//        public void Configure(EntityTypeBuilder<Resource> entityBuilder, DbContext dbContext, Type dbContextLocator)
//        {
//            entityBuilder.HasIndex(x => x.Key).IsUnique();

//            entityBuilder
//              .HasMany(x => x.Children)
//              .WithOne(x => x.Parent)
//              .HasForeignKey(x => x.ParentId)
//              .OnDelete(DeleteBehavior.NoAction); // 必须设置这一行

//        }

//        /// <summary>
//        /// 种子数据
//        /// </summary>
//        /// <param name="dbContext"></param>
//        /// <param name="dbContextLocator"></param>
//        /// <returns></returns>
//        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
//        {
//            return new[]
//            {
//                new Resource() {SupportMultiTenant=true,Name="后台根节点",Key="admin_root",Remark="根根节点不能删除，不能改变类型！！。",Path="",Icon="apartment",Order=0,Type=Enum.Parse<ResourceType>("Root"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2022-08-16 07:13:50"),Id=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868")},
//                new Resource() {SupportMultiTenant=true,Name="前台根节点",Key="front_root",Remark="根根节点不能删除，不能改变类型！！。",Path="",Icon="apartment",Order=1,Type=Enum.Parse<ResourceType>("Root"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2022-08-16 07:15:50"),Id=Guid.Parse("f4239a53-b5e1-49bd-99c6-967a86f07cdc")},
//            };
//        }
//    }
//}