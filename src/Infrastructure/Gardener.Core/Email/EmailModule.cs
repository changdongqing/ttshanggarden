// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.Email
{
    /// <summary>
    /// 模块
    /// </summary>
    public class EmailModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "Email";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "邮件模块";
        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 190;
    }
}
