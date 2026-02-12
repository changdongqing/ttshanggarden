// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using MQTTnet.Diagnostics.Logger;

namespace TTShang.Iot.Server.Mqtt
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    public class MqttNetLogger : IMqttNetLogger
    {
        private readonly ILogger logger;
        private readonly bool isEnabled = true;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="isEnabled"></param>
        public MqttNetLogger(ILogger logger, bool isEnabled = true)
        {
            this.logger = logger;
            this.isEnabled = isEnabled;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabled => isEnabled;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// <param name="exception"></param>
        public void Publish(MqttNetLogLevel logLevel, string source, string message, object[] parameters, Exception exception)
        {
            switch (logLevel)
            {
                case MqttNetLogLevel.Verbose:
                    logger.LogTrace(message, parameters);
                    break;

                case MqttNetLogLevel.Info:
                    logger.LogInformation(message, parameters);
                    break;

                case MqttNetLogLevel.Warning:
                    logger.LogWarning(message, parameters);
                    break;

                case MqttNetLogLevel.Error:
                    logger.LogError(exception, message, parameters);
                    break;
            }
        }
    }
}
