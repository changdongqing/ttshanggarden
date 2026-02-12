// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Attachment.Dtos;
using Gardener.Core.Attachment.Enums;
using Gardener.Core.Attachment.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Client.Impl.Attachment.Pages
{
    public partial class AttachmentEdit : EditOperationDialogBase<SystemAttachmentDto, Guid, AttachmentLocalResource>
    {

        [Required(ErrorMessage = "业务类型不能为空")]
        private string _currentEditModelBusinessType
        {
            get
            {
                return _editModel.BusinessType.ToString();
            }
            set
            {
                _editModel.BusinessType = (AttachmentBusinessType)Enum.Parse(typeof(AttachmentBusinessType), value);
            }
        }

        [Required(ErrorMessage = "文件类型不能为空")]
        private string _currentEditModelFileType
        {
            get
            {
                return _editModel.FileType.ToString();
            }
            set
            {
                _editModel.FileType = (AttachmentFileType)Enum.Parse(typeof(AttachmentFileType), value);
            }
        }
    }
}
