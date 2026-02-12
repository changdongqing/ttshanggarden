// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using TTShang.Core.Api.Impl.EntityFramwork.Internal.DbContexts;
using TTShang.Core.EntityFramwork;
using TTShang.Core.Module;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TTShang.Core.Api.Impl.EntityFramwork
{
    /// <summary>
    /// 模块
    /// </summary>
    public class EntityFramworkServerModule : EntityFramworkModule, IServerModule
    {
        private readonly ILogger<EntityFramworkServerModule> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public EntityFramworkServerModule(ILogger<EntityFramworkServerModule> logger)
        {
            _logger = logger;
        }

       

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// 尽量快
        /// </remarks>
        /// <returns></returns>
        public Task OnStart(CancellationToken cancellationToken)
        {
            //自动初始数据库
            AotuInitDb();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 自动初始化数据库
        /// </summary>
        private void AotuInitDb()
        {
            // 判断开发环境！！！必须！！！！
            if (App.WebHostEnvironment.IsDevelopment())
            {
                var initDb = bool.Parse(App.Configuration["DefaultDbSettings:InitDb"] ?? "false");
                var autoMigration = bool.Parse(App.Configuration["DefaultDbSettings:AutoMigration"] ?? "false");
                if (autoMigration || initDb)
                {
                    var d1 = Db.GetDbContext<MasterDbContextLocator>();
                    var d2 = Db.GetDbContext<GardenerMultiTenantDbContextLocator>();
                    var d3 = Db.GetDbContext<GardenerIgnoreAuditDbContextLocator>();
                    List<DbContext> defaultDbContexts = new List<DbContext>
                    {
                        d1,d2,d3
                    };
                    foreach (DbContext dbContext in defaultDbContexts)
                    {
                        if (autoMigration)
                        {
                            //需要有迁移文件，如果没有迁移文件不会迁移，迁移后会生成迁移记录在表中，如果迁移记录与实际表版本不一致，将异常。
                            dbContext.Database.Migrate();
                            _logger.LogInformation($"数据库{dbContext.GetType().FullName}迁移完成");
                        }
                        if (initDb)
                        {
                            //不需要迁移文件，如果数据库已存在，不生成，如果不存在，生成数据库，生成的数据库没有迁移记录，无法再使用migrate进行迁移。
                            dbContext.Database.EnsureCreated();
                            _logger.LogInformation($"数据库{dbContext.GetType().FullName}初始化完成");
                        }
                    }
                }
            }
        }
    }
}
