// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.QRCoder.Services;
using TTShang.Core.Module;
using TTShang.Core.QRCoder;

namespace TTShang.Core.Api.Impl.QRCoder
{
    /// <summary>
    /// 
    /// </summary>
    public class QRCoderServerModule : QRCoderModule, IServerModule
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
                return [typeof(QRCoderService)];
            }
        }
    }
}
