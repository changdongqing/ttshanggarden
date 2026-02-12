// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.NotificationSystem;

namespace TTShang.EasyJob.Dtos.Notification
{
    /// <summary>
    /// 定时任务触发器更新通知
    /// </summary>
    public class EasyJobTriggerUpdateNotificationData : NotificationData
    {
        /// <summary>
        /// 定时任务触发器更新通知
        /// </summary>
        public EasyJobTriggerUpdateNotificationData(SysJobTriggerDto trigger) : base()
        {
            this.Trigger = trigger;
        }
        /// <summary>
        /// 最新触发器
        /// </summary>
        public SysJobTriggerDto Trigger { get; set; }
    }
}
