// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.Authorization
{
    /// <summary>
    /// 模块
    /// </summary>
    public class AuthorizationModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "Authorization";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "认证授权模块";
        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 140;
    }
}
