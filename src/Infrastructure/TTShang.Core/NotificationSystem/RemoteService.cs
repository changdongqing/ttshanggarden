// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Core.NotificationSystem
{
    /// <summary>
    /// 远程服务
    /// </summary>
    public class RemoteService
    {
        /// <summary>
        /// 远程服务
        /// </summary>
        /// <param name="serviceKey"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceActions"></param>
        public RemoteService(string serviceKey,string serviceName, string serviceDescription, List<RemoteServiceAction> serviceActions)
        {
            ServiceKey = serviceKey;
            ServiceName = serviceName;
            ServiceDescription = serviceDescription;
            ServiceActions = serviceActions;
        }

        /// <summary>
        /// 服务键
        /// </summary>
        public string ServiceKey { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务描述
        /// </summary>
        public string ServiceDescription { get; set; }
        /// <summary>
        /// 服务动作列表
        /// </summary>
        public List<RemoteServiceAction> ServiceActions { get; set; }
    }
}
