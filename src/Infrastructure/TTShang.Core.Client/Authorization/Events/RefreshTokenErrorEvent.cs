// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Authorization.Events
{
    /// <summary>
    /// 刷新token失败事件
    /// </summary>
    public class RefreshTokenErrorEvent : EventBase
    {
        /// <summary>
        /// 刷新token失败事件
        /// </summary>
        public RefreshTokenErrorEvent() : base()
        {
        }
    }
}
