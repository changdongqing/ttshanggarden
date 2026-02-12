// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Swagger.Dtos;
using Gardener.Core.Swagger.Services;
using Gardener.Core.SystemAsset.Resources;
using Gardener.Core.SystemAsset.Services;

namespace Gardener.Core.Client.Impl.SystemAsset.Pages.FunctionView
{
    public partial class FunctionImport : OperationDialogBase<int, bool, SystemAssetResource>
    {
        [Inject]
        private ISwaggerService SwaggerService { get; set; } = null!;
        [Inject]
        private IFunctionService FunctionService { get; set; } = null!;
        [Inject]
        private IClientMessageService MessageService { get; set; } = null!;
        [Inject]
        private NotificationService NoticeService { get; set; } = null!;

        IEnumerable<SwaggerSpecificationOpenApiInfoDto> apiInfos = new List<SwaggerSpecificationOpenApiInfoDto>();
        private List<FunctionDto> _functionDtos = new List<FunctionDto>();
        private IEnumerable<FunctionDto> _selectedFunctionDtos = new List<FunctionDto>();
        private string _selectedGroupValue = string.Empty;
        private SwaggerSpecificationOpenApiInfoDto? _selectedGroup;
        private bool _loading = false;
        private bool _importLoading = false;
        private bool _importIsBegin = false;
        private double _importPercent = 0;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            apiInfos = await SwaggerService.GetApiGroup();
            if (apiInfos != null && apiInfos.Any())
            {
                OnSelectedItemChangedHandler(apiInfos.First());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void OnSelectedItemChangedHandler(SwaggerSpecificationOpenApiInfoDto value)
        {
            _functionDtos = new List<FunctionDto>();
            _selectedGroup = value;
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        private async Task OnLoad()
        {
            _loading = true;
            _functionDtos = new List<FunctionDto>();
            var result = await SwaggerService.GetApis(groupName: _selectedGroupValue);
            if (result != null)
            {
                result.ForEach(x =>
                {
                    FunctionDto functionDto = x.Adapt<FunctionDto>();
                    functionDto.FillApiInfo(x);
                    _functionDtos.Add(functionDto);
                });
            }
            _loading = false;

        }
        // <summary>
        /// 点击启用审计按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        private Task OnChangeEnableAudit(FunctionDto model, bool enableAudit)
        {
            //todo: AddField operation logic here
            return Task.CompletedTask;
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        private async Task OnCancleClick()
        {
            await base.FeedbackRef!.CloseAsync(false);
        }
        /// <summary>
        /// 导入
        /// </summary>
        /// <returns></returns>
        private async Task OnImportClick()
        {
            _importLoading = true;
            if (_selectedFunctionDtos == null || !_selectedFunctionDtos.Any())
            {
                MessageService.Warn(Localizer[nameof(SharedLocalResource.NoRowsAreSelected)]);
                _importLoading = false;
                return;
            }
            //开始导入
            _importIsBegin = true;
            _importPercent = 0;
            int count = 0, insertCount = 0, errorCount = 0, repetitionCount = 0;
            foreach (var item in _selectedFunctionDtos)
            {
                count++;
                FunctionDto? dto = await FunctionService.GetByKey(item.Key);
                if (dto == null)
                {
                    FunctionDto function = await FunctionService.Insert(item);
                    if (function == null)
                    {
                        errorCount++;
                    }
                    else
                    {
                        insertCount++;
                    }
                }
                else
                {
                    item.Id = dto.Id;
                    item.ModuleName = dto.ModuleName;
                    item.EnableAudit = dto.EnableAudit;
                    bool result = await FunctionService.Update(item);
                    if (result)
                    {
                        repetitionCount++;
                    }
                    else
                    {
                        errorCount++;
                    }

                }
                _importPercent = Math.Round((count / (double)_selectedFunctionDtos.Count()) * 100, 2);
                await InvokeAsync(StateHasChanged);

            }
            await NoticeService.Open(new NotificationConfig()
            {
                Message = "导入结果通知",
                Description = $"共选择{count}条,更新已存在{repetitionCount}条,导入{insertCount}条,失败{errorCount}条",
                NotificationType = NotificationType.Success,
                Duration = 2
            });

            _importIsBegin = false;
            _importLoading = false;
        }
    }
}
