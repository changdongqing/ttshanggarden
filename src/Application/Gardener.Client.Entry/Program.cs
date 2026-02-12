using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gardener.Client.Entry
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //配置
            builder.Services.TryAddSingleton<IConfiguration>(builder.Configuration);
            //根组件
            builder.Services.TryAddSingleton(builder.RootComponents);

            #region log
            builder.Logging.AddConfiguration(
                builder.Configuration.GetSection("Logging")
            );
            #endregion


            //添加基础服务
            ServiceCombine.AddServices(builder.Services, builder.Configuration, builder.HostEnvironment.BaseAddress);

            var host = builder.Build();
            //使用某些服务
            await ServiceCombine.UseInject(host.Services);
            await host.RunAsync();
        }
    }
}
