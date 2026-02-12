// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Iot.Client.Pages.ProductView
{
    /// <summary>
    /// 产品列表页
    /// </summary>
    public partial class Product : ListOperateTableBase<ProductDto, Guid, ProductEdit, IotLocalResource>
    {
        /// <summary>
        /// 结果集
        /// </summary>
        public List<UploadFileItem> FileList { get; set; } = new List<UploadFileItem>();
        protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            dialogSettings.Width = "800";
            base.SetOperationDialogSettings(dialogSettings);
        }

    }
}
