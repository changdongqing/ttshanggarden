// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Util.Extensions;
using System.Reflection;
using System.Text;

namespace TTShang.Core.Util
{
    /// <summary>
    /// 生成种子数据工具
    /// </summary>
    public static class SeedDataGenerateHelper
    {
        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="typeName"></param>
        /// <param name="maxDepth"></param>
        /// <param name="excludeFields"></param>
        /// <returns></returns>
        /// <remarks>
        /// 符合ef的种子数据
        /// </remarks>
        public static string GenerateSeedData(IEnumerable<object> datas, string? typeName = null, int maxDepth = 3, params string[] excludeFields)
        {
            if (datas == null || !datas.Any())
            {
                return string.Empty;
            }
            typeName = typeName ?? datas.First().GetType().Name;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/// <summary>");
            sb.AppendLine("/// 种子数据");
            sb.AppendLine("/// </summary>");
            sb.AppendLine($"public class {typeName}SeedData : IEntitySeedData<{typeName}>");
            sb.AppendLine("{");
            sb.AppendLine("     /// <summary>");
            sb.AppendLine("     /// 种子数据");
            sb.AppendLine("     /// </summary>");
            sb.AppendLine("     /// <param name=\"dbContext\"></param>");
            sb.AppendLine("     /// <param name=\"dbContextLocator\"></param>");
            sb.AppendLine("     /// <returns></returns>");
            sb.AppendLine($"    public IEnumerable<{typeName}> HasData(DbContext dbContext, Type dbContextLocator)");
            sb.AppendLine("     {");
            sb.AppendLine("         return " + Generate(datas, typeName, maxDepth, excludeFields));
            sb.AppendLine("     }");
            sb.AppendLine("}");
            return sb.ToString();
        }
        /// <summary>
        /// 生成数据
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="typeName"></param>
        /// <param name="maxDepth"></param>
        /// <param name="excludeFields"></param>
        /// <returns></returns>
        /// <remarks>
        /// 生成对象实例化语句
        /// </remarks>
        public static string Generate(IEnumerable<object> datas, string? typeName = null, int maxDepth = 3, params string[] excludeFields)
        {
            if (datas == null || !datas.Any())
            {
                return string.Empty;
            }
            typeName = typeName ?? datas.First().GetType().Name;

            return Generate(datas, typeName, maxDepth, 0, new Dictionary<object, int>(), excludeFields);
        }
        /// <summary>
        /// 生成数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="typeName"></param>
        /// <param name="maxDepth"></param>
        /// <param name="excludeFields"></param>
        /// <returns></returns>
        public static string Generate(object data, string? typeName = null, int maxDepth = 3, params string[] excludeFields)
        {
            typeName = typeName ?? data.GetType().Name;

            return Generate(data, typeName, maxDepth, 0, new Dictionary<object, int>(), excludeFields);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="typeName"></param>
        /// <param name="maxDepth"></param>
        /// <param name="currentDepth"></param>
        /// <param name="references"></param>
        /// <param name="excludeFields"></param>
        /// <returns></returns>
        private static string Generate(object data, string typeName, int maxDepth, int currentDepth, Dictionary<object, int> references, params string[] excludeFields)
        {
            currentDepth++;
            //大于深度
            if (currentDepth > maxDepth)
            {
                return string.Empty;
            }
            //循环引用
            if (references.ContainsKey(data))
            {
                return string.Empty;
            }
            string placeholder = string.Empty;
            for (int i = 0; i < currentDepth; i++)
            {
                placeholder += "    ";
            }
            references.Add(data, currentDepth);
            StringBuilder sb = new StringBuilder();
            if (data == null) { return string.Empty; }
            Type type = data.GetType();
            PropertyInfo[] properties = type.GetProperties();
            sb.AppendLine($"{placeholder}new {typeName}()");
            sb.AppendLine(placeholder + "{");
            List<string> propertieNames = new List<string>();
            foreach (PropertyInfo property in properties.OrderBy(x => x.Name))
            {
                string propertyName = property.Name;
                if (excludeFields.Contains(propertyName))
                {
                    continue;
                }
                //重复名称（派生类中）
                if (propertieNames.Contains(propertyName))
                {
                    continue;
                }
                propertieNames.Add(propertyName);
                var propertyType = property.PropertyType.GetUnNullableType();
                object? value = data.GetPropertyValue(propertyName);
                if (value == null)
                {
                    continue;
                }
                string? filedCode = null;
                if (propertyType.Equals(typeof(string)) || propertyType.Equals(typeof(char)))
                {
                    filedCode = $"{propertyName}=\"{value}\"";
                }
                else if (propertyType.Equals(typeof(Guid)))
                {
                    filedCode = $"{propertyName}=new Guid(\"{value}\")";
                }
                else if (propertyType.Equals(typeof(short))
                   || propertyType.Equals(typeof(int))
                   || propertyType.Equals(typeof(long))
                   || propertyType.Equals(typeof(float))
                   || propertyType.Equals(typeof(double))
                   || propertyType.Equals(typeof(decimal))
                   || propertyType.Equals(typeof(byte))
                   || propertyType.Equals(typeof(bool))
                   )
                {
                    filedCode = $"{propertyName}={(value.ToString() ?? "").ToLower()}";
                }
                else if (propertyType.Equals(typeof(DateTimeOffset)) || propertyType.Equals(typeof(DateTime)))
                {
                    if (value != null)
                    {
                        DateTimeOffset time = (DateTimeOffset)value;
                        filedCode = $"{propertyName}={typeof(DateTimeOffset).Name}.Parse(\"{time.ToString("yyyy-MM-dd HH:mm:ss")}\")";
                    }
                }
                else if (propertyType.IsEnum)
                {
                    filedCode = $"{propertyName}={propertyType.Name}.{value.ToString()}";
                }
                else if (propertyType.IsEnumerable())
                {
                    IEnumerable<object> list = (IEnumerable<object>)value;
                    if (list.Any())
                    {
                        filedCode = Generate((IEnumerable<object>)value, list.First().GetType().GetUnNullableType().Name, maxDepth, currentDepth, references, excludeFields);
                        if (!string.Empty.Equals(filedCode))
                        {
                            filedCode = $"{propertyName}={filedCode}";
                        }
                    }
                }
                else if (propertyType.IsClass)
                {
                    filedCode = Generate(value, propertyType.Name, maxDepth, currentDepth, references, excludeFields);
                    if (!string.Empty.Equals(filedCode))
                    {
                        filedCode = $"{propertyName}={filedCode}";
                    }
                }
                if (filedCode != null)
                {
                    sb.AppendLine($"{placeholder}   {filedCode},");
                }
            }

            sb.AppendLine(placeholder + "}");
            return sb.ToString();
        }
        /// <summary>
        /// 生成数据
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="typeName"></param>
        /// <param name="maxDepth"></param>
        /// <param name="currentDepth"></param>
        /// <param name="references"></param>
        /// <param name="excludeFields"></param>
        /// <returns></returns>
        private static string Generate(IEnumerable<object> datas, string typeName, int maxDepth, int currentDepth, Dictionary<object, int> references, params string[] excludeFields)
        {
            if (datas == null || !datas.Any())
            {
                return string.Empty;
            }
            string placeholder = string.Empty;
            for (int i = 0; i < currentDepth; i++)
            {
                placeholder += "    ";
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("new[]{");
            long index = 0;
            foreach (var item in datas)
            {
                string itemCode = Generate(item, typeName, maxDepth, currentDepth, references, excludeFields);
                if (itemCode != string.Empty)
                {
                    itemCode = placeholder + itemCode;

                    if (index < datas.Count() - 1)
                    {
                        sb.AppendLine(itemCode + ",");
                    }
                    else
                    {
                        sb.AppendLine(itemCode);
                    }
                }
                index++;
            }
            sb.Append(placeholder + "}");
            return sb.ToString();
        }
    }
}
