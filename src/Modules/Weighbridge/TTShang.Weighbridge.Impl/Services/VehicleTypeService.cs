using TTShang.Core.Common;
using TTShang.Weighbridge.Impl.Entities;
using TTShang.Weighbridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace TTShang.Weighbridge.Impl.Services
{
    /// <summary>
    /// 车辆类型服务
    /// </summary>
    [ApiDescriptionSettings("Weighbridge", Module = "weighbridge")]
    public class VehicleTypeService : ServiceBase<VehicleType, VehicleTypeDto, Int32, GardenerMultiTenantDbContextLocator>, IVehicleTypeService
    {

        /// <summary>
        /// 车辆类型服务
        /// </summary>
        /// <param name="repository"></param>
        public VehicleTypeService(IRepository<VehicleType, GardenerMultiTenantDbContextLocator> repository) : base(repository)
        {
        }
    }
}
