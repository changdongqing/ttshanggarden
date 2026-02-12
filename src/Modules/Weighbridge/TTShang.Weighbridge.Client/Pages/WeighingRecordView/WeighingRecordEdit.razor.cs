// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using TTShang.Core.Client.Authorization;
using TTShang.Core.Resources;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace TTShang.Weighbridge.Client.Pages.WeighingRecordView
{
    /// <summary>
    /// WeighingRecord编辑页
    /// </summary>
    public partial class WeighingRecordEdit : EditOperationDialogBase<WeighingRecordDto, Guid, WeighbridgeLocalResource>
    {
        private List<WeighbridgeConfigDto> weighbridgeConfigs = [];
        private List<CommodityDto> commodityConfigs = [];

        [Inject]
        private IWeighbridgeConfigService weighbridgeConfigService { get; set; } = default!;
        [Inject]
        private ICommodityService commodityService { get; set; } = default!;
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = default!;

        IDictionary<Guid, WeighingRecordLogDto> recordLogEditCache = new Dictionary<Guid, WeighingRecordLogDto>();

        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        private IEnumerable<string> commodityCodes
        {
            get
            {
                if (string.IsNullOrEmpty(_editModel.CommodityCode))
                {
                    return [];
                }
                return _editModel.CommodityCode.Split(",").Select(x => x.Trim()).ToArray();
            }
            set
            {
                if (value == null)
                {
                    _editModel.CommodityCode = string.Empty;
                    _editModel.CommodityName = string.Empty;

                }
                else
                {
                    _editModel.CommodityCode = string.Join(",", value);
                    List<string> names = new List<string>();
                    foreach (var code in value)
                    {
                        var cm = commodityConfigs.FirstOrDefault(x => x.CommodityCode.Equals(code));
                        if (cm != null)
                        {
                            names.Add(cm.CommodityName);
                        }
                    }
                    _editModel.CommodityName = string.Join(",", names);
                }
            }
        }
        /// <summary>
        /// 页面初始化后
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnDataLoadingAsync()
        {
            weighbridgeConfigs = await weighbridgeConfigService.GetAllUsable();
            commodityConfigs = await commodityService.GetAllUsable();
            await base.OnDataLoadingAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnDataLoaded()
        {
            foreach (var item in _editModel.WeighingRecordLogs)
            {
                recordLogEditCache[item.Id] = item;
            }

            base.OnDataLoaded();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commodity"></param>
        /// <returns></returns>
        private string FormatCommodityLable(CommodityDto commodity)
        {
            return $"{commodity.CommodityName}({commodity.CommodityCode})";
        }
        /// <summary>
        /// 选择商品
        /// </summary>
        /// <param name="commodity"></param>
        private void OnSelectedItemChangedCommodity(WeighingRecordLogDto log, CommodityDto commodity)
        {
            log.CommodityName = commodity.CommodityName;
        }
        protected override Task<bool> OnVerificationBefor()
        {
            _editModel.OperatorName = authenticationStateManager.GetCurrentUser()?.NickName ?? string.Empty;
            _editModel.CommodityCode = string.Join(",", _editModel.WeighingRecordLogs.Where(x=>!string.IsNullOrEmpty(x.CommodityCode)).Select(x => x.CommodityCode).Distinct());
            _editModel.CommodityName = string.Join(",", _editModel.WeighingRecordLogs.Where(x => !string.IsNullOrEmpty(x.CommodityName)).Select(x => x.CommodityName).Distinct());

            foreach (var item in _editModel.WeighingRecordLogs)
            {
                //服务端也进行赋值，此处只是为了过校验
                item.WeighbridgeConfigId = _editModel.WeighbridgeConfigId;
                item.WeighingRecordId = Guid.NewGuid();
                item.PlateNumber = _editModel.PlateNumber;
            }
            return base.OnVerificationBefor();
        }
    }
}
