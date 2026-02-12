// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Weighbridge.Dtos
{
    /// <summary>
    /// 地磅上传数据
    /// </summary>
    public class WeighbridgeUploadData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uploadDataType"></param>
        /// <param name="channel"></param>
        public WeighbridgeUploadData(UploadDataType uploadDataType, int channel)
        {
            UploadDataType = uploadDataType;
            Channel = channel;
        }
        /// <summary>
        /// 通道
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 上传数据类型
        /// </summary>
        public UploadDataType UploadDataType { get; set; }

        /// <summary>
        /// 功能码
        /// </summary>
        public int FunctionCode { get; set; }
        
        /// <summary>
        /// 重量
        /// </summary>
        public double Weight {  get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public UnitType? UnitType { get; set; }
        /// <summary>
        /// 小数位
        /// </summary>
        public PrecisionType? PrecisionType { get; set; }
        /// <summary>
        /// 是否去皮
        /// </summary>
        public bool NetWeight {  get; set; }
        /// <summary>
        /// 滤波系数
        /// </summary>
        public int? FilterCoefficient {  get; set; }
        /// <summary>
        /// AD转换速度
        /// </summary>
        public int? AdConversionSpeed {  get; set; }
        /// <summary>
        /// 零点跟踪范围
        /// </summary>
        public int? ZeroTrackingRange {  get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public int? MaxValue {  get; set; }
        /// <summary>
        /// 校准值
        /// </summary>
        public int? CalibrationValue {  get; set; }
        /// <summary>
        /// 分度值
        /// </summary>
        public int? DivisionValue {  get; set; }
    }
}
