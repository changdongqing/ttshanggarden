// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.FileStore;
using System.Text.Json;

namespace TTShang.Core.Api.Impl.FileStore.Internal.ALiOSS
{
    /// <summary>
    /// 阿里OSS存储服务提供者
    /// </summary>
    public class ALiOSSFileStoreServiceProvider : IFileStoreServiceProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public FileStoreSettingsBase? ConvertSettings(Dictionary<string, object> settings)
        {
            var json = JsonSerializer.Serialize(settings);
            return JsonSerializer.Deserialize<ALiOSSFileStoreSettings>(json);
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public IFileStoreService CreateFileStoreService(FileStoreSettingsBase settings)
        {
            return new ALiOSSFileStoreService((ALiOSSFileStoreSettings)settings);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public IFileStoreService CreateFileStoreService(Dictionary<string, object> settings)
        {
            FileStoreSettingsBase? config = ConvertSettings(settings);
            if (config == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(nameof(ALiOSSFileStoreServiceProvider) + " ConvertSettings result null,settings:" + JsonSerializer.Serialize(settings));
            }
            return CreateFileStoreService(config);
        }
    }
}
