// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Authorization.Dtos;
using Gardener.Core.Swagger.Dtos;
using Gardener.Core.Swagger.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Gardener.Core.Api.Impl.Swagger.Services
{
    /// <summary>
    /// 接口终结点服务
    /// </summary>
    internal class ApiEndpointService : IApiEndpointService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        /// <summary>
        /// 初始化完成
        /// </summary>
        private static bool inited = false;

        /// <summary>
        /// 初始化任务
        /// </summary>
        private static Task? initTask;
        /// <summary>
        /// 终结点-key为<see cref="ApiEndpoint.Key"/>
        /// </summary>
        private readonly Dictionary<string, ApiEndpoint> _endpoints = new Dictionary<string, ApiEndpoint>();
        /// <summary>
        /// 终结点-key为<see cref="ApiEndpoint.Method"/>+<see cref="ApiEndpoint.Path"/>
        /// </summary>
        private readonly Dictionary<string, ApiEndpoint> _endpoints1 = new Dictionary<string, ApiEndpoint>();
        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<string, TypeInfo> _actionControlTypeMap = new Dictionary<string, TypeInfo>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="actionDescriptorCollectionProvider"></param>
        public ApiEndpointService(IServiceProvider serviceProvider, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _serviceProvider = serviceProvider;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }
        /// <summary>
        /// 获取api信息
        /// </summary>
        /// <remarks>
        /// 根据api key 获取api信息
        /// </remarks>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ApiEndpoint?> GetApi(string key)
        {
            if (!inited && initTask != null)
            {
                await initTask;
            }
            ApiEndpoint? apiEndpoint;
            _endpoints.TryGetValue(key, out apiEndpoint);
            return apiEndpoint;
        }
        /// <summary>
        /// 获取api信息
        /// </summary>
        /// <remarks>
        /// 根据请求方法和请求路径，获取api信息
        /// </remarks>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<ApiEndpoint?> GetApi(ApiHttpMethod method, string path)
        {
            if (!inited && initTask != null)
            {
                await initTask;
            }
            ApiEndpoint? apiEndpoint;
            string key = method + path;
            _endpoints1.TryGetValue(key, out apiEndpoint);
            return apiEndpoint;
        }
        /// <summary>
        /// 获取api信息
        /// </summary>
        /// <remarks>
        /// 从上下文中获取api信息
        /// </remarks>
        /// <returns></returns>
        public async Task<ApiEndpoint?> GetApi(object httpContext)
        {
            if (!inited && initTask != null)
            {
                await initTask;
            }
            //特性查询
            string? apiKey = GetApiEndpointKeySecurityDefineAttribute((HttpContext)httpContext);
            if (!string.IsNullOrEmpty(apiKey))
            {
                return await GetApi(apiKey);
            }
            //根据path和请求方法查询
            var (method, path) = ParseHttpContext((HttpContext)httpContext);
            return await GetApi(method, path);
        }
        /// <summary>
        /// 获取api信息
        /// </summary>
        /// <remarks>
        /// 根据组名和服务标签，获取api信息
        /// </remarks>
        /// <param name="groupName">分组</param>
        /// <param name="tags">标签</param>
        /// <returns></returns>
        public async Task<IEnumerable<ApiEndpoint>> GetApis(string? groupName = null, string[]? tags = null)
        {
            if (!inited && initTask != null)
            {
                await initTask;
            }
            IEnumerable<ApiEndpoint> list = _endpoints.Select(x => x.Value);
            if (!string.IsNullOrEmpty(groupName))
            {
                list = list.Where(x => x.Group.Equals(groupName));
            }
            if (tags != null && tags.Length > 0)
            {
                list = list.Where(g => tags.Any(t => g.Tags != null && g.Tags.ContainsKey(t)));
            }
            return list;
        }
        /// <summary>
        /// 获取api分组
        /// </summary>
        /// <remarks>
        /// 获取api分组设置信息
        /// </remarks>
        /// <returns></returns>
        public IEnumerable<SwaggerSpecificationOpenApiInfoDto> GetApiGroup()
        {
            SwaggerGeneratorOptions options = _serviceProvider.GetRequiredService<IOptions<SwaggerGeneratorOptions>>().Value;

            if (options == null) return [];

            return options.SwaggerDocs.Select(x => new SwaggerSpecificationOpenApiInfoDto()
            {
                Group = x.Key,
                Title = x.Value.Title,
                Description = x.Value.Description
            });
        }
        /// <summary>
        /// 初始化
        /// </summary>
        internal void Init()
        {
            SwaggerGeneratorOptions options = _serviceProvider.GetRequiredService<IOptions<SwaggerGeneratorOptions>>().Value;
            ISwaggerProvider? _swaggerProvider = _serviceProvider.GetService<ISwaggerProvider>();

            if (options == null || _swaggerProvider == null) return;

            Task task = Task.Run(async () =>
            {

                var task1= Task.Run(() => {
                    // 获取所有 Action 列表
                    var actionDescriptors = _actionDescriptorCollectionProvider.ActionDescriptors.Items;
                    foreach (ActionDescriptor item in actionDescriptors)
                    {
                        if (item.ActionConstraints == null) continue;
                        ControllerActionDescriptor ad = ((ControllerActionDescriptor)item);
                        if (ad.ControllerTypeInfo.FullName == null) { continue; }
                        foreach (var acs in item.ActionConstraints)
                        {
                            if (ad.AttributeRouteInfo == null) continue;
                            if (acs is Microsoft.AspNetCore.Mvc.ActionConstraints.HttpMethodActionConstraint hac)
                            {
                                foreach (var mothd in hac.HttpMethods)
                                {
                                    if (mothd == null) continue;
                                    string key = mothd + "/" + ad.AttributeRouteInfo.Template;
                                    _actionControlTypeMap.TryAdd(key, ad.ControllerTypeInfo);
                                }
                            }
                        }
                    }
                });
                
                Dictionary<string, OpenApiDocument> documents = new Dictionary<string, OpenApiDocument>();
                await options.SwaggerDocs.Select(x => x.Key).ForEachAsync(group =>
                 {
                     return Task.Run(() => { documents.Add(group, _swaggerProvider.GetSwagger(group)); });
                 });
                await task1;

                foreach (var doc in options.SwaggerDocs)
                {
                    //过滤分组
                    OpenApiDocument apiDocument = documents[doc.Key];

                    Dictionary<string, string> tagMap = apiDocument.Tags.ToDictionary(x => x.Name, k => k.Description);
                    foreach (var item in apiDocument.Paths)
                    {
                        string apiPath = item.Key;
                        foreach (var opt in item.Value.Operations)
                        {
                            OpenApiOperation apiOperation = opt.Value;
                            //过滤标签
                            ApiHttpMethod httpMethod = (ApiHttpMethod)Enum.Parse(typeof(ApiHttpMethod), opt.Key.ToString().ToUpper());
                            string? tag = apiOperation.Tags == null ? null : string.Join("_", apiOperation.Tags.Select(x => x.Name));
                            string? tagTitle = apiOperation.Tags == null ? null : string.Join("_", apiOperation.Tags.Select(x => tagMap.ContainsKey(x.Name) ? tagMap[x.Name] : x.Name));
                            Dictionary<string, string> apiTags = new Dictionary<string, string>();
                            if (apiOperation.Tags != null)
                            {
                                foreach (var tagTemp in apiOperation.Tags)
                                {
                                    apiTags.Add(tagTemp.Name, tagMap.GetValueOrDefault(tagTemp.Name, tagTemp.Name));
                                }
                            }
                            _actionControlTypeMap.TryGetValue(httpMethod + apiPath, out TypeInfo? controlTypeInfo);
                            ApiEndpoint api = new ApiEndpoint(apiOperation.OperationId, apiPath, httpMethod, doc.Key)
                            {
                                Summary = opt.Value.Summary,
                                Description = opt.Value.Description,
                                GroupTitle = doc.Value.Title,
                                GroupDescription = doc.Value.Description,
                                Tags = apiTags,
                                ControllerTypeInfo = controlTypeInfo
                            };
                            _endpoints.Add(api.Key, api);
                            _endpoints1.Add(api.Method + api.Path, api);
                        }
                    }
                }
                inited = true;
            });

            initTask = task;
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private (ApiHttpMethod, string) ParseHttpContext(HttpContext httpContext)
        {
            ApiHttpMethod method = Enum.Parse<ApiHttpMethod>(httpContext.Request.Method.ToUpper());
            var point = httpContext.GetEndpoint();
            if (point == null)
            {
                throw new InvalidOperationException("Endpoint is null");
            }
            string? path = ((Microsoft.AspNetCore.Routing.RouteEndpoint)point).RoutePattern.RawText;
            if (path == null)
            {
                throw new InvalidOperationException("RoutePattern.RawText is null");
            }
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }
            return (method, path);

        }
        /// <summary>
        /// 从特性中获取api Key
        /// </summary>
        /// <returns></returns>
        private string? GetApiEndpointKeySecurityDefineAttribute(HttpContext httpContext)
        {
            // 获取权限特性
            var securityDefineAttribute = httpContext.GetMetadata<Microsoft.AspNetCore.Authorization.SecurityDefineAttribute>();
            if (securityDefineAttribute != null) return securityDefineAttribute.ResourceId;
            return null;
        }

        /// <summary>
        /// 获取api信息
        /// </summary>
        /// <remarks>
        /// 根据组名和控制器类，获取api信息
        /// </remarks>
        /// <param name="groupName">分组</param>
        /// <param name="controlTypes">控制器类</param>
        /// <returns></returns>
        public async Task<IEnumerable<ApiEndpoint>> GetApis(string? groupName = null, Type[]? controlTypes = null)
        {
            if (!inited && initTask != null)
            {
                await initTask;
            }
            IEnumerable<ApiEndpoint> list = _endpoints.Select(x => x.Value);
            if (groupName != null)
            {
                list = list.Where(x => x.Group.Equals(groupName));
            }
            if (controlTypes != null && controlTypes.Length > 0)
            {
                list = list.Where(x => x.ControllerTypeInfo != null && controlTypes.Contains(x.ControllerTypeInfo.AsType()));
            }

            return list;
        }
    }
}
