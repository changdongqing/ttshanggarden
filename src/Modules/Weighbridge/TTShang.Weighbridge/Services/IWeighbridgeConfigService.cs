// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Common;
using TTShang.Weighbridge.Dtos;

namespace TTShang.Weighbridge.Services
{
    /// <summary>
    ///  地磅配置服务
    /// </summary>
    public interface IWeighbridgeConfigService : IServiceBase<WeighbridgeConfigDto, Guid>
    {
        /// <summary>
        /// 保存我的地磅配置
        /// </summary>
        /// <remarks>
        /// 保存我的地磅配置
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> SaveMyWeighbridgeConfig(SaveMyWeighbridgeConfigInput input);
    }
}
