// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.NotificationSystem
{
    /// <summary>
    /// 动态通知数据基类
    /// </summary>
    /// <remarks>
    /// 动态通知基类会在客户端订阅时，开启推送；自动定时发送心跳，超时或取消订阅将关闭推送。
    /// </remarks>
    public abstract class DynamicNotificationData : NotificationData
    {
        /// <summary>
        /// 动态通知数据基类
        /// </summary>
        /// <param name="eventType"></param>
        public DynamicNotificationData(string? eventType = null) : base(eventType) { }
    }
}
