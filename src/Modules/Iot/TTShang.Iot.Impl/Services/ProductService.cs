// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Impl.Services
{
    /// <summary>
    /// 产品服务
    /// </summary>
    [ApiDescriptionSettings("Iot", Module = "iot")]
    public class ProductService : ServiceBase<Product, ProductDto, Guid, GardenerMultiTenantDbContextLocator>, IProductService
    {

        /// <summary>
        /// 产品服务
        /// </summary>
        /// <param name="repository"></param>
        public ProductService(IRepository<Product, GardenerMultiTenantDbContextLocator> repository) : base(repository)
        {
        }
    }
}
