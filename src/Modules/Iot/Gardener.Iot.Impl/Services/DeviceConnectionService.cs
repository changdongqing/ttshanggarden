// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Iot.Tools;

namespace Gardener.Iot.Impl.Services
{
    /// <summary>
    /// 设备连接服务
    /// </summary>
    [ApiDescriptionSettings("Iot", Module = "iot")]
    public class DeviceConnectionService : ServiceBase<DeviceConnection, DeviceConnectionDto, long, GardenerMultiTenantDbContextLocator>, IDeviceConnectionService
    {
        private readonly IDeviceConnectionTool deviceConnectionTool;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="deviceConnectionTool"></param>
        public DeviceConnectionService(IRepository<DeviceConnection, GardenerMultiTenantDbContextLocator> repository, IDeviceConnectionTool deviceConnectionTool) : base(repository)
        {
            this.deviceConnectionTool = deviceConnectionTool;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 添加单条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<DeviceConnectionDto> Insert(DeviceConnectionDto input)
        {
            DeviceConnectionDto deviceConnection = await base.Insert(input);
            await deviceConnectionTool.TryDeviceConnectionRemoveCache(deviceConnection);
            return deviceConnection;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <remarks>
        /// 更新一条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(DeviceConnectionDto input)
        {
            var result = await base.Update(input);
            if (result)
            {
                //移除缓存
                await deviceConnectionTool.TryDeviceConnectionRemoveCache(input);
            }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除一条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> Delete(long id)
        {
            var result = await base.Delete(id);
            if (result)
            {
                //移除缓存
                await DeviceConnectionRemoveCache(id);
            }
            return result;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<bool> Deletes([FromBody] long[] ids)
        {
            var result = await base.Deletes(ids);
            foreach (long id in ids)
            {
                await DeviceConnectionRemoveCache(id);
            }
            return result;
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <remarks>
        /// 根据主键逻辑删除
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public override async Task<bool> FakeDelete(long id)
        {
            var result = await base.FakeDelete(id);
            await DeviceConnectionRemoveCache(id);
            return result;
        }

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量逻辑删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<bool> FakeDeletes([FromBody] long[] ids)
        {

            var result = await base.FakeDeletes(ids);
            foreach (long id in ids)
            {
                await DeviceConnectionRemoveCache(id);
            }
            return result;
        }

        /// <summary>
        /// 清空设备连接缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task DeviceConnectionRemoveCache(long id)
        {
            var connection = await _repository.AsQueryable(false).FirstOrDefaultAsync(x => x.Id == id);
            await deviceConnectionTool.TryDeviceConnectionRemoveCache(connection);
        }
    }
}
