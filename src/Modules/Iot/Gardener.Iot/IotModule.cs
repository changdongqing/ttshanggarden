// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Iot
{
    /// <summary>
    /// 模块
    /// </summary>
    public class IotModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "Iot";

        /// <summary>
        /// 
        /// </summary>
        public string? Description => "物联网模块";
    }
}
