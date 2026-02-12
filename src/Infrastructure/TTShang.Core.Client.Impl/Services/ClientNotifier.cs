// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Impl.Services
{
    /// <summary>
    /// Client 通知提示
    /// </summary>
    [ScopedService]
    public class ClientNotifier : IClientNotifier
    {
        private readonly NotificationService notificationService;
        private readonly ILocalizationLocalizer localizer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationService"></param>
        /// <param name="localizer"></param>
        public ClientNotifier(NotificationService notificationService, ILocalizationLocalizer localizer)
        {
            this.notificationService = notificationService;
            this.localizer = localizer;
        }
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Task Notify(string title, string description, NotificationType type,double? duration=null)
        {
            return notificationService.Open(new NotificationConfig()
            {
                Message = title,
                Description = description,
                NotificationType = type,
                Duration = duration ?? ClientConstant.ClientNotifierMessageDuration
            });
        }

        public void Error(string description, string? title = null, Exception? ex = null, double? duration = null)
        {
            ErrorAsync(description, title, ex, duration);
        }

        public Task ErrorAsync(string description, string? title = null, Exception? ex = null, double? duration = null)
        {
            return Notify(title ?? localizer[nameof(SharedLocalResource.Error)], description, NotificationType.Error, duration);
        }

        public void Info(string description, string? title = null, double? duration = null)
        {
            InfoAsync(description, title, duration);
        }

        public Task InfoAsync(string description, string? title = null, double? duration = null)
        {
            return Notify(title ?? localizer[nameof(SharedLocalResource.Info)], description, NotificationType.Info, duration);
        }

        public void Success(string description, string? title = null, double? duration = null)
        {
            SuccessAsync(description, title, duration);
        }

        public Task SuccessAsync(string description, string? title = null, double? duration = null)
        {
            return Notify(title ?? localizer[nameof(SharedLocalResource.Success)], description, NotificationType.Success, duration);
        }

        public void Warn(string description, string? title = null, Exception? ex = null, double? duration = null)
        {
            WarnAsync(description, title, ex, duration);
        }

        public Task WarnAsync(string description, string? title = null, Exception? ex = null, double? duration = null)
        {
            return Notify(title ?? localizer[nameof(SharedLocalResource.Warn)], description, NotificationType.Warning, duration);
        }
    }
}
