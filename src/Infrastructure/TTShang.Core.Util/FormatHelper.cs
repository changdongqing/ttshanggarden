namespace TTShang.Core.Util
{
    /// <summary>
    /// 格式化工具
    /// </summary>
    public static class FormatHelper
    {
        /// <summary>
        /// 字节格式化
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetBytesReadable(long i)
        {
            // Get absolute value
            long absolute_i = (i < 0 ? -i : i);
            // Determine the suffix and readable value
            string suffix;
            double readable;
            if (absolute_i >= 0x1000000000000000) // EB
            {
                suffix = "EB";
                readable = (i >> 50);
            }
            else if (absolute_i >= 0x4000000000000) // PB
            {
                suffix = "PB";
                readable = (i >> 40);
            }
            else if (absolute_i >= 0x10000000000) // TB
            {
                suffix = "TB";
                readable = (i >> 30);
            }
            else if (absolute_i >= 0x40000000) // GB
            {
                suffix = "GB";
                readable = (i >> 20);
            }
            else if (absolute_i >= 0x100000) // MB
            {
                suffix = "MB";
                readable = (i >> 10);
            }
            else if (absolute_i >= 0x400) // KB
            {
                suffix = "KB";
                readable = i;
            }
            else
            {
                return i.ToString("0 B"); // B
            }
            readable = (readable / 1024);
            return readable.ToString("0.### ") + suffix;
        }
    }
}
