// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.CodeGeneration
{
    /// <summary>
    /// 模块
    /// </summary>
    public class CodeGenerationModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "CodeGeneration";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "代码生成模块";
    }
}
