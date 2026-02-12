// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.FileStore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Gardener.Core.Api.Impl.FileStore.Internal.LocalStore
{
    /// <summary>
    /// 本地存储服务提供者
    /// </summary>
    public class LocalFileStoreServiceProvider : IFileStoreServiceProvider
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public LocalFileStoreServiceProvider(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public FileStoreSettingsBase? ConvertSettings(Dictionary<string, object> settings)
        {
            var json = JsonSerializer.Serialize(settings);
            return JsonSerializer.Deserialize<LocalFileStoreSettings>(json);
        }
        /// <summary>
        /// 获取存储服务
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IFileStoreService CreateFileStoreService(FileStoreSettingsBase settings)
        {
            return new LocalFileStoreService(_serviceScopeFactory, (LocalFileStoreSettings)settings);
        }
        /// <summary>
        /// 获取存储服务
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public IFileStoreService CreateFileStoreService(Dictionary<string, object> settings)
        {
            FileStoreSettingsBase? config = ConvertSettings(settings);
            if (config == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(nameof(LocalFileStoreService) + " ConvertSettings result null,settings:" + JsonSerializer.Serialize(settings));
            }
            return CreateFileStoreService(config);
        }
    }
}
