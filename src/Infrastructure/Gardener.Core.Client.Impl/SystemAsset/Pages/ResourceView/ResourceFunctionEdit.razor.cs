// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.TableModels;
using Gardener.Core.SystemAsset.Resources;
using Gardener.Core.SystemAsset.Services;

namespace Gardener.Core.Client.Impl.SystemAsset.Pages.ResourceView
{

    /// <summary>
    /// 
    /// </summary>
    public partial class ResourceFunctionEdit : OperationDialogBase<ResourceFunctionEditOption, bool, SystemAssetResource>
    {
        [Inject]
        IFunctionService functionService { get; set; } = null!;
        [Inject]
        IResourceFunctionService resourceFunctionService { get; set; } = null!;
        [Inject]
        IResourceService resourceService { get; set; } = null!;
        [Inject]
        IClientMessageService messageService { get; set; } = null!;
        [Inject]
        IConfirmService confirmService { get; set; } = null!;
        private List<FunctionDto> _functionDtos = new List<FunctionDto>();
        private List<FunctionDto> _selectedFunctionDtos = new List<FunctionDto>();
        private List<FunctionDto> _oldFunctionDtos = new List<FunctionDto>();
        /// <summary>
        /// table引用
        /// </summary>
        private ITable? _table;
        /// <summary>
        /// tableSearch引用
        /// </summary>
        private TableSearch<FunctionDto>? tableSearch;

        private bool _loading = false;

        private TableSearchSettings tableSearchSettings = new TableSearchSettings();

        protected override void OnInitialized()
        {
            //默认增加模块参数选择
            if (this.Options.Resource.ModuleName != null)
            {
                tableSearchSettings.DefaultValue.Add(nameof(FunctionDto.ModuleName), this.Options.Resource.ModuleName);
            }

            base.OnInitialized();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        private async Task OnCancleClick()
        {
            await base.CloseAsync(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="functions"></param>
        /// <returns></returns>
        private void SelectedRowsChanged(IEnumerable<FunctionDto> functions)
        {
            _selectedFunctionDtos = functions.ToList();
        }

        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        private async Task OnFunctionDeletesClick()
        {
            if (_selectedFunctionDtos == null || _selectedFunctionDtos.Count <= 0)
            {
                messageService.Warn(Localizer[nameof(SharedLocalResource.NoRowsAreSelected)]);
                return;
            }
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                foreach (var item in _selectedFunctionDtos)
                {
                    await resourceFunctionService.Delete(this.Options.Resource.Id, item.Id);
                }
                messageService.Success(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Success)));
                await OnLoad();
                await RefreshPageDom();
            }
        }
        /// <summary>
        /// 点击显示关联按钮
        /// </summary>
        private async Task OnShowFunctionAddPageClick(ResourceDto resource)
        {
            await OpenOperationDialogAsync<ResourceFunctionEdit, ResourceFunctionEditOption, bool>(
                $"{Localizer[nameof(SystemAssetResource.BindingApi)]}-[{this.Options.Name}]",
                     new ResourceFunctionEditOption(resource, this.Options.Name, 1),
                     width: "1600",
            onClose: async result =>
            {
                if (result)
                {
                    await OnLoad();
                    await RefreshPageDom();
                }
            });

        }
        /// <summary>
        /// 绑定加载中
        /// </summary>
        private bool _bindLoading = false;
        /// <summary>
        /// 点击关联选中按钮
        /// </summary>
        private async Task OnFunctionAddClick()
        {
            if (_selectedFunctionDtos == null || _selectedFunctionDtos.Count <= 0)
            {
                messageService.Warn(Localizer[nameof(SharedLocalResource.NoRowsAreSelected)]);
                return;
            }
            _bindLoading = true;

            bool result = await resourceFunctionService.BatchInsert(_selectedFunctionDtos.Select(x =>
            {
                return new ResourceFunctionDto
                {
                    ResourceId = this.Options.Resource.Id,
                    FunctionId = x.Id,
                    CreatedTime = DateTimeOffset.Now
                };
            }).ToList());
            if (result)
            {
                messageService.Success(Localizer.Combination(nameof(SharedLocalResource.Binding), nameof(SharedLocalResource.Success)));
                await base.CloseAsync(true);
            }
            else
            {
                messageService.Error(Localizer.Combination(nameof(SharedLocalResource.Binding), nameof(SharedLocalResource.Fail)));
            }
            _bindLoading = false;
        }

        /// <summary>
        /// 下载种子数据
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadSeedDataClick(ResourceDto dto)
        {
            //找到所有编号
            List<Guid> resourceIds = new List<Guid>()
            {
                dto.Id
            };
            resourceIds.AddRange(TreeHelper.GetAllChildrenNodes(dto, dto => dto.Id, dto => dto.Children));


            Task<string> data = resourceFunctionService.GetSeedData(resourceIds);

            await OpenOperationDialogAsync<ShowCode, ShowCodeOptions, bool>(
                       Localizer[nameof(SharedLocalResource.SeedData)],
                       new ShowCodeOptions(data),
                       width: "1300");
        }
        /// <summary>
        /// 查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        private async Task onChange(QueryModel<FunctionDto> queryModel)
        {
            await OnLoad();
        }
        /// <summary>
        /// OnLoad
        /// </summary>
        /// <returns></returns>
        private async Task OnLoad()
        {
            _loading = true;
            //根据资源编号获取关联的接口
            _oldFunctionDtos = await resourceService.GetFunctions(this.Options.Resource.Id);
            if (this.Options.Type == 0)
            {
                //根据资源编号获取关联的接口
                _functionDtos = _oldFunctionDtos;
            }
            else if (this.Options.Type == 1)
            {
                if (this._table == null)
                {
                    return;
                }
                PageRequest pageRequest = this._table.GetPageRequest();
                pageRequest.PageSize = int.MaxValue;
                if (tableSearch != null)
                {
                    pageRequest.FilterGroups.AddRange(tableSearch.GetFilterGroups());
                }
                PageList<FunctionDto> page = await functionService.Search(pageRequest);
                IEnumerable<FunctionDto> tempFunctionDtos = page.Items;
                //移除已选择的
                if (_oldFunctionDtos != null)
                {
                    _functionDtos = tempFunctionDtos.Where(y => !_oldFunctionDtos.Any(x => x.Id.Equals(y.Id))).ToList();
                }
                else
                {
                    _functionDtos = tempFunctionDtos.ToList();
                }
            }
            _loading = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._table?.ResetData();
            }
            base.Dispose(disposing);
        }
    }
}
