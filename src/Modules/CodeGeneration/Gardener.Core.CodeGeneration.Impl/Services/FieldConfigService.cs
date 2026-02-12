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
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gardener.Core.CodeGeneration.Impl.Services
{
    /// <summary>
    /// 字段配置服务
    /// </summary>
    [ApiDescriptionSettings(groups: "CodeGeneration", Module = "code-gen")]
    public class FieldConfigService : ServiceBase<FieldConfig, FieldConfigDto, int>, IFieldConfigService
    {
        /// <summary>
        /// 字段配置服务
        /// </summary>
        /// <param name="repository"></param>
        public FieldConfigService(IRepository<FieldConfig, MasterDbContextLocator> repository) : base(repository)
        {
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <remarks>
        /// 根据实体类完整名称查询
        /// </remarks>
        /// <param name="entityTypeFullName"></param>
        /// <returns></returns>
        public Task<List<FieldConfigDto>> FindByEntityTypeFullName(string entityTypeFullName)
        {

            return base._repository.AsQueryable(false).Where(x => x.EntityTypeFullName.Equals(entityTypeFullName)).Select(x => x.Adapt<FieldConfigDto>()).ToListAsync();
        }
    }
}
