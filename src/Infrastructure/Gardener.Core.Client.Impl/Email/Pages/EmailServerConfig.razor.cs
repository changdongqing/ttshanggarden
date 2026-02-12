// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Email.Dtos;
using Gardener.Core.Email.Resources;

namespace Gardener.Core.Client.Impl.Email.Pages
{
    public partial class EmailServerConfig : ListOperateTableBase<EmailServerConfigDto, Guid, EmailServerConfigEdit, EmailLocalResource>
    {
        /// <summary>
        /// 点击发送按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected async Task OnClickSend(Guid id)
        {
            OperationDialogSettings drawerSettings = GetOperationDialogSettings();
            OperationDialogInput<Guid> input = OperationDialogInput<Guid>.Select(id);
            await OpenOperationDialogAsync<EmailServerConfigTest, OperationDialogInput<Guid>, OperationDialogOutput<Guid>>(Localizer[nameof(SharedLocalResource.Send)], input);
        }
    }
}
