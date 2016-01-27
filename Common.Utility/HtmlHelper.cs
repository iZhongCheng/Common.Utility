using System.Collections.Generic;
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
        public static string RemoveHTML(string content)
        {
            content = HTMLSymbolDecode(content);
            return FilterString(content, FilterOptions.Postil | FilterOptions.Font | FilterOptions.Tab | FilterOptions.Spaces | FilterOptions.NewLine | FilterOptions.Enter);
        }

        #region 过滤元素

        public enum FilterOptions
        {
            HTML = 1,
            Img = 2,
            Font = 4,
            Link = 8,
            Div = 16,
            Postil = 32,
            Script = 64,
            NewLine = 128,
            Enter = 256,
            Spaces = 512,
            Space = 1024,
            Tab = 2048
        }

        public static string FilterString(string content, FilterOptions options)
        {
            string result = content;

            if ((options & FilterOptions.Postil) > 0)
                result = FilterPostil(result);

            if ((options & FilterOptions.HTML) > 0)
                result = FilterHtml(result);

            if ((options & FilterOptions.Img) > 0)
                result = FilterImg(result);

            if ((options & FilterOptions.Font) > 0)
                result = FilterFont(result);

            if ((options & FilterOptions.Link) > 0)
                result = FilterLink(result);

            if ((options & FilterOptions.Div) > 0)
                result = FilterDiv(result);

            if ((options & FilterOptions.Script) > 0)
                result = FilterScript(result);

            if ((options & FilterOptions.NewLine) > 0)
                result = FilterNewLine(result);

            if ((options & FilterOptions.Enter) > 0)
                result = FilterEnter(result);

            if ((options & FilterOptions.Tab) > 0)
                result = FilterTab(result);

            if ((options & FilterOptions.Space) > 0)
                result = FilterSpace(result);

            if ((options & FilterOptions.Spaces) > 0)
                result = FilterSpaces(result);

            List<string> lstUnicode = GetUnicodes(result);
            foreach (string unicode in lstUnicode)
            {
                string str = UnicodeDecode2(unicode);
                if (!"".Equals(str))
                    result = Regex.Replace(result, unicode, str, RegexOptions.IgnoreCase);
            }

            return result;
        }

        private static string FilterDiv(string content)
        {
            string pattern = "<div[^>]*>";
            string result = Regex.Replace(content, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            pattern = "</div>";
            return Regex.Replace(result, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static string FilterEnter(string content)
        {
            string pattern = "\n";
            return Regex.Replace(content, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static string FilterFont(string content)
        {
            string pattern = "<font[^>]*>";
            string result = Regex.Replace(content, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            pattern = "</font>";
            return Regex.Replace(result, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static string FilterHtml(string content)
        {
            string pattern = "<.*?>";
            return Regex.Replace(content, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static string FilterImg(string content)
        {
            string pattern = "<img[^>]*>";
            return Regex.Replace(content, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static string FilterLink(string content)
        {
            string pattern = "<a[^>]*>";
            string result = Regex.Replace(content, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            pattern = "</a>";
            return Regex.Replace(result, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static string FilterNewLine(string content)
        {
            string pattern = "\r";
            return Regex.Replace(content, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static string FilterPostil(string content)
        {
            string pattern = "<!\\-\\-.*?\\-\\->";
            return Regex.Replace(content, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static string FilterScript(string content)
        {
            string pattern = "<script[^>]*>[^<]*</script>";
            return Regex.Replace(content, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static string FilterSpace(string content)
        {
            return content.Replace("&nbsp;", "").Replace(" ", "");
        }

        private static string FilterSpaces(string content)
        {
            content = content.Replace("&nbsp;", " ");
            string pattern = "[ ]{2,}";
            content = Regex.Replace(content, pattern, " ", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            pattern = "\\>[ ]*\\<";
            return Regex.Replace(content, pattern, "><", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static string FilterTab(string content)
        {
            string pattern = "\t";
            return Regex.Replace(content, pattern, "", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        #endregion 过滤元素
    }
}