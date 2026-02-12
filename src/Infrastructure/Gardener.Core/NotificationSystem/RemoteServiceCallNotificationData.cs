// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.NotificationSystem
{
    /// <summary>
    /// 远程服务调用通知
    /// </summary>
    public class RemoteServiceCallNotificationData : NotificationData
    {
        /// <summary>
        /// 远程服务调用通知
        /// </summary>
        /// <param name="serviceKey"></param>
        /// <param name="actionKey"></param>
        /// <param name="toConnectionId"></param>
        public RemoteServiceCallNotificationData(string serviceKey, string actionKey, string toConnectionId) : base()
        {
            CallId = Guid.NewGuid();
            ServiceKey = serviceKey;
            ActionKey = actionKey;
            ToConnectionId = toConnectionId;
        }
        /// <summary>
        /// 本次调用编号
        /// </summary>
        public Guid CallId { get; set; }

        /// <summary>
        /// 目标链接
        /// </summary>
        public string ToConnectionId { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceKey { get; set; }
        /// <summary>
        /// 动作名
        /// </summary>
        public string ActionKey { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string? Data { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public RemoteServiceCallNotificationState State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsRequest()
        {
            return RemoteServiceCallNotificationState.Request.Equals(State);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RemoteServiceCallNotificationData Request()
        {
            State = RemoteServiceCallNotificationState.Request;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsResponse()
        {
            return RemoteServiceCallNotificationState.Response.Equals(State);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RemoteServiceCallNotificationData Response()
        {
            State = RemoteServiceCallNotificationState.Response;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RemoteServiceCallNotificationData RequestToResponse()
        {
            if (FromConnectionId != null)
            {
                ToConnectionId = FromConnectionId;
            }
            Id = Guid.NewGuid();
            State = RemoteServiceCallNotificationState.Response;
            Timestamp = DateTime.Now;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsDiscoverRemoteService()
        {
            return ServiceKey.Equals("remoteService") && ActionKey.Equals("discover");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superServices"></param>
        /// <returns></returns>
        public RemoteServiceCallNotificationData SetSuperServices(List<RemoteService> superServices)
        {
            superServices.AddRange(GetInternalRemoteService());

            Data = System.Text.Json.JsonSerializer.Serialize(superServices);
            return this;
        }

        private List<RemoteService> GetInternalRemoteService()
        {
            return [new RemoteService("remoteService", "远程服务", "远程服务内置服务", [
                      new RemoteServiceAction("discover","发现服务","用于发现各端服务",false)

                  ])];
        }
        /// <summary>
        /// 创建发现服务通知
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public static RemoteServiceCallNotificationData CreateDiscoverRemoteServiceNotification(string connectionId)
        {
            return new RemoteServiceCallNotificationData("remoteService", "discover", connectionId);
        }

        /// <summary>
        /// 获取远程服务
        /// </summary>
        /// <returns></returns>
        public List<RemoteService>? GetRemoteServices()
        {
            if(string.IsNullOrEmpty(Data))
            {
                return null;
            }
            return System.Text.Json.JsonSerializer.Deserialize<List<RemoteService>>(Data);
        }
    }
}
