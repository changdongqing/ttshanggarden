// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.QRCoder.Dtos;
using TTShang.Core.QRCoder.Services;

namespace TTShang.Core.Client.Impl.QRCoder.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class QRCoderService : ClientServiceCaller, IQRCoderService
    {
        public QRCoderService(IApiCaller apiCaller) : base(apiCaller, "qr-coder")
        {
        }

        public Task<string?> CreateQrCodeBase64(QRCodeEncodeInput input)
        {
            return apiCaller.PostAsync<QRCodeEncodeInput, string?>($"{baseUrl}/qr-code-base64", input);
        }

        public Task<string?> DecodeFromBase64(QRCodeDecodeInput input)
        {
            return apiCaller.PostAsync<QRCodeDecodeInput, string?>($"{baseUrl}/decode-form-base64", input);
        }

        public Stream GetDimensionalCode(string contents, string color = "Black", int width = 500, int height = 500)
        {
            throw new NotImplementedException();
        }

        public Stream GetStripCode(string contents, int width = 800, int height = 400)
        {
            throw new NotImplementedException();
        }
    }
}
