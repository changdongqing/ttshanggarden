// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;

namespace TTShang.Core.VerifyCode
{
    /// <summary>
    /// 模块
    /// </summary>
    public class VerifyCodeModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "VerifyCode";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "验证码模块";

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 210;
    }
}
