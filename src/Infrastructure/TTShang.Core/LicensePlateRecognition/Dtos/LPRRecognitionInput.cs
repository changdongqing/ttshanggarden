// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.LicensePlateRecognition.Enums;

namespace TTShang.Core.LicensePlateRecognition.Dtos
{
    /// <summary>
    /// lpr识别输入
    /// </summary>
    public class LPRRecognitionInput
    {
        /// <summary>
        /// lpr服务类型
        /// </summary>
        public LPRServiceType ServiceType { get; set; }= LPRServiceType.OPENANPR;
        /// <summary>
        /// 最大搜索条数：默认5
        /// </summary>
        public int Limit { get; set; } = 5;

        /// <summary>
        /// Base64图片
        /// </summary>
        public string? Base64Image { get; set; }
    }
}
