// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.SignalR.Client;

namespace Gardener.Core.Client.Impl.SignalR
{
    /// <summary>
    /// 重试策略
    /// </summary>
    internal class CustomRetryPolicy : IRetryPolicy
    {
        /// <summary>
        /// 最大重试次数 0：无限制
        /// </summary>
        private long maxRetryNumber = 0;
        /// <summary>
        /// 重试间隔(秒)
        /// </summary>
        private double retryIntervalSeconds = 5.0;
        /// <summary>
        /// 重试策略
        /// </summary>
        /// <param name="maxRetryNumber">最大重试次数 0：无限制</param>
        /// <param name="retryIntervalSeconds">重试间隔(秒)</param>
        public CustomRetryPolicy(long maxRetryNumber, double retryIntervalSeconds)
        {
            this.maxRetryNumber = maxRetryNumber;
            this.retryIntervalSeconds = retryIntervalSeconds;
        }

        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            //
            if (maxRetryNumber != 0 && retryContext.PreviousRetryCount >= maxRetryNumber)
            {
                return null;
            }
            return TimeSpan.FromSeconds(retryIntervalSeconds);
        }
    }
}
