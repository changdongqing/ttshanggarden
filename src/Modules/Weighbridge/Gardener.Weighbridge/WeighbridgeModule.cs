// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;

namespace Gardener.Weighbridge
{
    /// <summary>
    /// 
    /// </summary>
    public class WeighbridgeModule : IModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "Weighbridge";
        /// <summary>
        /// 
        /// </summary>
        public string? Description => "电子地磅,基于Iot与地磅通讯，协议为Modbus-Rtu。";
        /// <summary>
        /// 
        /// </summary>
        public string Version => "1.0.5";
    }
}
