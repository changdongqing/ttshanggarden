// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Resources;
using Gardener.Iot.Dtos;
using Gardener.Iot.Services;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Weighbridge.Client.Pages.WeighbridgeConfigView
{
    /// <summary>
    /// 地磅配置编辑页
    /// </summary>
    public partial class WeighbridgeConfigEdit : EditOperationDialogBase<WeighbridgeConfigDto, Guid, WeighbridgeLocalResource>
    {
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        private IEnumerable<Guid> deviceIds
        {
            get
            {
                if (string.IsNullOrEmpty(_editModel.DeviceIds))
                {
                    return [];
                }
                return _editModel.DeviceIds.Split(",").Select(x=>Guid.Parse(x)).ToArray();
            }
            set
            {
                if(value==null)
                {
                    _editModel.DeviceIds = string.Empty;

                }else
                {
                    _editModel.DeviceIds = string.Join(",", value);
                }
            }
        }
        private List<DeviceDto>? devices;
        [Inject]
        private IDeviceService deviceService { get; set; } = null!;
        /// <summary>
        /// 页面初始化后
        /// </summary>
        protected override void OnInitialized()
        {

            _uniqueVerificationTool.AddField(x => x.Name);
            base.OnInitialized();
        }

        protected override async Task OnDataLoadingAsync()
        {
            devices = await deviceService.GetAllUsable();
            await base.OnDataLoadingAsync();
        }

        private string FtDeviceName(DeviceDto device)
        {
            return device.Name+(string.IsNullOrEmpty(device.Alias)?"":$"({device.Alias})");
        }
    }
}