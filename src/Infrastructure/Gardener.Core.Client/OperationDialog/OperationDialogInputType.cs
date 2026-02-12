// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.OperationDialog
{
    /// <summary>
    /// 操作框输入类型
    /// </summary>
    public enum OperationDialogInputType : int
    {
        /// <summary>
        /// 添加:00001
        /// </summary>
        Add = 1,
        /// <summary>
        /// 编辑:00010
        /// </summary>
        Edit = 2,
        /// <summary>
        /// 查看:00100
        /// </summary>
        Select = 4,
        /// <summary>
        /// 其它:10000
        /// </summary>
        Other = 16
    }
}
