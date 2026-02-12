// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.VerifyCode.Internal.Settings;
using TTShang.Core.VerifyCode.Core;
using TTShang.Core.VerifyCode.Dtos;
using TTShang.Core.VerifyCode.Enums;
using Microsoft.Extensions.Options;

namespace TTShang.Core.Api.Impl.VerifyCode.Internal
{
    /// <summary>
    /// 图片验证码服务
    /// </summary>
    internal class ImageVerifyCode : IVerifyCode
    {
        IVerifyCodeStoreService store;
        ImageVerifyCodeOptions settings;
        /// <summary>
        /// 图片验证码服务
        /// </summary>
        /// <param name="store"></param>
        /// <param name="options"></param>
        public ImageVerifyCode(IVerifyCodeStoreService store, IOptions<ImageVerifyCodeOptions> options)
        {
            this.store = store;
            settings = options.Value;
        }
        /// <summary>
        /// 创建校验码
        /// </summary>
        /// <param name="input">参数</param>
        /// <returns></returns>
        public async Task<VerifyCodeOutput> Create(VerifyCodeInput input)
        {
            ImageVerifyCodeInput imageInput = (ImageVerifyCodeInput)input;
            var (code, image, codeLength) = InnerCreate(imageInput);
            ImageVerifyCodeOutput verifyCodeInfo = new ImageVerifyCodeOutput()
            {
                Key = Guid.NewGuid().ToString(),
                Base64Image = Convert.ToBase64String(image),
                CodeLength = codeLength
            };
            await store.Add(VerifyCodeTypeEnum.Image, verifyCodeInfo.Key, code, settings.CodeExpire);
            return verifyCodeInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private (string, byte[], int?) InnerCreate(ImageVerifyCodeInput param)
        {
            param.CreateCodeParam = param.CreateCodeParam ?? new CharacterCodeCreateParam();
            if (!param.CreateCodeParam.CharacterCount.HasValue)
            {
                param.CreateCodeParam.CharacterCount = settings.CodeCharacterCount;
            }
            if (!param.CreateCodeParam.Type.HasValue)
            {
                param.CreateCodeParam.Type = settings.CodeType;
            }
            if (!param.FontSize.HasValue)
            {
                param.FontSize = settings.CodeFontSize;
            }

            CharacterCodeCreateParam characterVerifyCodeParam = param.CreateCodeParam;
            string code = RandomCodeCreator.Create(characterVerifyCodeParam.Type.Value, characterVerifyCodeParam.CharacterCount.Value);
            byte[] image = RandomCodeImageCreatorFromSkiaSharp.Create(code, param.FontSize.Value);
            return (code, image, characterVerifyCodeParam.CharacterCount.Value);
        }

        /// <summary>
        /// 校验校验码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<bool> Verify(string key, string code)
        {
            //code为空，直接返回错误
            if (string.IsNullOrEmpty(code)) return false;
            string? dbCode = await store.GetCode(VerifyCodeTypeEnum.Image, key);
            bool success = string.Compare(dbCode, code, settings.IgnoreCase) == 0;
            if (success && settings.UseOnce)
            {
                await store.Remove(VerifyCodeTypeEnum.Image, key);
            }

            return success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> Remove(string key)
        {
            await store.Remove(VerifyCodeTypeEnum.Image, key);

            return true;
        }
    }
}
