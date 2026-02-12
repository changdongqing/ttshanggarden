// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Client.Impl.UserCenter.Services
{
    [ScopedService]
    public class PositionService : ClientServiceBase<PositionDto, int>, IPositionService
    {
        public PositionService(IApiCaller apiCaller) : base(apiCaller, "position")
        {
        }
        
        public async Task<PageList<PositionDto>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object?> pramas = new Dictionary<string, object?>()
            {
                {"name",name }
            };
            return await apiCaller.GetAsync<PageList<PositionDto>>($"{this.baseUrl}/search/{pageIndex}/{pageSize}", pramas);
        }
    }
}
