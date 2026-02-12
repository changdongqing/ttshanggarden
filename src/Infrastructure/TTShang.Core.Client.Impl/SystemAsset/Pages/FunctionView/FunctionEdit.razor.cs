// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module.Services;
using TTShang.Core.SystemAsset.Resources;

namespace TTShang.Core.Client.Impl.SystemAsset.Pages.FunctionView
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
