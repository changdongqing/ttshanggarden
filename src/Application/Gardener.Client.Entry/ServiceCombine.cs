// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;
using Gardener.Core.AppManager.Dtos;
using Gardener.Core.AppManager.Enums;
using Gardener.Core.Client.Authorization;
using Gardener.Core.Client.Constants;
using Gardener.Core.Client.Extensions;
using Gardener.Core.Client.Impl.Authorization;
using Gardener.Core.Client.Impl.Extensions;
using Gardener.Core.Client.Impl.Services;
using Gardener.Core.Client.Impl.SignalR;
using Gardener.Core.Client.Settings;
using Gardener.Core.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Gardener.Client.Entry
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCombine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="hostBaseAddress"></param>
        public static void AddServices(IServiceCollection services, IConfiguration configuration, string? hostBaseAddress = null)
        {

            #region module
            //添加所有需要扫描的模块
            services.RegisterModulesAndScanServices(typeof(ServiceCombine).Assembly,
                typeof(Core.Client._Imports).Assembly,
                typeof(Core.Client.Impl._Imports).Assembly,
                typeof(ToolBox.Client._Imports).Assembly,
                typeof(EasyJob.Client._Imports).Assembly,
                typeof(WoChat.Client._Imports).Assembly,
                typeof(Iot.Client._Imports).Assembly,
                typeof(Core.CodeGeneration.Client._Imports).Assembly,
                typeof(Weighbridge.Client._Imports).Assembly
                );
            #endregion

            services.AddApiSetting(configuration, hostBaseAddress);

            #region httpclient
            services.TryAddScoped(sp =>
            {
                IOptions<ApiSettings> settings = sp.GetRequiredService<IOptions<ApiSettings>>();
                return new HttpClient(new HttpClientAddHeadersDelegatingHandler(sp))
                {
                    BaseAddress = new Uri(settings.Value.BaseAddres)
                };
            });
            #endregion

            #region ant design
            services.AddAntDesign();
            services.Configure<ProSettings>(configuration.GetSection("ProSettings"));
            #endregion

            #region 本地化
            services.AddAppLocalization<SharedLocalResource>();
            #endregion

            #region  Mapster 配置
            services.AddTypeAdapterConfigs();
            #endregion

            #region  SignalR
            services.AddSignalRClientManager();
            #endregion


            #region 认证、授权
            services.TryAddScoped<ILoginDataAccessor, WasmLoginDataAccessor>();
            services.AddScoped<IAuthenticationStateManager, AuthenticationStateManager>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddCascadingAuthenticationState();
            services.AddAuthorizationCore(option =>
            {
                option.AddPolicy(AuthConstant.DefaultAuthenticatedPolicy, a => a.RequireAuthenticatedUser());
                option.AddPolicy(AuthConstant.ClientUIResourcePolicy, a => a.Requirements.Add(new ClientUIAuthorizationRequirement()));
                var defaultPolicy = option.GetPolicy(AuthConstant.DefaultAuthenticatedPolicy);
                if (defaultPolicy != null)
                {
                    option.DefaultPolicy = defaultPolicy;
                }
            });
            services.AddScoped<IAuthorizationHandler, ClientUIResourceAuthorizationHandler>();
            services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
            #endregion

            services.AddScoped<AppClientInfo>(x =>
            {
                string appName = configuration.GetRequiredSection("AppClientInfo:AppName").Value ?? "h5";
                string currentVersionNumber = configuration.GetRequiredSection("AppClientInfo:CurrentVersionNumber").Value ?? "0";
                string currentVersioName = configuration.GetRequiredSection("AppClientInfo:CurrentVersioName").Value ?? "v0.0.0";
                string installLocal = configuration.GetRequiredSection("AppClientInfo:InstallLocal").Value ?? "false";
                string environment = configuration.GetRequiredSection("AppClientInfo:Environment").Value ?? AppEnvironments.Develop.ToString();
                AppClientInfo appClientInfo = new AppClientInfo(appName, appName, long.Parse(currentVersionNumber), currentVersioName)
                {
                    InstallLocal = bool.Parse(installLocal),
                    Environment = Enum.Parse<AppEnvironments>(environment)
                };
                return appClientInfo;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static async Task UseInject(IServiceProvider serviceProvider)
        {
            await serviceProvider.LoadModules();
            await serviceProvider.UseAppLocalization(ClientConstant.BlazorCultureKey, ClientConstant.DefaultCulture);
        }
    }
}