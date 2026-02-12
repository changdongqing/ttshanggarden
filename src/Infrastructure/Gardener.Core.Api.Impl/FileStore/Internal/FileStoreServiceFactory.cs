// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.FileStore;
using Gardener.Core.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Gardener.Core.Api.Impl.FileStore.Internal
{
    /// <summary>
    /// 文件存储系统工厂
    /// </summary>
    public class FileStoreServiceFactory : IFileStoreServiceFactory
    {
        private readonly static object _locker = new object();
        Dictionary<string, IFileStoreService> services = new Dictionary<string, IFileStoreService>();
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<FileStoreSettings> _options;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="options"></param>
        public FileStoreServiceFactory(IServiceProvider serviceProvider, IOptions<FileStoreSettings> options)
        {
            _serviceProvider = serviceProvider;
            _options = options;
        }

        /// <summary>
        /// 获取存储服务
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public IFileStoreService GetFileStoreService(string serviceId)
        {
            if (!services.ContainsKey(serviceId))
            {
                lock (_locker)
                {
                    if (!services.ContainsKey(serviceId))
                    {
                        FileStoreSettings settings = _options.Value;
                        var configDic = settings.Services.FirstOrDefault(x => x.ContainsKey(nameof(FileStoreSettingsBase.FileStoreServiceId)) && serviceId.Equals(x[nameof(FileStoreSettingsBase.FileStoreServiceId)]));
                        if (configDic == null)
                        {
                            throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.File_Store_Service_Config_Not_Find, serviceId);
                        }
                        object? fileStoreServiceType = null;
                        if (!configDic.TryGetValue(nameof(FileStoreSettingsBase.FileStoreServiceType), out fileStoreServiceType))
                        {
                            throw Oops.Bah(
                                Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.RequiredValidationError)),
                                Lo.GetValue<SharedLocalResource>(nameof(SharedLocalResource.FileStoreServiceType))
                            );
                        }
                        FileStoreServiceType type = FileStoreServiceType.Local;
                        if (!Enum.TryParse(fileStoreServiceType.ToString(), true, out type))
                        {
                            throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.File_Store_Service_Type_Unsupported, fileStoreServiceType);
                        }
                        IFileStoreServiceProvider fileStoreServiceProvider = _serviceProvider.GetRequiredKeyedService<IFileStoreServiceProvider>(type);
                        IFileStoreService service = fileStoreServiceProvider.CreateFileStoreService(configDic);
                        services.Add(serviceId, service);
                    }
                }
            }
            return services[serviceId];
        }

        /// <summary>
        /// 获取默认服务
        /// </summary>
        /// <returns></returns>
        public IFileStoreService GetDefaultFileStoreService()
        {
            FileStoreSettings settings = _options.Value;
            if (string.IsNullOrEmpty(settings.DefaultFileStoreService))
            {
                throw Oops.Bah(
                    Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.RequiredValidationError)),
                    Lo.GetValue<SharedLocalResource>(nameof(SharedLocalResource.DefaultFileStoreService))
                );
            }
            return GetFileStoreService(settings.DefaultFileStoreService);
        }
        /// <summary>
        /// 创建存储服务
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IFileStoreService CreateFileStoreService(FileStoreSettingsBase settings)
        {
            IFileStoreServiceProvider fileStoreServiceProvider = _serviceProvider.GetRequiredKeyedService<IFileStoreServiceProvider>(settings.FileStoreServiceType);
            IFileStoreService service = fileStoreServiceProvider.CreateFileStoreService(settings);
            return service;
        }
    }
}
