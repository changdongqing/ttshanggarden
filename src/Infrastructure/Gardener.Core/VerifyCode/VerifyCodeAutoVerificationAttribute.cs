// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.VerifyCode
{
    /// <summary>
    /// 验证码自动验证
    /// 但是要保证参数要继承<see cref="Dtos.VerifyCodeCheckInput"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class VerifyCodeAutoVerificationAttribute : Attribute
    {

    }
}
