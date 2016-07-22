using System;
using System.Diagnostics;
using System.Linq;

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
        /// 获取导致异常的代码行号
        /// 注：Debug 模式下才是源码里的代码行号，Release 模式下代码由于被优化，行号可能不准
        /// 返回 -1 表示异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>代码行号</returns>
        public static int GetErrorFileLineNumber(Exception ex)
        {
            try
            {
                var trace = new StackTrace(ex, true);
                var index = trace.FrameCount == 1 ? 0 : trace.FrameCount - 1;
                return trace.GetFrame(index).GetFileLineNumber();
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取导致异常的代码文件名
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>代码文件名</returns>
        public static string GetErrorFileName(Exception ex)
        {
            try
            {
                var trace = new StackTrace(ex, true);
                var index = trace.FrameCount == 1 ? 0 : trace.FrameCount - 1;
                return trace.GetFrame(index).GetFileName();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取导致异常的错误原因
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>错误原因</returns>
        public static string GetErrorMessage(Exception ex)
        {
            try
            {
                var trace = new StackTrace(ex, true);
                var fs = trace.GetFrames().Select(c => c.GetMethod().Name).Reverse();
                return string.Join(" -> ", fs) + " -> " + ex.Message;
            }
            catch
            {
                return string.Empty;
            }
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

        #region 获取值

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetValue(object obj, string defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString();
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static byte GetValue(object obj, byte defaultValue)
        {
            byte result = defaultValue;
            return (byte.TryParse(GetValue(obj, defaultValue.ToString()), out result)) ? result : defaultValue;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetValue(object obj, int defaultValue)
        {
            int result = defaultValue;
            return (int.TryParse(GetValue(obj, defaultValue.ToString()), out result)) ? result : defaultValue;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetValue(object obj, bool defaultValue)
        {
            bool result = defaultValue;
            return (bool.TryParse(GetValue(obj, defaultValue.ToString()), out result)) ? result : defaultValue;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static long GetValue(object obj, long defaultValue)
        {
            long result = defaultValue;
            return (long.TryParse(GetValue(obj, defaultValue.ToString()), out result)) ? result : defaultValue;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime GetValue(object obj, DateTime defaultValue)
        {
            DateTime result = defaultValue;
            return (DateTime.TryParse(GetValue(obj, defaultValue.ToString()), out result)) ? result : defaultValue;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static double GetValue(object obj, double defaultValue)
        {
            double result = defaultValue;
            return (double.TryParse(GetValue(obj, defaultValue.ToString()), out result)) ? result : defaultValue;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal GetValue(object obj, decimal defaultValue)
        {
            decimal result = defaultValue;
            return (decimal.TryParse(GetValue(obj, defaultValue.ToString()), out result)) ? result : defaultValue;
        }

        #endregion 获取值
    }
}