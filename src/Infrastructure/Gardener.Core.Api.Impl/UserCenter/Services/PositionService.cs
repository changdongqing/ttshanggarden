// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.UserCenter.Entities;
using Gardener.Core.UserCenter.Services;

namespace Gardener.Core.Api.Impl.UserCenter.Services
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
