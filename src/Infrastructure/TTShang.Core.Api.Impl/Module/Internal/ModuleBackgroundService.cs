// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Hosting;

namespace TTShang.Core.Api.Impl.Module.Internal
{
    /// <summary>
    /// 利用BackgroundService，提高初始化数据库的优先级
    /// </summary>
    internal class ModuleBackgroundService : BackgroundService
    {
        private readonly ServerModuleManager _moduleManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleManager"></param>
        public ModuleBackgroundService(ServerModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _moduleManager.OnStart(cancellationToken);
            await base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _moduleManager.OnExecute(stoppingToken);
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _moduleManager.OnStop(cancellationToken);
            await base.StopAsync(cancellationToken);
        }
    }
}
