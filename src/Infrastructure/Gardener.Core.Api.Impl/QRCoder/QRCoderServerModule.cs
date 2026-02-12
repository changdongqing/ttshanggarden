// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.QRCoder.Services;
using Gardener.Core.Module;
using Gardener.Core.QRCoder;

namespace Gardener.Core.Api.Impl.QRCoder
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
