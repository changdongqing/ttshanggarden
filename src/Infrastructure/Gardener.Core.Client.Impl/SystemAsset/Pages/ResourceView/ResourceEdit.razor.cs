// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Module;
using Gardener.Core.Module.Services;
using Gardener.Core.SystemAsset.Resources;
using Gardener.Core.SystemAsset.Services;

namespace Gardener.Core.Client.Impl.SystemAsset.Pages.ResourceView
{
    public partial class ResourceEdit : EditOperationDialogBase<ResourceDto,Guid, SystemAssetResource>
    {
        [Inject]
        private IResourceService resourceService { get; set; } = null!;
        [Inject]
        private ISystemModuleService systemModuleService { get; set; } = null!;

        /// <summary>
        /// 资源父级
        /// </summary>
        public string ParentId 
        {
            get 
            {
                return _editModel.ParentId?.ToString() ?? string.Empty;
            }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                { 
                    _editModel.ParentId = Guid.Parse(value);
                }
            }
        
        }

        /// <summary>
        /// 父级资源
        /// </summary>
        private List<ResourceDto> resources=new List<ResourceDto>();
        private List<SystemModuleDto> modules = new List<SystemModuleDto>();
        protected override async Task OnDataLoadingAsync()
        {
            resources = await resourceService.GetTree();
            modules = await systemModuleService.GetAll();
            await base.OnDataLoadingAsync();
        }

        protected override async Task OnDataLoadedAsync()
        {
            if (this.Options.Type.Equals(OperationDialogInputType.Add))
            {
                _editModel.Id = Guid.NewGuid();

                if (!Guid.Empty.Equals(this.Options.Data))
                {
                    _editModel.ParentId = this.Options.Data;

                    ResourceDto parent = await resourceService.Get(this.Options.Data);

                    _editModel.Type = parent.Type;

                    _editModel.Key = parent.Key + "_";

                    _editModel.ModuleName=parent.ModuleName;

                }
            }
            else 
            {
                if(_editModel.ParentId!=null && _editModel.ModuleName==null)
                {
                    ResourceDto parent = await resourceService.Get(_editModel.ParentId.Value);
                    _editModel.ModuleName = parent.ModuleName;
                }
            }
            await base.OnDataLoadedAsync();
        }
        protected override void OnInitialized()
        {
            _uniqueVerificationTool.AddField(x => x.Key);
            base.OnInitialized();
        }
    }
}
