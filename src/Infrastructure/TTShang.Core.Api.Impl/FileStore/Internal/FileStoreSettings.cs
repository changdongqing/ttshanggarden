// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace TTShang.Core.Api.Impl.FileStore.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class FileStoreSettings : IConfigurableOptions
    {
        /// <summary>
        /// 默认服务类型
        /// </summary>
        public string DefaultFileStoreService { get; set; } = null!;
        /// <summary>
        /// 服务配置集合
        /// </summary>
        public List<Dictionary<string, object>> Services { get; set; } = new();
    }
}
