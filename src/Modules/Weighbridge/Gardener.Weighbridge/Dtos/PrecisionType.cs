// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Weighbridge.Dtos
{
    /// <summary>
    /// 小数位类型
    /// </summary>
    public struct PrecisionType
    {
        /// <summary>
        /// 小数位类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        public PrecisionType(byte value, string name)
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        /// 类型值
        /// </summary>
        public byte Value { get; init; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// 个位
        /// </summary>
        public static PrecisionType Single = new PrecisionType(0, "个位");
        /// <summary>
        /// 十分位
        /// </summary>
        public static PrecisionType Tenths = new PrecisionType(1, "十分位");
        /// <summary>
        /// 百分位
        /// </summary>
        public static PrecisionType Percentile = new PrecisionType(2, "百分位");
        /// <summary>
        /// 千分位 
        /// </summary>
        public static PrecisionType Thousands = new PrecisionType(3, "千分位");
        /// <summary>
        /// 所有类型
        /// </summary>
        public static IEnumerable<PrecisionType> Types = [Single, Tenths, Percentile, Thousands];
        /// <summary>
        /// 获取小数位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static PrecisionType? GetPrecisionType(byte value)
        {
            foreach (var type in Types)
            {
                if (type.Value == value) return type;
            }
            return null;
        }
        /// <summary>
        /// 按小数位计算数值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Calculate(double value)
        {
            switch (this.Value)
            {
                case 0: return value;
                case 1: return value/10;
                case 2: return value/100;
                case 3: return value/1000;
                default: return value;
            }
        }
    }
}
