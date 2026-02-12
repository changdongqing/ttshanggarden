// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.UserCenter.Pages.TenantConfigView
{
    /// <summary>
    /// 租户配置列表页
    /// </summary>
    public partial class TenantConfig : ListOperateTableBase<SystemTenantConfigDto, Int32, TenantConfigEdit, UserCenterResource>
    {
        private List<SystemTenantConfigTemplateDto> _templates = new List<SystemTenantConfigTemplateDto>();
        [Inject]
        private ITenantConfigTemplateService tenantConfigTemplateService { get; set; } = null!;
        protected override async Task OnInitializedAsync()
        {
            _templates = await tenantConfigTemplateService.GetAllUsable();

            await base.OnInitializedAsync();
        }

        protected override async Task PageListDataHadnle(IEnumerable<SystemTenantConfigDto> datas)
        {
            await base.LoadTenants(true);
            foreach (var item in datas)
            {
                item.Tenant = GetTenant(item.TenantId);
            }
            await base.PageListDataHadnle(datas);
        }
    }
}
