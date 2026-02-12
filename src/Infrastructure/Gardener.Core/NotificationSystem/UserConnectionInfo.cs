// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.NotificationSystem
{
    /// <summary>
    /// 用户链接信息
    /// </summary>
    public class UserConnectionInfo
    {
        /// <summary>
        /// 用户链接信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="identity"></param>
        /// <param name="ip"></param>
        public UserConnectionInfo(string connectionId, Identity identity, string ip)
        {
            ConnectionId = connectionId;
            Identity = identity;
            Ip = ip;
        }

        /// <summary>
        /// 链接编号
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// 用户Ip
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 身份信息
        /// </summary>
        public Identity Identity { get; set; }
        /// <summary>
        /// 连接时间
        /// </summary>
        public DateTimeOffset ConnectTime { get; set; } = DateTimeOffset.Now;
    }
}
