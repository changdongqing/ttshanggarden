
namespace TTShang.Weighbridge.Client.Services
{
    /// <summary>
    ///  地磅配置服务
    /// </summary>
    [ScopedService]
    public class WeighbridgeConfigService : ClientServiceBase<WeighbridgeConfigDto, Guid>, IWeighbridgeConfigService
    {
        /// <summary>
        ///  地磅配置服务
        /// </summary>
        public WeighbridgeConfigService(IApiCaller apiCaller) : base(apiCaller, "weighbridge-config", "weighbridge")
        {
        }

        public Task<bool> SaveMyWeighbridgeConfig(SaveMyWeighbridgeConfigInput input)
        {
            return apiCaller.PostAsync<SaveMyWeighbridgeConfigInput, bool>($"{baseUrl}/save-my-weighbridge-config", input);
        }
    }

}
