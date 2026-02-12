// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.VerifyCode.Services;
using Gardener.Core.Module;
using Gardener.Core.VerifyCode;

namespace Gardener.Core.Api.Impl.VerifyCode
{
    /// <summary>
    /// 模块
    /// </summary>
    public class VerifyCodeServerModule : VerifyCodeModule, IServerModule
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApiGroupName => Constant.InfrastructureService;

        /// <summary>
        /// 限定ApiTag
        /// </summary>
        public Type[]? IncludeApiControlTypes
        {
            get
            {
                return [typeof(EmailVerifyCodeService), typeof(ImageVerifyCodeService)];
            }
        }
    }
}
