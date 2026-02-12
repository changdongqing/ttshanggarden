// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.SystemConfig.Dtos;
using TTShang.Core.SystemConfig.Services;

namespace TTShang.Core.Client.Shared
{
    public partial class LoginLayout
    {
        private string[] _locales = null!;

        private SystemConfigDto? systemConfig = null;
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigValueService systemConfigService { get; set; } = null!;
        [Inject]
        private IClientCultureService clientCultureService { get; set; } = null!;
        [Inject]
        public NavigationManager Navigation { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _locales = clientCultureService.GetSupportedCultures();
            systemConfig = await systemConfigService.GetSystemConfig();
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// HandleSelectLang
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task HandleSelectLang(MenuItem item)
        {
            string name = item.Key;
            if (await clientCultureService.SetCulture(name))
            {
                Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
            }
        }
    }
}
