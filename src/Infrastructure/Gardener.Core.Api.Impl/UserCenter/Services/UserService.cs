// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Linq.Expressions;
using Gardener.Core.Authorization.Services;
using Gardener.Core.UserCenter.Services;
using Gardener.Core.Api.Impl.UserCenter.Internal;
using Gardener.Core.Api.Impl.UserCenter.Entities;
using Gardener.Core.Dtos.Constraints;
using Gardener.Core.NotificationSystem.Services;

namespace Gardener.Core.Api.Impl.UserCenter.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class UserService : ServiceBase<User, UserDto, int, GardenerMultiTenantDbContextLocator>, IUserService
    {
        private readonly IRepository<User, GardenerMultiTenantDbContextLocator> _userRepository;
        private readonly IRepository<User, MasterDbContextLocator> _userNoTenantRepository;
        private readonly IRepository<Role, GardenerMultiTenantDbContextLocator> _roleRepository;
        private readonly IRepository<UserRole, GardenerMultiTenantDbContextLocator> _userRoleRepository;
        private readonly IRepository<UserExtension, GardenerMultiTenantDbContextLocator> _userExtensionRepository;
        private readonly IRepository<Dept, GardenerMultiTenantDbContextLocator> _deptRepository;
        private readonly IIdentityService _identityService;
        private readonly ITenantConfigService tenantConfigService;
        private readonly IUserConnectQueryService _userConnectQueryService;
        /// <summary>
        /// 用户服务
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="userExtensionRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="deptRepository"></param>
        /// <param name="identityService"></param>
        /// <param name="userNoTenantRepository"></param>
        /// <param name="tenantConfigService"></param>
        /// <param name="userConnectQueryService"></param>
        public UserService(
            IRepository<User, GardenerMultiTenantDbContextLocator> userRepository,
            IRepository<UserExtension, GardenerMultiTenantDbContextLocator> userExtensionRepository,
            IRepository<UserRole, GardenerMultiTenantDbContextLocator> userRoleRepository,
            IRepository<Role, GardenerMultiTenantDbContextLocator> roleRepository,
            IRepository<Dept, GardenerMultiTenantDbContextLocator> deptRepository,
            IIdentityService identityService,
            IRepository<User, MasterDbContextLocator> userNoTenantRepository,
            ITenantConfigService tenantConfigService,
            IUserConnectQueryService userConnectQueryService) : base(userRepository)
        {
            _userRepository = userRepository;
            _userExtensionRepository = userExtensionRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _deptRepository = deptRepository;
            _identityService = identityService;
            _userNoTenantRepository = userNoTenantRepository;
            this.tenantConfigService = tenantConfigService;
            _userConnectQueryService = userConnectQueryService;
        }

        /// <summary>
        /// 查看用户角色
        /// </summary>
        /// <remarks>
        /// 查看用户角色
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<RoleDto>> GetRoles([ApiSeat(ApiSeats.ActionStart)] int userId)
        {
            var roles = await _userRepository
                .DetachedEntities
                .Include(u => u.Roles.Where(r => r.IsDeleted == false))
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles)
                .ToListAsync();

            return roles.Adapt<List<RoleDto>>();
        }

        /// <summary>
        /// 查看用户权限
        /// </summary>
        /// <remarks>
        /// 查看用户权限
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetResources([ApiSeat(ApiSeats.ActionStart)] int userId)
        {
            return await _userRepository
               .Include(u => u.Roles, false)
                   .ThenInclude(u => u.RoleResources)
                   .ThenInclude(u => u.Resource)
               .Where(u => u.Id == userId)
               .Where(u => u.IsDeleted == false)
               .SelectMany(u => u.Roles
                   .SelectMany(u => u.RoleResources))
                    .Select(u => u.Resource)
               .ProjectToType<ResourceDto>()
               .ToListAsync();
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索用户数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<PageList<UserDto>> Search(PageRequest request)
        {
            Expression<Func<User, bool>> expression = FilterHelper.GetExpression<User>(request.FilterGroups);
            var users = _userRepository
              .Include(u => u.UserExtension)
              .Include(u => u.Dept)
              .Include(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false))
              .Where(u => u.IsDeleted == false)
              .Where(expression)
              .OrderConditions(request.OrderConditions)
              .Select(x => x.Adapt<UserDto>());
            var pageList = await users.ToPageAsync(request.PageIndex, request.PageSize);
            foreach (var item in pageList.Items)
            {
                item.Password = null;
                item.Online = await _userConnectQueryService.CheckUserIsOnline(IdentityType.User, item.Id.ToString());
            }
            return pageList;

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <remarks>
        /// 更新用户
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(UserDto input)
        {
            var userExists = await _userNoTenantRepository.AnyAsync(x => x.UserName.Equals(input.UserName) && x.Id != input.Id && x.IsDeleted == false, false);
            if (userExists)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.User_Name_Repeat);
            }
            //不操作角色关系
            input.Roles = null;
            var user = input.Adapt<User>();
            user.UpdatedTime = DateTimeOffset.Now;

            List<string> exclude = new List<string>()
            {
                nameof(User.CreatedTime)
            };
            //传入了密码就进行修改
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.PasswordEncryptKey = Guid.NewGuid().ToString().Replace("-", "");
                user.Password = PasswordEncryptHelper.Encrypt(user.Password, user.PasswordEncryptKey);
            }
            else
            {
                //不修改密码时要排除掉
                exclude.Add(nameof(User.Password));
                exclude.Add(nameof(User.PasswordEncryptKey));
            }

            //更新
            await _userRepository.UpdateExcludeAsync(user, exclude);

            var userExt = input.UserExtension?.Adapt<UserExtension>();
            if (userExt != null)
            {
                if (await _userExtensionRepository.AnyAsync(x => x.UserId == userExt.UserId, false))
                {
                    userExt.UpdatedTime = DateTimeOffset.Now;
                    userExt.TenantId = user.TenantId;
                    await _userExtensionRepository.UpdateExcludeAsync(userExt, new[] { nameof(UserExtension.CreatedTime) });
                }
                else
                {
                    userExt.CreatedTime = DateTimeOffset.Now;
                    userExt.TenantId = user.TenantId;
                    await _userExtensionRepository.InsertAsync(userExt);
                }
            }

            return true;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <remarks>
        /// 新增用户
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<UserDto> Insert(UserDto input)
        {
            if (_userNoTenantRepository.Any(x => x.UserName.Equals(input.UserName) && x.IsDeleted == false, false))
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.User_Name_Repeat);
            }
            var identity = _identityService.GetIdentity();
            if (identity != null && identity.TenantId.HasValue && identity is IModelTenantId tenantId && tenantId.IsTenant)
            {
                SystemTenantConfigDto? systemTenantConfig = await tenantConfigService.GetTenantConfigByConfigKey(identity.TenantId.Value, "TenantMaxUserNumber");
                if (systemTenantConfig != null && !string.IsNullOrEmpty(systemTenantConfig.ConfigValue))
                {
                    int count = _userRepository.Count(false);
                    if (count >= int.Parse(systemTenantConfig.ConfigValue))
                    {
                        throw Oops.BahLocalFrom<UserCenterResource>(ExceptionCode.Maximum_Number_Of_Users_Has_Been_Reached);
                    }
                }
            }
            //未传入密码时，自动生成密码
            if (string.IsNullOrEmpty(input.Password))
            {
                input.Password = PasswordGenerate.Create(10);
            }
            User user = input.Adapt<User>();
            user.PasswordEncryptKey = Guid.NewGuid().ToString().Replace("-", "");
            user.Password = PasswordEncryptHelper.Encrypt(input.Password, user.PasswordEncryptKey);
            user.CreatedTime = DateTimeOffset.Now;
            if (user.UserExtension == null)
            {
                user.UserExtension = new UserExtension();
            }
            user.UserExtension.CreatedTime = DateTimeOffset.Now;
            user.UserExtension.TenantId = user.TenantId;
            //查看是否有默认角色
            var defaultRoleIds = await _roleRepository.Where(x => x.IsDeleted == false && x.IsLocked == false && x.IsDefault == true).Select(x => x.Id).ToListAsync();
            if (defaultRoleIds != null && defaultRoleIds.Any())
            {
                user.UserRoles = defaultRoleIds.Select(x => new UserRole { RoleId = x, CreatedTime = DateTimeOffset.Now }).ToList();
            }
            var newEntity = await _userRepository.InsertAsync(user);

            return newEntity.Entity.Adapt<UserDto>();
        }

        /// <summary>
        /// 根据主键获取用户
        /// </summary>
        /// <remarks>
        /// 根据主键获取用户
        /// </remarks>
        /// <param name="id"></param>
        public override async Task<UserDto> Get(int id)
        {
            var person = await _userRepository
                .Include(x => x.UserExtension, false)
                .Include(x => x.Roles.Where(r => r.IsDeleted == false))
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (person == null)
            {
                throw Oops.BahLocalFrom<SharedLocalResource>(ExceptionCode.Data_Not_Find);
            }
            return person.Adapt<UserDto>();
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <remarks>
        /// 给用户设置角色
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Role([ApiSeat(ApiSeats.ActionStart)] int userId, [FromBody] int[] roleIds)
        {
            var user = await _userRepository.FindOrDefaultAsync(userId);
            if (user == null)
            {
                return false;
            }
            //先删除现有的
            var userRoles = await _userRoleRepository.AsQueryable(x => x.UserId == userId).ToListAsync();
            if (userRoles.Count > 0)
            {
                await _userRoleRepository.DeleteAsync(userRoles);
            }
            if (roleIds?.Length > 0)
            {
                var dbRoleIds = await _roleRepository.AsQueryable(false).Where(x => roleIds.Contains(x.Id)).Select(x => x.Id).ToListAsync();
                //添加新的
                var newUserRoles = dbRoleIds.Select(x => new UserRole()
                {
                    UserId = userId,
                    RoleId = x,
                    TenantId = user.TenantId
                });
                await _userRoleRepository.InsertAsync(newUserRoles);
            }
            return true;
        }

        /// <summary>
        /// 更新头像
        /// </summary>
        /// <remarks>
        /// 更新用户的头像
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAvatar(UpdateUserAvatarInput input)
        {
            User user = await _userRepository.FindAsync(input.Id);
            if (user == null)
            {
                return false;
            }
            user.Avatar = input.Avatar;
            user.UpdatedTime = DateTimeOffset.Now;
            await _userRepository.UpdateIncludeAsync(user, new[] {
                nameof(User.Avatar),
                nameof(User.UpdatedTime) }, true);
            return true;

        }
        /// <summary>
        /// 获取当前用户编号
        /// </summary>
        /// <returns></returns>
        public Task<string> GetCurrentUserId()
        {
            var id = _identityService.GetIdentity()?.Id;
            if (id == null)
            {
                throw new ArgumentNullException("CurrentUserId");
            }
            return Task.FromResult(id);
        }
        /// <summary>
        /// 根据用户编号集合获取多个用户
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public Task<List<UserDto>> GetUsers(IEnumerable<int> userIds)
        {
            return _userRepository
                .AsQueryable(false).Include(x => x.UserExtension)
               .Where(x => userIds.Contains(x.Id))
               .Select(x => x.Adapt<UserDto>())
               .ToListAsync();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除单条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Task<bool> Delete(int id)
        {
            return base.Delete(id);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override Task<bool> Deletes([FromBody] int[] ids)
        {
            return base.Deletes(ids);
        }
    }
}
