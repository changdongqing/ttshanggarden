// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Gardener.Core.Printer.Enums;

namespace Gardener.Weighbridge.Client.Services
{
    /// <summary>
    ///  WeighingRecord服务
    /// </summary>
    [ScopedService]
    public class WeighingRecordService : ClientServiceBase<WeighingRecordDto, Guid>, IWeighingRecordService
    {
        /// <summary>
        ///  WeighingRecord服务
        /// </summary>
        public WeighingRecordService(IApiCaller apiCaller) : base(apiCaller, "weighing-record", "weighbridge")
        {
        }

        public Task<WeighingRecordDto?> FindLastByPlateNumber(string plateNumber, bool noLoadPriority)
        {
            return apiCaller.GetAsync<WeighingRecordDto?>($"{baseUrl}/last-by-plate-number/{plateNumber}/{noLoadPriority}");
        }

        public Task<string?> GenerateRecordPrintDataBase64(GenerateRecordPrintDataInput input)
        {
            return apiCaller.PostAsync<GenerateRecordPrintDataInput, string?>($"{baseUrl}/generate-record-print-data-base64", input);
        }

        public Task<string?> GetRecordPrintDataBase64(Guid weighingRecord, PrintProtocolType protocolType, string templateKey)
        {
            return apiCaller.GetAsync<string?>($"{baseUrl}/record-print-data-base64/{weighingRecord}?{nameof(protocolType)}={protocolType}&{nameof(templateKey)}={templateKey}");
        }

        public Task<List<WeighingRecordDto>> GetTodayUnfinishedRecord(Guid weighbridgeConfigId)
        {
            return apiCaller.GetAsync<List<WeighingRecordDto>>($"{baseUrl}/today-unfinished-record/{weighbridgeConfigId}");
        }

        public Task<WeighingRecordDto?> SaveWeighingRecord(WeighingRecordDto weighingRecord)
        {
            return apiCaller.PostAsync<WeighingRecordDto, WeighingRecordDto?>($"{baseUrl}/save-weighing-record", weighingRecord);
        }
        public Task<List<WeighingRecordLogDto>> GetWeighingRecordLogs(Guid weighingRecordId)
        {
            return apiCaller.GetAsync<List<WeighingRecordLogDto>>($"{baseUrl}/{weighingRecordId}/weighing-record-logs");
        }
    }
}
