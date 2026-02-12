// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.SystemConfig.Dtos;

namespace TTShang.Core.SystemConfig.Services
{
    /// <summary>
    /// 系统配置服务
    /// </summary>
    public interface ISystemConfigValueService : IServiceBase<SystemConfigValueDto, string>
    {
        /// <summary>
        /// 系统配置
        /// </summary>
        /// <remarks>
        /// 查询系统配置
        /// </remarks>
        /// <returns></returns>
        Task<SystemConfigDto> GetSystemConfig();
    }
}