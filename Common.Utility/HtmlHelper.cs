using System;
using System.Text.RegularExpressions;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Generateed：2011-04-01
    /// Description：Html-工具类
    /// </summary>
    public partial class StringHelper
    {
        /// <summary>
        /// 将HTML字符替换成英文符号
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string HTMLSymbolDecode(string content)
        {
            return content.Replace("&nbsp;", " ").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&pound;", "£").Replace("&yen;", "¥").Replace("&euro;", "€").Replace("&copy;", "©").Replace("&reg;", "®").Replace("&trade;", "™").Replace("&times;", "×").Replace("&divide;", "÷").Replace("&brvbar;", "¦");
        }

        /// <summary>
        /// 将英文符号替换成HTML字符
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string HTMLSymbolEncode(string content)
        {
            return content.Replace(" ", "&nbsp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("\"", "&quot;").Replace("£", "&pound;").Replace("¥", "&yen;").Replace("€", "&euro;").Replace("©", "&copy;").Replace("®", "&reg;").Replace("™", "&trade;").Replace("×", "&times;").Replace("÷", "&divide;").Replace("¦", "&brvbar;");
        }

        /// <summary>
        /// 去除HTML标签
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string RemoveHTML(string content, FilterOptions options = FilterOptions.Html|FilterOptions.Head|FilterOptions.Meta|FilterOptions.Link|FilterOptions.Script)
        {
            try
            {
                content = content.Replace("&nbsp;", " ").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&pound;", "£").Replace("&yen;", "¥").Replace("&euro;", "€").Replace("&copy;", "©").Replace("&reg;", "®").Replace("&trade;", "™").Replace("&times;", "×").Replace("&divide;", "÷").Replace("&brvbar;", "¦");
                content = FilterNewLine(content);
                content = FilterSpace(content);
                content = Regex.Replace(content, "(<doctype|<!doctype)([^>])*(>)", "$1$3", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, "(<)(html|head|title|meta|link|script|body|center|strong|form|textarea|input|table|span|font|img|div|h1|h2|h3|td|tr|ol|ul|li|br|tt|em|a|b|s|i|p)([^>])*(>)", "$1$2$4", RegexOptions.IgnoreCase);

                if ((options & FilterOptions.Head) > 0)
                    content = FilterHead(content);

                if ((options & FilterOptions.Meta) > 0)
                    content = FilterMeta(content);

                if ((options & FilterOptions.Link) > 0)
                    content = FilterLink(content);

                if ((options & FilterOptions.Script) > 0)
                    content = FilterScript(content);

                if ((options & FilterOptions.Html) > 0)
                    content = FilterHtml(content);

                return content;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        #region 过滤元素

        public enum FilterOptions
        {
            /// <summary>
            /// Html 标签
            /// </summary>
            Html = 1,

            /// <summary>
            /// 头部
            /// </summary>
            Head = 2,

            /// <summary>
            /// 标签
            /// </summary>
            Meta = 3,

            /// <summary>
            /// 样式
            /// </summary>
            Link = 4,

            /// <summary>
            /// 脚本
            /// </summary>
            Script = 5
        }

        private static string FilterHead(string content)
        {
            string pattern = "<head>.*</head>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        private static string FilterHtml(string content)
        {
            string pattern = "<.*?>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        private static string FilterLink(string content)
        {
            string pattern = "<link>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        private static string FilterMeta(string content)
        {
            string pattern = "<meta>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        private static string FilterNewLine(string content)
        {
            return content.Replace("\n\r", string.Empty).Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        private static string FilterScript(string content)
        {
            string pattern = "<script>[^<]*</script>";
            content = Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);

            pattern = "<script>.*</script>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        private static string FilterSpace(string content)
        {
            return content.Replace("&nbsp;", string.Empty).Replace(" ", string.Empty).Replace("\t", string.Empty);
        }

        #endregion 过滤元素
    }
}