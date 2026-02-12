// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Client.Services
{
    /// <summary>
    /// Client 通知提示
    /// </summary>
    public interface IClientNotifier
    {
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        /// <param name="duration"></param>
        void Error(string description, string? title = null, Exception? ex = null, double? duration = null);
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        Task ErrorAsync(string description, string? title = null, Exception? ex = null, double? duration = null);
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="duration"></param>
        void Info(string description, string? title = null, double? duration = null);
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        Task InfoAsync(string description, string? title = null, double? duration = null);
        /// <summary>
        /// Success
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        void Success(string description, string? title = null, double? duration = null);
        /// <summary>
        /// Success
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        Task SuccessAsync(string description, string? title = null, double? duration = null);
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        /// <param name="duration"></param>
        void Warn(string description, string? title = null, Exception? ex = null, double? duration = null);
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        Task WarnAsync(string description, string? title = null, Exception? ex = null, double? duration = null);
    }
}