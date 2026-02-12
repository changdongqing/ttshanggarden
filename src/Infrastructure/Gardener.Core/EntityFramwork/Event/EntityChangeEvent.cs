// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.EventBus;

namespace Gardener.Core.EntityFramwork.Event
{
    /// <summary>
    /// 实体对象变化
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class EntityChangeEvent<TData> : EventBase<TData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="data"></param>
        public EntityChangeEvent(string groupName, TData data) : base(EventGroup.EntityOperate, groupName, data)
        {
            //可以多次消费
            this.IsConsumOnce = false;
        }
    }
}
