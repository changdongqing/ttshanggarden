// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Weighbridge.Dtos.Cmds;
using Gardener.Weighbridge.Dtos;

namespace Gardener.Weighbridge.Services
{
    /// <summary>
    /// 地磅设备服务
    /// </summary>
    public interface IWeighbridgeDeviceService
    {
        /// <summary>
        /// 清零
        /// </summary>
        /// <remarks>
        /// 清零
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> ZeroOut(DeviceCmdInput input);

        /// <summary>
        /// 设置最大值
        /// </summary>
        /// <remarks>
        /// 设置最大值
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SetMaxValue(DeviceCmdInput<int> input);

        /// <summary>
        /// 校准值
        /// </summary>
        /// <remarks>
        /// 校准值
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CalibrationValue(DeviceCmdInput<int> input);

        /// <summary>
        /// 设置分度值
        /// </summary>
        /// <remarks>
        /// 设置分度值
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SetDivisionValue(DeviceCmdInput<int> input);


        /// <summary>
        /// 设置滤波系数
        /// </summary>
        /// <remarks>
        /// 设置滤波系数
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SetFilterCoefficient(DeviceCmdInput<int> input);

        /// <summary>
        /// 设置AD转换速度
        /// </summary>
        /// <remarks>
        /// 设置AD转换速度
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SetAdConversionSpeed(DeviceCmdInput<int> input);

        /// <summary>
        /// 设置零点跟踪范围
        /// </summary>
        /// <remarks>
        /// 设置零点跟踪范围
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SetZeroTrackingRange(DeviceCmdInput<int> input);

        /// <summary>
        /// 读值
        /// </summary>
        /// <remarks>
        /// 读值
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> ReadValue(DeviceCmdInput<ReadValueCmd> input);

        /// <summary>
        /// 设置小数位
        /// </summary>
        /// <remarks>
        /// 设置小数位
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SetPrecision(DeviceCmdInput<PrecisionCmd> input);

        /// <summary>
        /// 获取设备最后一条数据
        /// </summary>
        /// <remarks>
        /// 获取设备最后一条数据
        /// </remarks>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        Task<WeighbridgeUploadData?> GetDeviceLastData(Guid deviceId);
    }
}
