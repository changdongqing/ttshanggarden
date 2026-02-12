// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;
using Gardener.Core.Client.Authorization.Events;
using Gardener.Core.Client.Module;
using Gardener.Core.Module;
using Gardener.Core.SystemConfig.Dtos;
using Gardener.Core.SystemConfig.Services;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;


namespace Gardener.Core.Client.Shared
{
    public partial class MainLayout
    {

        private MenuDataItem[] _menuData { get; set; } = { };

        /// <summary>
        /// 菜单栏收缩
        /// </summary>
        private bool collapsed;
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigValueService systemConfigService { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private ILocalizationLocalizer<SharedLocalResource> Loc { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private IEventBus eventBus { get; set; } = null!;

        [Inject]
        private ClientModuleManager clientModuleManager { get; set; } = null!;

        [Inject]
        private IClientLogger clientLogger { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        private SystemConfigDto? systemConfig = null;
        /// <summary>
        /// 
        /// </summary>
        private List<MenuDataItem> menuDataItems = new List<MenuDataItem>();

        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <param name="resourceDto"></param>
        /// <param name="parent"></param>
        private void InitMenu(ResourceDto resourceDto, MenuDataItem? parent = null)
        {
            string key = "menu:" + resourceDto.Key;
            LocalizedString localNameStr = Loc.Get(key);
            //模块设置本地化资源的，优先使用模块资源
            if (!string.IsNullOrEmpty(resourceDto.ModuleName))
            {
                IModule? module = clientModuleManager.GetModule(resourceDto.ModuleName);
                if (module != null && module is IClientModule clientModule)
                {
                    var localizerType = clientModule.GetLocalizationLocalizerType();
                    if (localizerType != null)
                    {
                        var localNameTemp = Lo.Get(localizerType, key);
                        if (!localNameTemp.ResourceNotFound)
                        {
                            localNameStr = localNameTemp;
                        }
                    }
                }
            }
            string localName = string.Empty;
            if (localNameStr.ResourceNotFound)
            {
                //未配置菜单本地化使用名称
                localName = resourceDto.Name;
                clientLogger.Warn($"client menu {resourceDto.Name}  find {key} localName is not find.", sendNotify: false);
            }
            else
            {
                localName = localNameStr;
            }
            string? path = resourceDto.Path;
            //path为空，还没有子级的会报错，设置个key
            if (string.IsNullOrEmpty(path) && (resourceDto.Children == null || !resourceDto.Children.Any()))
            {
                path = resourceDto.Key;
            }
            var current = new MenuDataItem
            {
                Path = path,
                Name = localName,
                Key = resourceDto.Key,
                Icon = resourceDto.Icon ?? "",
                HideInMenu = resourceDto.Hide,
                Match = NavLinkMatch.Prefix
            };
            ClientMenuCache.Add(current);
            if (parent == null)
            {
                menuDataItems.Add(current);
            }
            else if (!current.HideInMenu)
            {
                List<string> pks = new List<string>();
                if (parent.ParentKeys != null && parent.ParentKeys.Length > 0)
                {
                    pks.AddRange(parent.ParentKeys);
                }
                pks.Add(parent.Key);
                current.ParentKeys = pks.ToArray();

                var tempList = parent.Children ?? new MenuDataItem[] { };
                var temp = tempList.ToList();
                temp.Add(current);
                parent.Children = temp.ToArray();
            }
            if (resourceDto.Children != null)
            {
                foreach (var c in resourceDto.Children)
                {
                    InitMenu(c, current);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if (ClientConstant.EnableFooterView)
            {
                systemConfig = await systemConfigService.GetSystemConfig();
            }
            var currentMenus = authenticationStateManager.GetCurrentUserMenus();
            if (currentMenus != null)
            {
                //已有数据
                menuDataItems = new List<MenuDataItem>();
                currentMenus.ForEach(x => InitMenu(x));
                _menuData = menuDataItems.ToArray();
            }
            else
            {
                //设置个回调
                eventBus.Subscribe<ReloadCurrentUserEvent>(e =>
                {
                    if (_menuData.Length > 0)
                    {
                        return Task.CompletedTask;
                    }
                    var menus = e.LoginUserInfo.MenuResources;
                    if (menus != null)
                    {
                        menuDataItems = new List<MenuDataItem>();
                        menus.ForEach(x => InitMenu(x));
                        _menuData = menuDataItems.ToArray();
                    }

                    return Task.CompletedTask;
                });
            }
            await eventBus.PublishAsync(new MainInitializedEvent());
            await base.OnInitializedAsync();
        }

        void toggle()
        {
            collapsed = !collapsed;
        }
        void OnCollapse(bool isCollapsed)
        {
            Console.WriteLine($"Collapsed: {isCollapsed}");
        }

    }
}
