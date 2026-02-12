// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;

namespace TTShang.Core.EntityFramwork
{
    /// <summary>
    /// 模块
    /// </summary>
    public class EntityFramworkModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "EntityFramwork";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "EntityFramwork模块";
        /// <summary>
        /// 排序
        /// </summary>
        /// <remarks>
        /// 越小越靠前
        /// </remarks>
        public int Order { get { return 100; } }
    }
}
