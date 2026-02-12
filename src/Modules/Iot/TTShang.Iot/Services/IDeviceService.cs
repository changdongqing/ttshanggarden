// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Iot.Services
{
    /// <summary>
    /// 设备服务
    /// </summary>
    public interface IDeviceService : IServiceBase<DeviceDto, Guid>
    {
        /// <summary>
        /// 验证设备的密钥正确性
        /// </summary>
        /// <param name="deviceId">设备编号</param>
        /// <param name="account">账户</param>
        /// <param name="secretKey">密钥</param>
        /// <returns></returns>
        Task<bool> VerifySecretKey(Guid deviceId, string account, string secretKey);


        /// <summary>
        /// 根据ClientId查询设备，不存在返回null
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<DeviceDto?> TryGetByClientId(string clientId);

        /// <summary>
        /// 向设备发送消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SendMessage(SendDataInput input);

        /// <summary>
        /// 向设备发送消息
        /// </summary>
        /// <remarks>
        /// 向某个设备发送信息
        /// </remarks>
        /// <param name="clientId"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<bool> SendMessage(string clientId, byte[] content, DeviceDataContentType? contentType = null)
        {
            return Task.FromResult(false);
        }
        /// <summary>
        /// 向设备发送消息
        /// </summary>
        /// <remarks>
        /// 向某个设备发送信息
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<bool> SendMessage(Guid deviceId, byte[] content, DeviceDataContentType? contentType = null)
        {
            return Task.FromResult(false);
        }
        /// <summary>
        /// 向设备发送消息
        /// </summary>
        /// <remarks>
        /// 向某个设备发送信息
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <param name="contents"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        Task<bool> SendMessage(Guid deviceId, IEnumerable<byte[]> contents, DeviceDataContentType? contentType = null)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// 查询-根据设备编号查找全局
        /// </summary>
        /// <remarks>
        /// 查询-根据设备编号查找全局,未找到返回null
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<DeviceDto?> FindGlobal(Guid deviceId);

        /// <summary>
        /// 绑定租户
        /// </summary>
        /// <remarks>
        /// 将设备绑定给租户
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> BindTenant(DeviceBindTenantInput input);

        /// <summary>
        /// 更新设备别名
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 更新设备别名
        /// </remarks>
        /// <returns></returns>
        Task<bool> UpdateDeviceAlias(UpdateDeviceAliasInput input);

        /// <summary>
        /// 获取设备的产品
        /// </summary>
        /// <remarks>
        /// 产品数据一般不会变化，这里缓存时间较久（2小时）
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<ProductDto?> FindDeviceProduct(Guid deviceId);

    }
}
