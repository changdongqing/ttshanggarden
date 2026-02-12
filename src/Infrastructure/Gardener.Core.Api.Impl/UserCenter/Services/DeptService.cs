// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.UserCenter.Entities;
using Gardener.Core.UserCenter.Services;

namespace Gardener.Core.Api.Impl.UserCenter.Services
{
    /// <summary>
    /// 部门服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class DeptService : ServiceBase<Dept, DeptDto, int, GardenerMultiTenantDbContextLocator>, IDeptService
    {
        /// <summary>
        /// 部门服务
        /// </summary>
        /// <param name="repository"></param>
        public DeptService(IRepository<Dept, GardenerMultiTenantDbContextLocator> repository) : base(repository)
        {
        }
        /// <summary>
        /// 获取种子数据
        /// </summary>
        /// <returns></returns>
        public Task<string> GetSeedData()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取所有部门数据，以树形结构返回
        /// </summary>
        /// <param name="includeLocked"></param>
        /// <returns></returns>
        public async Task<List<DeptDto>> GetTree(bool includeLocked = false)
        {
            var list = await _repository
                .Where(x => x.IsDeleted == false && (x.IsLocked == false || includeLocked))
                .OrderBy(x => x.Order)
                .ToListAsync();
            return list.Where(x => !x.ParentId.HasValue).Select(x => x.Adapt<DeptDto>()).ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除单条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> Delete(int id)
        {
            await base.TreeDataDelete(id, x => x.ParentId, x=>x.Id);
            return true;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override async Task<bool> Deletes([FromBody] int[] ids)
        {
            foreach (var id in ids)
            {
                await Delete(id);
            }
            return true;
        }
    }
}
