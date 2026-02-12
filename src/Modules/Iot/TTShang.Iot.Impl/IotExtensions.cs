// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Module;
using TTShang.Iot.Impl.Core;
using TTShang.Iot.Impl.Core.Options;
using TTShang.Iot.Impl.Services;
using TTShang.Iot.Impl.Subscribes;
using TTShang.Iot.Impl.Tools;
using TTShang.Iot.Server.Mqtt;
using TTShang.Iot.Server.Tcp;
using TTShang.Iot.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Iot.Impl
{
    /// <summary>
    /// 物联网
    /// </summary>
    public static class IotExtensions
    {
        /// <summary>
        /// 启用mqtt服务器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIot(this IServiceCollection services)
        {
            //module
            services.AddSingleton<IServerModule, IotServerModule>();

            //tool
            services.AddScoped<IDeviceConnectionTool, DeviceConnectionTool>();

            //service
            services.AddScoped<IDeviceConnectionService, DeviceConnectionService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceSystemLogService, DeviceSystemLogService>();
            services.AddScoped<IProductService, ProductService>();

            //api
            services.AddRestController<DeviceConnectionService>();
            services.AddRestController<DeviceGroupService>();
            services.AddRestController<DeviceService>();
            services.AddRestController<DeviceSystemLogService>();
            services.AddRestController<ProductService>();
            services.AddRestController<DeviceDataService>();

            //Subscriber
            services.AddSingleton<IEventSubscriber, DeviceDataHandleEventStoreSubscriber>();
            services.AddSingleton<IEventSubscriber, DeviceDataSaveAfterNotificationSubscriber>();

            //iot配置
            services.AddConfigurableOptions<IotOptions>();
            //通讯服务
            services.AddHostedService<CommunicationBackgroundService>();
            //设备数据处理器
            services.AddSingleton<IDeviceDataHandler, DeviceDataToEventBusMessageHandler>();
            //数据存储服务
            services.AddSingleton<IDeviceDataStoreService, DeviceDataStoreToDbService>();
            //通讯对接服务
            services.AddSingleton<IDeviceCommunicationCableSplicer, DefaultDeviceCommunicationCableSplicer>();
            //mqtt
            services.AddMqttServer();
            //tcp
            services.AddTcpServer();
            return services;
        }
    }
}
