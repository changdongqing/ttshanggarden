// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Dict.Dtos;

namespace Gardener.Core.Api.Impl.Dict.Entities
{
    /// <summary>
    /// 字典类型
    /// </summary>
    public class CodeType : CodeTypeDto, IEntityBase, IEntityTypeBuilder<CodeType>
    {
        /// <summary>
        /// 字典集合
        /// </summary>
        public new ICollection<Code>? Codes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<CodeType> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => x.CodeTypeValue).IsUnique();
        }
    }
}
