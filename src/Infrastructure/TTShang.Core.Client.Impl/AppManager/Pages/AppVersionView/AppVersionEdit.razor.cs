// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Attachment.Enums;

namespace TTShang.Core.Client.Impl.AppManager.Pages.AppVersionView
{
    /// <summary>
    /// 应用版本编辑页
    /// </summary>
    public partial class AppVersionEdit : EditOperationDialogBase<AppVersionDto, Int64, AppManagerResource>
    {
        [Inject]
        private IAppService appService { get; set; } = null!;

        private List<AppDto> _apps = new List<AppDto>();
        /// <summary>
        /// 页面初始化后
        /// </summary>
        protected override void OnInitialized()
        {
            _uniqueVerificationTool.AddField(x => x.VersionNumber, x => x.AppId, x => x.Environment);
            _uniqueVerificationTool.AddField(x => x.VersionName, x => x.AppId, x => x.Environment);
            base.OnInitialized();
        }

        protected override async Task OnDataLoadingAsync()
        {
            _apps = await appService.GetAllUsable();
            await base.OnDataLoadingAsync();
        }

        protected override void OnDataLoaded()
        {
            UploadParams = new MultiFileUploadParams(() => _editModel.AppId + "_" + _editModel.VersionNumber, AttachmentBusinessType.AppPackage, 0, saveOriginalName: true)
            {
                FileMaxSize = 1024 * 1024 * 500,
                UploadFileTypes = new List<string> { ".apk" },
                ShowUploadBtn = OperationDialogInputType.Add.Equals(this.Options.Type) || OperationDialogInputType.Edit.Equals(this.Options.Type),
                ShowRemoveBtn = OperationDialogInputType.Add.Equals(this.Options.Type) || OperationDialogInputType.Edit.Equals(this.Options.Type),
                ShowUploadList = false
            };
            base.OnDataLoaded();
        }
        MultiFileUploadParams? UploadParams = null;


        private void OnSingleCompleted(UploadInfo uploadInfo)
        {
            _editModel.AppLocalUrl = uploadInfo.File.Url;
        }
    }
}
