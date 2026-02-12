// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Weighbridge.Dtos
{
    /// <summary>
    /// 今日称重统计
    /// </summary>
    public class TodayWeighingStatistics
    {
        /// <summary>
        /// 今日称重统计
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <param name="lastWeighingTime"></param>
        /// <param name="count"></param>
        /// <param name="lastWeighingRecord"></param>
        public TodayWeighingStatistics(string plateNumber, DateTimeOffset lastWeighingTime, int count, WeighingRecordDto lastWeighingRecord)
        {
            PlateNumber = plateNumber;
            LastWeighingTime = lastWeighingTime;
            Count = count;
            LastWeighingRecord = lastWeighingRecord;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber {  get; set; }
        /// <summary>
        /// 最后称重事件
        /// </summary>
        public DateTimeOffset LastWeighingTime {  get; set; }
        /// <summary>
        /// /称重次数
        /// </summary>
        public int Count {  get; set; }
        /// <summary>
        /// 最后一次记录
        /// </summary>
        public WeighingRecordDto LastWeighingRecord { get; set; }
    }
}
