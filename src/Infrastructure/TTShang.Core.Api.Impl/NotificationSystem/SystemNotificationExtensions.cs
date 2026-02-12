// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using TTShang.Core.Api.Impl.NotificationSystem.Internal;
using TTShang.Core.Api.Impl.NotificationSystem.Internal.Options;
using TTShang.Core.Api.Impl.NotificationSystem.Internal.Subscribes;
using TTShang.Core.Api.Impl.NotificationSystem.Services;
using TTShang.Core.Module;
using TTShang.Core.NotificationSystem;
using TTShang.Core.NotificationSystem.Services;
using TTShang.Core.Util.JsonConverters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TTShang.Core.Api.Impl.NotificationSystem
{
    /// <summary>
    /// 系统通知服务扩展
    /// </summary>
    public static class SystemNotificationExtensions
    {

        /// <summary>
        /// 添加系统通知服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSystemNotify(this IServiceCollection services)
        {
            services.AddSingleton<IServerModule, NotificationSystemServerModule>();

            //事件订阅
            services.AddSingleton<IEventSubscriber, UserOnlineChangeNotificationDataEventSubscriber>();
            services.AddSingleton<IEventSubscriber, DynamicSubscribeNotificationDataEventSubscriber>();
            services.AddSingleton<IEventSubscriber, RemoteServiceCallNotificationDataEventSubscriber>();

            //添加配置信息
            services.AddConfigurableOptions<SignalROptions>();
            // 添加即时通讯
            services.AddSignalR().AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions = new System.Text.Json.JsonSerializerOptions()
                {
                    WriteIndented = true,
                    Converters =
                    {
                        new DateTimeJsonConverter(),
                        new DateTimeOffsetJsonConverter(),
                        new NotificationDataJsonConverter()
                    }
                };
            });
            //用户编号获取
            services.TryAddSingleton<IUserIdProvider, JwtUserIdProvider>();
            //系统通知服务
            services.TryAddSingleton<ISystemNotificationService, SystemNotificationService>();
            //api
            services.AddRestController<UserConnectQueryService>();
            services.AddScoped<IUserConnectQueryService, UserConnectQueryService>();
            return services;
        }
    }
}
