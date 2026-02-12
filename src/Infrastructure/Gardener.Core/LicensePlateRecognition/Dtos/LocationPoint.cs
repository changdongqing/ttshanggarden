// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.LicensePlateRecognition.Dtos
{
    /// <summary>
    /// 坐标点
    /// </summary>
    public class LocationPoint
    {
        /// <summary>
        /// 坐标点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public LocationPoint(long x, long y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// X坐标
        /// </summary>
        public long X {  get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public long Y { get; set; }
    }
}
