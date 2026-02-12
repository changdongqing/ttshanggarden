// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Gardener.Core.EventBus;

namespace Gardener.Core.NotificationSystem
{
    /// <summary>
    /// 系统通知数据
    /// </summary>
    public abstract class NotificationData : EventBase
    {
        /// <summary>
        /// 系统通知数据
        /// </summary>
        /// <param name="eventType">通知类型</param>
        public NotificationData(string? eventType = null) : base(EventGroup.SystemNotify)
        {
            TypeAssemblyName = GetType().AssemblyQualifiedName;
            EventType = eventType ?? base.EventType;
        }

        /// <summary>
        /// 程序类型
        /// </summary>
        public string? TypeAssemblyName { get; init; }

        /// <summary>
        /// 发送者身份
        /// </summary>
        public Identity? Identity { get; set; }

        /// <summary>
        /// 用户ip
        /// </summary>
        public string? Ip { get; set; }

        /// <summary>
        /// 来自于那个链接
        /// </summary>
        /// <remarks>
        /// 如果是服务端发送，无链接
        /// </remarks>
        public string? FromConnectionId { get; set; }
    }
}
