// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Common;
using TTShang.Weighbridge.Dtos;

namespace TTShang.Weighbridge.Services
{
    /// <summary>
    ///  货物服务
    /// </summary>
    public interface ICommodityService : IServiceBase<CommodityDto, Int32>
    {
        /// <summary>
        /// 根据货码查询货物信息
        /// </summary>
        /// <remarks>
        /// 根据货码查询货物信息
        /// </remarks>
        /// <param name="commodityCode"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<CommodityDto?> FindByCode(string commodityCode, Guid? tenantId=null);
        /// <summary>
        /// 根据货名查询货物信息
        /// </summary>
        /// <param name="commodityName"></param>
        /// <param name="tenantId"></param>
        /// <remarks>
        /// 根据货名查询货物信息
        /// </remarks>
        /// <returns></returns>
        Task<CommodityDto?> FindByName(string commodityName, Guid? tenantId = null);
    }
}