// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.Impl.NotificationSystem.Constants;
using Gardener.Core.Client.SignalR;
using Gardener.Core.NotificationSystem;
using System.Text.Json;

namespace Gardener.Core.Client.Impl.NotificationSystem
{
    [ScopedService]
    public class SystemNotificationSignalRClientProvider : ISignalRClientProvider
    {
        private readonly IEventBus _eventBus;
        private readonly ISignalRClientBuilder signalRClientBuilder;
        private readonly IClientLogger clientLogger;
        private readonly ILocalizationLocalizer localizer;

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="signalRClientBuilder"></param>
        /// <param name="clientLogger"></param>
        /// <param name="localizer"></param>
        public SystemNotificationSignalRClientProvider(IEventBus eventBus, ISignalRClientBuilder signalRClientBuilder, IClientLogger clientLogger, ILocalizationLocalizer localizer)
        {
            _eventBus = eventBus;
            this.signalRClientBuilder = signalRClientBuilder;
            jsonSerializerOptions.Converters.Add(new NotificationDataJsonConverter());
            jsonSerializerOptions.IncludeFields = true;
            this.clientLogger = clientLogger;
            this.localizer = localizer;
        }

        public ISignalRClient GetSignalRClient()
        {
            ISignalRClient signalRClient = signalRClientBuilder
                .GetInstance()
                .SetClientName(NotificationSystemSignalRClientNames.SystemNotificationSignalRClientName)
                .SetUrl("ws/system-notification")
                .SetJsonSerializerOptions(jsonSerializerOptions)
                .Build();
            //重试
            signalRClient.AutomaticReconnectCustomRetryPolicy(0, 5);

            signalRClient.On<object>("ReceiveMessage", CallBack);

            return signalRClient;
        }
        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        private Task CallBack(object? json)
        {
            try
            {
                if (json == null || json is not JsonElement jsonElement)
                {
                    return Task.CompletedTask;
                }

                NotificationData? notificationData = jsonElement.Deserialize<NotificationData>(jsonSerializerOptions);
                //注册接收调用方法
                if (notificationData == null)
                {
                    return Task.CompletedTask;
                }
                _eventBus.Publish(notificationData);
            }
            catch (Exception ex)
            {
                clientLogger.Error(localizer.Combination(NotificationSystemSignalRClientNames.SystemNotificationSignalRClientName, "CallBack", "Error"), ex: ex, sendNotify: false);
            }
            return Task.CompletedTask;
        }

    }
}
