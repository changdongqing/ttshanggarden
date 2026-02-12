// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.OperationDialog
{
    /// <summary>
    /// 空的弹出框-后续可以做成动态生成
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLocalResource"></typeparam>
    public class EmptyOperationDialog<TDto,TKey, TLocalResource> : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
    {

    }
}
