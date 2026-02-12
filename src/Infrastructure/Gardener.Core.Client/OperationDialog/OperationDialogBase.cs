// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Components.PageBaseClass;

namespace Gardener.Core.Client.OperationDialog
{

    /// <summary>
    /// 弹出框基类
    /// </summary>
    /// <typeparam name="TDialogInput">输入参数的类型</typeparam>
    /// <typeparam name="TDialogOutput">输出参数的类型</typeparam>
    /// <typeparam name="TLocalResource">本地化资源类</typeparam>
    public abstract class OperationDialogBase<TDialogInput, TDialogOutput, TLocalResource> : ReuseTabsPageAndFormBase<TDialogInput, TDialogOutput>
    {
        /// <summary>
        /// 本地化
        /// </summary>
        [Inject]
        protected ILocalizationLocalizer<TLocalResource> Localizer { get; set; } = null!;
        /// <summary>
        /// 操作对话框
        /// </summary>
        [Inject]
        protected IOperationDialogService OperationDialogService { get; set; } = null!;

        /// <summary>
        /// 获取操作会话配置
        /// </summary>
        /// <remarks>
        /// 设置的是，在本页面打开的操作框，而不是设置自己作为操作框被打开，设置权在调用者手里
        /// </remarks>
        /// <returns></returns>
        protected virtual OperationDialogSettings GetOperationDialogSettings()
        {
            OperationDialogSettings dialogSettings = new OperationDialogSettings();
            ClientConstant.DefaultOperationDialogSettings.Adapt(dialogSettings);
            SetOperationDialogSettings(dialogSettings);
            return dialogSettings;
        }

        /// <summary>
        /// 设置操作会话配置
        /// </summary>
        /// <remarks>
        /// 设置的是，在本页面打开的操作框，而不是设置自己作为操作框被打开，设置权在调用者手里
        /// </remarks>
        /// <param name="dialogSettings"></param>
        protected virtual void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            //set OperationDialogSettings
        }


        /// <summary>
        /// 打开操作对话框
        /// </summary>
        /// <remarks>
        /// 基于<see cref="IOperationDialogService"/>的快捷操作
        /// </remarks>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        protected async Task OpenOperationDialogAsync<TOperationDialog, TInput, TOutput>(string title,
            TInput input,
            Func<TOutput?, Task>? onClose = null,
            OperationDialogSettings? operationDialogSettings = null,
            string? width = null) where TOperationDialog : FeedbackComponent<TInput, TOutput>
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            if (width!=null)
            {
                settings.Width = width;
            }
            await OperationDialogService.OpenAsync<TOperationDialog, TInput, TOutput>(title, input, onClose, settings);
        }


        /// <summary>
        /// 打开操作对话框
        /// </summary>
        /// <remarks>
        /// 基于<see cref="IOperationDialogService"/>的快捷操作
        /// </remarks>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        protected async Task OpenOperationDialogAsync<TOperationDialog, TInput>(string title,
            TInput input,
            Func<Task>? onClose = null,
            OperationDialogSettings? operationDialogSettings = null,
            string? width = null) where TOperationDialog : FeedbackComponent<TInput, bool>
        {
            Func<bool, Task> close = r =>
            {
                return onClose == null ? Task.CompletedTask : onClose.Invoke();
            };

            await OpenOperationDialogAsync<TOperationDialog, TInput, bool>(title, input, close, operationDialogSettings, width);
        }
    }

    /// <summary>
    /// 弹出框基类
    /// </summary>
    /// <typeparam name="TDialogInput"></typeparam>
    /// <typeparam name="TDialogOutput"></typeparam>
    /// <typeparam name="TLocalResource"></typeparam>
    public class OperationDialogBase<TDialogInput, TDialogOutput> : OperationDialogBase<TDialogInput, TDialogOutput, SharedLocalResource>
    {
    }

    /// <summary>
    /// 弹出框基类
    /// </summary>
    /// <remarks>
    /// 看似没有返回参数，其实是默认认为是 <see cref="bool"/> 类型
    /// </remarks>
    /// <typeparam name="TDialogInput">输入参数的类型</typeparam>
    public abstract class OperationDialogBase<TDialogInput> : OperationDialogBase<TDialogInput, bool>
    {

    }

}
