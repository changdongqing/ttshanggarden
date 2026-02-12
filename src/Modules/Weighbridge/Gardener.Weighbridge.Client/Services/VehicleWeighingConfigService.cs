// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Weighbridge.Client.Services
{
    /// <summary>
    ///  车辆称重配置服务
    /// </summary>
    [ScopedService]
    public class VehicleWeighingConfigService : ClientServiceBase<VehicleWeighingConfigDto, Int32>, IVehicleWeighingConfigService
    {
        /// <summary>
        ///  车辆称重配置服务
        /// </summary>
        public VehicleWeighingConfigService(IApiCaller apiCaller) : base(apiCaller, "vehicle-weighing-config", "weighbridge")
        {
        }
        /// <summary>
        /// 查询-根据车牌号查询配置
        /// </summary>
        /// <remarks>
        /// 查询-根据车牌号查询配置
        /// </remarks>
        /// <param name="plateNumber"></param>
        /// <returns></returns>
        public Task<VehicleWeighingConfigDto?> FindByPlateNumber(string plateNumber)
        {
            return apiCaller.GetAsync<VehicleWeighingConfigDto?>($"{baseUrl}/by-plate-number", [new KeyValuePair<string, object?>(nameof(plateNumber), plateNumber)]);
        }

        /// <summary>
        /// 保存车辆配置
        /// </summary>
        /// <remarks>
        /// 保存车辆配置
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> SaveVehicleConfig(VehicleWeighingConfigDto input)
        {
            return apiCaller.PostAsync<VehicleWeighingConfigDto, bool>($"{baseUrl}/save-vehicle-config", input);
        }
    }
}
