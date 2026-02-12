// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Core.CodeGeneration.Dtos;
using Gardener.Core.CodeGeneration.Impl.Entities;
using Gardener.Core.CodeGeneration.Services;
using Gardener.Core.Common;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Core.CodeGeneration.Impl.Services
{
    /// <summary>
    /// 实体类配置服务
    /// </summary>
    [ApiDescriptionSettings(groups: "CodeGeneration", Module = "code-gen")]
    public class EntityConfigService : ServiceBase<EntityConfig, EntityConfigDto, string>, IEntityConfigService
    {
        /// <summary>
        /// 实体类配置服务
        /// </summary>
        /// <param name="repository"></param>
        public EntityConfigService(IRepository<EntityConfig, MasterDbContextLocator> repository) : base(repository)
        {
        }
    }
}
