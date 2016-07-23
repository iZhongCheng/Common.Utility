using System;
using System.Configuration;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：配置文件-工具类
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// 获取 web.config 数据库连接字符串
        /// </summary>
        /// <param name="name">字符串名</param>
        /// <returns></returns>
        public static string GetConnection(string name)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[name].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 读取 web.config 配置
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}