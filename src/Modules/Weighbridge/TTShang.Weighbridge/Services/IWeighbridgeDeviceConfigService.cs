// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Common;
using TTShang.Weighbridge.Dtos;

namespace TTShang.Weighbridge.Services
{
    /// <summary>
    ///  地磅设备配置服务
    /// </summary>
    public interface IWeighbridgeDeviceConfigService : IServiceBase<WeighbridgeDeviceConfigDto, Int32>
    {

        /// <summary>
        /// 查询-获取多个设备配置信息
        /// </summary>
        /// <param name="deviceIds"></param>
        /// <returns></returns>
        Task<Dictionary<Guid, WeighbridgeDeviceConfigDto>> FindDeviceConfig(Guid[] deviceIds);

        /// <summary>
        /// 查询-获取设备配置信息
        /// </summary>
        /// <remarks>
        /// 根据设备编号获取设备配置
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<WeighbridgeDeviceConfigDto?> FindDeviceConfigByDeviceId(Guid deviceId);
    }
}
