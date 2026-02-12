// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Cache;
using Gardener.Core.NotificationSystem;
using Gardener.Iot.Dtos;
using Gardener.Iot.Services;
using Gardener.Weighbridge.Dtos.Cmds;
using Gardener.Weighbridge.Impl.Core;
using Gardener.Weighbridge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Gardener.Weighbridge.Impl.Services
{
    /// <summary>
    /// 地磅设备服务
    /// </summary>
    [ApiDescriptionSettings("Weighbridge", Module = "weighbridge")]
    public class WeighbridgeDeviceService : IWeighbridgeDeviceService
    {
        private readonly ILogger<WeighbridgeDeviceService> logger;
        private readonly IDeviceService deviceService;
        private readonly IServiceProvider serviceProvider;
        private readonly ICache cache;
        private readonly IProductService productService;
        private readonly ISystemNotificationService systemNotificationService;
        private readonly IDeviceDataStoreService deviceDataStoreService;
        private readonly IWeighbridgeDeviceConfigService weighbridgeDeviceConfigService;
        /// <summary>
        /// 地磅设备服务
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="deviceService"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="cache"></param>
        /// <param name="productService"></param>
        /// <param name="systemNotificationService"></param>
        /// <param name="deviceDataStoreService"></param>
        /// <param name="weighbridgeDeviceConfigService"></param>
        public WeighbridgeDeviceService(ILogger<WeighbridgeDeviceService> logger, IDeviceService deviceService, IServiceProvider serviceProvider, ICache cache, IProductService productService, ISystemNotificationService systemNotificationService, IDeviceDataStoreService deviceDataStoreService, IWeighbridgeDeviceConfigService weighbridgeDeviceConfigService)
        {
            this.logger = logger;
            this.deviceService = deviceService;
            this.serviceProvider = serviceProvider;
            this.cache = cache;
            this.productService = productService;
            this.systemNotificationService = systemNotificationService;
            this.deviceDataStoreService = deviceDataStoreService;
            this.weighbridgeDeviceConfigService = weighbridgeDeviceConfigService;
        }

        /// <summary>
        /// 清零
        /// </summary>
        /// <remarks>
        /// 清零
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> ZeroOut(DeviceCmdInput input)
        {
            return SendCmd(input, x => x.ZeroOut());
        }

        /// <summary>
        /// 设置最大值
        /// </summary>
        /// <remarks>
        /// 设置最大值
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> SetMaxValue(DeviceCmdInput<int> input)
        {
            return SendCmd(input, x => x.SetMaxValue(input.Config));
        }

        /// <summary>
        /// 校准值
        /// </summary>
        /// <remarks>
        /// 校准值
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> CalibrationValue(DeviceCmdInput<int> input)
        {
            return SendCmd(input, x => x.CalibrationValue(input.Config));
        }

        /// <summary>
        /// 设置分度值
        /// </summary>
        /// <remarks>
        /// 设置分度值
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> SetDivisionValue(DeviceCmdInput<int> input)
        {
            return SendCmd(input, x => x.SetDivisionValue(input.Config));
        }


        /// <summary>
        /// 设置滤波系数
        /// </summary>
        /// <remarks>
        /// 设置滤波系数
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> SetFilterCoefficient(DeviceCmdInput<int> input)
        {
            return SendCmd(input, x => x.SetFilterCoefficient(input.Config));
        }

        /// <summary>
        /// 设置AD转换速度
        /// </summary>
        /// <remarks>
        /// 设置AD转换速度
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> SetAdConversionSpeed(DeviceCmdInput<int> input)
        {
            return SendCmd(input, x => x.SetAdConversionSpeed(input.Config));
        }

        /// <summary>
        /// 设置零点跟踪范围
        /// </summary>
        /// <remarks>
        /// 设置零点跟踪范围
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> SetZeroTrackingRange(DeviceCmdInput<int> input)
        {
            return SendCmd(input, x => x.SetZeroTrackingRange(input.Config));
        }

        /// <summary>
        /// 读值
        /// </summary>
        /// <remarks>
        /// 读值
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> ReadValue(DeviceCmdInput<ReadValueCmd> input)
        {
            return SendCmd(input, x => x.ReadValue(input.Config.Length));
        }

        /// <summary>
        /// 设置小数位
        /// </summary>
        /// <remarks>
        /// 设置小数位
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> SetPrecision(DeviceCmdInput<PrecisionCmd> input)
        {
            return SendCmd(input, p => p.SetPrecision(input.Config.PrecisionType));
        }

        /// <summary>
        /// 获取设备最后一条数据
        /// </summary>
        /// <remarks>
        /// 获取设备最后一条数据
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task<WeighbridgeUploadData?> GetDeviceLastData(Guid deviceId)
        {
            DeviceDataDto? deviceData = await deviceDataStoreService.GetLastDeviceData(deviceId);

            if (deviceData == null || !deviceData.DeviceId.HasValue || string.IsNullOrEmpty(deviceData.Content))
            {
                return null;
            }

            IWeighbridgeAdapter? weighbridgeAdapter = await GetWeighbridgeAdapter(deviceData.DeviceId.Value);
            if (weighbridgeAdapter == null)
            {
                return null;
            }
            WeighbridgeUploadData? weighbridgeData = weighbridgeAdapter.Parse(Convert.FromBase64String(deviceData.Content));

            return weighbridgeData;
        }

        #region nonAction
        /// <summary>
        /// 获取地磅适配器
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [NonAction]
        internal async Task<IWeighbridgeAdapter?> GetWeighbridgeAdapter(Guid deviceId)
        {
            ProductDto? product = await deviceService.FindDeviceProduct(deviceId);
            if (product == null || product.ProductType == null)
            {
                return null;
            }
            IWeighbridgeAdapter? weighbridgeAdapter = serviceProvider.GetKeyedService<IWeighbridgeAdapter>(product.ProductType);
            return weighbridgeAdapter;
        }
        /// <summary>
        /// 向设备发送指令
        /// </summary>
        /// <param name="cmdInput">设备号</param>
        /// <param name="weighbridgeAdapterHandle"></param>
        /// <returns></returns>
        [NonAction]
        internal async Task<bool> SendCmd(DeviceCmdInput cmdInput, Func<IWeighbridgeAdapter, ModbusCmdBuilder> weighbridgeAdapterHandle)
        {
            return await SendCmd(cmdInput.DeviceId, weighbridgeAdapterHandle, cmdInput.ChannelId);
        }
        /// <summary>
        /// 向设备发送指令
        /// </summary>
        /// <param name="deviceId">设备号</param>
        /// <param name="weighbridgeAdapterHandle"></param>
        /// <param name="channelId">通道号</param>
        /// <returns></returns>
        [NonAction]
        internal async Task<bool> SendCmd(Guid deviceId, Func<IWeighbridgeAdapter, ModbusCmdBuilder> weighbridgeAdapterHandle, int channelId = 1)
        {
            IWeighbridgeAdapter? weighbridgeAdapter = await GetWeighbridgeAdapter(deviceId);
            if (weighbridgeAdapter == null)
            {
                return false;
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ModbusCmd c = weighbridgeAdapterHandle(weighbridgeAdapter).Address((byte)channelId).Build();
            var tempResult = await deviceService.SendMessage(deviceId, c.GetBody());
            stopwatch.Stop();
            logger.LogInformation($"send cmd {c.ToString()} to {deviceId} ,result {tempResult} time {stopwatch.ElapsedMilliseconds}");
            return tempResult;
        }
        /// <summary>
        /// 通知到客户端
        /// </summary>
        /// <param name="deviceData"></param>
        /// <returns></returns>
        [NonAction]
        internal async Task NotifyToClient(DeviceDataSaveAfterNotificationData deviceData)
        {
            if (deviceData.DeviceData.DeviceId == null || deviceData.DeviceData.OriginalContent == null || deviceData.DeviceData.OriginalContent.Length == 0)
            {
                return;
            }
            WeighbridgeNotificationData notificationData = new WeighbridgeNotificationData(deviceData.DeviceData.DeviceId.Value);
            if (!await systemNotificationService.ExistsDynamicSubscriber(notificationData))
            {
                //无订阅
                return;
            }
            IWeighbridgeAdapter? weighbridgeAdapter = await GetWeighbridgeAdapter(deviceData.DeviceData.DeviceId.Value);
            if (weighbridgeAdapter == null)
            {
                return;
            }
            WeighbridgeUploadData? weighbridgeData = weighbridgeAdapter.Parse(deviceData.DeviceData.OriginalContent);


            if (weighbridgeData != null)
            {
                var config = await weighbridgeDeviceConfigService.FindDeviceConfigByDeviceId(notificationData.DeviceId);
                if (config != null && config.ErrorCoefficient.HasValue)
                {
                    weighbridgeData.Weight = weighbridgeData.Weight + weighbridgeData.Weight * config.ErrorCoefficient.Value;
                }
            }
            notificationData.Data = weighbridgeData;
            //入库
            //发送通知给动态订阅者
            await systemNotificationService.SendToDynamicSubscriber(notificationData);
        }
        #endregion
    }
}
