// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Core.Client.Components;
using Gardener.Core.EventBus;
using Gardener.Iot.Services;
using Gardener.Weighbridge.Dtos.Cmds;
using IdGen;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace Gardener.Weighbridge.Client.Pages.WeighbridgeConfigView
{
    public partial class WeighbridgeControl : OperationDialogBase<Guid?, bool, WeighbridgeLocalResource>
    {
        private Guid? currentWeighbridgeConfigId;
        private WeighbridgeConfigDto? currentWeighbridgeConfig;
        private Dictionary<string, ISubscriber> subscribers = new Dictionary<string, ISubscriber>();
        private List<WeighbridgeConfigDto>? weighbridgeConfigs;
        ClientListBindValue<string, double> channelValues = new ClientListBindValue<string, double>(0);
        ClientListBindValue<string, PrecisionType> channelPrecisions = new ClientListBindValue<string, PrecisionType>(PrecisionType.Single);
        ClientListBindValue<string, UnitType> channelUnits = new ClientListBindValue<string, UnitType>(UnitType.G);
        ClientListBindValue<string, int> channelStates = new ClientListBindValue<string, int>(0);
        ClientListBindValue<string, bool> channelNetweights = new ClientListBindValue<string, bool>(false);

        private double total = 0;
        private int totalPrecision = 5;
        private string totalUnit = UnitType.G.Name;
        private int cardWidth = 228;
        private int cardHeight = 228;
        private int cardFontSize = 24;
        private UnitType unitType = UnitType.MPa;
        private PrecisionType precisionType = PrecisionType.Single;
        private bool netweight = false;
        [Inject]
        private IWeighbridgeConfigService weighbridgeConfigService { get; set; } = null!;
        [Inject]
        private IWeighbridgeControlService weighbridgeControlService { get; set; } = null!;
        [Inject]
        private IDeviceService deviceService { get; set; } = null!;

        [Inject]
        private IEventBus eventBus { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            if (this.Options != null)
            {
                currentWeighbridgeConfigId = this.Options;
            }
            else
            {
                Dictionary<string, StringValues> queryParams = base.GetQueryParams();
                if (queryParams.TryGetValue("weighbridgeConfigId", out StringValues id))
                {
                    currentWeighbridgeConfigId = Guid.Parse(id.ToString());
                }
            }
            base.OnInitialized();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (currentWeighbridgeConfigId != null)
            {
                currentWeighbridgeConfig = await weighbridgeConfigService.Find(currentWeighbridgeConfigId.Value);
            }
            weighbridgeConfigs = await weighbridgeConfigService.GetAllUsable();
            ReSubscribeData();
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 选中新地磅
        /// </summary>
        /// <param name="configDto"></param>
        private async Task OnSelectedItemChanged(WeighbridgeConfigDto configDto)
        {
            currentWeighbridgeConfig = configDto;
            ReSubscribeData();
            await base.RefreshPageDom();
        }

        async Task ChangeUnitType()
        {
            if (currentWeighbridgeConfig == null)
            {
                return;
            }
            channelStates.SetAllValue(3);
            var result = await weighbridgeControlService.SetUnit(new WeighbridgeCmdInput<UnitCmd>(currentWeighbridgeConfig.Id, new UnitCmd(unitType), 1));
        }

        async Task ChangePrecisionType()
        {
            if (currentWeighbridgeConfig == null)
            {
                return;
            }
            channelStates.SetAllValue(3);
            var result = await weighbridgeControlService.SetPrecision(new WeighbridgeCmdInput<PrecisionCmd>(currentWeighbridgeConfig.Id, new PrecisionCmd(precisionType)));
        }

        async Task ChangeNetweight()
        {
            if (currentWeighbridgeConfig == null)
            {
                return;
            }
            channelStates.SetAllValue(3);
            var result = await weighbridgeControlService.Netweight(new WeighbridgeCmdInput<NetweightCmd>(currentWeighbridgeConfig.Id, new NetweightCmd(netweight)));
        }

        async Task ZeroOut()
        {
            if (currentWeighbridgeConfig == null)
            {
                return;
            }
            channelStates.SetAllValue(3);
            var result = await weighbridgeControlService.ZeroOut(new WeighbridgeCmdInput(currentWeighbridgeConfig.Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        async void ReadValue()
        {
            if (currentWeighbridgeConfig != null)
            {
                channelStates.SetAllValue(3);
                var result = await weighbridgeControlService.ReadValue(new WeighbridgeCmdInput<ReadValueCmd>(currentWeighbridgeConfig.Id,new ReadValueCmd()));
                await base.RefreshPageDom();
            }
        }
        /// <summary>
        /// 订阅数据
        /// </summary>
        private void ReSubscribeData()
        {
            if (currentWeighbridgeConfig == null)
            {
                return;
            }
            if (subscribers.Any())
            {
                subscribers.Values.ForEach(x => eventBus.UnSubscribe(x));

                subscribers.Clear();
            }
            foreach (var id in currentWeighbridgeConfig.DeviceIds.Split(","))
            {
                string eventKey = WeighbridgeNotificationData.GetEventKey(Guid.Parse(id));
                //订阅该数据
                var subscriber = eventBus.Subscribe<WeighbridgeNotificationData>(Handle, eventKey);
                subscribers.Add(eventKey, subscriber);
            }

        }

        /// <summary>
        /// 处理实时数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task Handle(WeighbridgeNotificationData data)
        {
            System.Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            if (data.Data != null && currentWeighbridgeConfigId != null)
            {
                System.Console.WriteLine($"{JsonSerializer.Serialize(data.Data)}");

                WeighbridgeUploadData uploadData = data.Data;
                string key = currentWeighbridgeConfigId.ToString() + data.DeviceId;
                if (channelStates[key] == 1)
                {
                    channelStates[key] = 4;
                }
                else if (channelStates[key] == 4)
                {
                    channelStates[key] = 1;
                }
                else
                {
                    channelStates[key] = 1;
                }

                if (uploadData.UploadDataType.Equals(UploadDataType.ReadValue))
                {
                    if (uploadData.UnitType.HasValue)
                    {
                        channelUnits[key] = uploadData.UnitType.Value;
                    }
                    //目前都是整数位，出厂已设定
                    //if (uploadData.PrecisionType.HasValue)
                    //{
                    //    channelPrecisions[key] = uploadData.PrecisionType.Value;
                    //    channelValues[key] = uploadData.PrecisionType.Value.Calculate(uploadData.Weight);
                    //}
                    //else
                    //{
                    channelValues[key] = uploadData.Weight;
                    //}
                    channelNetweights[key] = uploadData.NetWeight;
                    TotalCount();
                    return base.RefreshPageDom();
                }
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// 
        /// </summary>
        private void TotalCount()
        {
            if (currentWeighbridgeConfig != null)
            {
                var ids = currentWeighbridgeConfig.DeviceIds.Split(",");
                double count = 0;
                UnitType unit = channelUnits.GetValue(currentWeighbridgeConfigId.ToString() + ids[0]);
                foreach (var id in ids)
                {
                    string key = currentWeighbridgeConfigId.ToString() + id;
                    UnitType itemUnit = channelUnits.GetValue(key);
                    if (itemUnit.Equals(unit))
                    {
                        double value = channelValues.GetValue(key);
                        count += value;
                    }
                }
                total = count;
                totalUnit = unit.Name;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (subscribers.Any())
            {
                subscribers.Values.ForEach(x => eventBus.UnSubscribe(x));
                subscribers.Clear();
            }
            base.Dispose(disposing);
        }

    }
}
