namespace cn.tdr.common
{
    /// <summary>
    /// 一般性工具函数
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// 生成新的 GUID 对象，并返回新对象的 16 位长度格式的字符串
        /// </summary>
        /// <returns></returns>
        public static string GuidToString16()
        {
            long i = 1;
            foreach (byte b in System.Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - System.DateTime.Now.Ticks);
        }

        /// <summary>
        /// 生成新的 GUID 对象，并返回新对象的字符串
        /// </summary>
        /// <returns></returns>
        public static string GuidToString()
        {
            return System.Guid.NewGuid().ToString("N");
        }
    }
}
