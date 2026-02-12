// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.AppManager.Dtos;

namespace Gardener.Core.Api.Impl.AppManager.Entities
{
    /// <summary>
    /// 应用版本
    /// </summary>
    public class AppVersion : AppVersionDto, IEntityBase, IEntityTypeBuilder<AppVersion>
    {


        /// <summary>
        /// 应用
        /// </summary>
        public new App? App { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<AppVersion> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(x => x.App)
                .WithMany(x => x.AppVersions)
                .HasForeignKey(x => x.AppId);

            entityBuilder.HasIndex([nameof(AppVersion.AppId), nameof(AppVersion.Environment), nameof(AppVersion.VersionNumber)]).IsUnique();
            entityBuilder.HasIndex([nameof(AppVersion.AppId), nameof(AppVersion.Environment), nameof(AppVersion.VersionName)]).IsUnique();
        }
    }
}
