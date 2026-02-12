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
    /// 生成模板服务
    /// </summary>
    [ApiDescriptionSettings(groups: "CodeGeneration", Module = "code-gen")]
    public class GenerateTemplateService : ServiceBase<GenerateTemplate, GenerateTemplateDto, int>, IGenerateTemplateService
    {
        /// <summary>
        /// 生成模板服务
        /// </summary>
        /// <param name="repository"></param>
        public GenerateTemplateService(IRepository<GenerateTemplate, MasterDbContextLocator> repository) : base(repository)
        {
        }
    }
}
