// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Weighbridge.Impl.Entities
{
    /// <summary>
    /// 称重记录
    /// </summary>
    [Table("Wbg" + nameof(WeighingRecord))]
    public class WeighingRecord : WeighingRecordDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
    }
}
