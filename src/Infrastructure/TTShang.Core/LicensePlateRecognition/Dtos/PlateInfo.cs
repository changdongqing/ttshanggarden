// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.LicensePlateRecognition.Enums;

namespace TTShang.Core.LicensePlateRecognition.Dtos
{
    /// <summary>
    /// 车牌信息
    /// </summary>
    public class PlateInfo
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        /// <param name="plateNo"></param>
        public PlateInfo(string plateNo)
        {
            PlateNo = plateNo;
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNo { get; set; }

        /// <summary>
        /// 车牌布局
        /// </summary>
        public PlateLayout PlateLayout { get; set; } = PlateLayout.UNKNOWN;

        /// <summary>
        /// 车牌颜色
        /// </summary>
        public PlateColor PlateColor { get; set; } = PlateColor.UNKNOWN;
    }
}
