// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.EventBus
{
    /// <summary>
    /// 订阅者定义
    /// </summary>
    public interface ISubscriber
    {
        /// <summary>
        /// 订阅者唯一编号
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public string EventType { get; }
        /// <summary>
        /// 事件分组
        /// </summary>
        public EventGroup EventGroup { get; }
        /// <summary>
        /// 事件类型唯一值
        /// </summary>
        public string EventTypeId
        {
            get
            {
                return EventGroup + "_" + EventType;
            }
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        Task CallBack(EventBase e);

        /// <summary>
        /// 根据业务细分组
        /// </summary>
        public string[]? EventKeys { get; set; }
    }
}
