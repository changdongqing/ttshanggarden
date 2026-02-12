// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.UserCenter.Entities;
using TTShang.Core.UserCenter.Services;

namespace TTShang.Core.Api.Impl.UserCenter.Services
{
    /// <summary>
    /// 岗位管理服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class PositionService : ServiceBase<Position, PositionDto, int, GardenerMultiTenantDbContextLocator>, IPositionService
    {

        private readonly IRepository<Position, GardenerMultiTenantDbContextLocator> _positionRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionRepository"></param>
        public PositionService(IRepository<Position, GardenerMultiTenantDbContextLocator> positionRepository) : base(positionRepository)
        {
            _positionRepository = positionRepository;
        }
    }
}
