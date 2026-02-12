// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Weighbridge.Impl.Core
{
    /// <summary>
    /// 地磅modbus构建器提供者
    /// </summary>
    internal interface IWeighbridgeAdapter
    {
        /// <summary>
        /// 读值
        /// </summary>
        /// <param name="length">读取长度</param>
        /// <returns></returns>
        ModbusCmdBuilder ReadValue(byte length = 1);

        /// <summary>
        /// 去皮
        /// </summary>
        /// <returns></returns>
        ModbusCmdBuilder Netweight();

        /// <summary>
        /// 取消去皮
        /// </summary>
        /// <returns></returns>
        ModbusCmdBuilder UnNetweight();

        /// <summary>
        /// 设置单位
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        ModbusCmdBuilder SetUnit(UnitType unitType);

        /// <summary>
        /// 设置小数位
        /// </summary>
        /// <param name="precisionType"></param>
        /// <returns></returns>
        ModbusCmdBuilder SetPrecision(PrecisionType precisionType);

        /// <summary>
        /// 清零
        /// </summary>
        /// <returns></returns>
        ModbusCmdBuilder ZeroOut();

        /// <summary>
        /// 滤波系数
        /// </summary>
        /// <param name="value">1-9</param>
        /// <returns></returns>
        ModbusCmdBuilder SetFilterCoefficient(int value);

        /// <summary>
        /// AD转换速度
        /// </summary>
        /// <param name="value">0:10Hz,1:20Hz,2:80Hz</param>
        /// <returns></returns>
        ModbusCmdBuilder SetAdConversionSpeed(int value);

        /// <summary>
        /// 零点跟踪范围
        /// </summary>
        /// <param name="value">0-10(0表示不跟踪)</param>
        /// <returns></returns>
        ModbusCmdBuilder SetZeroTrackingRange(int value);

        /// <summary>
        /// 设置最大值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        ModbusCmdBuilder SetMaxValue(int value);

        /// <summary>
        /// 校准值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        ModbusCmdBuilder CalibrationValue(int value);

        /// <summary>
        /// 设置分度值
        /// </summary>
        /// <param name="value">分度值：[1,2,5,10,20]</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        ModbusCmdBuilder SetDivisionValue(int value);

        /// <summary>
        /// 数据解析
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        WeighbridgeUploadData? Parse(byte[] data);
    }
}
