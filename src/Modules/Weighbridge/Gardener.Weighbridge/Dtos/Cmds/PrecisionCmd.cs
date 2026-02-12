// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Weighbridge.Dtos.Cmds
{
    /// <summary>
    /// 小数位命令
    /// </summary>
    public class PrecisionCmd
    {
        /// <summary>
        /// 小数位命令
        /// </summary>
        /// <param name="precisionType"></param>
        public PrecisionCmd(PrecisionType precisionType)
        {
            PrecisionType = precisionType;
        }

        /// <summary>
        /// 
        /// </summary>
        public PrecisionType PrecisionType { get; init; }
    }
}
