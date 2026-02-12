// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Iot.Enums
{
    /// <summary>
    /// 连接身份验证状态
    /// </summary>
    public enum ConnectionIdentityAuthenticationState : int
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeed=1,
        /// <summary>
        /// 无效的设备编号或密钥
        /// </summary>
        BadDeviceIdOrSecretKey = 2
    }
}
