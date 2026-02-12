// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Iot.Impl.Services
{
    /// <summary>
    /// 设备组服务
    /// </summary>
    [ApiDescriptionSettings("Iot", Module = "iot")]
    public class DeviceGroupService : ServiceBase<DeviceGroup, DeviceGroupDto, int, GardenerMultiTenantDbContextLocator>, IDeviceGroupService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public DeviceGroupService(IRepository<DeviceGroup, GardenerMultiTenantDbContextLocator> repository) : base(repository)
        {
        }
        /// <summary>
        /// 查询所有设备组 按树形结构返回
        /// </summary>
        /// <param name="includeLocked"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<DeviceGroupDto>> GetTree(bool includeLocked = false)
        {
            var list = await _repository
                .Where(x => x.IsDeleted == false && (x.IsLocked == false || includeLocked))
                .OrderBy(x => x.Order)
                .ToListAsync();
            return list.Where(x => !x.ParentId.HasValue).Select(x => x.Adapt<DeviceGroupDto>()).ToList();
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
            await base.TreeDataDelete(id, x => x.ParentId, x => x.Id);
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
