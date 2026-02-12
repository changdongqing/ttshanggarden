// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.UserCenter.Pages.RoleView
{
    public partial class RoleEdit : EditOperationDialogBase<RoleDto, int, UserCenterResource>
    {
        protected override void OnInitialized()
        {
            _uniqueVerificationTool.AddField(x => x.Name, x => x.TenantId);
            base.OnInitialized();
        }
    }
}
