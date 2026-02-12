// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module.Services;
using Gardener.Core.SystemAsset.Resources;

namespace Gardener.Core.Client.Impl.SystemAsset.Pages.FunctionView
{
    public partial class FunctionEdit: EditOperationDialogBase<FunctionDto,Guid, SystemAssetResource>
    {
        private List<string> modules = new List<string>();

        [Inject]
        private ISystemModuleService systemModuleService { get; set; } = null!;
        protected override async Task OnDataLoadingAsync()
        {
            var moduleInfos = await systemModuleService.GetAll();
            if (moduleInfos != null)
            {
                modules = moduleInfos.Select(x => x.Name).ToList();
            }
            await base.OnDataLoadingAsync();
        }
    }
}
