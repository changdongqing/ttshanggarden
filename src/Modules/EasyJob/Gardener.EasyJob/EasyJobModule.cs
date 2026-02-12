// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.EasyJob
{
    /// <summary>
    /// 模块
    /// </summary>
    public class EasyJobModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "EasyJob";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "任务调度模块";
    }
}
