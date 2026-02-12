
namespace Gardener.Weighbridge.Client.Services
{
    /// <summary>
    /// 用户地磅配置服务
    /// </summary>
    [ScopedService]
    public class UserWeighbridgeConfigService : ClientServiceBase<UserWeighbridgeConfigDto, int>, IUserWeighbridgeConfigService
    {
        /// <summary>
        /// 用户地磅配置服务
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="controller"></param>
        public UserWeighbridgeConfigService(IApiCaller apiCaller) : base(apiCaller, "user-weighbridge-config")
        {
        }

        public Task<UserWeighbridgeConfigDto?> FindMyUserConfig()
        {
            return apiCaller.GetAsync<UserWeighbridgeConfigDto?>($"{baseUrl}/my-user-config");
        }

        public Task<UserWeighbridgeConfigDto?> FindUserConfig(int userId)
        {
            return apiCaller.GetAsync<UserWeighbridgeConfigDto?>($"{baseUrl}/user-config/{userId}");
        }

        public Task<UserWeighbridgeConfigDto?> SaveMyUserWeighbridgeConfig(UserWeighbridgeConfigDto userWeighbridgeConfig)
        {
            return apiCaller.PostAsync<UserWeighbridgeConfigDto, UserWeighbridgeConfigDto?>($"{baseUrl}/save-my-user-weighbridge-config", userWeighbridgeConfig);
        }
    }
}
