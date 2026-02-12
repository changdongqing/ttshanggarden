// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.CodeGeneration.Dtos;
using Gardener.Core.Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Core.CodeGeneration.Impl.Entities
{
    /// <summary>
    /// 实体类配置
    /// </summary>
    [Table("CodeGen" + nameof(EntityConfig))]
    public class EntityConfig : EntityConfigDto,IEntityBase
    {
    }
}
