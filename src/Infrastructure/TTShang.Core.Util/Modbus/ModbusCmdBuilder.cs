// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace TTShang.Core.Util.Modbus
{
    /// <summary>
    /// ModbusCmd构建器
    /// </summary>
    public class ModbusCmdBuilder
    {
        /// <summary>
        /// 地址
        /// </summary>
        private byte address;
        /// <summary>
        /// 功能码
        /// </summary>
        private byte functionCode;
        /// <summary>
        /// 寄存器地址-高位
        /// </summary>
        private byte registerH;
        /// <summary>
        /// 寄存器地址-低位想
        /// </summary>
        private byte registerL;
        /// <summary>
        /// 数据-高位
        /// </summary>
        private byte dataH;
        /// <summary>
        /// 数据-低位想
        /// </summary>
        private byte dataL;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ModbusCmdBuilder Create()
        {
            return new ModbusCmdBuilder();
        }
        /// <summary>
        /// ModbusCmd构建器
        /// </summary>
        /// <param name="address"></param>
        /// <param name="functionCode"></param>
        /// <param name="registerH"></param>
        /// <param name="registerL"></param>
        /// <param name="dataH"></param>
        /// <param name="dataL"></param>
        public ModbusCmdBuilder(byte address, byte functionCode, byte registerH, byte registerL, byte dataH, byte dataL)
        {
            this.address = address;
            this.functionCode = functionCode;
            this.registerH = registerH;
            this.registerL = registerL;
            this.dataH = dataH;
            this.dataL = dataL;
        }
        /// <summary>
        /// ModbusCmd构建器
        /// </summary>
        public ModbusCmdBuilder()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public ModbusCmdBuilder Address(byte address)
        {
            this.address = address;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        public ModbusCmdBuilder FunctionCode(byte functionCode)
        {
            this.functionCode = functionCode;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public ModbusCmdBuilder Register(int register)
        {
            registerH = (byte)(register >> 8 & 0xFF);
            registerL = (byte)(register & 0xFF);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerH"></param>
        /// <returns></returns>
        public ModbusCmdBuilder RegisterH(byte registerH)
        {
            this.registerH = registerH;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerL"></param>
        /// <returns></returns>
        public ModbusCmdBuilder RegisterL(byte registerL)
        {
            this.registerL = registerL;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataH"></param>
        /// <returns></returns>
        public ModbusCmdBuilder DataH(byte dataH)
        {
            this.dataH = dataH;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataL"></param>
        /// <returns></returns>
        public ModbusCmdBuilder DataL(byte dataL)
        {
            this.dataL = dataL;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ModbusCmdBuilder Data(int data)
        {
            dataH = (byte)(data >> 8 & 0xFF);
            dataL = (byte)(data & 0xFF);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ModbusCmd Build()
        {
            var crc = CRCCalc([address, functionCode, registerH, registerL, dataH, dataL]);
            return new ModbusCmd(address, functionCode, registerH, registerL, dataH, dataL, crc[0], crc[1]);
        }
        /// <summary>
        /// CRC校验，参数data为byte数组
        /// </summary>
        /// <param name="data">校验数据，字节数组</param>
        /// <returns>字节0是高8位，字节1是低8位</returns>
        public static byte[] CRCCalc(byte[] data)
        {
            //crc计算赋初始值
            int crc = 0xffff;
            for (int i = 0; i < data.Length; i++)
            {
                crc = crc ^ data[i];
                for (int j = 0; j < 8; j++)
                {
                    int temp;
                    temp = crc & 1;
                    crc = crc >> 1;
                    crc = crc & 0x7fff;
                    if (temp == 1)
                    {
                        crc = crc ^ 0xa001;
                    }
                    crc = crc & 0xffff;
                }
            }
            //CRC寄存器的高低位进行互换
            byte[] crc16 = new byte[2];
            //CRC寄存器的高8位变成低8位，
            crc16[1] = (byte)(crc >> 8 & 0xff);
            //CRC寄存器的低8位变成高8位
            crc16[0] = (byte)(crc & 0xff);
            return crc16;
        }


    }
}
