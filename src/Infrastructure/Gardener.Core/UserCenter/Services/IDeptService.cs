// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.UserCenter.Services
{
    /// <summary>
    /// 部门服务
    /// </summary>
    public interface IDeptService : IServiceBase<DeptDto, int>
    {
        /// <summary>
        /// 查询所有部门 按树形结构返回
        /// </summary>
        /// <param name="includeLocked"></param>
        /// <returns></returns>
        Task<List<DeptDto>> GetTree(bool includeLocked = false);

        /// <summary>
        /// 获取资源的种子数据
        /// </summary>
        /// <returns></returns>
        Task<string> GetSeedData();
    }
}
