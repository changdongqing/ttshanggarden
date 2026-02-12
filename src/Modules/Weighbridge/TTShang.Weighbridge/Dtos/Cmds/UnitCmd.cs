// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Weighbridge.Dtos.Cmds
{
    /// <summary>
    /// 单位命令
    /// </summary>
    public class UnitCmd
    {
        /// <summary>
        /// 单位命令
        /// </summary>
        /// <param name="unitType"></param>
        public UnitCmd(UnitType unitType)
        {
            UnitType = unitType;
        }

        /// <summary>
        /// 
        /// </summary>
        public UnitType UnitType { get; init; }
    }
}
