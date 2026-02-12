// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Client.Services
{
    /// <summary>
    /// 设备系统日志服务
    /// </summary>
    [ScopedService]
    public class DeviceSystemLogService : ClientServiceBase<DeviceSystemLogDto, long>, IDeviceSystemLogService
    {
        public DeviceSystemLogService(IApiCaller apiCaller) : base(apiCaller, "device-system-log", "iot")
        {
        }
    }
}
