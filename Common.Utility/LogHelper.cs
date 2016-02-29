using System;
using System.IO;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：日志对象-工具类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logPath">日志路径</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static bool Write(string logPath, string content)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(logPath))
                    throw new Exception("日志路径不能为空！");

                var folderPath = Path.GetDirectoryName(logPath);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                File.AppendAllText(logPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + content + Environment.NewLine);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}