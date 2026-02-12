// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TTShang.Iot.Impl.Core
{
    /// <summary>
    /// mqtt后台服务
    /// </summary>
    public class CommunicationBackgroundService : BackgroundService
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private readonly ILogger<CommunicationBackgroundService> logger;


        private readonly IDeviceCommunicationCableSplicer communicationCableSplicer;

        private readonly IServiceProvider serviceProvider;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="communicationCableSplicer"></param>
        /// <param name="serviceProvider"></param>
        public CommunicationBackgroundService(ILogger<CommunicationBackgroundService> logger, IDeviceCommunicationCableSplicer communicationCableSplicer, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.communicationCableSplicer = communicationCableSplicer;
            this.serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 服务启动时
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            IEnumerable<IDeviceCommunicationService> deviceCommunicationServices = serviceProvider.GetKeyedServices<IDeviceCommunicationService>(nameof(DeviceConnectionType));
            foreach (var deviceCommunicationService in deviceCommunicationServices)
            { 
                await deviceCommunicationService.StartAsync(cancellationToken, communicationCableSplicer);
            }
            await base.StartAsync(cancellationToken);
        }
        /// <summary>
        /// 服务执行
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IEnumerable<IDeviceCommunicationService> deviceCommunicationServices = serviceProvider.GetKeyedServices<IDeviceCommunicationService>(nameof(DeviceConnectionType));

            foreach (var deviceCommunicationService in deviceCommunicationServices)
            {
                await deviceCommunicationService.ExecuteAsync(stoppingToken, communicationCableSplicer);
            }
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            IEnumerable<IDeviceCommunicationService> deviceCommunicationServices = serviceProvider.GetKeyedServices<IDeviceCommunicationService>(nameof(DeviceConnectionType));

            foreach (var deviceCommunicationService in deviceCommunicationServices)
            {
                await deviceCommunicationService.StopAsync(cancellationToken, communicationCableSplicer);
            }
            await base.StopAsync(cancellationToken);
        }
    }
}
