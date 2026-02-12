// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;

namespace TTShang.Core.Swagger
{
    /// <summary>
    /// 模块
    /// </summary>
    public class SwaggerModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "Swagger";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "Swagger模块";

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 101;

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get
            {
                return "8.0.2";
            }
        }
    }
}
