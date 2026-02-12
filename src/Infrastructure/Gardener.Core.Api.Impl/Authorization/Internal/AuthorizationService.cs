// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Authorization.Dtos;
using Gardener.Core.Authorization.Services;
using Gardener.Core.Swagger.Services;
using Microsoft.AspNetCore.Http;

namespace Gardener.Core.Api.Impl.Authorization.Internal
{
    /// <summary>
    /// 当前请求的权限管理 
    /// </summary>
    internal class AuthorizationService : IAuthService
    {
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// api信息
        /// </summary>
        private readonly IApiEndpointService _apiEndpointService;
        /// <summary>
        /// 身份权限服务
        /// </summary>
        private readonly IIdentityPermissionService _identityPermissionService;
        /// <summary>
        /// 
        /// </summary>
        private readonly IIdentityService _identityService;
        /// <summary>
        /// 
        /// </summary>
        private readonly ILoginTokenStorageService _loginTokenStorageService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="apiEndpointStoreService"></param>
        /// <param name="identityPermissionService"></param>
        /// <param name="identityService"></param>
        /// <param name="loginTokenStorageService"></param>
        public AuthorizationService(IHttpContextAccessor httpContextAccessor,
            IApiEndpointService apiEndpointStoreService,
            IIdentityPermissionService identityPermissionService,
            IIdentityService identityService,
            ILoginTokenStorageService loginTokenStorageService)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiEndpointService = apiEndpointStoreService;
            _identityPermissionService = identityPermissionService;
            _identityService = identityService;
            _loginTokenStorageService = loginTokenStorageService;
        }


        /// <summary>
        /// 获取当前请求的功能
        /// </summary>
        /// <returns></returns>
        public async Task<ApiEndpoint?> GetApiEndpoint()
        {
            return await GetApiEndpointFromContext();
        }

        /// <summary>
        /// 检测身份可用性
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckIdentityUsability()
        {
            Identity? identity = _identityService.GetIdentity();
            if (identity == null)
            {
                return false;
            }
            //LoginId 已不可用
            if (!await _loginTokenStorageService.Verify(identity))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ChecktContenxtApiEndpoint()
        {
            Identity? identity = _identityService.GetIdentity();
            if (identity == null)
            {
                return false;
            }
            ApiEndpoint? api = await GetApiEndpointFromContext();
            return await _identityPermissionService.Check(identity, api);
        }


        /// <summary>
        /// 获取当前请求的功能
        /// </summary>
        /// <returns></returns>
        public async Task<ApiEndpoint?> GetApiEndpointFromContext()
        {
            ApiEndpoint? apiEndpoint = null;
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                apiEndpoint = await _apiEndpointService.GetApi(context);
            }
            return apiEndpoint;
        }

        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        public Identity? GetIdentity()
        {
            return _identityService.GetIdentity();
        }

        /// <summary>
        /// 获取身份的编号
        /// </summary>
        /// <returns></returns>
        public object? GetIdentityId()
        {
            var identity = GetIdentity();
            if (identity == null)
            {
                return null;
            }
            return _identityPermissionService.GetIdentityId(identity);
        }

        /// <summary>
        /// 判断是否是超级管理员
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> IsSuperAdministrator()
        {
            return _identityPermissionService.IsSuperAdministrator(GetIdentity());
        }

        /// <summary>
        /// 判断当前登录对象是否有该资源
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> CheckCurrentIdentityHaveResource(string resourceKey)
        {
            return _identityPermissionService.Check(GetIdentity(), resourceKey);
        }
    }
}