// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.EventBus;

namespace TTShang.Core.NotificationSystem
{
    /// <summary>
    /// 动态订阅通知
    /// </summary>
    public class DynamicSubscribeNotificationData : NotificationData
    {
        /// <summary>
        /// 动态订阅通知
        /// </summary>
        /// <param name="subscribeEventGroup">订阅事件组</param>
        /// <param name="subscribeEventType">订阅事件类型</param>
        /// <param name="connectionId">websocket连接编号</param>
        /// <param name="subscribeEventKeys">订阅的事件key</param>
        public DynamicSubscribeNotificationData(EventGroup subscribeEventGroup, string subscribeEventType, string connectionId, string[]? subscribeEventKeys = null) : base()
        {
            SubscribeEventGroup = subscribeEventGroup;
            SubscribeEventType = subscribeEventType;
            SubscribeEventKeys = subscribeEventKeys;
            ConnectionId = connectionId;
        }
        /// <summary>
        /// 订阅事件组
        /// </summary>
        public EventGroup SubscribeEventGroup { get; set; }
        /// <summary>
        /// 订阅类型
        /// </summary>
        public string SubscribeEventType { get; set; }
        /// <summary>
        /// 订阅的事件key
        /// </summary>
        public string[]? SubscribeEventKeys { get; set; }

        /// <summary>
        /// websocket连接编号
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <remarks>
        /// true:订阅，false:取消订阅
        /// </remarks>
        public bool Subscribe { get; set; } = true;
    }
}
