// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Impl.Services
{
    /// <summary>
    /// 设备系统日志服务
    /// </summary>
    [ApiDescriptionSettings("Iot", Module = "iot")]
    public class DeviceSystemLogService : ServiceBase<DeviceSystemLog, DeviceSystemLogDto, long, GardenerMultiTenantDbContextLocator>, IDeviceSystemLogService
    {
        /// <summary>
        /// 设备系统日志服务
        /// </summary>
        /// <param name="repository"></param>
        public DeviceSystemLogService(IRepository<DeviceSystemLog, GardenerMultiTenantDbContextLocator> repository) : base(repository)
        {
        }
    }
}
