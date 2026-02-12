// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Services
{
    /// <summary>
    /// 设备通讯服务
    /// </summary>
    public interface IDeviceCommunicationService
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="communicationCableSplicer"></param>
        /// <returns></returns>
        Task StartAsync(CancellationToken cancellationToken, IDeviceCommunicationCableSplicer communicationCableSplicer);


        /// <summary>
        /// 服务执行
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <param name="communicationCableSplicer"></param>
        /// <returns></returns>
        Task ExecuteAsync(CancellationToken stoppingToken, IDeviceCommunicationCableSplicer communicationCableSplicer);


        /// <summary>
        /// 服务停止
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="communicationCableSplicer"></param>
        /// <returns></returns>
        Task StopAsync(CancellationToken cancellationToken, IDeviceCommunicationCableSplicer communicationCableSplicer);
    }
}
