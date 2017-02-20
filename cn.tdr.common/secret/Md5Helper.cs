namespace cn.tdr.common.secret
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// MD5 加密相关处理
    /// </summary>
    public static class Md5Helper
    {
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="source">需要加密的数据</param>
        /// <param name="encodingName">编码</param>
        /// <param name="extentions">加密扩展数据</param>
        /// <returns></returns>
        public static string ToMd5(this string source, string encodingName, params string[] extentions)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var arr =
                extentions
                .Concat(new string[] { source })
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .OrderBy(t => t);
                var valstr = string.Join("", arr);
                var encode = Encoding.GetEncoding(encodingName);
                var buffer = encode.GetBytes(valstr);
                buffer = md5.ComputeHash(buffer, 0, buffer.Length);
                valstr = encode.GetString(buffer);
                return valstr;
            }
        }

        /// <summary>
        /// 基于 UTF-8 编码 MD5 加密
        /// </summary>
        /// <param name="source">需要加密的数据</param>
        /// <param name="extentions">加密扩展数据</param>
        /// <returns></returns>
        public static string ToMd5(this string source, params string[] extentions)
        {
            return source.ToMd5("utf-8", extentions);
        }
    }
}
