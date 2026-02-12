// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Authorization;
using Gardener.Core.Client.TableSearch;
using Gardener.Core.Dtos;
using Gardener.Iot.Client.Pages.DeviceConnectionView;
using Gardener.Iot.Client.Pages.DeviceDataView;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Gardener.Iot.Client.Pages.DeviceView
{
    public partial class Device : ListOperateTableBase<DeviceDto, Guid, DeviceEdit, IotLocalResource>
    {
        private Tree<DeviceGroupDto>? _groupTree;

        private bool _groupTreeIsLoading = false;

        private int _currentDeviceGroupId = 0;

        private List<DeviceGroupDto>? groups;

        [Inject]
        private IDeviceGroupService deviceGroupService { get; set; } = null!;
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await ReLoadGroups(null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableSearchSettings"></param>
        /// <param name="tableSearchFilterGroupProviders"></param>
        protected override void SetTableSearchParameters(TableSearchSettings tableSearchSettings, List<Func<List<FilterGroup>?>> tableSearchFilterGroupProviders)
        {
            //排除搜索字段
            base.AddExcludeSearchFields(nameof(DeviceDto.DeviceGroupId));
            base.SetTableSearchParameters(tableSearchSettings, tableSearchFilterGroupProviders);
        }
        /// <summary>
        /// 重载分组信息
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadGroups(MouseEventArgs? eventArgs)
        {
            if (AuthenticationStateManager.CheckCurrentUserHaveResource("iot_device_group"))
            {
                _groupTreeIsLoading = true;
                groups = await deviceGroupService.GetTree();
                _groupTreeIsLoading = false;
            }
        }
        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private Task SelectedDeptChanged(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _currentDeviceGroupId = 0;
            }
            else
            {
                int newId = int.Parse(key);
                _currentDeviceGroupId = newId;
            }
            return ReLoadTable(true);
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        protected override void ConfigurationPageRequest(PageRequest pageRequest)
        {
            if (_currentDeviceGroupId > 0 && groups != null)
            {
                var node = TreeHelper.QueryNode(groups, d => d.Id.Equals(_currentDeviceGroupId), d => d.Children);
                if (null != node)
                {
                    List<int> ids = TreeHelper.GetAllChildrenNodes(node, d => d.Id, d => d.Children);
                    if (ids != null)
                    {
                        pageRequest.FilterGroups.Add(new FilterGroup().AddRule(new FilterRule(nameof(DeviceDto.DeviceGroupId), ids, FilterOperate.In)));
                    }
                }
            }
        }

        /// <summary>
        /// 查看连接列表
        /// </summary>
        /// <param name="id"></param>
        protected void OnClickShowConnections(DeviceDto device)
        {
            Navigation.NavigateTo(ReuseTabsPageHelper.CreateTabsUrlBuilder("./iot/device_connection")
                .AddParameter(nameof(DeviceConnectionDto.DeviceId), device.Id)
                .FormatTitle(title => $"{title}[{device.Name}]")
                .Build());
        }
        /// <summary>
        /// 查看日志列表
        /// </summary>
        /// <param name="id"></param>
        protected void OnClickShowLogs(DeviceDto device)
        {
            Navigation.NavigateTo(ReuseTabsPageHelper.CreateTabsUrlBuilder("./iot/device_system_log")
                .AddParameter(nameof(DeviceSystemLogDto.DeviceId), device.Id)
                .FormatTitle(title => $"{title}[{device.Name}]")
                .Build());
        }
        /// <summary>
        /// 显示连接信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected Task OnClickShowConnection(long id)
        {
            OperationDialogInput<long> input = new OperationDialogInput<long>() { Type = OperationDialogInputType.Select, Data = id };
            return OpenOperationDialogAsync<DeviceConnectionEdit, OperationDialogInput<long>, OperationDialogOutput<long>>(
                SharedLocalResource.Detail,
                input,
                output => { return Task.CompletedTask; }, width: "1200");
        }
        /// <summary>
        /// 数据控制
        /// </summary>
        /// <param name="device"></param>
        protected Task OnClickShowDataControl(DeviceDto device)
        {
            return OpenOperationDialogAsync<DeviceDataDetail, DeviceDto, bool>(
                SharedLocalResource.Data, device,
                output => { return Task.CompletedTask; }, width: "1300");
        }

        /// <summary>
        /// 生成连接配置
        /// </summary>
        /// <param name="device"></param>
        protected Task OnClickGenerateConnectConfig(DeviceDto device)
        {
            return OpenOperationDialogAsync<GenerateConnectConfig, DeviceDto, bool>(
                IotLocalResource.GenerateConnectConfig,
                device,
                output => { return Task.CompletedTask; }, width: "800");
        }
        /// <summary>
        /// 绑定租户
        /// </summary>
        protected Task OnClickBindTenant(Guid? deviceId = null)
        {
            return OpenOperationDialogAsync<DeviceBindTenant, Guid?, bool>(
                SharedLocalResource.Binding,
                deviceId,
                output =>
                {
                    if (!output)
                    {
                        return Task.CompletedTask;
                    }
                    return base.ReLoadTable();
                }, width: "500");
        }
    }
}
