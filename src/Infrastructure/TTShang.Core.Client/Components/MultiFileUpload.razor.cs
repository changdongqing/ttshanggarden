// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Attachment.Services;
using TTShang.Core.Client.Settings;

namespace TTShang.Core.Client.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MultiFileUpload : OperationDialogBase<MultiFileUploadParams, MultiFileUploadResult>
    {
        [Inject]
        MessageService messagerService { get; set; } = null!;
        [Inject]
        IOptions<ApiSettings> apiSettings { get; set; } = null!;
        [Inject]
        IUserService userService { get; set; } = null!;
        [Inject]
        IAuthenticationStateManager authenticationStateManager { get; set; } = null!;
        [Inject]
        ILocalizationLocalizer localizer { get; set; } = null!;
        [Inject]
        IAttachmentService attachmentService { get; set; } = null!;
        /// <summary>
        /// 上传组件
        /// </summary>
        Upload? upload;
        /// 上传地址
        /// </summary>
        private string? uploadUrl;
        /// <summary>
        /// 上传附带头
        /// </summary>
        private Dictionary<string, string> headers = new Dictionary<string, string>();
        /// <summary>
        /// 按钮加载状态
        /// </summary>
        private bool loading = false;
        /// <summary>
        /// 上传参数
        /// </summary>
        [Parameter]
        public required MultiFileUploadParams UploadParams { get; set; }
        /// <summary>
        /// 结果集
        /// </summary>
        [Parameter]
        public List<UploadAttachmentOutput> FileList { get; set; } = new List<UploadAttachmentOutput>();
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<List<UploadAttachmentOutput>> FileListChanged { get; set; }
        /// <summary>
        /// 回调函数，上传完成后执行
        /// </summary>
        [Parameter]
        public EventCallback<UploadInfo> OnSingleCompleted { get; set; }
        /// <summary>
        /// 回调函数，当所有上传成功或失败时执行
        /// </summary>
        [Parameter]
        public EventCallback<UploadInfo> OnCompleted { get; set; }
        /// <summary>
        /// 自定义过程处理
        /// </summary>
        [Parameter]
        public EventCallback<UploadInfo> OnHandleChange { get; set; }
        /// <summary>
        /// 自定义上传前检查,返回false,终止上传
        /// </summary>
        [Parameter]
        public Func<UploadAttachmentOutput, bool>? OnBeforeUpload { get; set; }
        /// <summary>
        /// 自定义上传前检查,返回false,终止上传
        /// </summary>
        [Parameter]
        public Func<List<UploadAttachmentOutput>, Task<bool>>? OnBeforeAllUploadAsync { get; set; }



        private ClientListBindValue<Guid, bool> imagesPreviewBindValue = new ClientListBindValue<Guid, bool>(false);

        protected override void OnInitialized()
        {
            //如果是弹框，参数再Options中
            if (this.Options != null)
            {
                UploadParams = this.Options;
            }
            if (UploadParams != null)
            {
                uploadUrl = apiSettings.Value.BaseAddres + (UploadParams.UploadPath ?? apiSettings.Value.UploadPath);
            }
            base.OnInitialized();
        }
        /// <summary>
        /// 移除前
        /// </summary>
        /// <param name="fileinfo"></param>
        private Task<bool> OnRemove(UploadFileItem fileinfo)
        {
            if (UploadParams.RightAwayRemoveOnServer)
            {
                return attachmentService.Delete(Guid.Parse(fileinfo.Id));
            }

            //调用接口移除
            return Task.FromResult(true);
        }
        /// <summary>
        /// 处理变化
        /// </summary>
        /// <param name="fileInfo"></param>
        private async Task HandleChange(UploadInfo fileInfo)
        {
            if (upload == null)
            {
                return;
            }
            if (OnHandleChange.HasDelegate)
            {
                await OnHandleChange.InvokeAsync(fileInfo);
            }
            loading = fileInfo.File.State == UploadState.Uploading;

            if (fileInfo.File.State == UploadState.Success)
            {
                ApiResult<UploadAttachmentOutput> apiResult =
                    fileInfo.File.GetResponse<ApiResult<UploadAttachmentOutput>>(new System.Text.Json.JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true });
                if (apiResult.Succeeded && apiResult.Data != null)
                {
                    fileInfo.File.Url = apiResult.Data.Url;
                    fileInfo.File.Id=apiResult.Data.Id.ToString();
                    fileInfo.File.Response = System.Text.Json.JsonSerializer.Serialize(apiResult.Data);
                    await OnFileListChanged(upload.FileList);
                }
                else
                {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    messagerService.Error($"{apiResult.Errors} [{apiResult.StatusCode}]");
                    messagerService.Error(localizer.Combination(nameof(SharedLocalResource.Upload), nameof(SharedLocalResource.Fail)));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法

                    //标记为失败
                    fileInfo.File.State = UploadState.Fail;
                    //失败后从list中移除
                    upload.FileList.Remove(fileInfo.File);
                    await OnFileListChanged(upload.FileList);
                }
            }
            else if (fileInfo.File.State == UploadState.Fail)
            {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                messagerService.Error(localizer.Combination(nameof(SharedLocalResource.Upload), nameof(SharedLocalResource.Error)));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                //失败后从list中移除
                upload.FileList.Remove(fileInfo.File);
                await OnFileListChanged(upload.FileList);
            }
            else if (fileInfo.File.State == UploadState.Uploading)
            {
                //上传中
            }
            await InvokeAsync(StateHasChanged);
        }
        /// <summary>
        /// 上传前
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool BeforeUpload(UploadFileItem file)
        {
            //自定义
            if (OnBeforeUpload != null)
            {
                bool result = OnBeforeUpload.Invoke(Convert(file));
                if (!result)
                {
                    return false;
                }
            }
            //数量
            if (UploadParams.MaxFileNumber > 0 && FileList.Count >= UploadParams.MaxFileNumber)
            {
                messagerService.Error(string.Format(localizer[nameof(SharedLocalResource.MoreFilesTips)], UploadParams.MaxFileNumber));
                return false;
            }
            //type
            if (UploadParams.UploadFileTypes != null)
            {
                if (!UploadParams.UploadFileTypes.Any(x => x.ToLower().Equals(file.Ext.ToLower())))
                {
                    messagerService.Error(localizer[nameof(SharedLocalResource.FileTypeIsNotSupported)]);
                    return false;
                }
            }
            //size
            if (UploadParams.FileMaxSize.HasValue && file.Size > UploadParams.FileMaxSize.Value)
            {
                messagerService.Error(string.Format(localizer[nameof(SharedLocalResource.FileSizeShouldNotExceed)], FormatHelper.GetBytesReadable(UploadParams.FileMaxSize.Value)));
                return false;
            }
            return true;
        }
        /// <summary>
        /// 上传前
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private async Task<bool> BeforeAllUploadAsync(List<UploadFileItem> files)
        {
            if (OnBeforeAllUploadAsync != null)
            {
                bool result = await OnBeforeAllUploadAsync(Convert(files));
                if (!result)
                {
                    return false;
                }
            }
            //检测token
            await authenticationStateManager.TestToken("multiFileUpload");

            headers.Clear();
            //上传附件附带身份信息
            var authDic = await authenticationStateManager.GetCurrentTokenHeaders();
            if (authDic != null)
            {
                foreach (var auth in authDic)
                {
                    headers.Add(auth.Key, auth.Value);
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private Task OnFileListChanged(List<UploadFileItem> files)
        {
            this.FileList = Convert(files);
            return FileListChanged.InvokeAsync(Convert(files));
        }

        /// <summary>
        /// UploadFileItem 转 UploadAttachmentOutput
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public List<UploadAttachmentOutput> Convert(List<UploadFileItem> files)
        {
            List<UploadAttachmentOutput> uploadAttachments = files.Select(x =>
            {
                return Convert(x);
            }).ToList();
            return uploadAttachments;

        }

        /// <summary>
        /// UploadFileItem 转 UploadAttachmentOutput
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public UploadAttachmentOutput Convert(UploadFileItem file)
        {
            UploadAttachmentOutput result = new UploadAttachmentOutput() { Id = Guid.Parse(file.Id), Url = file.Url, FileSize = file.Size, FileName = file.FileName };
            if (file.Response == null)
            {
                return result;
            }
            return System.Text.Json.JsonSerializer.Deserialize<UploadAttachmentOutput>(file.Response) ?? result;
        }

        /// <summary>
        /// UploadAttachmentOutput转 UploadFileItem
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public List<UploadFileItem> Convert(List<UploadAttachmentOutput> files)
        {
            List<UploadFileItem> list = files.Select(x =>
            {
                return Convert(x);
            }).ToList();
            return list;
        }

        /// <summary>
        /// UploadAttachmentOutput转 UploadFileItem
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public UploadFileItem Convert(UploadAttachmentOutput file)
        {
            return new UploadFileItem()
            {
                Id = file.Id.ToString(),
                Url=file.Url,
                State=Enum.Parse<UploadState>(file.UploadState.ToString()),
                FileName = file.OriginalName ?? file.FileName,
                Size = file.FileSize,
                Response = System.Text.Json.JsonSerializer.Serialize(file)
            };
        }
    }
}
