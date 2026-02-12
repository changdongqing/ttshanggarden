// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.VerifyCode.Enums;

namespace Gardener.Core.Api.Impl.VerifyCode.Internal.DbStore
{
    /// <summary>
    /// 验证码
    /// </summary>
    [IgnoreAudit]
    public class VerifyCodeLog : GardenerEntityBase<Guid>
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        public VerifyCodeTypeEnum VerifyCodeType { get; set; }

        /// <summary>
        /// 验证码唯一键
        /// </summary>
        public string Key { get; set; } = null!;

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTimeOffset EndTime { get; set; }
    }
}
