using Furion.DynamicApiController;
using Gardener.Core.Common;
using Gardener.Weighbridge.Impl.Entities;
using Gardener.Weighbridge.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gardener.Weighbridge.Impl.Services
{
    /// <summary>
    /// 货物服务
    /// </summary>
    [ApiDescriptionSettings("Weighbridge", Module = "weighbridge")]
    public class CommodityService : ServiceBase<Commodity, CommodityDto, Int32, GardenerMultiTenantDbContextLocator>, ICommodityService
    {

        /// <summary>
        /// 货物服务
        /// </summary>
        /// <param name="repository"></param>
        public CommodityService(IRepository<Commodity, GardenerMultiTenantDbContextLocator> repository) : base(repository)
        {
        }

        /// <summary>
        /// 根据货码查询货物信息
        /// </summary>
        /// <remarks>
        /// 根据货码查询货物信息
        /// </remarks>
        /// <param name="commodityCode"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<CommodityDto?> FindByCode([FromQuery] string commodityCode, [FromQuery] Guid? tenantId = null)
        {
            return _repository.AsQueryable(false)
                .Where(x => x.CommodityCode.Equals(commodityCode))
                .Where(tenantId.HasValue, x => x.TenantId.Equals(tenantId))
                .Select(x => x.Adapt<CommodityDto>())
                .FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据货名查询货物信息
        /// </summary>
        /// <param name="commodityName"></param>
        /// <param name="tenantId"></param>
        /// <remarks>
        /// 根据货名查询货物信息
        /// </remarks>
        /// <returns></returns>
        public Task<CommodityDto?> FindByName([FromQuery] string commodityName, [FromQuery] Guid? tenantId = null)
        {
            return _repository.AsQueryable(false)
                .Where(x => x.CommodityName.Equals(commodityName))
                .Where(tenantId.HasValue, x => x.TenantId.Equals(tenantId))
                .Select(x => x.Adapt<CommodityDto>())
                .FirstOrDefaultAsync();
        }
    }
}
