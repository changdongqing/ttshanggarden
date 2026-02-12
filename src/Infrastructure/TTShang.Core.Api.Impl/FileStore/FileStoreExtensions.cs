// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.FileStore.Internal;
using TTShang.Core.Api.Impl.FileStore.Internal.ALiOSS;
using TTShang.Core.Api.Impl.FileStore.Internal.LocalStore;
using TTShang.Core.FileStore;
using Microsoft.Extensions.DependencyInjection;

namespace TTShang.Core.Api.Impl.FileStore
{
    /// <summary>
    /// 文件存储扩展
    /// </summary>
    public static class FileStoreExtensions
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileStore(this IServiceCollection services)
        {
            //配置
            services.AddConfigurableOptions<FileStoreSettings>();

            //文件存储服务工厂
            services.AddSingleton<IFileStoreServiceFactory, FileStoreServiceFactory>();

            //本地
            services.AddKeyedSingleton<IFileStoreServiceProvider, LocalFileStoreServiceProvider>(FileStoreServiceType.Local);

            //阿里OSS
            services.AddKeyedSingleton<IFileStoreServiceProvider, ALiOSSFileStoreServiceProvider>(FileStoreServiceType.ALiOSS);

            //默认的文件存储服务
            services.AddSingleton(serviceProvider =>
            {
                var fileStoreServiceFactory = serviceProvider.GetRequiredService<IFileStoreServiceFactory>();
                var service = fileStoreServiceFactory.GetDefaultFileStoreService();
                if (service == null)
                {
                    throw new ArgumentNullException("DefaultFileStoreService");
                }
                return service;
            });
            return services;
        }
    }
}
