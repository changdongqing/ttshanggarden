// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.SystemAsset.Resources;
using TTShang.Core.SystemAsset.Services;

namespace TTShang.Core.Client.Impl.SystemAsset.Pages.ResourceView
{
    public partial class ResourceFunctionBind : OperationDialogBase<Guid,bool, SystemAssetResource>
    {

        [Inject]
        IResourceService resourceService { get; set; } = null!;
        [Inject]
        IFunctionService functionService { get; set; } = null!;
        [Inject]
        IResourceFunctionService resourceFunctionService { get; set; } = null!;
        [Inject]
        IAuthenticationStateManager authenticationStateManager { get; set; }=null!;
        private Tree<ResourceDto>? _tree;
        private Table<FunctionDto>? _table;
        private bool _treeLoading = false;
        private bool _tableLoading = false;
        private ResourceDto? selectResource;
        /// <summary>
        /// 资源
        /// </summary>
        private List<ResourceDto> resources = new List<ResourceDto>();
        /// <summary>
        /// 所有接口
        /// </summary>
        private List<FunctionDto> _functionDtos = new List<FunctionDto>();
        /// <summary>
        /// 选中的接口
        /// </summary>
        private IEnumerable<FunctionDto> _selectedFunctions = new List<FunctionDto>();
        /// <summary>
        /// 已有接口
        /// </summary>
        private List<FunctionDto> _oldFunctions = new List<FunctionDto>();

        private bool hasBindPermission = false;
        protected override void OnInitialized()
        {
            hasBindPermission = authenticationStateManager.CheckCurrentUserHaveResource("system_manager_resource_function_bind_update");
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            _treeLoading = true;
            resources = await resourceService.GetTree();
            await base.OnInitializedAsync();
            _treeLoading = false;

            await LoadFunctions();
        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task TreeSelectedChanged(ResourceDto? resource)
        {
            _tableLoading=true;
            selectResource = resource;

            await LoadOldFunctions();

            if (resource != null)
            {
                _functionDtos = _functionDtos
                    .OrderByDescending(x => _oldFunctions.Any(o => o.Id.Equals(x.Id)), new FunctionBoolComparer())
                    .ThenByDescending(x => x.ModuleName != null && resource?.ModuleName != null && x.ModuleName.Equals(resource.ModuleName), new FunctionBoolComparer())
                    .ThenByDescending(x => x.Group)
                    .ThenByDescending(x => x.Tags)
                    .ThenByDescending(x => x.Key)
                    .ThenByDescending(x => x.Id)
                    .ToList();
            }
            _tableLoading = false;
        }

        private async Task LoadFunctions()
        {
            _tableLoading = true;
            var re = _table?.GetPageRequest();
            var request = new PageRequest()
            {
                PageSize = int.MaxValue,
            };
            var result = await functionService.Search(request);
            _functionDtos = result.Items.Select(model => {

                model.Group = model.GroupTitle ?? model.Group;
                model.Tags= model.TagTitles ?? model.Tags;
                model.Description = (model.Summary ?? string.Empty) + "-" + (model.Description ?? string.Empty);
                return model;
            
            }).ToList();
            _tableLoading = false;
        }

        private async Task LoadOldFunctions()
        {
            if(selectResource==null)
            {
                _oldFunctions=new();
            }else
            {
                _oldFunctions = await resourceService.GetFunctions(selectResource.Id);
            }
        }

        private async Task OnClickBind(FunctionDto function)
        {
            if (selectResource == null)
            {
                return;
            }
            _tableLoading = true;
            //新增
            var result = await resourceFunctionService.Insert(new ResourceFunctionDto()
            {
                ModuleName = selectResource.ModuleName,
                ResourceId = selectResource.Id,
                FunctionId = function.Id
            });
            if (result != null)
            {
                await LoadOldFunctions();
            }
            _tableLoading = false;
        }
        private async Task OnClickUnBind(FunctionDto function)
        {
            if(selectResource==null)
            {
                return;
            }
            _tableLoading = true;
            //被移除
            bool removed = await resourceFunctionService.Delete(selectResource.Id, function.Id);
            if(removed)
            {
                await LoadOldFunctions();
            }
            _tableLoading= false;
        }

        private bool CheckIsBind(FunctionDto function)
        {
            return _oldFunctions.Any(x => x.Id.Equals(function.Id));
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class FunctionBoolComparer : IComparer<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(bool x, bool y)
        {
            return x == y ? 0 : x == true && y == false ? 1 : -1;
        }
    }
}
