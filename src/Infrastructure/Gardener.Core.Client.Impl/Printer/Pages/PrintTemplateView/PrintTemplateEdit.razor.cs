// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using BlazorMonaco;
using BlazorMonaco.Editor;
using Gardener.Core.Attachment.Dtos;
using Gardener.Core.Attachment.Enums;
using Gardener.Core.Printer.Dtos;
using Gardener.Core.Printer.Resources;
using System.Text.Json;

namespace Gardener.Core.Client.Impl.Printer.Pages.PrintTemplateView
{
    /// <summary>
    /// 打印模板编辑页
    /// </summary>
    public partial class PrintTemplateEdit : EditOperationDialogBase<PrintTemplateDto, Guid, PrinterLocalResource>
    {
        [Inject]
        private IClientMessageService clientMessageService { get; set; } = null!;
        private StandaloneCodeEditor? _templateContentEditor = null;
        MultiFileUploadParams? UploadParams = null;

        /// <summary>
        /// 结果集
        /// </summary>
        public List<UploadAttachmentOutput> FileList { get; set; } = new List<UploadAttachmentOutput>();
        

        /// <summary>
        /// 页面初始化后
        /// </summary>
        protected override void OnInitialized()
        {

            base._uniqueVerificationTool.AddField(x => x.TemplateKey, x => x.TenantId);
            base.OnInitialized();
        }



        protected override void OnDataLoaded()
        {
            if (this.Options.Type.Equals(OperationDialogInputType.Add))
            {
                _editModel.Id = Guid.NewGuid();
            }else
            {
                _templateContentEditor?.SetValue(_editModel.TemplateContent);
            }
            UploadParams = new MultiFileUploadParams(_editModel.Id.ToString(), AttachmentBusinessType.PrintTemplatePreviewImage, 1)
            {
                FileMaxSize = 1024 * 500,
                UploadFileTypes = new List<string> { ".jpg", ".jpeg", ".png", ".gif" },
                ShowUploadBtn = OperationDialogInputType.Add.Equals(this.Options.Type) || OperationDialogInputType.Edit.Equals(this.Options.Type),
                ShowRemoveBtn = OperationDialogInputType.Add.Equals(this.Options.Type) || OperationDialogInputType.Edit.Equals(this.Options.Type),
                UploadBtnStyle = UploadBtnStyle.BlockText,
                UploadListType = UploadListType.PictureCard
            };
            if (!string.IsNullOrEmpty(_editModel.TemplatePreviewImage))
            {
                var images = _editModel.GetTemplatePreviewImageInfos();
                if (images != null)
                {
                    foreach (var item in images)
                    {
                        FileList.Add(item);
                    }
                }
            }
            base.OnDataLoaded();
        }


        protected override async Task<bool> OnVerificationBefor()
        {
            
            if (_editForm != null)
            {
                if (FileList.Any())
                {
                    _editModel.TemplatePreviewImage = JsonSerializer.Serialize(FileList);
                }
                else
                {
                    _editModel.TemplatePreviewImage = string.Empty;
                }
            }
            if (_templateContentEditor != null)
            {
                string value = await _templateContentEditor.GetValue();
                _editModel.TemplateContent = value;
            }
            if (string.IsNullOrEmpty(_editModel.TemplateContent))
            {
                clientMessageService.Warn(string.Format(ValidateErrorMessagesResource.RequiredValidationError, PrinterLocalResource.TemplateContent));
                return false;
            }
            return await base.OnVerificationBefor();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        private StandaloneEditorConstructionOptions TemplateEditorConstructionOptions(Editor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "csharp",
                FormatOnPaste = true,
                FormatOnType = true,
                ReadOnly = false
            };
        }

        protected override void Dispose(bool disposing)
        {
            _templateContentEditor?.SetValue("");
            _templateContentEditor?.Dispose();
            base.Dispose(disposing);
        }
    }
}
