using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Generateed：2011-04-01
    /// Description：字符串对象-工具类
    /// </summary>
    public partial class StringHelper
    {
        #region 随机数

        /// <summary>
        /// 创建一个指定长度的随机验证码
        /// </summary>
        /// <param name="length">验证码的位数</param>
        /// <param name="isNum">是否为纯数字</param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length, bool isNum = false)
        {
            return isNum ? GenerateRandomNumCode(length) : GenerateRandomVerifyCode(length);
        }

        /// <summary>
        /// 创建一个指定长度的随机编码(纯数字)
        /// </summary>
        /// <param name="length">验证码的位数</param>
        /// <returns>生成的随机纯数字的验证码</returns>
        private static string GenerateRandomNumCode(int length)
        {
            var rand = new Random();
            var relust = string.Empty;
            for (var i = 0; i < length; i++)
                relust += rand.Next(0, 10);

            return relust;
        }

        /// <summary>
        /// 创建随机码
        /// </summary>
        /// <param name="length">验证码的位数</param>
        /// <returns>生成的随机验证码</returns>
        private static string GenerateRandomVerifyCode(int length)
        {
            var arrChar = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var relust = string.Empty;

            var rand = new Random(Guid.NewGuid().GetHashCode());
            for (var i = 0; i < length; i++)
                relust += arrChar[rand.Next(0, 10)];

            return relust;
        }

        #endregion 随机数

        /// <summary>
        ///  Base64 转 符串
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static string Base64ToString(string content, Encoding encode)
        {
            try
            {
                var bytes = Convert.FromBase64String(content);
                return encode.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据文件后缀来获取MIME类型字符串
        /// </summary>
        /// <param name="extension">文件后缀 如.jpg</param>
        /// <returns></returns>
        public static string GetMimeType(string extension)
        {
            switch (extension.ToLower().Trim())
            {
                case ".avi":
                    return "video/x-msvideo";

                case ".html":
                    return "text/html";

                case ".htm":
                    return "text/html";

                case ".shtml":
                    return "text/html";

                case ".css":
                    return "text/css";

                case ".js":
                    return "text/javascript";

                case ".json":
                    return "application/json";

                case ".doc":
                    return "application/msword";

                case ".dot":
                    return "application/msword";

                case ".docx":
                    return "application/msword";

                case ".xla":
                    return "application/vnd.ms-excel";

                case ".xls":
                    return "application/vnd.ms-excel";

                case ".xlsx":
                    return "application/msexcel";

                case ".ppt":
                    return "application/vnd.ms-powerpoint";

                case ".pptx":
                    return "application/mspowerpoint";

                case ".gz":
                    return "application/gzip";

                case ".gif":
                    return "image/gif";

                case ".bmp":
                    return "image/bmp";

                case ".jpeg":
                    return "image/jpeg";

                case ".jpg":
                    return "image/jpeg";

                case ".jpe":
                    return "image/jpeg";

                case ".png":
                    return "image/jpeg";

                case ".mpeg":
                    return "video/mpeg";

                case ".mpg":
                    return "video/mpeg";

                case ".mpe":
                    return "video/mpeg";

                case ".wmv":
                    return "video/mpeg";

                case ".mp3":
                    return "audio/mpeg";

                case ".wma":
                    return "audio/mpeg";

                case ".pdf":
                    return "application/pdf";

                case ".txt":
                    return "text/plain";

                case ".zip":
                    return "application/zip";

                case ".swf":
                    return "application/x-shockwave-flash";

                case ".ram":
                    return "audio/x-pn-realaudio";

                case ".rmvb":
                    return "video/vnd.rn-realvideo";

                default:
                    return "application/octet-stream";
            }
        }

        /// <summary>
        /// 去除非法字符，防止SQL注入，并截取指定位数的字符
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="maxLength">截取长度</param>
        /// <returns></returns>
        public static string InputText(string content, int maxLength = 0)
        {
            if (VerifyDataHelper.IsNullOrWhiteSpace(content)) return string.Empty;

            StringBuilder retVal = new StringBuilder();

            content = content.Trim();

            if (content.Length > maxLength && maxLength > 0)
                content = content.Substring(0, maxLength);

            for (int i = 0; i < content.Length; i++)
            {
                switch (content[i])
                {
                    case '"':
                        retVal.Append("&quot;");
                        break;

                    case '<':
                        retVal.Append("&lt;");
                        break;

                    case '>':
                        retVal.Append("&gt;");
                        break;

                    default:
                        retVal.Append(content[i]);
                        break;
                }
            }

            retVal.Replace("'", " ");

            return retVal.ToString();
        }

        /// <summary>
        /// 去除空格字符
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string RemoveSpace(string content)
        {
            return new Regex(@"\s").Replace(content + "", string.Empty);
        }

        /// <summary>
        /// 替换换行符
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="newValue">替换后的内容</param>
        /// <returns></returns>
        public static string ReplaceNewline(string content, string newValue)
        {
            return (content + "").Replace("\n\r", newValue).Replace("\r\n", newValue).Replace("\r", newValue).Replace("\n", newValue).Replace("\t", newValue);
        }

        /// <summary>
        /// 替换全角数字
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string ReplaceSBCNumber(string content)
        {
            return content.Replace("０", "0").Replace("１", "1").Replace("２", "2").Replace("３", "3").Replace("４", "4").Replace("５", "5").Replace("６", "6").Replace("７", "7").Replace("８", "8").Replace("９", "9");
        }

        /// <summary>
        /// 把元素以参数名称升序排序，并按照指定的格式拼接成新的字符串
        /// 【常用于：百度/支付宝-生成加密Sign】
        /// </summary>
        /// <param name="parameters">数据源</param>
        /// <param name="concatSymbol">连接符</param>
        /// <param name="separatorSymbol">分隔符</param>
        /// <param name="passNullKey">是否跳过Key为空的数据</param>
        /// <param name="passNullValue">是否跳过Value为空的数据</param>
        /// <param name="notInKeys">需要跳过处理Keys</param>
        /// <returns>拼接完成的字符串</returns>
        public static string SortedJoinString(Dictionary<string, object> parameters, string concatSymbol = "=", string separatorSymbol = "&", bool passNullKey = true, bool passNullValue = false, List<string> notInKeys = null)
        {
            try
            {
                if (parameters == null || parameters.Count <= 0)
                    throw new Exception("Dictionary Is Null");

                var sortedParams = new SortedDictionary<string, object>(parameters);
                var iterator = sortedParams.GetEnumerator();
                var basestring = new List<string>();
                while (iterator.MoveNext())
                {
                    var key = iterator.Current.Key;
                    var value = iterator.Current.Value;

                    if (passNullKey && VerifyDataHelper.IsNullOrWhiteSpace(key))
                        continue;

                    if (passNullValue && value == null)
                        continue;

                    if (notInKeys == null || !notInKeys.Contains(key))
                        basestring.Add(key + concatSymbol + value);
                }

                return string.Join(separatorSymbol, basestring);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 截取指定长度的字符串，如果字符串原内容大于指定截取长度，则结尾处拼接“结束符号”
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="maxLength">截取长度</param>
        /// <param name="removeSpace">是否去掉空格</param>
        /// <param name="removeHtml">是否去掉HTML</param>
        /// <param name="endChars">结束符号</param>
        /// <returns></returns>
        public static string SubString(string content, int maxLength, bool removeSpace = false, bool removeHtml = false, string endChars = "…")
        {
            if (removeSpace)
                content = RemoveSpace(content);

            if (removeHtml)
                content = RemoveHTML(content);

            if (content.Length > maxLength)
                return content.Substring(0, maxLength) + endChars;
            else
                return content;
        }

        /// <summary>
        /// 字符串 转 Base64
        /// </summary>
        /// <param name="content">内容</param>
        ///  <param name="encode">编码</param>
        /// <returns></returns>
        public static string ToBase64String(string content, Encoding encode)
        {
            try
            {
                var bytes = encode.GetBytes(content);
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将手机号转换为 186****5720
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public static string ToFuzzyMobile(string mobile)
        {
            return VerifyDataHelper.IsMobile(mobile) ? mobile.Substring(0, 3) + "****" + mobile.Substring(7) : "";
        }

        /// <summary>
        /// 拼接
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="values">内容</param>
        /// <param name="separator">拼接字符</param>
        /// <returns></returns>
        public static string ToJoin<T>(IEnumerable<T> values, string separator = ",")
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// 得到内容中的Unicode字符
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static List<string> GetUnicodes(string content)
        {
            try
            {
                MatchCollection mc = Regex.Matches(content, @"\\u(?<value>[0-9a-f]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                if (mc == null || mc.Count <= 0)
                    return new List<string> { };

                List<string> fieldItems = new List<string>();
                for (int idx = 0; idx < mc.Count; idx++)
                {
                    if (!fieldItems.Contains(mc[idx].Groups["value"].Value))
                        fieldItems.Add(@"\u" + mc[idx].Groups["value"].Value);
                }
                return fieldItems;
            }
            catch (Exception ex)
            {
                return new List<string> { };
            }
        }

        /// <summary>
        /// Unicode 转 中文
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string UnicodeDecode(string content)
        {
            try
            {
                string outStr = "";
                Regex reg = new Regex(@"\\u([0-9a-f]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                outStr = reg.Replace(content, delegate (Match m1)
                {
                    return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();
                });
                return outStr;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Unicode 转 中文
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string UnicodeDecode2(string content)
        {
            string outStr = "";
            MatchCollection mc = Regex.Matches(content, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            if (mc != null && mc.Count > 0)
            {
                try
                {
                    foreach (Match m in mc)
                    {
                        bts[0] = (byte)int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                        bts[1] = (byte)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                        outStr += Encoding.Unicode.GetString(bts);
                    }
                }
                catch (Exception e)
                {
                }
            }
            return outStr;
        }

        /// <summary>
        /// 将已经为在 URL 中传输而编码的字符串转换为解码的字符串
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string UrlDecode(string content)
        {
            return HttpUtility.UrlDecode(content);
        }

        /// <summary>
        /// 对 URL 字符串进行编码
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string UrlEncode(string content)
        {
            return HttpUtility.UrlEncode(content);
        }
    }
}