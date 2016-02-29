using System;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：时间戳-工具类
    /// </summary>
    public class DateHelper
    {
        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="unixTimeStamp">Unix时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string unixTimeStamp)
        {
            return ToDateTime(Convert.ToInt64(unixTimeStamp));
        }

        /// <summary>
        /// 将Unix时间戳转成DateTime
        /// </summary>
        /// <param name="timestamp">13位或10位时间戳</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(long timestamp)
        {
            var ticks = DateTime.Now.ToUniversalTime().Ticks - (timestamp * (timestamp.ToString().Length == 13 ? 10000 : 10000000) + 621355968000000000);
            TimeSpan ts = new TimeSpan(ticks);
            return DateTime.Now.Add(-ts);
        }

        /// <summary>
        /// 将指定时间转换为Unix时间戳
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public static long ToUnixTimeStamp(DateTime dateTime)
        {
            double intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (dateTime - startTime).TotalSeconds;
            return (long)intResult;
        }

        /// <summary>
        /// 将指定时间转换为Unix时间戳
        /// </summary>
        /// <param name="dateTime">时间字符串</param>
        /// <returns></returns>
        public static long ToUnixTimeStamp(string dateTime)
        {
            return ToUnixTimeStamp(Convert.ToDateTime(dateTime));
        }

        /// <summary>
        /// 将当前时间转成时间戳
        /// </summary>
        /// <param name="len">13位或10位</param>
        /// <returns>时间戳</returns>
        public static long ToUnixTimeStamp(int len)
        {
            return ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / (len == 13 ? 10000 : 10000000));
        }

        /// <summary>
        /// 时间格式化
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static string ToTimeString(DateTime dt)
        {
            var item = dt - DateTime.Now;
            if (item.Days == 0 && item.Hours == 0 && item.Minutes == 0)
                return item.Seconds > 0 ? item.Seconds + "秒后" : item.Seconds * -1 + "秒前";
            else if (item.Days == 0 && item.Hours == 0)
                return item.Minutes > 0 ? item.Minutes + "分钟后" : item.Minutes * -1 + "分钟前";
            else if (item.Days == 0)
                return item.Hours > 0 ? item.Hours + "小时后" : item.Hours * -1 + "小时前";
            else if (item.Days > 0)
                return (int)item.Days + "天后";
            else
                return (int)(item.Days * -1) + "天前";
        }
    }
}