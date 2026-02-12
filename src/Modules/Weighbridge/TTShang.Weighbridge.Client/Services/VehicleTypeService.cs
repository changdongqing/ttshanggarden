// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Weighbridge.Client.Services
{
    /// <summary>
    ///  车辆类型服务
    /// </summary>
    [ScopedService]
    public class VehicleTypeService : ClientServiceBase<VehicleTypeDto, Int32>, IVehicleTypeService
    {
        /// <summary>
        ///  车辆类型服务
        /// </summary>
        public VehicleTypeService(IApiCaller apiCaller) : base(apiCaller, "vehicle-type", "weighbridge")
        {
        }
    }
}
