// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.NotificationSystem
{
    /// <summary>
    /// 远程服务动作
    /// </summary>
    public class RemoteServiceAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionKey"></param>
        /// <param name="actionName"></param>
        /// <param name="actionDescription"></param>
        /// <param name="superInputData"></param>
        public RemoteServiceAction(string actionKey, string actionName, string actionDescription, bool superInputData)
        {
            ActionName = actionName;
            ActionDescription = actionDescription;
            SuperInputData = superInputData;
            ActionKey = actionKey;
        }

        /// <summary>
        /// 动作键
        /// </summary>
        public string ActionKey { get; set; }

        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 动作描述
        /// </summary>
        public string ActionDescription { get; set; }
        /// <summary>
        /// 支持输入参数
        /// </summary>
        public bool SuperInputData { get; set; }
        /// <summary>
        /// 数据描述
        /// </summary>
        public string? DataDescription { get; set; }
    }
}
