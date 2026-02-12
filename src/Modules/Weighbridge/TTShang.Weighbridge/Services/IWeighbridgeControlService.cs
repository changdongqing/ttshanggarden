// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Weighbridge.Dtos.Cmds;

namespace TTShang.Weighbridge.Services
{
    /// <summary>
    /// 地磅控制服务
    /// </summary>
    public interface IWeighbridgeControlService
    {
        /// <summary>
        /// 清零
        /// </summary>
        /// <remarks>
        /// 清零
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> ZeroOut(WeighbridgeCmdInput input);

        /// <summary>
        /// 读值
        /// </summary>
        /// <remarks>
        /// 读值
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> ReadValue(WeighbridgeCmdInput<ReadValueCmd> input);

        /// <summary>
        /// 读值
        /// </summary>
        /// <remarks>
        /// 读值
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <param name="channelIds"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<bool> ReadValue(Guid deviceId, int[] channelIds, byte length)
        {
            return Task.FromResult(false);
        }
        /// <summary>
        /// 设置单位
        /// </summary>
        /// <remarks>
        /// 设置单位
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SetUnit(WeighbridgeCmdInput<UnitCmd> input);

        /// <summary>
        /// 设置小数位
        /// </summary>
        /// <remarks>
        /// 设置小数位
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SetPrecision(WeighbridgeCmdInput<PrecisionCmd> input);

        /// <summary>
        /// 去皮
        /// </summary>
        /// <remarks>
        /// 设置去皮
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> Netweight(WeighbridgeCmdInput<NetweightCmd> input);
    }
}
