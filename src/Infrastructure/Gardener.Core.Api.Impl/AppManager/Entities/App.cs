// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.AppManager.Dtos;

namespace Gardener.Core.Api.Impl.AppManager.Entities
{
    /// <summary>
    /// 应用
    /// </summary>
    public class App : AppDto, IEntityBase, IEntityTypeBuilder<App>
    {
        /// <summary>
        /// 应用版本
        /// </summary>

        public new ICollection<AppVersion> AppVersions { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<App> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x=>x.AppName).IsUnique();
            entityBuilder.HasIndex(x=>x.PackageName).IsUnique();
        }
    }
}
