// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.UserCenter.Pages.TenantView
{
    public partial class TenantEdit : EditOperationDialogBase<SystemTenantDto, Guid, UserCenterResource>
    {
        protected override void OnInitialized()
        {
            _uniqueVerificationTool.AddField(x => x.Name);
            base.OnInitialized();
        }
    }
}
