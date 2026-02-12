// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Iot.Enums;
using TTShang.Iot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Iot.Server.Mqtt
{
    /// <summary>
    /// mqtt服务器扩展
    /// </summary>
    public static class MqttServerExtensions
    {
        /// <summary>
        /// 启用mqtt服务器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMqttServer(this IServiceCollection services)
        {
            //mqtt后台服务配置
            services.AddOptions<MqttServerOptions>().Configure<IConfiguration>((opt, conf) => {
                    conf.GetSection("MqttServer").Bind(opt);
            }) ;
       
            //mqtt 为 key的服务
            services.AddKeyedSingleton<IDeviceCommunicationControlService, MqttDeviceCommunicationService>(DeviceConnectionType.Mqtt);

            //通讯服务列表
            services.AddKeyedSingleton(nameof(DeviceConnectionType), (sp,key) => {
                return (IDeviceCommunicationService)sp.GetRequiredKeyedService<IDeviceCommunicationControlService>(DeviceConnectionType.Mqtt);
            });

            return services;
        }
    }
}
