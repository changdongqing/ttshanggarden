// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Dtos;
using TTShang.Core.Printer.Resources;

namespace TTShang.Core.Client.Impl.Printer.Pages.PrintTemplateView
{
    /// <summary>
    /// 打印模板列表页
    /// </summary>
    public partial class PrintTemplate : ListOperateTableBase<PrintTemplateDto, Guid, PrintTemplateEdit, PrinterLocalResource>
    {
        protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            dialogSettings.Width = "80%";
            dialogSettings.Height = "90Vh";
            base.SetOperationDialogSettings(dialogSettings);
        }
    }
}
