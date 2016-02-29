using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：Web 请求对象-工具类
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// 当前 HTTP 请求
        /// </summary>
        public static HttpRequest CurrentRequest
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        /// <summary>
        /// 获取客户端IP, MVC使用
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string clientIP = "127.0.0.1";

            if (HttpContext.Current != null)
            {
                if (CurrentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) //判断是否使用代理
                {
                    clientIP = CurrentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else if (CurrentRequest.ServerVariables["REMOTE_ADDR"] != null)
                {
                    clientIP = CurrentRequest.ServerVariables["REMOTE_ADDR"].ToString();
                }
                else
                {
                    clientIP = CurrentRequest.UserHostAddress;
                }
            }
            return clientIP;
        }

        /// <summary>
        /// 获取客户端IP , WebApi使用
        /// </summary>
        /// <param name="request">WebApi Request</param>
        /// <returns></returns>
        public static string GetClientIP(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop;
                prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取客户端IP , WCF使用
        /// </summary>
        /// <param name="context">WCF Context</param>
        /// <returns></returns>
        public static string GetClientIP(OperationContext context)
        {
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty prop = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            return prop.Address;
        }
    }
}