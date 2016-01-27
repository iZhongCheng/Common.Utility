using System;
using System.Diagnostics;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：方法对象-工具类
    /// </summary>
    public static class FunHelper
    {
        /// <summary>
        /// 获取币种
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="defaultType">默认币种</param>
        /// <returns></returns>
        public static Enums.CurrencyType GetCurrencyType(string content, Enums.CurrencyType defaultType = Enums.CurrencyType.RMB)
        {
            try
            {
                content = (content + "").ToUpper();
                if (content.Contains("$") || content.Contains("USD") || content.Contains("美元") || content.Contains("＄"))
                    return Enums.CurrencyType.USD;
                else if (content.Contains("欧元") || content.Contains("EUR"))
                    return Enums.CurrencyType.EUR;
                else if (content.Contains("HKD") || content.Contains("港币"))
                    return Enums.CurrencyType.HKD;
                else if (content.Contains("JPY") || content.Contains("日元"))
                    return Enums.CurrencyType.JPY;
                else if (content.Contains("KRW"))
                    return Enums.CurrencyType.KRW;
                else if (content.Contains("AUD"))
                    return Enums.CurrencyType.AUD;
                else if (content.Contains("CHF"))
                    return Enums.CurrencyType.CHF;
                else if (content.Contains("SGD"))
                    return Enums.CurrencyType.SGD;
                else if (content.Contains("CAD"))
                    return Enums.CurrencyType.CAD;
                else if (content.Contains("NZD"))
                    return Enums.CurrencyType.NZD;
                else if (content.Contains("THB"))
                    return Enums.CurrencyType.THB;
                else if (content.Contains("PHP"))
                    return Enums.CurrencyType.PHP;
                else if (content.Contains("GBP"))
                    return Enums.CurrencyType.GBP;

                return defaultType;
            }
            catch (Exception)
            {
                return defaultType;
            }
        }

        /// <summary>
        /// 错误代码文件名
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>错误代码文件名</returns>
        public static string GetErrorCodeFileName(Exception ex)
        {
            var trace = new StackTrace(ex, true);

            return trace.GetFrame(0).GetMethod().Name;
        }

        /// <summary>
        /// 错误代码行号
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>错误代码行号</returns>
        public static int GetErrorCodeRowNum(Exception ex)
        {
            var trace = new StackTrace(ex, true);

            return trace.GetFrame(0).GetFileLineNumber();
        }

        /// <summary>
        /// 10进制转换16进制
        /// </summary>
        /// <param name="i">int值</param>
        /// <returns></returns>
        public static string ToFString(int i)
        {
            var str = i.ToString("X");
            return str.Length % 2 == 0 ? "0x" + str : "0x0" + str;
        }
    }
}