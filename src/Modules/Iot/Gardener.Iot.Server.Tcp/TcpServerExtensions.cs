// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Iot.Enums;
using Gardener.Iot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Iot.Server.Tcp
{
    /// <summary>
    /// Tcp服务器扩展
    /// </summary>
    public static class TcpServerExtensions
    {
        /// <summary>
        /// 启用Tcp服务器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddTcpServer(this IServiceCollection services)
        {
            //tcp后台服务配置
            services.AddOptions<TcpServerOptions>().Configure<IConfiguration>((opt, conf) =>
            {
                conf.GetSection("TcpServer").Bind(opt);
            });

            //tcp 为 key的服务
            services.AddKeyedSingleton<IDeviceCommunicationControlService, TcpDeviceCommunicationService>(DeviceConnectionType.Tcp);

            //通讯服务列表
            services.AddKeyedSingleton(nameof(DeviceConnectionType), (sp, key) =>
            {
                return (IDeviceCommunicationService)sp.GetRequiredKeyedService<IDeviceCommunicationControlService>(DeviceConnectionType.Tcp);
            });

            return services;
        }
    }
}
