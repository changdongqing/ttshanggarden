// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;

namespace Gardener.Iot.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class DeviceService : ClientServiceBase<DeviceDto, Guid>, IDeviceService
    {
        public DeviceService(IApiCaller apiCaller) : base(apiCaller, "device", "iot")
        {
        }

        public Task<bool> BindTenant(DeviceBindTenantInput input)
        {
            return apiCaller.PostAsync<DeviceBindTenantInput, bool>($"{baseUrl}/bind-tenant", input);
        }

        public Task<ProductDto?> FindDeviceProduct(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceDto?> FindGlobal(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendMessage(SendDataInput input)
        {
            return apiCaller.PostAsync<SendDataInput, bool>($"{baseUrl}/send-message", input);
        }

        public Task<DeviceDto?> TryGetByClientId(string clientId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDeviceAlias(UpdateDeviceAliasInput input)
        {
            return apiCaller.PutAsync<UpdateDeviceAliasInput, bool>($"{baseUrl}/device-alias", input);
        }

        public Task<bool> VerifySecretKey(Guid deviceId,string account, string secretKey)
        {
            throw new NotImplementedException();
        }
    }
}
