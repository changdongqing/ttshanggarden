// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Weighbridge.Dtos.Cmds
{
    /// <summary>
    /// 读值命令
    /// </summary>
    public class ReadValueCmd
    {
        /// <summary>
        /// 读值命令
        /// </summary>
        /// <param name="length"></param>
        public ReadValueCmd(byte length = 24)
        {
            Length = length;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Length { get; init; }
    }
}
