// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Api.Impl.Module.Entities;
using TTShang.Core.Api.Impl.Module.Internal;
using TTShang.Core.Module;
using TTShang.Core.Module.Services;

namespace TTShang.Core.Api.Impl.Module.Services
{
    /// <summary>
    /// 系统模块服务
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService))]
    public class SystemModuleService : ServiceBase<SystemModule, SystemModuleDto, int>, ISystemModuleService
    {
        private readonly ServerModuleManager serverModuleManager;


        /// <summary>
        /// 系统模块服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="serverModuleManager"></param>
        public SystemModuleService(IRepository<SystemModule, MasterDbContextLocator> repository, ServerModuleManager serverModuleManager) : base(repository)
        {
            this.serverModuleManager = serverModuleManager;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 高级查询，根据输入条件组合进行数据查询和排序
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>

        public override async Task<PageList<SystemModuleDto>> Search(PageRequest request)
        {
            IEnumerable<IModule> modeules = serverModuleManager.GetModules();
            var page = await base.Search(request);
            if (page.Items.Any())
            {
                foreach (var item in page.Items)
                {
                    item.RuningModule = modeules.FirstOrDefault(x => x.Name.Equals(item.Name))?.Adapt<SystemModuleDto>();
                }
            }
            return page;

        }

    }
}
