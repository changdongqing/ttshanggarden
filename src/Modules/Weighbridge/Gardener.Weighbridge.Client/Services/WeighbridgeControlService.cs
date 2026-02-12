// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Weighbridge.Dtos.Cmds;

namespace Gardener.Weighbridge.Client.Services
{
    /// <summary>
    /// 地磅控制服务
    /// </summary>
    [ScopedService]
    public class WeighbridgeControlService : ClientServiceCaller, IWeighbridgeControlService
    {
        public WeighbridgeControlService(IApiCaller apiCaller) : base(apiCaller, "weighbridge-control", "weighbridge")
        {
        }

        public Task<bool> Netweight(WeighbridgeCmdInput<NetweightCmd> input)
        {
            return apiCaller.PostAsync<WeighbridgeCmdInput<NetweightCmd>, bool>($"{baseUrl}/netweight", input);
        }

        public Task<bool> ReadValue(WeighbridgeCmdInput<ReadValueCmd> input)
        {
            return apiCaller.PostAsync<WeighbridgeCmdInput<ReadValueCmd>, bool>($"{baseUrl}/read-value", input);
        }
        public Task<bool> SetPrecision(WeighbridgeCmdInput<PrecisionCmd> input)
        {
            return apiCaller.PostAsync<WeighbridgeCmdInput<PrecisionCmd>, bool>($"{baseUrl}/set-precision", input);
        }

        public Task<bool> SetUnit(WeighbridgeCmdInput<UnitCmd> input)
        {
            return apiCaller.PostAsync<WeighbridgeCmdInput<UnitCmd>, bool>($"{baseUrl}/set-unit", input);
        }

        public Task<bool> ZeroOut(WeighbridgeCmdInput input)
        {
            return apiCaller.PostAsync<WeighbridgeCmdInput, bool>($"{baseUrl}/zero-out", input);
        }
    }
}
