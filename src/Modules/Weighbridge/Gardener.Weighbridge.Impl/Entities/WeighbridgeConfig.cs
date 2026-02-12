// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Weighbridge.Impl.Entities
{
    /// <summary>
    /// 地磅配置
    /// </summary>
    [Table("Wbg" + nameof(WeighbridgeConfig))]
    public class WeighbridgeConfig : WeighbridgeConfigDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<WeighbridgeConfig, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<WeighbridgeConfig> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return [
                new WeighbridgeConfig(){
                    Id=Guid.Parse("93528604-d539-46c3-b697-8388e307144d"),
                    DeviceIds="4a63d763-cae2-41bb-889d-e575ddcd2f07,c7577810-ade6-4365-afc7-f72fb8e11d6b,8d24834a-2933-4efa-a1aa-796d0151674e",
                    Name="123组合"
                },new WeighbridgeConfig(){
                    Id=Guid.Parse("ffd1661c-dc4e-42e0-82fd-36df7adb869f"),
                    DeviceIds="4a63d763-cae2-41bb-889d-e575ddcd2f07",
                    Name="#3称台"
                },new WeighbridgeConfig(){
                    Id=Guid.Parse("0bd89f53-133a-4d2e-a383-87f364117c5f"),
                    DeviceIds="8d24834a-2933-4efa-a1aa-796d0151674e",
                    Name="#2称台"
                },new WeighbridgeConfig(){
                    Id=Guid.Parse("3fd87af7-432d-46d6-abe1-70e80e522e05"),
                    DeviceIds="c7577810-ade6-4365-afc7-f72fb8e11d6b",
                    Name="#1称台"
                }
                ];
        }
    }
}
