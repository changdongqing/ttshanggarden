// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Weighbridge.Dtos.Cmds
{
    /// <summary>
    /// 去皮命令输入
    /// </summary>
    public class NetweightCmd
    {

        /// <summary>
        /// 去皮命令输入
        /// </summary>
        /// <param name="netWeight"></param>
        public NetweightCmd(bool netWeight)
        {
            NetWeight = netWeight;
        }

        /// <summary>
        /// 是否去皮
        /// </summary>
        public bool NetWeight { get; init; }
    }
}
