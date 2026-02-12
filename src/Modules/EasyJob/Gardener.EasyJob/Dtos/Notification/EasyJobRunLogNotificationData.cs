// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.NotificationSystem;

namespace Gardener.EasyJob.Dtos.Notification
{
    /// <summary>
    /// 定时任务运行日志通知
    /// </summary>
    public class EasyJobRunLogNotificationData : NotificationData
    {
        /// <summary>
        /// 定时任务运行日志通知
        /// </summary>
        /// <param name="log" ></param>
        public EasyJobRunLogNotificationData(SysJobLogDto log) : base()
        {
            this.Log = log;
        }

        /// <summary>
        /// 日志
        /// </summary>
        public SysJobLogDto Log { get; set; }
    }
}
