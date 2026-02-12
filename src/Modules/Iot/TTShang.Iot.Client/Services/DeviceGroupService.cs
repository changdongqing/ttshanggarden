// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class DeviceGroupService : ClientServiceBase<DeviceGroupDto, int>, IDeviceGroupService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiCaller"></param>
        public DeviceGroupService(IApiCaller apiCaller) : base(apiCaller, "device-group", "iot")
        {
        }

        public Task<List<DeviceGroupDto>> GetTree(bool includeLocked = false)
        {
            return apiCaller.GetAsync<List<DeviceGroupDto>>($"{this.baseUrl}/tree/{includeLocked}");
        }
    }
}
