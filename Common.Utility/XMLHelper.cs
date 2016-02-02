using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：XML对象-工具类
    /// </summary>
    public class XMLHelper
    {
        /// <summary>
        /// 序列化XML字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ToWeChatXml(object obj)
        {
            try
            {
                if (obj == null)
                    throw new Exception("Object Is Null");
                PropertyInfo[] Properties = obj.GetType().GetProperties();
                string xml = "<xml>";
                foreach (PropertyInfo p in Properties)
                {
                    xml += "<" + p.Name + ">" + "<![CDATA[" + p.GetValue(obj, null) + "]]></" + p.Name + ">";
                }

                xml += "</xml>";
                return xml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 序列化XML字符串
        /// </summary>
        /// <param name="obj">字典对象</param>
        /// <returns></returns>
        public static string ToWeChatXml(Dictionary<string, object> obj)
        {
            try
            {
                if (obj == null || obj.Count <= 0)
                    throw new Exception("Object Is Null");

                string xml = "<xml>";
                foreach (var key in obj.Keys)
                {
                    xml += "<" + key + ">" + "<![CDATA[" + obj[key] + "]]></" + key + ">";
                }
                xml += "</xml>";
                return xml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 序列化XML字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ToXml(object obj, Encoding encoding, bool? removeXmlHeader = false)
        {
            var xml = "";
            MemoryStream stream = null;

            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = encoding;
                settings.Indent = true;
                settings.OmitXmlDeclaration = removeXmlHeader.Value;

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                XmlSerializer xs = new XmlSerializer(obj.GetType());

                stream = new MemoryStream();
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    xs.Serialize(writer, obj, ns);
                }
                xml = encoding.GetString(stream.ToArray());
                return xml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }
    }
}