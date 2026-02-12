// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.WoChat
{
    /// <summary>
    /// 模块
    /// </summary>
    public class WoChatModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "WoChat";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "WoChat聊天模块";
    }
}
