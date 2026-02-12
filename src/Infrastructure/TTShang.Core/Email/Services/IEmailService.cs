// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Email.Dtos;

namespace TTShang.Core.Email.Services
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> Send(SendEmailInputDto input);
    }
}
