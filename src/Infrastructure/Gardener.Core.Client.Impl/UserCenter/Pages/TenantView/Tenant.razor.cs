// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Impl.UserCenter.Pages.TenantView
{
    public partial class Tenant : ListOperateTableBase<SystemTenantDto, Guid, TenantEdit, UserCenterResource>
    {

        [Inject]
        private ITenantService tenantService { get; set; } = default!;

        private ClientListBindValue<Guid, bool> copyTenantLoading = new ClientListBindValue<Guid, bool>(false);
        /// <summary>
        /// 点击分配资源
        /// </summary>
        /// <returns></returns>
        private async Task OnEditTenantResourceClick(SystemTenantDto tenant)
        {
            await OpenOperationDialogAsync<TenantResourceEdit, SystemTenantDto, bool>(Localizer[nameof(SharedLocalResource.BindingResource)], tenant, width: "600");
        }


        /// <summary>
        /// 复制一个新租户
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        private async Task CopyNewTenant(Guid tenantId)
        {
            copyTenantLoading[tenantId] = true;
          
            var f = await ConfirmService.YesNo(Localizer.Combination(nameof(SharedLocalResource.Copy), nameof(SharedLocalResource.Tenant)));
            if (f == ConfirmResult.Yes)
            {
                base.StartTableLoading(true);
                var result = await tenantService.CopyNewTenant(tenantId);
                if (result)
                {
                    MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Copy), nameof(SharedLocalResource.Success)));
                    await base.ReLoadTable(true);
                }
                else
                {
                    MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Copy), nameof(SharedLocalResource.Fail)));
                } 
                base.StopTableLoading(true);
            }
            copyTenantLoading[tenantId] = false;
           
        }
    }
}
