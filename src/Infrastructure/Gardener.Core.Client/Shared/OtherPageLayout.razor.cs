// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.SystemConfig.Dtos;
using Gardener.Core.SystemConfig.Services;

namespace Gardener.Core.Client.Shared
{
    public partial class OtherPageLayout
    {
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigValueService systemConfigService { get; set; } = null!;
        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfigDto systemConfig = null!;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            systemConfig =await systemConfigService.GetSystemConfig();
            await base.OnInitializedAsync();
        }
    }
}
