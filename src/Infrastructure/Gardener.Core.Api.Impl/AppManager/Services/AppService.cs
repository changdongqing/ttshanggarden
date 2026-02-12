// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.AppManager.Entities;
using Gardener.Core.AppManager.Dtos;
using Gardener.Core.AppManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace Gardener.Core.Api.Impl.AppManager.Services
{
    /// <summary>
    /// 应用服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService), Module = "app-manager")]
    public class AppService : ServiceBase<App, AppDto, Guid>, IAppService
    {

        /// <summary>
        /// 应用服务
        /// </summary>
        /// <param name="repository"></param>
        public AppService(IRepository<App> repository) : base(repository)
        {
        }

        /// <summary>
        /// 查询-根据包名查询app信息
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public Task<AppDto?> FindByPackageName(string packageName)
        {
            return _repository.AsQueryable(false).Where(x => x.PackageName.Equals(packageName) && !x.IsLocked && !x.IsDeleted).Select(x => x.Adapt<AppDto>()).FirstOrDefaultAsync();
        }
    }
}
