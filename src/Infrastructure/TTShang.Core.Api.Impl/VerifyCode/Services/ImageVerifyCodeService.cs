// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.VerifyCode.Core;
using TTShang.Core.VerifyCode.Dtos;
using TTShang.Core.VerifyCode.Enums;
using TTShang.Core.VerifyCode.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Api.Impl.VerifyCode.Services
{
    /// <summary>
    /// 图片验证码服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class ImageVerifyCodeService : IImageVerifyCodeService
    {
        private readonly IVerifyCode verifyCodeService;
        /// <summary>
        /// 验证码服务
        /// </summary>
        /// <param name="verifyCodeService"></param>
        public ImageVerifyCodeService([FromKeyedServices(nameof(VerifyCodeTypeEnum) + nameof(VerifyCodeTypeEnum.Image))] IVerifyCode verifyCodeService)
        {
            this.verifyCodeService = verifyCodeService;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="input">类型</param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<ImageVerifyCodeOutput> Create(ImageVerifyCodeInput input)
        {
            ImageVerifyCodeOutput imageVerifyCode = (ImageVerifyCodeOutput)await verifyCodeService.Create(input);
            return imageVerifyCode;
        }
        /// <summary>
        /// 移除验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> Remove(string key)
        {
            return await verifyCodeService.Remove(key);
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> Verify(ImageVerifyCodeCheckInput input)
        {
            return await verifyCodeService.Verify(input.VerifyCode, input.VerifyCodeKey);
        }
    }
}
