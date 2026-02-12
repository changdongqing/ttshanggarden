// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Weighbridge.Impl.Core
{
    /// <summary>
    /// 上载数据类型
    /// </summary>
    public struct T100UploadDataType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueH"></param>
        /// <param name="valueL"></param>
        public T100UploadDataType(byte valueH, byte valueL)
        {
            ValueH = valueH;
            ValueL = valueL;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte ValueH { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public byte ValueL { get; init; }

        /// <summary>
        /// 读值（功能码03）
        /// </summary>
        public static T100UploadDataType ReadValue = new T100UploadDataType(0, 0);

        /// <summary>
        /// 去皮
        /// </summary>
        public static T100UploadDataType Netweight = new T100UploadDataType(0, 17);

        /// <summary>
        /// 单位
        /// </summary>
        public static T100UploadDataType Unit = new T100UploadDataType(0, 2);

        /// <summary>
        /// 小数位
        /// </summary>
        public static T100UploadDataType Precision = new T100UploadDataType(0, 1);

        /// <summary>
        /// 清零
        /// </summary>
        public static T100UploadDataType ZeroOut = new T100UploadDataType(0, 22);
    }
}
