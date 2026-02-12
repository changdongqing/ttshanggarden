// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.LicensePlateRecognition.Dtos
{
    /// <summary>
    /// 车牌识别结果
    /// </summary>
    public class LPRRecognitionInfo
    {
        /// <summary>
        /// 车牌置信分数:[0,100]
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// 车牌信息
        /// </summary>
        public PlateInfo? PlateInfo { get; set; }

        /// <summary>
        /// 车牌位置
        /// </summary>
        public PlateLocation? PlateLocation { get; set; }

    }
}
