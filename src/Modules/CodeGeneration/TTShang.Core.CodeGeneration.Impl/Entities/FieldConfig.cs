using TTShang.Core.CodeGeneration.Dtos;
using TTShang.Core.Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTShang.Core.CodeGeneration.Impl.Entities
{
    /// <summary>
    /// 字段配置
    /// </summary>
    [Table("CodeGen" + nameof(FieldConfig))]
    public class FieldConfig : FieldConfigDto, IEntityBase
    {
    }
}
