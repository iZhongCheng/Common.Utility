using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：Get/Post请求-工具类
    /// </summary>
    public class HttpHelper
    {
        private List<string> ContentTypes = new List<string> { "application/json", "application/x-www-form-urlencoded" };

        /// <summary>
        /// Http Get
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="encode">编码</param>
        /// <param name="contentType">类型</param>
        /// <param name="nvc">头</param>
        /// <param name="isGzip">是否Gzip压缩</param>
        /// <returns>返回请求结果，如果请求失败则返回空字符串</returns>
        public static string HttpGet(string url, string encode, string contentType, NameValueCollection nvc = null, bool isGzip = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                    throw new Exception("Url Is Null");

                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                if (!string.IsNullOrWhiteSpace(contentType))
                    request.ContentType = contentType;

                if (nvc != null && nvc.Count > 0)
                    request.Headers.Add(nvc);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                if (isGzip) myResponseStream = new GZipStream(myResponseStream, CompressionMode.Decompress);
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encode));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        ///  Http Get
        ///  Encoding : utf-8
        ///  ContentType : text/html;charset=UTF-8
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns>返回请求结果，如果请求失败则返回空字符串</returns>
        public static string HttpGet(string url)
        {
            return HttpGet(url, "utf-8", "application/x-www-form-urlencoded", null, false);
        }

        /// <summary>
        /// Http Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">数据</param>
        /// <param name="encode">编码</param>
        /// <param name="contentType">类型</param>
        /// <param name="nvc">头</param>
        /// <returns>返回请求结果，如果请求失败则返回空字符串</returns>
        public static string HttppPost(string url, string data, string encode, string contentType, NameValueCollection nvc)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                    throw new Exception("Url Is Null");

                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

                WebRequest hr = WebRequest.Create(url);
                byte[] buf = Encoding.GetEncoding(encode).GetBytes(data);
                hr.Method = "POST";
                hr.ContentLength = buf.Length;
                if (!string.IsNullOrWhiteSpace(contentType))
                    hr.ContentType = contentType;

                if (nvc != null && nvc.Count > 0)
                    hr.Headers.Add(nvc);

                Stream RequestStream = hr.GetRequestStream();
                RequestStream.Write(buf, 0, buf.Length);
                RequestStream.Close();

                System.Net.WebResponse response = hr.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encode));
                var result = reader.ReadToEnd();
                reader.Close();
                response.Close();

                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        ///  Http Post
        ///  Encoding : utf-8
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">数据</param>
        /// <param name="contentType">类型</param>
        /// <returns>返回请求结果，如果请求失败则返回空字符串</returns>
        public static string HttppPost(string url, string data, string contentType = "application/x-www-form-urlencoded")
        {
            NameValueCollection nv = new NameValueCollection();
            return HttppPost(url, data, "utf-8", contentType, null);
        }

        /// <summary>
        /// 发送Http请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">数据</param>
        /// <param name="method">请求的方法</param>
        /// <param name="encode">编码</param>
        /// <param name="accept"> Accept HTTP 标头的值</param>
        /// <param name="contentType">类型</param>
        /// <param name="userAgent">User-agent HTTP 标头的值</param>
        /// <param name="isGzip">Gzip压缩</param>
        /// <returns>返回请求结果，如果请求失败则返回空字符串</returns>
        public static string Sent(string url, string data, string method = "Post", string encode = "utf-8", string accept = "text/html;", string contentType = "application/x-www-form-urlencoded", string userAgent = "", bool isGzip = false)
        {
            try
            {
                HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
                req.Method = method;
                req.Accept = accept;
                req.ContentType = contentType;
                req.UserAgent = userAgent;

                WebResponse wr = null;
                wr = req.GetResponse();
                Stream strm = wr.GetResponseStream();

                if (isGzip) strm = new GZipStream(strm, CompressionMode.Decompress);
                StreamReader myStreamReader = new StreamReader(strm, Encoding.GetEncoding(encode));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                strm.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}