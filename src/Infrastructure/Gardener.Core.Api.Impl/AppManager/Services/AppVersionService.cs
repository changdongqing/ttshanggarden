using Gardener.Core.Api.Impl.AppManager.Entities;
using Gardener.Core.AppManager.Dtos;
using Gardener.Core.AppManager.Enums;
using Gardener.Core.AppManager.Services;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Net.Http;
using System.Net;

namespace Gardener.Core.Api.Impl.AppManager.Services
{
    /// <summary>
    /// 应用版本服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService), Module = "app-manager")]
    public class AppVersionService : ServiceBase<AppVersion, AppVersionDto, Int64>, IAppVersionService
    {
        private readonly IRepository<App> appRep;
        /// <summary>
        /// 应用版本服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="appRep"></param>
        public AppVersionService(IRepository<AppVersion> repository, IRepository<App> appRep) : base(repository)
        {
            this.appRep = appRep;
        }
        /// <summary>
        /// 查找新版本
        /// </summary>
        /// <remarks>
        /// 查找新版本
        /// </remarks>
        /// <param name="packageName"></param>
        /// <param name="version"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<AppVersionDto?> FindNewVersion(string packageName, long version, AppEnvironments environment)
        {
            App? app = await appRep.AsQueryable(false).FirstOrDefaultAsync(x => x.PackageName.Equals(packageName) && !x.IsLocked && !x.IsDeleted);
            if (app == null)
            {
                return null;
            }
            var entity = await _repository.AsQueryable(false).Where(x =>
            !x.IsLocked &&
            !x.IsDeleted &&
            x.Environment.Equals(environment) &&
            x.AppId.Equals(app.Id) &&
            x.VersionNumber > version
            ).OrderByDescending(x => x.VersionNumber).FirstOrDefaultAsync();
            if (entity != null)
            {
                entity.App = app;
            }
            return entity?.Adapt<AppVersionDto>();
        }


        /// <summary>
        /// 查询-获取最后一个版本
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<AppVersionDto?> FindLastVersion(string packageName, AppEnvironments environment)
        {
            App? app = await appRep.AsQueryable(false).FirstOrDefaultAsync(x => x.PackageName.Equals(packageName) && !x.IsLocked && !x.IsDeleted);
            if (app == null)
            {
                return null;
            }
            var entity = await _repository.AsQueryable(false).Where(x =>
            !x.IsLocked &&
            !x.IsDeleted &&
            x.Environment.Equals(environment) &&
            x.AppId.Equals(app.Id)
            ).OrderByDescending(x => x.VersionNumber).FirstOrDefaultAsync();
            if (entity != null)
            {
                entity.App = app;
            }
            return entity?.Adapt<AppVersionDto>();
        }

        /// <summary>
        /// 下载-下载最后一个版本
        /// </summary>
        /// <remarks>
        /// 只能登录后下载，避免资源占用
        /// </remarks>
        /// <param name="packageName"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DownloadLastVersion(string packageName, AppEnvironments environment)
        {
            AppVersionDto? appVersion = await FindLastVersion(packageName, environment);

            if (appVersion == null)
            {
                return new EmptyResult();
            }

            HttpClient httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(10)
            };
            var response = await httpClient.GetAsync(appVersion.AppUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            // 获取文件内容
            var fileStream = await response.Content.ReadAsStreamAsync();

            return new FileStreamResult(fileStream, "application/vnd.android.package-archive")
            {
                FileDownloadName = appVersion.FileName
            };
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 高级查询，根据输入条件组合进行数据查询和排序
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        public override async Task<PageList<AppVersionDto>> Search(PageRequest request)
        {
            PageList<AppVersionDto> result =await base.Search(request);
            if (result.Items.Any())
            {
                var appMap= await appRep.AsQueryable(false).Where(x => result.Items.Select(y => y.AppId).Any(y => y.Equals(x.Id))).ToDictionaryAsync(x => x.Id, x => x);
                foreach (var item in result.Items)
                {
                    if (appMap.ContainsKey(item.AppId))
                    {
                        item.App=appMap[item.AppId].Adapt<AppDto>();
                    }
                }
            }

            return result;
        }
    }
}
