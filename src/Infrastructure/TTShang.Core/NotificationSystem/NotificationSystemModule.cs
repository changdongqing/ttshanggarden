// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;

namespace TTShang.Core.NotificationSystem
{
    /// <summary>
    /// 模块
    /// </summary>
    public class NotificationSystemModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "NotificationSystem";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "通知系统模块";

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 220;
    }
}
