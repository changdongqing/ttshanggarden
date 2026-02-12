// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Iot.Enums;

namespace Gardener.Iot.Client.Services
{
    [ScopedService]
    public class DeviceConnectionService : ClientServiceBase<DeviceConnectionDto, long>, IDeviceConnectionService
    {
        public DeviceConnectionService(IApiCaller apiCaller) : base(apiCaller, "device-connection", "iot")
        {
        }
    }
}
