// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Weighbridge.Impl.Core
{
    internal class T100WeighbridgeAdapter : IWeighbridgeAdapter
    {
        /// <summary>
        /// 获取读值命令构建器
        /// </summary>
        /// <param name="length">读取长度</param>
        /// <returns></returns>
        public ModbusCmdBuilder ReadValue(byte length = 1)
        {
            return ModbusCmdBuilder.Create().FunctionCode(3).RegisterH(0).RegisterL(0).DataH(0).DataL(length);
        }
        /// <summary>
        /// 获取去皮命令构建器
        /// </summary>
        /// <returns></returns>
        public ModbusCmdBuilder Netweight()
        {
            return ModbusCmdBuilder.Create().FunctionCode(6).RegisterH(0).RegisterL(17).DataH(0).DataL(1);
        }
        /// <summary>
        /// 获取取消去皮命令构建器
        /// </summary>
        /// <returns></returns>
        public ModbusCmdBuilder UnNetweight()
        {
            return ModbusCmdBuilder.Create().FunctionCode(6).RegisterH(0).RegisterL(17).DataH(0).DataL(2);
        }
        /// <summary>
        /// 获取设置单位命令构建器
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public ModbusCmdBuilder SetUnit(UnitType unitType)
        {
            return ModbusCmdBuilder.Create().FunctionCode(6).RegisterH(0).RegisterL(2).DataH(0).DataL(unitType.Value);
        }
        /// <summary>
        /// 获取设置小数位命令构建器
        /// </summary>
        /// <param name="precisionType"></param>
        /// <returns></returns>
        public ModbusCmdBuilder SetPrecision(PrecisionType precisionType)
        {
            return ModbusCmdBuilder.Create().FunctionCode(6).RegisterH(0).RegisterL(1).DataH(0).DataL(precisionType.Value);
        }
        /// <summary>
        /// 清零
        /// </summary>
        /// <returns></returns>
        public ModbusCmdBuilder ZeroOut()
        {
            return ModbusCmdBuilder.Create().FunctionCode(6).RegisterH(0).RegisterL(22).Data(17);
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
                int index = 0;
                int[] datas = new int[length / 2];
                while (index < length / 2)
                {
                    int begin = (index * 2) + 3;
                    int end = begin + 1;

                    if (index == 0)
                    {
                        //重量
                        Int16 int16 = BitConverter.ToInt16([data[end], data[begin]], 0);
                        weighbridgeData.Weight = int16;
                    }
                    else if (index == 1)
                    {
                        //小数点
                        weighbridgeData.PrecisionType = PrecisionType.GetPrecisionType(data[end]);

                    }
                    else if (index == 2)
                    {
                        //单位
                        weighbridgeData.UnitType = UnitType.GetUnitType(data[end]);

                    }
                    else if (index == 17)
                    {
                        //去皮
                        weighbridgeData.NetWeight = data[end] == 01;
                    }

                    index++;
                }

                return weighbridgeData;
            }
            else if (functionCode == 6)
            {
                T100UploadDataType uploadDataType = new T100UploadDataType(data[2], data[3]);
                WeighbridgeUploadData weighbridgeData = new WeighbridgeUploadData(UploadDataType.Unknown, channel);
                if (T100UploadDataType.Unit.Equals(uploadDataType))
                {
                    weighbridgeData.UnitType = UnitType.GetUnitType(data[5]);
                    weighbridgeData.UploadDataType = UploadDataType.Unit;
                }
                else if (T100UploadDataType.Precision.Equals(uploadDataType))
                {
                    weighbridgeData.PrecisionType = PrecisionType.GetPrecisionType(data[5]);
                    weighbridgeData.UploadDataType = UploadDataType.Precision;
                }
                else if (T100UploadDataType.Netweight.Equals(uploadDataType))
                {
                    weighbridgeData.NetWeight = data[5] == 01;
                    weighbridgeData.UploadDataType = UploadDataType.Netweight;
                }
                else if (T100UploadDataType.ZeroOut.Equals(uploadDataType))
                {
                    weighbridgeData.UploadDataType = UploadDataType.ZeroOut;
                }

                return weighbridgeData;
            }
            return null;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ModbusCmdBuilder SetFilterCoefficient(int value)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ModbusCmdBuilder SetAdConversionSpeed(int value)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ModbusCmdBuilder SetZeroTrackingRange(int value)
        {
            throw new NotImplementedException();
        }

        public ModbusCmdBuilder SetMaxValue(int value)
        {
            throw new NotImplementedException();
        }

        public ModbusCmdBuilder CalibrationValue(int value)
        {
            throw new NotImplementedException();
        }

        public ModbusCmdBuilder SetDivisionValue(int value)
        {
            throw new NotImplementedException();
        }
    }
}
