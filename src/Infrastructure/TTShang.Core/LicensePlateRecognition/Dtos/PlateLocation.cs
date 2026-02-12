// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.LicensePlateRecognition.Dtos
{
    /// <summary>
    /// 车牌位置
    /// </summary>
    public class PlateLocation
    {
        /// <summary>
        /// 车牌位置
        /// </summary>
        /// <param name="leftTop"></param>
        /// <param name="rightTop"></param>
        /// <param name="rightBottom"></param>
        /// <param name="leftBottom"></param>
        public PlateLocation(LocationPoint leftTop, LocationPoint rightTop, LocationPoint rightBottom, LocationPoint leftBottom)
        {
            LeftTop = leftTop;
            RightTop = rightTop;
            RightBottom = rightBottom;
            LeftBottom = leftBottom;
        }

        /// <summary>
        /// 左上角坐标值
        /// </summary>
        public LocationPoint LeftTop { get; set; }
        /// <summary>
        /// 右上角坐标
        /// </summary>
        public LocationPoint RightTop { get; set; }
        /// <summary>
        /// 右下角坐标
        /// </summary>
        public LocationPoint RightBottom { get; set; }
        /// <summary>
        /// 左下角坐标
        /// </summary>
        public LocationPoint LeftBottom { get; set; }
    }
}
