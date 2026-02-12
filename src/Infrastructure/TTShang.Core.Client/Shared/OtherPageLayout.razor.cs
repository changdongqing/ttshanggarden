// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.SystemConfig.Dtos;
using TTShang.Core.SystemConfig.Services;

namespace TTShang.Core.Client.Shared
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
