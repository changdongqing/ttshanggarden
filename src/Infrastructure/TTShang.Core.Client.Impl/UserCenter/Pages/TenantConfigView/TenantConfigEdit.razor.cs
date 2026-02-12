// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------



using TTShang.Core.Attachment.Enums;

namespace TTShang.Core.Client.Impl.UserCenter.Pages.TenantConfigView
{

    /// <summary>
    /// 租户配置编辑页
    /// </summary>
    public partial class TenantConfigEdit : EditOperationDialogBase<SystemTenantConfigDto, Int32, UserCenterResource>
    {

        private List<SystemTenantConfigTemplateDto> _templates = new List<SystemTenantConfigTemplateDto>();

        [Inject]
        private ITenantConfigTemplateService tenantConfigTemplateService { get; set; } = null!;

        MultiFileUploadParams? UploadParams = null;
        private void OnUploadCompleted(UploadInfo upload)
        {
            _editModel.ConfigValue = upload.File.Url;
        }
        /// <summary>
        /// 页面初始化后
        /// </summary>
        protected override void OnInitialized()
        {
            UploadParams = new MultiFileUploadParams(_editModel.Id.ToString(), AttachmentBusinessType.TenantConfigFile, 1)
            {
                FileMaxSize = 1024 * 1024 * 10,
                //UploadFileTypes = new List<string> { ".jpg", ".jpeg", ".png", ".gif" },
                ShowUploadBtn = OperationDialogInputType.Add.Equals(this.Options.Type) || OperationDialogInputType.Edit.Equals(this.Options.Type),
                ShowRemoveBtn = OperationDialogInputType.Add.Equals(this.Options.Type) || OperationDialogInputType.Edit.Equals(this.Options.Type),
                UploadBtnStyle = UploadBtnStyle.Button,
                UploadListType = UploadListType.Text
            };
            base.OnInitialized();
        }

        protected override async Task OnDataLoadingAsync()
        {
            _templates = await tenantConfigTemplateService.GetAllUsable();
            await base.OnDataLoadingAsync();
        }

        private void OnSelectedItemChanged(SystemTenantConfigTemplateDto systemTenantConfigTemplate)
        {
            _editModel.ConfigValue = systemTenantConfigTemplate.DefaultConfigValue;
        }

    }
}
