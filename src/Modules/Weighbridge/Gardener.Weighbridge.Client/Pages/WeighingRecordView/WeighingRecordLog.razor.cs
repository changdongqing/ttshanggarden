// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Weighbridge.Client.Pages.WeighingRecordView
{
    public partial class WeighingRecordLog : OperationDialogBase<WeighingRecordDto, bool, WeighbridgeLocalResource>
    {
        private List<WeighingRecordLogDto> weighingRecordLogs = new List<WeighingRecordLogDto>();


        protected override void OnInitialized()
        {
            weighingRecordLogs = this.Options.WeighingRecordLogs;
            base.OnInitialized();
        }
    }
}
