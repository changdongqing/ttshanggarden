// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.VerifyCode.Dtos;
using TTShang.Core.VerifyCode.Services;

namespace TTShang.Core.Client.Impl.VerifyCode.Services
{
    [ScopedService]
    public class ImageVerifyCodeService :ClientServiceCaller, IImageVerifyCodeService
    {
        public ImageVerifyCodeService(IApiCaller apiCaller):base(apiCaller, "image-verify-code")
        {
        }

        public async Task<ImageVerifyCodeOutput> Create(ImageVerifyCodeInput input)
        {
            return await apiCaller.PostAsync<VerifyCodeInput, ImageVerifyCodeOutput>($"{this.baseUrl}", input);
        }

        public async Task<bool> Remove(string key)
        {
            return await apiCaller.DeleteAsync<bool>($"{this.baseUrl}/{key}");
        }

        public async Task<bool> Verify(ImageVerifyCodeCheckInput verifyCodeInput)
        {
            return await apiCaller.PostAsync<ImageVerifyCodeCheckInput, bool>($"{this.baseUrl}/verify", verifyCodeInput);
        }
    }
}
