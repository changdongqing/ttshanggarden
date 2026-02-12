// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Gardener.Core.SystemAsset.Resources;
using Gardener.Core.SystemAsset.Services;

namespace Gardener.Core.Client.Impl.SystemAsset.Pages.FunctionView
{
    public partial class Function : ListOperateTableBase<FunctionDto, Guid, FunctionEdit, SystemAssetResource>
    {
        [Inject]
        public IFunctionService functionService { get; set; } = null!;

        // <summary>
        /// 点击启用审计按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private async Task OnChangeEnableAudit(FunctionDto model, bool enableAudit)
        {
            var result = await functionService.EnableAudit(model.Id, enableAudit);
            if (!result)
            {
                model.EnableAudit = !enableAudit;
                MessageService.Error((enableAudit ? nameof(SharedLocalResource.Enable) : nameof(SharedLocalResource.Disabled)) + nameof(SharedLocalResource.Fail));
            }
        }

        /// <summary>
        /// 点击导入按钮
        /// </summary>
        /// <returns></returns>
        private async Task OnImportClick()
        {
            var setting = base.GetOperationDialogSettings();
            setting.Width = "1000";
            //setting.ModalMaximizable = true;
            setting.ModalDefaultMaximized = true;
            await OpenOperationDialogAsync<FunctionImport, int, bool>(Localizer[nameof(SharedLocalResource.Import)], 0, async r =>
            {
                await ReLoadTable();

            }, setting);
        }

    }
}
