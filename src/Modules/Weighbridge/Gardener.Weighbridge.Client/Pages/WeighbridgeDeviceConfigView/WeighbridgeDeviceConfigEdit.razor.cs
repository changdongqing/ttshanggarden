// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Iot.Dtos;
using Gardener.Iot.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.Weighbridge.Client.Pages.WeighbridgeDeviceConfigView
{
    /// <summary>
    /// 地磅设备配置编辑页
    /// </summary>
    public partial class WeighbridgeDeviceConfigEdit : EditOperationDialogBase<WeighbridgeDeviceConfigDto, Int32, WeighbridgeLocalResource>
    {
        private List<DeviceDto> _devices = new List<DeviceDto>();
        [Inject]
        private IDeviceService deviceService { get; set; } = null!;
        /// <summary>
        /// 页面初始化后
        /// </summary>
        protected override void OnInitialized()
        {
            _uniqueVerificationTool.AddField(x => x.DeviceId);
            base.OnInitialized();
        }

        protected override async Task OnDataLoadingAsync()
        {
            _devices = await deviceService.GetAllUsable(includLocked: true);
            await base.OnDataLoadingAsync();
        }
    }
}
