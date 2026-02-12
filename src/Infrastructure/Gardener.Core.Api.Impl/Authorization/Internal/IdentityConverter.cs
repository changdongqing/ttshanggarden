// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Authorization.Services;
using System.Security.Claims;

namespace Gardener.Core.Api.Impl.Authorization.Internal
{
    /// <summary>
    /// 身份转换器
    /// </summary>
    internal class IdentityConverter : IIdentityConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public Identity? ClaimsPrincipalToIdentity(ClaimsPrincipal principal)
        {

            string? id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            string? loginId = principal.FindFirstValue(AuthKeyConstants.ClientIdKeyName);
            //无法解析
            if (id == null || loginId == null)
            {
                return null;
            }
            string? identityType = principal.FindFirstValue(AuthKeyConstants.IdentityType);
            string? loginClientType = principal.FindFirstValue(AuthKeyConstants.ClientTypeKeyName);
            LoginClientType loginClientType1 = loginClientType == null ? LoginClientType.Unknown : Enum.Parse<LoginClientType>(loginClientType, true);
            IdentityType identityType1 = identityType == null ? IdentityType.Unknown : Enum.Parse<IdentityType>(identityType, true);

            Identity identity = new Identity(id, identityType1, loginClientType1, loginId);
            identity.Name = principal.FindFirstValue(ClaimTypes.Name);
            identity.NickName = principal.FindFirstValue(ClaimTypes.GivenName);
            identity.CustomData = principal.FindFirstValue(AuthKeyConstants.CustomData);
            identity.ClientName = principal.FindFirstValue(AuthKeyConstants.ClientName);
            identity.ClientVersion = principal.FindFirstValue(AuthKeyConstants.ClientVersion);
            string? tenantId = principal.FindFirstValue(AuthKeyConstants.TenantId);
            identity.TenantId = string.IsNullOrEmpty(tenantId) ? null : Guid.Parse(tenantId);
            return identity;
        }
        /// <summary>
        /// identity生成ClaimsIdentity
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public ClaimsIdentity IdentityToClaimsIdentity(Identity identity, JwtTokenType tokenType)
        {
            IEnumerable<Claim> claims =
                [
                new Claim(ClaimTypes.NameIdentifier, identity.Id),
                new Claim(AuthKeyConstants.IdentityType, identity.IdentityType.ToString()),
                new Claim(AuthKeyConstants.ClientIdKeyName, identity.LoginId),
                new Claim(AuthKeyConstants.ClientTypeKeyName, identity.LoginClientType.ToString()),
                new Claim(AuthKeyConstants.TokenTypeKey, tokenType.ToString())
            ];
            if (!string.IsNullOrEmpty(identity.Name))
            {
                claims = claims.Append(new Claim(ClaimTypes.Name, identity.Name));
            }
            if (!string.IsNullOrEmpty(identity.NickName))
            {
                claims = claims.Append(new Claim(ClaimTypes.GivenName, identity.NickName));
            }
            if (!string.IsNullOrEmpty(identity.ClientName))
            {
                claims = claims.Append(new Claim(AuthKeyConstants.ClientName, identity.ClientName));
            }
            if (!string.IsNullOrEmpty(identity.ClientVersion))
            {
                claims = claims.Append(new Claim(AuthKeyConstants.ClientVersion, identity.ClientVersion));
            }
            if (!string.IsNullOrEmpty(identity.CustomData))
            {
                claims = claims.Append(new Claim(AuthKeyConstants.CustomData, identity.CustomData));
            }
            if (identity.TenantId.HasValue)
            {
                claims = claims.Append(new Claim(AuthKeyConstants.TenantId, identity.TenantId.Value.ToString()));
            }
            return new ClaimsIdentity(claims);
        }
    }
}
