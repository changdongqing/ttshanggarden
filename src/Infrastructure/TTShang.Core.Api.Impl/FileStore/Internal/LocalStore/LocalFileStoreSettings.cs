// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using TTShang.Core.FileStore;

namespace TTShang.Core.Api.Impl.FileStore.Internal.LocalStore
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalFileStoreSettings : FileStoreSettingsBase, IConfigurableOptions
    {
        /// <summary>
        /// 存储的基础目录,在wwwroot下
        /// 为空时，默认是upload 路径
        /// </summary>
        public string BaseDirectory { get; set; } = "upload";


        /// <summary>
        /// 域名
        /// </summary>
        /// <remarks>
        /// 为空时，使用服务访问地址
        /// </remarks>
        public string? Domain { get; set; }
    }
}
