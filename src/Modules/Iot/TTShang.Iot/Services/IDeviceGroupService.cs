// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Services
{
    /// <summary>
    /// 设备组服务
    /// </summary>
    public interface IDeviceGroupService : IServiceBase<DeviceGroupDto, int>
    {
        /// <summary>
        /// 查询所有设备组 按树形结构返回
        /// </summary>
        /// <param name="includeLocked"></param>
        /// <returns></returns>
        Task<List<DeviceGroupDto>> GetTree(bool includeLocked = false);
    }
}
