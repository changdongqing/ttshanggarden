// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.Services
{
    /// <summary>
    /// 操作对话框服务
    /// </summary>
    [ScopedService]
    public class OperationDialogService(ModalService modalService, DrawerService drawerService) : IOperationDialogService
    {
        private readonly ModalService modalService = modalService;
        private readonly DrawerService drawerService = drawerService;

        /// <summary>
        /// 打开
        /// </summary>
        /// <remarks>
        /// 有输入，有输出，输出通过onClose回调返回
        /// </remarks>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TDialogInput"></typeparam>
        /// <typeparam name="TDialogOutput"></typeparam>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="dialogSettings"></param>
        /// <returns></returns>
        public async Task OpenAsync<TOperationDialog, TDialogInput, TDialogOutput>(string title, TDialogInput input, Func<TDialogOutput?, Task>? onClose = null, OperationDialogSettings? dialogSettings = null) where TOperationDialog : FeedbackComponent<TDialogInput, TDialogOutput>
        {
            dialogSettings ??= ClientConstant.DefaultOperationDialogSettings;

            if (int.TryParse(dialogSettings.Width, out int width))
            {
                dialogSettings.Width = width + "px";
            }

            if (dialogSettings.DialogType.Equals(OperationDialogType.Modal))
            {
                string bodyStyle= dialogSettings.BodyStyle ?? string.Empty;

                if (!string.IsNullOrEmpty(dialogSettings.Height))
                {
                    if (int.TryParse(dialogSettings.Height, out int height))
                    {
                        dialogSettings.Height = height + "px";
                    }
                    bodyStyle = $"height:{dialogSettings.Height};overflow:auto;" + bodyStyle;
                }
                ModalOptions modalOptions = new ()
                {
                    Title = title,
                    Centered = dialogSettings.ModalCentered,
                    MaskClosable = dialogSettings.MaskClosable,
                    Width = dialogSettings.Width,
                    Footer = null,
                    DestroyOnClose = true,
                    Maximizable = dialogSettings.ModalMaximizable,
                    BodyStyle = bodyStyle,
                    DefaultMaximized = dialogSettings.ModalDefaultMaximized
                };
                ModalRef<TDialogOutput> result = modalService.CreateModal<TOperationDialog, TDialogInput, TDialogOutput>(modalOptions, input);
                if (onClose != null)
                {
                    result.OnOk = onClose;
                    result.OnClose = () =>
                    {
                        return onClose(default);
                    };
                }
            }
            else if (dialogSettings.DialogType.Equals(OperationDialogType.Drawer))
            {
                DrawerOptions config = new ()
                {
                    Closable = dialogSettings.Closable,
                    MaskClosable = dialogSettings.MaskClosable,
                    Title = title,
                    Width = dialogSettings.Width,
                    Height = dialogSettings.Height,
                    BodyStyle = dialogSettings.BodyStyle,
                    HeaderStyle = dialogSettings.HeaderStyle,
                    Placement = dialogSettings.DrawerPlacement.ToString().ToLower()
                };

                DrawerRef<TDialogOutput> drawerRef = await drawerService.CreateAsync<TOperationDialog, TDialogInput, TDialogOutput>(config, input);
                if (onClose != null)
                {
                    drawerRef.OnClosed = onClose;
                }
            }
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <remarks>
        /// 有输入，无输出
        /// </remarks>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TDialogInput"></typeparam>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="dialogSettings"></param>
        /// <returns></returns>
        public async Task OpenAsync<TOperationDialog, TDialogInput>(string title, TDialogInput input, Func<Task>? onClose = null, OperationDialogSettings? dialogSettings = null) where TOperationDialog : FeedbackComponent<TDialogInput, bool>
        {
            Task close(bool r)
            {
                return onClose?.Invoke() ?? Task.CompletedTask;
            }
            await OpenAsync<TOperationDialog, TDialogInput, bool>(title, input, close, dialogSettings);
        }
    }
}
