// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Iot.Dtos
{
    /// <summary>
    /// JSON字段类型
    /// </summary>
    public struct JsonDataFieldType
    {
        /// <summary>
        /// JSON字段类型
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// JSON字段类型
        /// </summary>
        public Type Type { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public JsonDataFieldType(Type type)
        {
            Type = type;
            Name = type.Name;
        }
        /// <summary>
        /// String
        /// </summary>
        public static JsonDataFieldType String = new JsonDataFieldType(typeof(string));
        /// <summary>
        /// Boolean
        /// </summary>
        public static JsonDataFieldType Boolean = new JsonDataFieldType(typeof(bool));
        /// <summary>
        /// Short
        /// </summary>
        public static JsonDataFieldType Short = new JsonDataFieldType(typeof(short));
        /// <summary>
        /// Int
        /// </summary>
        public static JsonDataFieldType Int = new JsonDataFieldType(typeof(int));
        /// <summary>
        /// Long
        /// </summary>
        public static JsonDataFieldType Long = new JsonDataFieldType(typeof(long));
        /// <summary>
        /// Float
        /// </summary>
        public static JsonDataFieldType Float = new JsonDataFieldType(typeof(float));
        /// <summary>
        /// Decimal
        /// </summary>
        public static JsonDataFieldType Decimal = new JsonDataFieldType(typeof(decimal));
        /// <summary>
        /// Double
        /// </summary>
        public static JsonDataFieldType Double = new JsonDataFieldType(typeof(double));
        /// <summary>
        /// DateTime
        /// </summary>
        public static JsonDataFieldType DateTime = new JsonDataFieldType(typeof(DateTime));
        /// <summary>
        /// Array
        /// </summary>
        public static JsonDataFieldType Array = new JsonDataFieldType(typeof(Array));
        /// <summary>
        /// Object
        /// </summary>
        public static JsonDataFieldType Object = new JsonDataFieldType(typeof(Object));
        /// <summary>
        /// Types
        /// </summary>
        public static IEnumerable<JsonDataFieldType> Types = new[] { String, Boolean, Short, Int, Long, Float, Decimal, Double, DateTime, Array, Object };
    }
}
