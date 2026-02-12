// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Authorization.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TTShang.Core.Api.Impl.Authorization.Internal
{
    /// <summary>
    /// 身份服务
    /// 每次请求都是新的对象
    /// </summary>
    internal class IdentityService : IIdentityService
    {
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 身份转换器
        /// </summary>
        private readonly IIdentityConverter identityConverter;
        /// <summary>
        /// 当前请求的身份信息
        /// </summary>
        private Identity? _identity;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="identityConverter"></param>
        public IdentityService(IHttpContextAccessor httpContextAccessor, IIdentityConverter identityConverter)
        {
            _httpContextAccessor = httpContextAccessor;
            this.identityConverter = identityConverter;
        }
        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        public Identity? GetIdentity()
        {
            if (_identity != null)
            {
                return _identity;
            }
            _identity = GetIdentityFromContext();
            return _identity;
        }


        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        private Identity? GetIdentityFromContext()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                //非http请求
                return null;
            }
            if (httpContext.User.Identity == null || !httpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }
            //违法使用
            string? tokenTypeKey = httpContext.User.FindFirstValue(AuthKeyConstants.TokenTypeKey);
            if (string.IsNullOrEmpty(tokenTypeKey) || JwtTokenType.RefreshToken.ToString().Equals(tokenTypeKey))
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Refreshtoken_Cannot_Used_In_Authentication);
            }
            return identityConverter.ClaimsPrincipalToIdentity(httpContext.User);
        }
    }
}
