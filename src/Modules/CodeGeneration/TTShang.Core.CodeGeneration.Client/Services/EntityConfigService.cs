// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.CodeGeneration.Client.Services
{
    /// <summary>
    /// 实体类配置服务
    /// </summary>
    [ScopedService]
    public class EntityConfigService : ClientServiceBase<EntityConfigDto, string>, IEntityConfigService
    {
        public EntityConfigService(IApiCaller apiCaller) : base(apiCaller, "entity-config", "code-gen")
        {
        }
    }
}
