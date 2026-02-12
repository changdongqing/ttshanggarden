// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Server.Mqtt
{
    /// <summary>
    /// 服务器配置
    /// </summary>
    public class MqttServerOptions
    {
        /// <summary>
        /// 启用logger
        /// </summary>
        public bool LoggerIsEnabled { get; set; } = true;
        /// <summary>
        /// 监听端口
        /// </summary>
        public int Port { get; set; } = 28888;
        /// <summary>
        /// 发送者客户端编号
        /// </summary>
        public string SenderClientId { get; set; } = null!;
        /// <summary>
        /// 客户端订阅所有数据主题
        /// </summary>
        public string ClientSubscribeAllDataTopic { get; set; } = null!;
        /// <summary>
        /// 客户端订阅自己数据主题前缀
        /// </summary>
        public string ClientSubscribeSelfDataTopicPrefix { get; set; } = null!;
    }
}
