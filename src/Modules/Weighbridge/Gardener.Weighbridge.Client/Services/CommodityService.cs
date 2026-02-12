// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


namespace Gardener.Weighbridge.Client.Services
{
    /// <summary>
    ///  货物服务
    /// </summary>
    [ScopedService]
    public class CommodityService : ClientServiceBase<CommodityDto, Int32>, ICommodityService
    {
        /// <summary>
        ///  货物服务
        /// </summary>
        public CommodityService(IApiCaller apiCaller) : base(apiCaller, "commodity", "weighbridge")
        {
        }

        public Task<CommodityDto?> FindByCode(string commodityCode, Guid? tenantId = null)
        {
            List<KeyValuePair<string,object?>> query = new List<KeyValuePair<string,object?>>()
            {
                new KeyValuePair<string, object?>(nameof(commodityCode), commodityCode),
                new KeyValuePair<string, object?>(nameof(tenantId), tenantId)
            };
            return apiCaller.GetAsync<CommodityDto?>($"{baseUrl}/by-code",query);
        }

        public Task<CommodityDto?> FindByName(string commodityName, Guid? tenantId = null)
        {
            List<KeyValuePair<string, object?>> query = new List<KeyValuePair<string, object?>>()
            {
                new KeyValuePair<string, object?>(nameof(commodityName), commodityName),
                new KeyValuePair<string, object?>(nameof(tenantId), tenantId)
            };
            return apiCaller.GetAsync<CommodityDto?>($"{baseUrl}/by-name", query);
        }
    }
}
