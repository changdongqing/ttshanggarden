// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Iot.Client.Pages.DeviceConnectionView
{
    public partial class DeviceConnection : ListOperateTableBase<DeviceConnectionDto, long, DeviceConnectionEdit, IotLocalResource>
    {
        protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            dialogSettings.Width = "1200";
            base.SetOperationDialogSettings(dialogSettings);
        }


        /// <summary>
        /// 查看日志列表
        /// </summary>
        /// <param name="id"></param>
        protected void OnClickShowLogs(DeviceConnectionDto deviceConnection)
        {
            Navigation.NavigateTo(ReuseTabsPageHelper.CreateTabsUrlBuilder("./iot/device_system_log")
                .AddParameter(nameof(DeviceSystemLogDto.DeviceConnectionId), deviceConnection.Id)
                .FormatTitle(title => $"{title}[{deviceConnection.Id}]")
                .Build());
        }
    }
}
