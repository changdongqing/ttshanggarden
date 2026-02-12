// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Gardener.Core.Client.Module;
using Gardener.Core.Module;
using Gardener.Core.Impl.DependencyInjection;

namespace Gardener.Core.Client.Impl.Extensions
{
    /// <summary>
    /// 模块扩展
    /// </summary>
    /// <remarks>
    /// 解决Client无法扫描到所有引用包的问题
    /// </remarks>
    public static class ModuleExtension
    {
        public static Assembly[] AllModeuleAssemblies = [];
        /// <summary>
        /// 加载各个dll，并扫描需要注册的服务
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterModulesAndScanServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            //加载各个dll
            services.RegisterModules(assemblies);
            //扫描需要注册的服务
            services.AddServicesFromAttribute(assemblies);
        }

        /// <summary>
        /// 加载各个dll
        /// </summary>
        /// <param name="services"></param>
        public static Assembly[] RegisterModules(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped(typeof(ClientModuleManager), sp =>
            {
                IEnumerable<IClientModule> modules = sp.GetServices<IClientModule>();
                return new ClientModuleManager(assemblies, modules);
            });
            services.AddScoped<IModuleManager>(sp =>
            {
                return sp.GetRequiredService<ClientModuleManager>();
            });
            AllModeuleAssemblies = assemblies.ToArray();
            return AllModeuleAssemblies;
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static Task LoadModules(this IServiceProvider serviceProvider)
        {
            var clientModuleManager = serviceProvider.GetRequiredService<IModuleManager>();
            if (clientModuleManager != null)
            {
                RootComponentMappingCollection rootComponents = serviceProvider.GetRequiredService<RootComponentMappingCollection>();
                ICollection<Task> tasks = new List<Task>();
                foreach (IModule module in clientModuleManager.GetModules())
                {
                    if (module is IClientModule clientModule)
                    {
                        foreach (var component in clientModule.GetAutoRegisterComponents())
                        {
                            rootComponents.Add(component.Component, component.Selector);
                        }
                        tasks.Add(clientModule.Load(serviceProvider));
                    }
                }
                return Task.WhenAll(tasks.ToArray());
            }
            return Task.CompletedTask;
        }
    }
}
