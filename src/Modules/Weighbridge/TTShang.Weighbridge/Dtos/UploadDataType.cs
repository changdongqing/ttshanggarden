// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Weighbridge.Dtos
{
    /// <summary>
    /// 上载数据类型
    /// </summary>
    public enum UploadDataType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,
        /// <summary>
        /// 读值（功能码03）
        /// </summary>
        ReadValue,
        /// <summary>
        /// 去皮
        /// </summary>
        Netweight,
        /// <summary>
        /// 单位
        /// </summary>
        Unit,
        /// <summary>
        /// 小数位
        /// </summary>
        Precision,
        /// <summary>
        /// 清零
        /// </summary>
        ZeroOut
    }
}
