// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace TTShang.Core.Attachment.Enums
{
    /// <summary>
    /// 附件业务类型类型
    /// </summary>
    public enum AttachmentBusinessType
    {
        /// <summary>
        /// 头像
        /// </summary>
        [Description("Avatar")]
        Avatar,
        /// <summary>
        /// 订单
        /// </summary>
        [Description("Order")]
        Order,
        /// <summary>
        /// 聊天
        /// </summary>
        [Description("WoChat")]
        WoChat,

        /// <summary>
        /// 报表
        /// </summary>
        [Description("Report")]
        Report,
        
        /// <summary>
        /// Iot产品图
        /// </summary>
        [Description("IotProduct")]
        IotProduct,

        /// <summary>
        /// AppPackage
        /// </summary>
        [Description("AppPackage")]
        AppPackage,

        /// <summary>
        /// 打印模板预览图
        /// </summary>
        [Description("PrintTemplatePreviewImage")]
        PrintTemplatePreviewImage,

        /// <summary>
        /// 租户配置文件
        /// </summary>
        [Description("TenantConfigFile")]
        TenantConfigFile

    }
}
