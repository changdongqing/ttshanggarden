// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Core.LicensePlateRecognition
{
    /// <summary>
    /// LPR
    /// </summary>
    public class LPRModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "LPR";
        /// <summary>
        /// 
        /// </summary>
        public string? Description => "车牌识别";
    }
}
