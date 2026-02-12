// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.Audit
{
    /// <summary>
    /// 模块
    /// </summary>
    public class AuditModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "Audit";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "审计模块";

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 170;
    }
}
