using Gardener.Core.CodeGeneration.Dtos;
using Gardener.Core.Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Core.CodeGeneration.Impl.Entities
{
    /// <summary>
    /// 字段配置
    /// </summary>
    [Table("CodeGen" + nameof(FieldConfig))]
    public class FieldConfig : FieldConfigDto, IEntityBase
    {
    }
}
