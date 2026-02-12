// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.UserCenter
{
    /// <summary>
    /// 模块
    /// </summary>
    public class UserCenterModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "UserCenter";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "用户中心模块";

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 130;
    }
}
