namespace TTShang.Iot.Client.Services
{
    /// <summary>
    ///  产品服务
    /// </summary>
    [ScopedService]
    public class ProductService : ClientServiceBase<ProductDto, Guid>, IProductService
    {
        /// <summary>
        ///  产品服务
        /// </summary>
        public ProductService(IApiCaller apiCaller) : base(apiCaller, "product", "iot")
        {
        }
    }

}