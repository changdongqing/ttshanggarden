// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Impl.UserCenter.Pages.RoleView
{
    public partial class Role : ListOperateTableBase<RoleDto, int, RoleEdit, UserCenterResource>
    {
        [Inject]
        public IRoleService roleService { get; set; } = null!;
        /// <summary>
        /// 点击分配资源
        /// </summary>
        /// <returns></returns>
        private async Task OnEditRoleResourceClick(RoleDto role)
        {
            await OpenOperationDialogAsync<RoleResourceEdit, RoleDto, bool>(Localizer[nameof(SharedLocalResource.BindingResource)], role, width: "600");
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            Task<string> seedData = roleService.GetRoleResourceSeedData();
            await OpenOperationDialogAsync<ShowCode, ShowCodeOptions, bool>(SharedLocalResource.SeedData, new ShowCodeOptions(seedData), width: "1300");
        }
    }
}
