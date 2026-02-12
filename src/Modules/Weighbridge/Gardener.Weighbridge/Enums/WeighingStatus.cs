// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Weighbridge.Enums
{
    /// <summary>
    /// 称重状态
    /// </summary>
    public enum WeighingStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown =0,
        /// <summary>
        /// 空车
        /// </summary>
        [Description("空车")]
        NoLoadGoods =1,
        /// <summary>
        /// 载货
        /// </summary>
        [Description("载货")]
        LoadGoods =2,
        /// <summary>
        /// 已卸货
        /// </summary>
        [Description("已卸货")]
        UnloadedGoods =3,
        /// <summary>
        /// 已装货
        /// </summary>
        [Description("已装货")]
        LoadedGoods =4,
        /// <summary>
        /// 卸货中
        /// </summary>
        [Description("卸货中")]
        UnloadingGoods =5,
        /// <summary>
        /// 装货中
        /// </summary>
        [Description("装货中")]
        LoadingGoods = 6
    }
}
