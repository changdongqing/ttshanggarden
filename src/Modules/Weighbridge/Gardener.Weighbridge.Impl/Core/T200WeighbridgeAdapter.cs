// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Weighbridge.Impl.Core
{
    internal class T200WeighbridgeAdapter : IWeighbridgeAdapter
    {
        /// <summary>
        /// 读值
        /// </summary>
        /// <param name="length">读取长度</param>
        /// <returns></returns>
        public ModbusCmdBuilder ReadValue(byte length = 1)
        {
            return ModbusCmdBuilder.Create().FunctionCode(3).RegisterH(0).RegisterL(0).DataH(0).DataL(length);
        }
        /// <summary>
        /// 去皮
        /// </summary>
        /// <returns></returns>
        public ModbusCmdBuilder Netweight()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 取消去皮
        /// </summary>
        /// <returns></returns>
        public ModbusCmdBuilder UnNetweight()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设置单位
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public ModbusCmdBuilder SetUnit(UnitType unitType)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设置小数位
        /// </summary>
        /// <param name="precisionType"></param>
        /// <returns></returns>
        public ModbusCmdBuilder SetPrecision(PrecisionType precisionType)
        {
            return ModbusCmdBuilder.Create().FunctionCode(6).Register(1).DataH(0).DataL(precisionType.Value);
        }
        /// <summary>
        /// 清零
        /// </summary>
        /// <returns></returns>
        public ModbusCmdBuilder ZeroOut()
        {
            return ModbusCmdBuilder.Create().FunctionCode(6).Register(4).DataH(0).DataL(0);
        }
        /// <summary>
        /// 设置滤波系数
        /// </summary>
        /// <param name="value">1-9</param>
        /// <returns></returns>
        public ModbusCmdBuilder SetFilterCoefficient(int value)
        {
            if (value < 1 || value > 9)
            {
                throw new ArgumentException("滤波系数只能接收[1-9]");
            }
            return ModbusCmdBuilder.Create().FunctionCode(6).Register(6).Data(value);
        }
        /// <summary>
        /// 设置AD转换速度
        /// </summary>
        /// <param name="value">0:10Hz,1:20Hz,2:80Hz</param>
        /// <returns></returns>
        public ModbusCmdBuilder SetAdConversionSpeed(int value)
        {
            if (value < 0 || value > 2)
            {
                throw new ArgumentException("AD转换速度[0-2]，对应[10Hz,20Hz,80Hz]");
            }
            return ModbusCmdBuilder.Create().FunctionCode(6).Register(7).Data(value);
        }
        /// <summary>
        /// 设置零点跟踪范围
        /// </summary>
        /// <param name="value">0-10(0表示不跟踪)</param>
        /// <returns></returns>
        public ModbusCmdBuilder SetZeroTrackingRange(int value)
        {
            if (value < 0 || value > 10)
            {
                throw new ArgumentException("AD转换速度[0-10]");
            }
            return ModbusCmdBuilder.Create().FunctionCode(6).Register(8).Data(value);
        }

        /// <summary>
        /// 设置最大值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ModbusCmdBuilder SetMaxValue(int value)
        {
            if (value < 0 || value > 60000)
            {
                throw new ArgumentException("最大值有效范围为[0-60000]");
            }
            return ModbusCmdBuilder.Create().FunctionCode(6).Register(0).Data(value);
        }


        /// <summary>
        /// 校准值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ModbusCmdBuilder CalibrationValue(int value)
        {
            if (value < 0 || value > 60000)
            {
                throw new ArgumentException("校准值有效范围为[0-60000]");
            }
            return ModbusCmdBuilder.Create().FunctionCode(6).Register(5).Data(value);
        }

        /// <summary>
        /// 设置分度值
        /// </summary>
        /// <param name="value">分度值：[0-4]，对应[1,2,5,10,20]</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public ModbusCmdBuilder SetDivisionValue(int value)
        {
            int[] values = [1, 2, 5, 10, 20];
            if (!values.Contains(value))
            {
                throw new ArgumentException($"分度值只支持[{string.Join(",", values)}]");
            }
            int index = Array.IndexOf(values, value);
            return ModbusCmdBuilder.Create().FunctionCode(6).Register(12).Data(index);
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public WeighbridgeUploadData? Parse(byte[] data)
        {
            int channel = data[0];
            int functionCode = data[1];
            if (functionCode == 3)
            {
                //读值
                int length = data[2];
                WeighbridgeUploadData weighbridgeData = new WeighbridgeUploadData(UploadDataType.ReadValue, channel);
                weighbridgeData.UnitType = UnitType.Kg;
                int index = 0;
                int[] datas = new int[length / 2];
                bool plus = true;
                while (index < length / 2)
                {
                    int begin = (index * 2) + 3;
                    int end = begin + 1;

                    if (index == 0)
                    {
                        Int16 int16 = BitConverter.ToInt16([data[end], data[begin]], 0);
                        weighbridgeData.MaxValue = int16;

                    }
                    if (index == 1)
                    {
                        //小数点
                        weighbridgeData.PrecisionType = PrecisionType.GetPrecisionType(data[end]);

                    }
                    else if (index == 2)
                    {
                        //正负：0 正 1 负
                        int temp = data[end];
                        plus = temp == 0;
                    }
                    else if (index == 3)
                    {
                        //重量
                        Int16 int16 = BitConverter.ToInt16([data[end], data[begin]], 0);
                        weighbridgeData.Weight = (plus ? int16 : int16 * -1);
                    }
                    else if (index == 4)
                    {
                       
                    }
                    else if (index == 5)
                    {
                        Int16 int16 = BitConverter.ToInt16([data[end], data[begin]], 0);
                        weighbridgeData.CalibrationValue = int16;
                    }
                    else if (index == 6)
                    {
                        int temp = data[end];
                        weighbridgeData.FilterCoefficient = temp;
                    }
                    else if (index == 7)
                    {
                        int temp = data[end];
                        weighbridgeData.AdConversionSpeed = temp;
                    }
                    
                    else if (index == 8)
                    {
                        int temp = data[end];
                        weighbridgeData.ZeroTrackingRange = temp;
                    }
                    
                    else if (index == 12)
                    {
                        int temp = data[end];
                        int[] values = [1, 2, 5, 10, 20];
                        if(temp<values.Length)
                        {
                            weighbridgeData.DivisionValue = values[temp];
                        }
                    }

                    index++;
                }

                return weighbridgeData;
            }
            else if (functionCode == 6)
            {
                WeighbridgeUploadData weighbridgeData = new WeighbridgeUploadData(UploadDataType.Unknown, channel);
                return weighbridgeData;
            }
            return null;

        }
    }
}
