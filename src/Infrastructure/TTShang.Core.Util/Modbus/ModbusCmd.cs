// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Util.Modbus
{
    /// <summary>
    /// ModbusCmd
    /// </summary>
    public struct ModbusCmd
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="functionCode"></param>
        /// <param name="registerH"></param>
        /// <param name="registerL"></param>
        /// <param name="dataH"></param>
        /// <param name="dataL"></param>
        /// <param name="crcH"></param>
        /// <param name="crcL"></param>
        public ModbusCmd(byte address, byte functionCode, byte registerH, byte registerL, byte dataH, byte dataL, byte crcH, byte crcL)
        {
            Address = address;
            FunctionCode = functionCode;
            RegisterH = registerH;
            RegisterL = registerL;
            DataH = dataH;
            DataL = dataL;
            CrcH = crcH;
            CrcL = crcL;
        }

        /// <summary>
        /// 地址
        /// </summary>
        public byte Address { get; init; }
        /// <summary>
        /// 功能码
        /// </summary>
        public byte FunctionCode { get; init; }
        /// <summary>
        /// 寄存器地址-高位
        /// </summary>
        public byte RegisterH { get; init; }
        /// <summary>
        /// 寄存器地址-低位想
        /// </summary>
        public byte RegisterL { get; init; }
        /// <summary>
        /// 数据-高位
        /// </summary>
        public byte DataH { get; init; }
        /// <summary>
        /// 数据-低位想
        /// </summary>
        public byte DataL { get; init; }
        /// <summary>
        /// 数据-高位
        /// </summary>
        public byte CrcH { get; init; }
        /// <summary>
        /// 数据-低位想
        /// </summary>
        public byte CrcL { get; init; }
        /// <summary>
        /// ToHexString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Convert.ToHexString(GetBody());
        }
        /// <summary>
        /// 获取内容
        /// </summary>
        /// <returns></returns>
        public byte[] GetBody()
        {
            return [Address, FunctionCode, RegisterH, RegisterL, DataH, DataL, CrcH, CrcL];
        }
    }
}
