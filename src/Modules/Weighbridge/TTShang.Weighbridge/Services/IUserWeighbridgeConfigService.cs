// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Common;
using TTShang.Weighbridge.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Weighbridge.Services
{
    /// <summary>
    /// 用户地磅配置服务
    /// </summary>
    public interface IUserWeighbridgeConfigService : IServiceBase<UserWeighbridgeConfigDto, int>
    {

        /// <summary>
        /// 查询-根据用户编号获取配置数据
        /// </summary>
        /// <remarks>
        /// 查询-根据用户编号获取配置数据
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserWeighbridgeConfigDto?> FindUserConfig(int userId);

        /// <summary>
        /// 查询-获取自己的配置数据
        /// </summary>
        /// <remarks>
        /// 查询-获取自己的配置数据
        /// </remarks>
        /// <returns></returns>
        Task<UserWeighbridgeConfigDto?> FindMyUserConfig();
        /// <summary>
        /// 保存-添加或更新当前用户配置
        /// </summary>
        /// <remarks>
        /// 保存-添加或更新当前用户配置
        /// </remarks>
        /// <param name="userWeighbridgeConfig"></param>
        /// <returns></returns>
        Task<UserWeighbridgeConfigDto?> SaveMyUserWeighbridgeConfig(UserWeighbridgeConfigDto userWeighbridgeConfig);
    }
}
