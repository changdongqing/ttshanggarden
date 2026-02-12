// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Services
{
    /// <summary>
    /// 客户端消息服务
    /// </summary>
    public interface IClientMessageService
    {
        void Error(string content, double? duration = null, Action? onClose = null);

        void Info(string content, double? duration = null, Action? onClose = null);

        void Loading(string content, double? duration = null, Action? onClose = null);

        void Success(string content, double? duration = null, Action? onClose = null);

        void Warning(string content, double? duration = null, Action? onClose = null);

        void Warn(string content, double? duration = null, Action? onClose = null);

        Task ErrorAsync(string content, double? duration = null, Action? onClose = null);

        Task InfoAsync(string content, double? duration = null, Action? onClose = null);

        Task LoadingAsync(string content, double? duration = null, Action? onClose = null);

        Task SuccessAsync(string content, double? duration = null, Action? onClose = null);

        Task WarningAsync(string content, double? duration = null, Action? onClose = null);

        Task WarnAsync(string content, double? duration = null, Action? onClose = null);
    }
}
