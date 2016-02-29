using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：字典对象-工具类
    /// </summary>
    public class DictionaryHelper
    {
        /// <summary>
        /// 将XML转换成字典
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static Dictionary<string, string> FromXml(string xml)
        {
            try
            {
                if (string.IsNullOrEmpty(xml))
                    throw new Exception("XML Is Null");

                var result = new Dictionary<string, string> { };
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                XmlNode xmlNode = xmlDoc.FirstChild;
                XmlNodeList nodes = xmlNode.ChildNodes;
                foreach (XmlNode xn in nodes)
                {
                    XmlElement xe = (XmlElement)xn;
                    result[xe.Name] = xe.InnerText;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将对象转换成字典
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="notInKeys">需要跳过处理Keys</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(object obj, List<string> notInKeys = null)
        {
            try
            {
                if (obj == null)
                    throw new Exception("Object Is Null");
                var result = new Dictionary<string, object> { };
                PropertyInfo[] Properties = obj.GetType().GetProperties();

                foreach (PropertyInfo p in Properties)
                {
                    if (notInKeys == null || !notInKeys.Contains(p.Name))
                        result.Add(p.Name, p.GetValue(obj, null));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}