// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Components;

namespace TTShang.Weighbridge.Client.Pages.WeighbridgeConfigView
{
    /// <summary>
    /// 地磅配置列表页
    /// </summary>
    public partial class WeighbridgeConfig : ListOperateTableBase<WeighbridgeConfigDto, Guid, WeighbridgeConfigEdit, WeighbridgeLocalResource>
    {
        /// <summary>
        /// 去往控制页面
        /// </summary>
        /// <param name="weighbridgeConfig"></param>
        public void OnClickControl(WeighbridgeConfigDto weighbridgeConfig)
        {
            string url = new ReuseTabsUrlBuilder(typeof(WeighbridgeControl))
                .AddParameter("weighbridgeConfigId", weighbridgeConfig.Id).FormatTitle(title =>
                {
                    return $"{title}[{weighbridgeConfig.Name}]";
                }).Build();
            Navigation.NavigateTo(url);
        }
    }
}