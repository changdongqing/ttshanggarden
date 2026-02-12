// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Core.Email.Enums
{
    /// <summary>
    /// 邮件服务器标签
    /// 随便自定义
    /// </summary>
    public enum EmailServerTag
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("Default")]
        Default = 0,
        /// <summary>
        /// QQ邮箱
        /// </summary>
        [Description("QQEmail")]
        QQ,
        /// <summary>
        /// 163邮箱
        /// </summary>
        [Description("163Email")]
        E163,
        /// <summary>
        /// Gmail邮箱
        /// </summary>
        [Description("Gmail")]
        Gmail,
        /// <summary>
        /// 企业邮箱
        /// </summary>
        [Description("EnterpriseEmail")]
        Enterprise,
        /// <summary>
        /// 活动邮箱
        /// </summary>
        [Description("ActivityEmail")]
        Activity
    }
}
