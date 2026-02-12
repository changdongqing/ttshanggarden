// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using TTShang.Core.Module;
using TTShang.Weighbridge.Impl.Core;
using TTShang.Weighbridge.Impl.Services;
using TTShang.Weighbridge.Impl.Subscribes;
using TTShang.Weighbridge.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Weighbridge.Impl
{
    /// <summary>
    /// 智能地磅
    /// </summary>
    public static class WeighbridgeExtensions
    {
        /// <summary>
        /// 启用mqtt服务器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWeighbridge(this IServiceCollection services)
        {
            //Subscriber
            services.AddSingleton<IEventSubscriber, DeviceDataSaveAfterNotificationSubscriber>();
            services.AddSingleton<IEventSubscriber, TenantEventSubscriber>();

            //module
            services.AddSingleton<IServerModule, WeighbridgeServerModule>();
            services.AddSingleton<IServerModule, WeighbridgeAppModule>();
            //service
            services.AddScoped<IWeighbridgeConfigService, WeighbridgeConfigService>();
            services.AddScoped<IWeighbridgeControlService, WeighbridgeControlService>();
            services.AddScoped<IVehicleTypeService, VehicleTypeService>();
            services.AddKeyedSingleton<IWeighbridgeAdapter, T100WeighbridgeAdapter>("weighbridge-t100");
            services.AddKeyedSingleton<IWeighbridgeAdapter, T200WeighbridgeAdapter>("weighbridge-t200");
            services.AddScoped<ICommodityService, CommodityService>();
            services.AddScoped<WeighbridgeDeviceService>();
            services.AddScoped<IWeighbridgeDeviceConfigService, WeighbridgeDeviceConfigService>();
            //api
            services.AddRestController<WeighbridgeConfigService>();
            services.AddRestController<WeighbridgeControlService>();
            services.AddRestController<WeighingRecordService>();
            services.AddRestController<VehicleTypeService>();
            services.AddRestController<CommodityService>();
            services.AddRestController<UserWeighbridgeConfigService>();
            services.AddRestController<VehicleWeighingConfigService>();
            services.AddRestController<WeighbridgeDeviceConfigService>();
            services.AddRestController<WeighbridgeDeviceService>();
            return services;
        }
    }
}
