// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.Core.Api.Impl.EntityFramwork.Internal;
using Gardener.Core.Api.Impl.EntityFramwork.Internal.DbContexts;
using Gardener.Core.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core.Api.Impl.EntityFramwork
{
    /// <summary>
    /// ef扩展
    /// </summary>
    public static class EntityFramworkExtensions
    {
        /// <summary>
        /// 添加EF支持
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <remarks>
        /// 请在使用到ef的模块之前添加。
        /// </remarks>
        public static IServiceCollection AddEF(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, EntityFramworkServerModule>();

            // TODO: dbsettings.json里使用db type, 根据db type 自动设置dbProvider
            string? dbProvider = App.Configuration["DefaultDbSettings:DbProvider"];
            if (dbProvider == null)
            {
                throw new ArgumentNullException(nameof(dbProvider));
            }else if (dbProvider == DbProvider.Npgsql)
            {
                //解决切换postgresql时可能出错 
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
            else if(dbProvider == DbProvider.MySql)
            {
                dbProvider = $"{DbProvider.MySql}@8.0.22";
            }
            string? migrationAssemblyName = App.Configuration["DefaultDbSettings:MigrationAssemblyName"];
            services.AddDatabaseAccessor(options =>
            {
                //注入默认数据库上下文
                options.AddDbPool<GardenerDbContext>(dbProvider);
                //注入忽略审计数据库上下文
                options.AddDbPool<GardenerIgnoreAuditDbContext, GardenerIgnoreAuditDbContextLocator>(dbProvider);
                //注入多租户数据库上下文
                options.AddDbPool<GardenerMultiTenantDbContext, GardenerMultiTenantDbContextLocator>(dbProvider);

            }, migrationAssemblyName);

            //提供审计数据
            services.AddScoped<EFAuditEntityDataHander>();
            return services;
        }
    }
}
