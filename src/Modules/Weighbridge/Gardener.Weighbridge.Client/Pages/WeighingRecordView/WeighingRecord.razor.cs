// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Weighbridge.Client.Pages.WeighingRecordView
{
    /// <summary>
    /// WeighingRecord列表页
    /// </summary>
    public partial class WeighingRecord : ListOperateTableBase<WeighingRecordDto, Guid, WeighingRecordEdit, WeighbridgeLocalResource>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialogSettings"></param>
        protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            dialogSettings.Width = "1200";
        }

        private Task OpenWeighingRecordLogList(WeighingRecordDto weighingRecord)
        {
            return OpenOperationDialogAsync<WeighingRecordLog, WeighingRecordDto, bool>(Localizer[nameof(WeighbridgeLocalResource.WeighingRecordLog)], weighingRecord, width: "1400");
        }
    }
}
