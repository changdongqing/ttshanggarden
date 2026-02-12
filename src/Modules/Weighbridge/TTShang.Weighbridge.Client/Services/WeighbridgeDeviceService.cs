// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Weighbridge.Dtos.Cmds;

namespace TTShang.Weighbridge.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class WeighbridgeDeviceService : ClientServiceCaller, IWeighbridgeDeviceService
    {
        public WeighbridgeDeviceService(IApiCaller apiCaller, string controller, string? module = null) : base(apiCaller, controller, module)
        {
        }

        public Task<bool> CalibrationValue(DeviceCmdInput<int> input)
        {
            return apiCaller.PostAsync<DeviceCmdInput<int>, bool>($"{baseUrl}/calibration-value", input);
        }

        public Task<WeighbridgeUploadData?> GetDeviceLastData(Guid deviceId)
        {
            return apiCaller.GetAsync<WeighbridgeUploadData?>($"{baseUrl}/device-last-data/{deviceId}");
        }

        public Task<bool> ReadValue(DeviceCmdInput<ReadValueCmd> input)
        {
            return apiCaller.PostAsync<DeviceCmdInput<ReadValueCmd>, bool>($"{baseUrl}/read-value", input);
        }

        public Task<bool> SetAdConversionSpeed(DeviceCmdInput<int> input)
        {
            return apiCaller.PostAsync<DeviceCmdInput<int>, bool>($"{baseUrl}/set-ad-conversion-speed", input);
        }

        public Task<bool> SetDivisionValue(DeviceCmdInput<int> input)
        {
            return apiCaller.PostAsync<DeviceCmdInput<int>, bool>($"{baseUrl}/set-division-value", input);
        }

        public Task<bool> SetFilterCoefficient(DeviceCmdInput<int> input)
        {
            return apiCaller.PostAsync<DeviceCmdInput<int>, bool>($"{baseUrl}/set-filter-coefficient", input);
        }

        public Task<bool> SetMaxValue(DeviceCmdInput<int> input)
        {
            return apiCaller.PostAsync<DeviceCmdInput<int>, bool>($"{baseUrl}/set-max-value", input);
        }

        public Task<bool> SetPrecision(DeviceCmdInput<PrecisionCmd> input)
        {
            return apiCaller.PostAsync<DeviceCmdInput<PrecisionCmd>, bool>($"{baseUrl}/set-precision", input);
        }

        public Task<bool> SetZeroTrackingRange(DeviceCmdInput<int> input)
        {
            return apiCaller.PostAsync<DeviceCmdInput<int>, bool>($"{baseUrl}/set-zero-tracking-range", input);
        }

        public Task<bool> ZeroOut(DeviceCmdInput input)
        {
            return apiCaller.PostAsync<DeviceCmdInput, bool>($"{baseUrl}/zero-out", input);
        }
    }
}
