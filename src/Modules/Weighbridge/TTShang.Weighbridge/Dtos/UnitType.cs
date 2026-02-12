namespace TTShang.Weighbridge.Dtos
{
    /// <summary>
    /// 单位类型
    /// </summary>
    public struct UnitType
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public byte Value { get; init; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// 类型值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        public UnitType(byte value, string name)
        {
            Value = value;
            Name = name;
        }
        /// <summary>
        /// 克
        /// </summary>
        public static UnitType MPa = new UnitType(1, nameof(MPa));
        /// <summary>
        /// 千克
        /// </summary>
        public static UnitType Kg = new UnitType(2, nameof(Kg));
        /// <summary>
        /// 吨
        /// </summary>
        public static UnitType T = new UnitType(3, nameof(T));
        /// <summary>
        /// 克
        /// </summary>
        public static UnitType G = new UnitType(4, nameof(G));
        /// <summary>
        /// 牛
        /// </summary>
        public static UnitType N = new UnitType(5, nameof(N));
        /// <summary>
        /// 千牛
        /// </summary>
        public static UnitType KN = new UnitType(6, nameof(KN));

        /// <summary>
        /// 所有类型
        /// </summary>
        public static IEnumerable<UnitType> Types = [MPa, Kg, T, G, N, KN];
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UnitType? GetUnitType(byte value)
        {
            foreach (var item in Types)
            {
                if (item.Value.Equals(value))
                {

                    return item;
                }
            }
            return null;
        }
    }
}
