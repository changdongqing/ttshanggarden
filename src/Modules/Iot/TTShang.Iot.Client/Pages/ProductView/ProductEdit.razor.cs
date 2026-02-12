// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Attachment.Dtos;
using TTShang.Core.Attachment.Enums;
using System.Text.Json;

namespace TTShang.Iot.Client.Pages.ProductView
{
    /// <summary>
    /// 产品编辑页
    /// </summary>
    public partial class ProductEdit : EditOperationDialogBase<ProductDto, Guid, IotLocalResource>
    {

        MultiFileUploadParams? UploadParams = null;

        /// <summary>
        /// 结果集
        /// </summary>
        public List<UploadAttachmentOutput> FileList { get; set; } = new List<UploadAttachmentOutput>();


        protected override void OnDataLoaded()
        {
            if (this.Options.Type.Equals(OperationDialogInputType.Add))
            {
                _editModel.Id = Guid.NewGuid();
            }

            UploadParams = new MultiFileUploadParams(_editModel.Id.ToString(), AttachmentBusinessType.IotProduct, 10)
            {
                FileMaxSize = 1024 * 1024 * 1,
                UploadFileTypes = new List<string> { ".jpg", ".jpeg", ".png", ".gif" },
                ShowUploadBtn = OperationDialogInputType.Add.Equals(this.Options.Type) || OperationDialogInputType.Edit.Equals(this.Options.Type),
                ShowRemoveBtn = OperationDialogInputType.Add.Equals(this.Options.Type) || OperationDialogInputType.Edit.Equals(this.Options.Type),
                UploadBtnStyle= UploadBtnStyle.BlockText,
                UploadListType= UploadListType.PictureCard
            };
            if (!string.IsNullOrEmpty(_editModel.ProductImages))
            {
                var images = _editModel.GetProductImageInfos();
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

        protected override void OnInitialized()
        {
            _uniqueVerificationTool.AddField(x => x.ProductName);
            base.OnInitialized();
        }
        protected override Task<bool> OnVerificationBefor()
        {
            if (_editForm != null)
            {
                if (FileList.Any())
                {
                    _editModel.ProductImages = JsonSerializer.Serialize(FileList);
                }
                else
                {
                    _editModel.ProductImages = string.Empty;
                }
            }
            return base.OnVerificationBefor();
        }

        private void OnFileChange(List<UploadFileItem> fileItems)
        {
            Console.WriteLine(JsonSerializer.Serialize(fileItems));
        }
    }
}
