// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Dict.Dtos;
using TTShang.Core.Dict.Resources;
using System.ComponentModel.DataAnnotations;

namespace TTShang.Core.Api.Impl.Dict.Entities
{
    /// <summary>
    /// 字典信息
    /// </summary>
    public class Code : CodeDto, IEntityBase, IEntityTypeBuilder<Code>
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [Display(Name = nameof(DictResource.CodeType), ResourceType = typeof(DictResource))]
        public new CodeType CodeType { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<Code> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(x => x.CodeType).WithMany(x => x.Codes).HasForeignKey(x => x.CodeTypeId);
        }
    }
}
