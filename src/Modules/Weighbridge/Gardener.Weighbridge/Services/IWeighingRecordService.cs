// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Common;
using Gardener.Core.Printer.Enums;
using Gardener.Weighbridge.Dtos;

namespace Gardener.Weighbridge.Services
{
    /// <summary>
    ///  WeighingRecord服务
    /// </summary>
    public interface IWeighingRecordService : IServiceBase<WeighingRecordDto, Guid>
    {
        /// <summary>
        /// 根据车牌号获取最新记录
        /// </summary>
        /// <remarks>
        /// 根据车牌号获取最新称重记录，如果没有返回null
        /// </remarks>
        /// <param name="plateNumber">车牌号</param>
        /// <param name="noLoadPriority">无货优先</param>
        /// <returns></returns>
        Task<WeighingRecordDto?> FindLastByPlateNumber(string plateNumber, bool noLoadPriority);
        /// <summary>
        /// 保存称重记录
        /// </summary>
        /// <remarks>
        /// 保存称重记录
        /// </remarks>
        /// <param name="weighingRecord"></param>
        /// <returns></returns>
        Task<WeighingRecordDto?> SaveWeighingRecord(WeighingRecordDto weighingRecord);

        /// <summary>
        /// 获取当天未结束记录
        /// </summary>
        /// <remarks>
        /// 获取当天未结束记录
        /// </remarks>
        /// <param name="weighbridgeConfigId"></param>
        /// <returns></returns>
        Task<List<WeighingRecordDto>> GetTodayUnfinishedRecord(Guid weighbridgeConfigId);

        /// <summary>
        /// 获取记录打印数据base64格式
        /// </summary>
        /// <param name="weighingRecord"></param>
        /// <param name="protocolType"></param>
        /// <param name="templateKey"></param>
        /// <returns></returns>
        Task<string?> GetRecordPrintDataBase64(Guid weighingRecord, PrintProtocolType protocolType, string templateKey);

        /// <summary>
        /// 将记录生成base64格式打印数据
        /// </summary>
        /// <remarks>
        /// 将记录生成base64格式打印数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string?> GenerateRecordPrintDataBase64(GenerateRecordPrintDataInput input);


        /// <summary>
        /// 获取称重记录日志列表
        /// </summary>
        /// <remarks>
        /// 根据称重记录编号，获取称重记录日志列表
        /// </remarks>
        /// <param name="weighingRecordId"></param>
        /// <returns></returns>
        Task<List<WeighingRecordLogDto>> GetWeighingRecordLogs(Guid weighingRecordId);
    }
}
