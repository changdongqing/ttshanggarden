// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;

namespace TTShang.Core.Common
{
    /// <summary>
    /// 公共模块
    /// </summary>
    /// <remarks>
    /// 配置一些基础公共模块信息
    /// </remarks>
    public class CommonModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "Common";

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description => "公共模块";

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 110;
    }
}
