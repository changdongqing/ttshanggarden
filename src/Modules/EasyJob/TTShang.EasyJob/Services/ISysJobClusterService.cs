// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.EasyJob.Dtos;

namespace TTShang.EasyJob.Services
{
    /// <summary>
    /// 作业集群服务
    /// </summary>
    /// <remarks>集群并不能达到负载均衡的效果，而仅仅提供了故障转移的功能，当一个任务服务器宕机时，另一个任务服务器会启动</remarks>
    public interface ISysJobClusterService : IServiceBase<SysJobClusterDto, int>
    {
        /// <summary>
        /// 当前作业调度器启动通知
        /// </summary>
        /// <param name="context">作业集群服务上下文</param>
        /// <remarks>
        /// 当前作业调度器启动通知
        /// </remarks>
        /// <returns></returns>
        void Start(JobClusterContext context);

        /// <summary>
        /// 等待被唤醒
        /// </summary>
        /// <param name="context">作业集群服务上下文</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// 等待被唤醒
        /// </remarks>
        /// <returns></returns>
        Task<bool> Waiting(JobClusterContext context, CancellationToken cancellationToken);

        /// <summary>
        /// 当前作业调度器停止通知
        /// </summary>
        /// <param name="context">作业集群服务上下文</param>
        /// <remarks>
        /// 当前作业调度器停止通知
        /// </remarks>
        /// <returns></returns>
        void Stop(JobClusterContext context);

        /// <summary>
        /// 当前作业调度器宕机
        /// </summary>
        /// <param name="context">作业集群服务上下文</param>
        /// <remarks>
        /// 当前作业调度器宕机
        /// </remarks>
        /// <returns></returns>
        void Crash(JobClusterContext context);
    }
}
