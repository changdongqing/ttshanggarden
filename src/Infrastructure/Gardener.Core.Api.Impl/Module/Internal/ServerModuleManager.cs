// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.Core.Api.Impl.Module.Entities;
using Gardener.Core.Authorization.Dtos;
using Gardener.Core.Dict.Dtos;
using Gardener.Core.Dict.Services;
using Gardener.Core.Module;
using Gardener.Core.Module.Services;
using Gardener.Core.Swagger.Services;
using Gardener.Core.SystemAsset.Services;
using Gardener.Core.UserCenter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;

namespace Gardener.Core.Api.Impl.Module.Internal
{
    /// <summary>
    /// 服务器模块管理
    /// </summary>
    public class ServerModuleManager : IModuleManager
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<ServerModuleManager> logger;
        private Dictionary<string, string> resourceKeys = new Dictionary<string, string>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="scopeFactory"></param>
        public ServerModuleManager(ILogger<ServerModuleManager> logger, IServiceScopeFactory scopeFactory)
        {
            this.logger = logger;
            this.scopeFactory = scopeFactory;
        }
        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IModule> GetModules()
        {
            return GetServerModules();
        }
        /// <summary>
        /// 获取模块
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IModule? GetModule(string name)
        {
            return GetServerModules().Where(x => x.Name.Equals(name)).FirstOrDefault();
        }
        /// <summary>
        /// 获取所有服务端模块
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<IServerModule> GetServerModules()
        {
            using var scope = scopeFactory.CreateScope();
            return scope.ServiceProvider.GetServices<IServerModule>().OrderBy(x => x.Order);
        }

        /// <summary>
        /// 获取已存储模块信息
        /// </summary>
        /// <returns></returns>
        internal Task<List<SystemModuleDto>> GetSystemModules()
        {
            ISystemModuleService moduleService = App.GetRequiredService<ISystemModuleService>();
            return moduleService.GetAll();
        }

        /// <summary>
        /// 卸载模块
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="serverModule"></param>
        /// <param name="systemModule"></param>
        /// <returns></returns>
        internal async Task Uninstall(CancellationToken cancellationToken, IServerModule serverModule, SystemModuleDto? systemModule)
        {
            string name = serverModule.Name;
            //删除 codeType,code,resource,function,resourceFunction
            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                var codeTypeService = scope.ServiceProvider.GetRequiredService<ICodeTypeService>();
                await codeTypeService.DeleteByModuleName(name, false);
                var codeService = scope.ServiceProvider.GetRequiredService<ICodeService>();
                await codeService.DeleteByModuleName(name, false);
                var resourceFunctionService = scope.ServiceProvider.GetRequiredService<IResourceFunctionService>();
                await resourceFunctionService.DeleteByModuleName(name, false);
                var resourceService = scope.ServiceProvider.GetRequiredService<IResourceService>();
                await resourceService.DeleteByModuleName(name, false);
                var functionService = scope.ServiceProvider.GetRequiredService<IFunctionService>();
                await functionService.DeleteByModuleName(name, false);
                var tenantConfigTemplateService = scope.ServiceProvider.GetRequiredService<ITenantConfigTemplateService>();
                await tenantConfigTemplateService.DeleteByModuleName(name, false);
                if (systemModule != null)
                {
                    //删除 systemModule
                    ISystemModuleService moduleService = scope.ServiceProvider.GetRequiredService<ISystemModuleService>();
                    await moduleService.Delete(systemModule.Id);
                }
            }
            //执行模块自定义事件
            await serverModule.OnUninstall(cancellationToken);
        }

        /// <summary>
        /// 安装模块
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="serverModule"></param>
        /// <returns></returns>
        internal async Task Install(CancellationToken cancellationToken, IServerModule serverModule)
        {
            SystemModule systemModule = new SystemModule()
            {
                Name = serverModule.Name,
                Description = serverModule.Description,
                Version = serverModule.Version,
                Author = serverModule.Author,
                AuthorHome = serverModule.AuthorHome,
                Order = serverModule.Order
            };
            using IServiceScope scope = scopeFactory.CreateScope(); ;
            //写入 systemModule
            ISystemModuleService moduleService = scope.ServiceProvider.GetRequiredService<ISystemModuleService>();
            await moduleService.Insert(systemModule);
            //写入 codeType,code,resource,function,resourceFunction

            #region code
            var dics = serverModule.RegisterDic();
            if (dics != null)
            {
                var codeTypeService = scope.ServiceProvider.GetRequiredService<ICodeTypeService>();
                List<CodeDto> codes = new List<CodeDto>();
                foreach (var codeType in dics)
                {
                    codeType.Id = 0;
                    codeType.ModuleName = serverModule.Name;
                    if (codeType.Codes != null)
                    {
                        foreach (var codeDto in codeType.Codes)
                        {
                            codeDto.Id = 0;
                            codeDto.ModuleName = serverModule.Name;
                            codeDto.CodeTypeId = codeType.Id;
                        }
                    }
                    await codeTypeService.Insert(codeType);
                }
            }
            #endregion

            #region function
            List<FunctionDto> functionList = new List<FunctionDto>();
            if (serverModule.AutoRegisterFunction)
            {
                //自动扫描接口
                IApiEndpointService? swagger = App.GetService<IApiEndpointService>();
                if (swagger != null)
                {
                    IEnumerable<ApiEndpoint>? apiEndpoints = await swagger.GetApis(serverModule.ApiGroupName, serverModule.IncludeApiControlTypes);
                    if (apiEndpoints != null)
                    {
                        foreach (ApiEndpoint apiEndpoint in apiEndpoints)
                        {
                            Guid id = Guid.Parse(MD5Helper.Encrypt(apiEndpoint.Key));
                            FunctionDto function = new FunctionDto()
                            {
                                Id = id,
                                Key = apiEndpoint.Key,
                                Group = apiEndpoint.Group,
                                GroupTitle = apiEndpoint.GroupTitle,
                                Summary = apiEndpoint.Summary,
                                Description = apiEndpoint.Description,
                                Path = apiEndpoint.Path,
                                Method = apiEndpoint.Method,
                                Tags = apiEndpoint.Tags != null ? string.Join(',', apiEndpoint.Tags.Keys.ToList()) : null,
                                TagTitles = apiEndpoint.Tags != null ? string.Join(',', apiEndpoint.Tags.Values.ToList()) : null,
                                ModuleName = serverModule.Name,
                                EnableAudit = !ApiHttpMethod.GET.Equals(apiEndpoint.Method)
                            };
                            functionList.Add(function);
                        }
                    }
                }
            }
            var registerFunctions = serverModule.RegisterFunction();
            if (registerFunctions != null)
            {
                functionList.AddRange(registerFunctions);
            }
            if (functionList.Any())
            {
                foreach (FunctionDto item in functionList)
                {
                    //如果未设置编号
                    if (item.Id == Guid.Empty)
                    {
                        //根据唯一key固化Id
                        item.Id = Guid.Parse(MD5Helper.Encrypt(item.Key));
                    }
                }
                var functionService = scope.ServiceProvider.GetRequiredService<IFunctionService>();
                await functionService.BatchInsert(functionList);
            }
            #endregion

            #region tenantConfigTemplate
            var tenantConfigTemplates = serverModule.RegisterTenantConfigTemplate();
            if (tenantConfigTemplates != null && tenantConfigTemplates.Any())
            {
                foreach (var template in tenantConfigTemplates)
                {
                    template.ModuleName = serverModule.Name;
                }
                var tenantConfigTemplateService = scope.ServiceProvider.GetRequiredService<ITenantConfigTemplateService>();
                await tenantConfigTemplateService.BatchInsert(tenantConfigTemplates);
            }
            #endregion

            //执行模块自定义事件
            await serverModule.OnInstall(cancellationToken);
        }
        /// <summary>
        /// 安装资源
        /// </summary>
        /// <remarks>
        /// 资源依赖接口，所以单独后置安装
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <param name="serverModule"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        internal async Task InstallResources(CancellationToken cancellationToken, IServerModule serverModule)
        {
            #region resource
            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                var functionService = scope.ServiceProvider.GetRequiredService<IFunctionService>();
                List<FunctionDto> functions = await functionService.GetAll();
                Dictionary<Guid, bool> functionMap = functions.ToDictionary(x => x.Id, x => true);
                var resources = serverModule.RegisterResource();
                if (resources != null)
                {
                    foreach (var item in resources)
                    {
                        //如果未设置编号
                        if (item.Id == Guid.Empty)
                        {
                            //根据唯一key固化Id
                            item.Id = Guid.Parse(MD5Helper.Encrypt(item.Key));
                        }
                        item.ModuleName = serverModule.Name;
                        if (resourceKeys.ContainsKey(item.Key))
                        {
                            throw new Exception($"Module [{item.ModuleName}] resource Key [{item.Key}] with module [{resourceKeys[item.Key]}] conflict。");
                        }
                        List<ResourceFunctionDto> rfs = new List<ResourceFunctionDto>();
                        foreach (var rf in item.ResourceFunctions)
                        {
                            if (!functionMap.GetValueOrDefault(rf.FunctionId, false))
                            {
                                //已经被删除的function
                                continue;
                            }
                            rf.ModuleName = item.ModuleName;
                            rfs.Add(rf);
                        }
                        item.ResourceFunctions = rfs;
                        resourceKeys.Add(item.Key, item.ModuleName);
                    }
                    var resourceService = scope.ServiceProvider.GetRequiredService<IResourceService>();
                    await resourceService.BatchInsert(resources);
                }
            }
            #endregion
        }
        /// <summary>
        /// 启动时处理
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task OnStart(CancellationToken cancellationToken)
        {
            IEnumerable<IServerModule> modules = GetServerModules();
            Dictionary<string, List<string>> apiGroups = new Dictionary<string, List<string>>();
            Dictionary<string, List<Type>> apiTypes = new Dictionary<string, List<Type>>();
            List<string> moduleNames = new();
            //启动
            foreach (var module in modules)
            {
                if (moduleNames.Contains(module.Name))
                {
                    throw new Exception($"{module.Name} is repeat.");
                }
                moduleNames.Add(module.Name);
                if (!module.AutoRegisterFunction)
                {
                    continue;
                }
                foreach (var module1 in modules)
                {
                    if (!module1.AutoRegisterFunction || module1.Name.Equals(module.Name) || !module.ApiGroupName.Equals(module1.ApiGroupName))
                    {
                        continue;
                    }

                    if (module.IncludeApiControlTypes == null || module1.IncludeApiControlTypes == null)
                    {
                        throw new Exception($"{module.Name} and {module1.Name} apiGroupName {module1.ApiGroupName} is repeat.");
                    }
                    else
                    {

                        var list = module.IncludeApiControlTypes.Intersect(module1.IncludeApiControlTypes);
                        if (list.Any())
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            stringBuilder.AppendLine($"{module.Name} and {module1.Name} apiGroupName {module1.ApiGroupName} typs is repeat.");
                            stringBuilder.AppendLine($"typs=>{string.Join(",", list.Select(x => x.FullName))}");
                            throw new Exception(stringBuilder.ToString());
                        }

                    }
                }
            }
            foreach (var module in modules)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                await module.OnStart(cancellationToken);
                logger.LogInformation("module {0} OnStart run complete elapsed time {1} ms.", module.Name, stopwatch.ElapsedMilliseconds);
            }

            List<SystemModuleDto> systemModules = await GetSystemModules();
            List<IServerModule> installedModules = new List<IServerModule>();
            //注册数据
            foreach (var module in modules)
            {
                var moduleInfo = systemModules.FirstOrDefault(x => x.Name == module.Name);
                //不能自动安装
                if (moduleInfo == null && !module.AutoInstall)
                {
                    logger.LogInformation("module {0} complete.", module.Name);
                    continue;
                }
                if (moduleInfo != null)
                {
                    logger.LogInformation("module {0} already exists.", module.Name);
                    //模块已存在
                    continue;
                }
                //安装
                await Install(cancellationToken, module);
                logger.LogInformation("module {0} Install complete.", module.Name);
                installedModules.Add(module);
            }
            foreach (var module in installedModules)
            {
                await InstallResources(cancellationToken, module);
                logger.LogInformation("module {0} Install resources complete.", module.Name);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task OnExecute(CancellationToken cancellationToken)
        {
            IEnumerable<IServerModule> modules = GetServerModules();
            foreach (var module in modules)
            {
                await module.OnExecute(cancellationToken);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task OnStop(CancellationToken cancellationToken)
        {
            IEnumerable<IServerModule> modules = GetServerModules();
            foreach (var module in modules)
            {
                await module.OnStop(cancellationToken);
            }
        }

    }
}
