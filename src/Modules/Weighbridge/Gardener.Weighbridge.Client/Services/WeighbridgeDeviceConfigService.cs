// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Weighbridge.Dtos.Cmds;

namespace Gardener.Weighbridge.Client.Services
{
    /// <summary>
    ///  地磅设备配置服务
    /// </summary>
    [ScopedService]
    public class WeighbridgeDeviceConfigService : ClientServiceBase<WeighbridgeDeviceConfigDto, Int32>, IWeighbridgeDeviceConfigService
    {
        /// <summary>
        ///  地磅设备配置服务
        /// </summary>
        public WeighbridgeDeviceConfigService(IApiCaller apiCaller) : base(apiCaller, "weighbridge-device-config", "weighbridge")
        {
        }

        public Task<Dictionary<Guid, WeighbridgeDeviceConfigDto>> FindDeviceConfig(Guid[] deviceIds)
        {
            List<KeyValuePair<string, object?>> paramList = new List<KeyValuePair<string, object?>>();
            foreach (var item in deviceIds)
            {
                paramList.Add(new KeyValuePair<string, object?>(nameof(deviceIds), item));
            }
            return apiCaller.GetAsync<Dictionary<Guid, WeighbridgeDeviceConfigDto>>($"{baseUrl}/device-config", paramList);
        }

        public Task<WeighbridgeDeviceConfigDto?> FindDeviceConfigByDeviceId(Guid deviceId)
        {
            return apiCaller.GetAsync<WeighbridgeDeviceConfigDto?>($"{baseUrl}/device-config-by-device-id/{deviceId}");
        }
    }
}
