// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Common;
using TTShang.Weighbridge.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Weighbridge.Services
{
    /// <summary>
    ///  车辆称重配置服务
    /// </summary>
    public interface IVehicleWeighingConfigService : IServiceBase<VehicleWeighingConfigDto, Int32>
    {
        /// <summary>
        /// 保存车辆配置
        /// </summary>
        /// <remarks>
        /// 保存车辆配置
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SaveVehicleConfig(VehicleWeighingConfigDto input);

        /// <summary>
        /// 查询-根据车牌号查询配置
        /// </summary>
        /// <remarks>
        /// 查询-根据车牌号查询配置
        /// </remarks>
        /// <param name="plateNumber"></param>
        /// <returns></returns>
        Task<VehicleWeighingConfigDto?> FindByPlateNumber(string plateNumber);
    }
}
