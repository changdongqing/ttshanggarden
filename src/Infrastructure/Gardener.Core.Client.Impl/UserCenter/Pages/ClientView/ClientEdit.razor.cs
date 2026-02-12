// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Impl.UserCenter.Pages.ClientView
{
    public partial class ClientEdit : EditOperationDialogBase<ClientDto, Guid, UserCenterResource>
    {

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (this.Options.Type.Equals(OperationDialogInputType.Add))
            { 
                 _editModel.Id = Guid.NewGuid();
                 _editModel.SecretKey = Guid.NewGuid().ToString();
            }
        }
    }
}
