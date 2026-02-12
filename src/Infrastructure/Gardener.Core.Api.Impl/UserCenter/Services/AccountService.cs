// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Gardener.Core.Authorization.Services;
using Gardener.Core.UserCenter.Services;
using Gardener.Core.VerifyCode;
using Gardener.Core.Authorization.Dtos;
using Gardener.Core.Api.Impl.UserCenter.Internal;
using Gardener.Core.Api.Impl.SystemAsset.Entities;
using Gardener.Core.Api.Impl.UserCenter.Entities;
using Gardener.Core.Dtos.Constraints;
using Humanizer.Localisation;

namespace Gardener.Core.Api.Impl.UserCenter.Services
{
    /// <summary>
    /// 用户账户认证授权服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _userRepository;
        private readonly IAuthService _authorizationManager;
        private readonly IJwtService _jwtBearerService;
        private readonly IIdentityPermissionService _identityPermissionService;
        private readonly ILoginLogService _loginLogService;
        private readonly IRepository<SystemTenantResource> _tenantResourceRepository;
        /// <summary>
        /// 资源仓储
        /// </summary>
        private readonly IRepository<Resource> _resourceRepository;
        /// <summary>
        /// 角色管理服务
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        /// <param name="authorizationManager"></param>
        /// <param name="jwtBearerService"></param>
        /// <param name="resourceRepository"></param>
        /// <param name="identityPermissionService"></param>
        /// <param name="loginLogService"></param>
        /// <param name="tenantResourceRepository"></param>
        public AccountService(
            IHttpContextAccessor httpContextAccessor,
            IRepository<User> userRepository,
            IAuthService authorizationManager,
            IJwtService jwtBearerService,
            IRepository<Resource> resourceRepository,
            IIdentityPermissionService identityPermissionService,
            ILoginLogService loginLogService,
            IRepository<SystemTenantResource> tenantResourceRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _authorizationManager = authorizationManager;
            _jwtBearerService = jwtBearerService;
            _resourceRepository = resourceRepository;
            _identityPermissionService = identityPermissionService;
            _loginLogService = loginLogService;
            _tenantResourceRepository = tenantResourceRepository;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>登录接口</remarks>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.AllowAnonymous, IgnoreAudit]
        [VerifyCodeAutoVerification]
        public async Task<TokenOutput> Login(LoginInput input)
        {
            string loginId = Guid.NewGuid().ToString();
            // 验证用户是否存在
            var user = _userRepository.FirstOrDefault(x => x.UserName.Equals(input.UserName) && x.IsDeleted == false, false);
            string? ip = _httpContextAccessor.HttpContext?.GetRemoteIpAddressToIPv4(true);
            if (user == null)
            {
                await _loginLogService.Insert(new LoginLogDto()
                {
                    LoginId = loginId,
                    LoginClientType = input.LoginClientType,
                    LoginTime = DateTimeOffset.Now,
                    LoginStatus = LoginStatus.Failed,
                    LoginFailReason = LoginFailReason.UserNotExist,
                    IdentityName = input.UserName,
                    IdentityType = IdentityType.User,
                    Ip = ip,
                    ClientName = input.ClientName,
                    ClientVersion = input.ClientVersion
                });
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.User_Name_Or_Password_Error);
            }
            if (user.IsLocked)
            {
                await _loginLogService.Insert(new LoginLogDto()
                {
                    LoginId = loginId,
                    LoginClientType = input.LoginClientType,
                    LoginTime = DateTimeOffset.Now,
                    LoginStatus = LoginStatus.Failed,
                    IdentityId = user.Id.ToString(),
                    LoginFailReason = LoginFailReason.UserLocked,
                    IdentityName = input.UserName,
                    IdentityType = IdentityType.User,
                    Ip = ip,
                    TenantId = user.TenantId,
                    ClientName = input.ClientName,
                    ClientVersion = input.ClientVersion
                });
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.User_Locked);
            }
            //密码是否正确
            var encryptedPassword = PasswordEncryptHelper.Encrypt(input.Password, user.PasswordEncryptKey);
            if (!encryptedPassword.Equals(user.Password))
            {
                await _loginLogService.Insert(new LoginLogDto()
                {
                    LoginId = loginId,
                    LoginClientType = input.LoginClientType,
                    LoginTime = DateTimeOffset.Now,
                    LoginStatus = LoginStatus.Failed,
                    IdentityId = user.Id.ToString(),
                    LoginFailReason = LoginFailReason.WrongPassword,
                    IdentityName = input.UserName,
                    IdentityType = IdentityType.User,
                    Ip = ip,
                    TenantId = user.TenantId,
                    ClientName = input.ClientName,
                    ClientVersion = input.ClientVersion
                });
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.User_Name_Or_Password_Error);
            }
            Identity identity = new Identity(user.Id.ToString(), IdentityType.User, input.LoginClientType, loginId)
            {
                Name = user.UserName,
                NickName = user.NickName,
                TenantId = user.TenantId,
                ClientName = input.ClientName,
                ClientVersion = input.ClientVersion
            };

            var token = await _jwtBearerService.CreateToken(identity);
            await _loginLogService.Insert(new LoginLogDto()
            {
                LoginId = loginId,
                LoginClientType = input.LoginClientType,
                LoginTime = DateTimeOffset.Now,
                LoginStatus = LoginStatus.Succeed,
                IdentityId = user.Id.ToString(),
                IdentityName = input.UserName,
                IdentityType = IdentityType.User,
                Ip = ip,
                TenantId = user.TenantId,
                ClientName = input.ClientName,
                ClientVersion = input.ClientVersion
            });
            return token.Adapt<TokenOutput>();
        }
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <remarks>
        /// 通过刷新token获取新的token
        /// </remarks>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.AllowAnonymous, IgnoreAudit]
        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            var token = await _jwtBearerService.RefreshToken(input.RefreshToken);
            return token.Adapt<TokenOutput>();
        }
        /// <summary>
        /// 移除当前用户token
        /// </summary>
        /// <remarks>
        /// 移除当前用户token
        /// </remarks>
        /// <returns></returns>
        public async Task<bool> RemoveCurrentUserRefreshToken()
        {
            var identity = _authorizationManager.GetIdentity();
            if (identity == null) return false;
            return await _jwtBearerService.RemoveRefreshToken(identity);
        }
        /// <summary>
        /// 查看用户角色
        /// </summary>
        /// <remarks>
        /// 查看当前用户角色
        /// </remarks>
        /// <returns></returns>
        public async Task<List<RoleDto>> GetCurrentUserRoles()
        {
            var userId = _authorizationManager.GetIdentityId();
            var roles = await _userRepository.AsQueryable(false).Where(x => x.Id.Equals(userId)).SelectMany(x => x.Roles.Where(r => r.IsDeleted == false && r.IsLocked == false)).ToListAsync();

            return roles.Adapt<List<RoleDto>>();
        }
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <remarks>
        /// 获取当前用户信息
        /// </remarks>
        /// <returns></returns>
        public async Task<UserDto> GetCurrentUser()
        {
            var userId = _authorizationManager.GetIdentityId();
            var userDto = await _userRepository.AsQueryable(false)
                .Include(x => x.Dept)
                .Include(x => x.Position)
                .Include(x => x.UserExtension)
                .Where(x => x.Id.Equals(userId))
                .Select(x => x.Adapt<UserDto>())
                .FirstOrDefaultAsync();
            if (userDto == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Not_Find);
            }
            // set roles for resource auth
            var roles = await GetCurrentUserRoles();
            userDto.Roles = roles;
            userDto.IsSuperAdministrator = await _authorizationManager.IsSuperAdministrator();
            userDto.Password = null;
            return userDto;
        }
        /// <summary>
        /// 获取用户资源
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetCurrentUserResources([FromQuery] params ResourceType[] resourceTypes)
        {
            resourceTypes = resourceTypes ?? new ResourceType[] { };
            List<ResourceDto> resources = await GetUserResources(resourceTypes);
            return resources;
        }
        /// <summary>
        /// 获取用户资源的key
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        public async Task<List<string>> GetCurrentUserResourceKeys([FromQuery] params ResourceType[] resourceTypes)
        {
            resourceTypes = resourceTypes ?? new ResourceType[] { };
            List<string> resourceKeys = await GetUserResourceKeys(resourceTypes);
            return resourceKeys;

        }

        /// <summary>
        /// 获取当前用户的所有菜单
        /// </summary>
        /// <remarks>
        /// 获取当前用户的所有菜单
        /// </remarks>
        /// <param name="rootKey"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetCurrentUserMenus([FromQuery] string? rootKey = null)
        {
            // 获取用户Id
            List<ResourceDto> resources = await GetCurrentUserResources(ResourceType.Root, ResourceType.Menu);

            var result = resources.Where(x => x.Type.Equals(ResourceType.Root) && (string.IsNullOrEmpty(rootKey) || x.Key.Equals(rootKey))).FirstOrDefault()?.Children?.ToList();

            return result ?? new List<ResourceDto>();
        }

        /// <summary>
        /// 测试token是否可用
        /// </summary>
        /// <param name="flag">标记</param>
        /// <returns></returns>
        /// <remarks>
        /// 不执行任何内容，token无效将响应401
        /// </remarks>
        public Task<bool> TestToken([FromQuery] string? flag = null)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// 更新当前用户基本信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// 更新 <see cref="UserDto.NickName"/>、<see cref="UserDto.Gender"/>、<see cref="UserDto.Avatar"/>
        /// </remarks>
        public async Task<bool> UpdateCurrentUserBaseInfo(UserDto user)
        {
            //更新
            Identity? identity = _authorizationManager.GetIdentity();
            if (identity == null || !identity.IdentityType.Equals(IdentityType.User)) return false;
            var userEntity = await _userRepository.FindOrDefaultAsync(int.Parse(identity.Id));
            if (userEntity == null) return false;
            userEntity.Id = int.Parse(identity.Id);
            userEntity.Avatar = user.Avatar;
            userEntity.NickName = user.NickName;
            userEntity.Gender = user.Gender;
            await _userRepository.UpdateIncludeAsync(userEntity, new string[] { nameof(UserDto.NickName), nameof(UserDto.Gender), nameof(UserDto.Avatar) });
            return true;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="changePasswordInput"></param>
        /// <returns></returns>
        /// <remarks>
        /// 修改当前用户密码
        /// </remarks>
        public async Task<bool> ChangePassword(ChangePasswordInput changePasswordInput)
        {
            if (!string.Equals(changePasswordInput.NewPassword, changePasswordInput.ConfirmNewPassword))
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Confirm_New_Password_Inconformity);
            }
            Identity? identity = _authorizationManager.GetIdentity();
            if (identity == null || !identity.IdentityType.Equals(IdentityType.User))
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Forbidden);
            }

            User? user = await _userRepository.SingleOrDefaultAsync(x => x.Id == int.Parse(identity.Id), false);
            if (user == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Not_Find);
            }
            string oldPasswordEncryptValue = PasswordEncryptHelper.Encrypt(changePasswordInput.OldPassword, user.PasswordEncryptKey);
            if (!oldPasswordEncryptValue.Equals(user.Password))
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Password_Error);
            }

            user.PasswordEncryptKey = Guid.NewGuid().ToString() + ":" + DateTimeOffset.Now.ToLocalTime().ToString("yyyyMMddHHmmssfff");
            user.Password = PasswordEncryptHelper.Encrypt(changePasswordInput.NewPassword, user.PasswordEncryptKey);
            await _userRepository.UpdateIncludeAsync(user, new string[] { nameof(User.Password), nameof(User.PasswordEncryptKey) });

            return true;
        }

        #region 私有

        /// <summary>
        /// 判断是否是超级管理员
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CurrentUserIsSuperAdmin()
        {
            List<RoleDto> roleDtos = await GetCurrentUserRoles();
            if (roleDtos.Any(x => x.IsSuperAdministrator))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取用户资源
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        private async Task<List<ResourceDto>> GetUserResources(params ResourceType[] resourceTypes)
        {
            var queryable = await GetUserResourcesQueryable(resourceTypes);
            if (queryable == null)
            {
                return [];
            }
            var resources = await queryable.Select(x => x.Adapt<ResourceDto>()).ToListAsync();

            foreach (var item in resources)
            {
                if (item.ParentId != null)
                {
                    var parent = resources.FirstOrDefault(x => x.Id.Equals(item.ParentId));
                    if (parent != null)
                    {
                        //item.Parent = parent;
                        parent.Children ??= [];
                        parent.Children.Add(item);
                    }
                }
            }

            return resources;
        }
        /// <summary>
        /// 获取用户资源
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        private async Task<IQueryable<Resource>?> GetUserResourcesQueryable(params ResourceType[] resourceTypes)
        {
            resourceTypes = resourceTypes ?? new ResourceType[] { };

            IQueryable<Resource>? resources = null;
            if (await CurrentUserIsSuperAdmin())
            {
                //超级管库有拥有所有资源
                resources = _resourceRepository.AsQueryable(false)
                    .Where(x => x.IsDeleted == false && x.IsLocked == false && resourceTypes.Contains(x.Type));

            }
            else
            {
                Identity? user = _authorizationManager.GetIdentity();
                if (user == null)
                {
                    return resources;
                }
                int userId = int.Parse(user.Id);
                if (user.GetType().IsAssignableTo(typeof(IModelTenantId)) && ((IModelTenantId)user).IsTenant)
                {
                    IQueryable<Guid> tenantResourceIds = _tenantResourceRepository.AsQueryable(false).Where(x => !x.Tenant.IsLocked && !x.Tenant.IsDeleted && x.IsLocked.Equals(false) && x.IsDeleted.Equals(false) && x.TenantId.Equals(user.TenantId)).Select(x => x.ResourceId);


                    // _userRepository
                    //.Include(u => u.Roles)
                    //    .ThenInclude(u => u.RoleResources)
                    //    .ThenInclude(u => u.Resource)
                    //.Where(u => u.Id.Equals(userId) && u.IsDeleted == false && u.IsLocked == false)
                    //.SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                    //    .SelectMany(u => u.RoleResources
                    //           .Select(u => u.Resource)
                    //               .Where(x => x.IsDeleted == false && x.IsLocked == false && resourceTypes.Contains(x.Type))
                    //    )).Select(x => x).OrderBy(x => x.Order).ToListAsync();

                    resources = _userRepository.AsQueryable(false)
                             .Where(x => x.Id.Equals(userId) && x.IsDeleted == false && x.IsLocked == false)
                             .SelectMany(x => x.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                                 .SelectMany(x => x.RoleResources
                                        .Where(x => x.IsDeleted == false && x.IsLocked == false)
                                            .Select(x => x.Resource)
                                                .Where(x => x.IsDeleted == false && x.IsLocked == false && resourceTypes.Contains(x.Type) && tenantResourceIds.Contains(x.Id))

                                 )
                             );
                }
                else
                {
                    resources = _userRepository.AsQueryable(false)
                         .Where(x => x.Id.Equals(userId) && x.IsDeleted == false && x.IsLocked == false)
                         .SelectMany(x => x.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                             .SelectMany(x => x.RoleResources
                                    .Select(x => x.Resource)
                                        .Where(x => x.IsDeleted == false && x.IsLocked == false && resourceTypes.Contains(x.Type))
                             ));
                }
            }
            if (resources != null)
            {
                resources = resources.OrderBy(x => x.Order);
            }
            return resources;
        }
        /// <summary>
        /// 获取用户资源所有Key
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        private async Task<List<string>> GetUserResourceKeys(params ResourceType[] resourceTypes)
        {
            var queryable = await GetUserResourcesQueryable(resourceTypes);
            if (queryable == null)
            {
                return [];
            }
            return await queryable.Select(x => x.Key).ToListAsync();
        }

        #endregion
    }
}