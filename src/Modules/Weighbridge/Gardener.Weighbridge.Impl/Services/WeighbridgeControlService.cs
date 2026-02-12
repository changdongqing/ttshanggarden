// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Cache;
using Gardener.Core.NotificationSystem;
using Gardener.Core.Util.Modbus;
using Gardener.Iot.Dtos;
using Gardener.Iot.Services;
using Gardener.Weighbridge.Dtos.Cmds;
using Gardener.Weighbridge.Impl.Core;
using Gardener.Weighbridge.Services;
using IdGen;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gardener.Weighbridge.Impl.Services
{
    /// <summary>
    /// 地磅控制服务
    /// </summary>
    [ApiDescriptionSettings("Weighbridge", Module = "weighbridge")]
    public class WeighbridgeControlService : IWeighbridgeControlService
    {
        private readonly IWeighbridgeConfigService weighbridgeConfigService;
        private readonly WeighbridgeDeviceService weighbridgeDeviceService;
        private readonly IDeviceService deviceService;

        /// <summary>
        /// 地磅控制服务
        /// </summary>
        /// <param name="weighbridgeConfigService"></param>
        /// <param name="weighbridgeDeviceService"></param>
        /// <param name="deviceService"></param>
        public WeighbridgeControlService(IWeighbridgeConfigService weighbridgeConfigService, WeighbridgeDeviceService weighbridgeDeviceService, IDeviceService deviceService)
        {
            this.weighbridgeConfigService = weighbridgeConfigService;
            this.weighbridgeDeviceService = weighbridgeDeviceService;
            this.deviceService = deviceService;
        }

        /// <summary>
        /// 清零
        /// </summary>
        /// <remarks>
        /// 清零
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> ZeroOut(WeighbridgeCmdInput input)
        {
            return SendCmd(input.ConfigId, input.ChannelIds, b => b.ZeroOut());
        }

        /// <summary>
        /// 读值
        /// </summary>
        /// <remarks>
        /// 读值
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> ReadValue(WeighbridgeCmdInput<ReadValueCmd> input)
        {
            return SendCmd(input.ConfigId, input.ChannelIds, b => b.ReadValue(input.Config.Length));
        }

        /// <summary>
        /// 读值
        /// </summary>
        /// <remarks>
        /// 读值
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <param name="channelIds"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [NonAction]
        public Task<bool> ReadValue(Guid deviceId, int[] channelIds, byte length)
        {
            return SendCmd(deviceId, channelIds, b => b.ReadValue(length));
        }

        /// <summary>
        /// 设置单位
        /// </summary>
        /// <remarks>
        /// 设置单位
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> SetUnit(WeighbridgeCmdInput<UnitCmd> input)
        {
            return SendCmd(input.ConfigId, input.ChannelIds, b => b.SetUnit(input.Config.UnitType));
        }

        /// <summary>
        /// 设置小数位
        /// </summary>
        /// <remarks>
        /// 设置小数位
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> SetPrecision(WeighbridgeCmdInput<PrecisionCmd> input)
        {
            return SendCmd(input.ConfigId, input.ChannelIds, p => p.SetPrecision(input.Config.PrecisionType));
        }

        /// <summary>
        /// 去皮
        /// </summary>
        /// <remarks>
        /// 设置去皮
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> Netweight(WeighbridgeCmdInput<NetweightCmd> input)
        {
            return SendCmd(input.ConfigId, input.ChannelIds, p =>
            {
                if (input.Config.NetWeight)
                {
                    return p.Netweight();
                }
                else
                {
                    return p.UnNetweight();
                }
            });

        }

        #region 命令发送
        /// <summary>
        /// 向设备发送指令
        /// </summary>
        /// <param name="configId"></param>
        /// <param name="channelIds"></param>
        /// <param name="weighbridgeAdapterHandle"></param>
        /// <returns></returns>
        private async Task<bool> SendCmd(Guid configId, int[] channelIds, Func<IWeighbridgeAdapter, ModbusCmdBuilder> weighbridgeAdapterHandle)
        {
            var config = await weighbridgeConfigService.Get(configId);
            List<Task<bool>> tasks = new List<Task<bool>>();
            var ids = config.DeviceIds.Split(",");
            foreach (var id in ids)
            {
                Guid deviceId = Guid.Parse(id);
                var device= await deviceService.Find(deviceId);
                if (device == null)
                {
                    continue;
                }
                tasks.Add(SendCmd(deviceId, weighbridgeAdapterHandle, channelIds));
            }
            bool[] result = await Task.WhenAll<bool>(tasks);

            return !result.Any(x => !x);
        }
        /// <summary>
        /// 向设备发送指令
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="weighbridgeAdapterHandle"></param>
        /// <param name="channelIds"></param>
        /// <returns></returns>
        private async Task<bool> SendCmd(Guid deviceId, Func<IWeighbridgeAdapter, ModbusCmdBuilder> weighbridgeAdapterHandle, params int[] channelIds)
        {
            bool result = true;
            for (int i = 0; i < channelIds.Length; i++)
            {
                if (i > 0)
                {
                    Thread.Sleep(1000);
                }
                var flag = await weighbridgeDeviceService.SendCmd(deviceId, weighbridgeAdapterHandle, channelIds[i]);
                if (!flag)
                {
                    result = false;
                }
            }
            return result;
        }
        #endregion
    }
}
