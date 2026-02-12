// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Api.Impl.Dict.Entities;
using Gardener.Core.Dict.Dtos;
using Gardener.Core.Dict.Services;

namespace Gardener.Core.Api.Impl.Dict.Services
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [ApiDescriptionSettings(nameof(Constant.InfrastructureService), Module = "dict")]
    public class CodeService : ServiceBase<Code, CodeDto, int>, ICodeService
    {

        /// <summary>
        /// 字典管理
        /// </summary>
        /// <param name="repository"></param>
        public CodeService(IRepository<Code, MasterDbContextLocator> repository) : base(repository)
        {
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<PageList<CodeDto>> Search(PageRequest request)
        {
            IQueryable<Code> queryable = base.GetSearchQueryable(request.FilterGroups);
            var codes = DictHelper.GetCodesFromCache("mood");
            PageList<CodeDto> result = await queryable
                .Include(x => x.CodeType)
                .Select(x => x)
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<CodeDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);

            if (result.Items.Any())
            {
                foreach (var item in result.Items)
                {
                    item.CodeType.Codes = null;
                }
            }
            return result;
        }
    }
}
