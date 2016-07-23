using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-12-12
    /// Description：群发Smtp邮件-工具类
    /// Tips：如发送失败，1. 检查你要发送的邮件地址是否开启Smtp服务。2. 检查你填写信息，如Smtp服务的端口、账号、密码。
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 邮件发送状态
        /// </summary>
        public enum SendStatus
        {
            /// <summary>
            /// 默认,正常,准备发送
            /// </summary>
            Normal = 0,

            /// <summary>
            /// 发送成功
            /// </summary>
            Success = 1,

            /// <summary>
            /// 发送失败
            /// </summary>
            Failed = 2,

            /// <summary>
            /// 发送邮箱或收件邮箱未通过验证
            /// </summary>
            Unmatch = 3,

            /// <summary>
            /// 发件地址未通过验证
            /// </summary>
            SendMailAddressIsUnmatch = 4,

            /// <summary>
            /// 发件密码为空
            /// </summary>
            PassWordIsNull = 5,

            /// <summary>
            /// 收件地址未通过验证
            /// </summary>
            FromMailAddressIsUnmatch = 6
        }

        /// <summary>
        /// 发送标题和内容的字符集，默认是UTF8
        /// </summary>
        public Encoding CharSet = Encoding.UTF8;

        /// <summary>
        /// 设置指示邮件正文是否为 Html 格式
        /// 默认为False，不是Html格式
        /// </summary>
        public bool IsBodyHtml = false;

        /// <summary>
        /// 是否使用抄送
        /// </summary>
        public bool IsCC = false;

        /// <summary>
        /// 是否发送附件
        /// </summary>
        public bool IsSendAttachments = false;

        /// <summary>
        /// 是否开启安全套接字层 (SSL) 加密连接
        /// </summary>
        public bool IsSSL = false;

        /// <summary>
        /// SMTP服务器是否不需要身份认证
        /// 默认为False，需要使用账号和密码登陆
        /// </summary>
        public bool IsUseDefaultCredentials = false;

        /// <summary>
        /// 邮箱正则表达式(不区分大小写)，可修改成自定义正则表达式
        /// </summary>
        public string RegexText = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        /// Smtp 服务器端口,默认25
        /// </summary>
        public int SmtpPort = 25;

        /// <summary>
        /// 默认初始化
        /// </summary>
        public MailHelper() { }

        /// <summary>
        /// 创建一个SendMailMessage对象
        /// </summary>
        /// <param name="smtpAddress"> Smtp 服务器地址</param>
        /// <param name="sendMailAddress">发件邮箱地址</param>
        /// <param name="passWord">邮箱密码</param>
        /// <param name="fromMailAddress">收件邮箱地址</param>
        public MailHelper(string smtpAddress, string sendMailAddress, string passWord, string[] fromMailAddress)
        {
            this.SmtpAddress = smtpAddress;
            this.SendMailAddress = sendMailAddress;
            this.PassWord = passWord;
            this.FromMailAddress = fromMailAddress;
        }

        /// <summary>
        /// 创建一个SendMailMessage对象
        /// </summary>
        /// <param name="smtpAddress"> Smtp 服务器地址</param>
        /// <param name="smtpPort">Smtp 服务器端口</param>
        /// <param name="sendMailAddress">发件邮箱地址</param>
        /// <param name="passWord">邮箱密码</param>
        /// <param name="fromMailAddress">收件邮箱地址</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件正文内容</param>
        /// <param name="isSSL">是否开启安全套接字层 (SSL) 加密连接</param>
        public MailHelper(string smtpAddress, int smtpPort, string sendMailAddress, string passWord, string[] fromMailAddress, string title, string body, bool isSSL)
            : this(smtpAddress, sendMailAddress, passWord, fromMailAddress)
        {
            this.SmtpPort = smtpPort;
            this.Title = title;
            this.Body = body;
            this.IsSSL = isSSL;
        }

        /// <summary>
        /// 邮件附件
        /// 如果确定发送附件请将IsSendAttachments设置为true
        /// </summary>
        public string[] Attachments { get; set; }

        /// <summary>
        /// 邮件正文内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 邮件的抄送者
        /// 如果确定发送邮件的抄送者请将IsCC设置为true
        /// </summary>
        public string[] CarbonCopy { get; set; }

        /// <summary>
        /// 收件邮箱地址
        /// </summary>
        public string[] FromMailAddress { get; set; }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        ///  发件邮箱地址
        /// </summary>
        public string SendMailAddress { get; set; }

        /// <summary>
        ///  发件邮箱昵称 ,默认为发件地址
        /// </summary>
        public string SendMailName { get; set; }

        /// <summary>
        /// Smtp 服务器地址
        /// </summary>
        public string SmtpAddress { get; set; }

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 发送邮件，并且返回发送结果
        /// </summary>
        /// <returns></returns>
        public SendStatus Send(out string relustMessage)
        {
            relustMessage = string.Empty;

            #region 验证基本信息

            Regex reg = new Regex(this.RegexText, RegexOptions.IgnoreCase);
            if (!IsUseDefaultCredentials) // 使用账号密码登陆Smtp服务
            {
                if (!reg.IsMatch(this.SendMailAddress))
                {
                    relustMessage = "发件地址未通过验证";
                    return SendStatus.SendMailAddressIsUnmatch;
                }
                if (string.IsNullOrEmpty(this.PassWord))
                {
                    relustMessage = "发件密码为空";
                    return SendStatus.PassWordIsNull;
                }
            }

            if (this.FromMailAddress == null || this.FromMailAddress.Length <= 0)
            {
                relustMessage = "收件地址为空";
                return SendStatus.FromMailAddressIsUnmatch;
            }

            StringBuilder FailedELst = new StringBuilder();
            foreach (string item in this.FromMailAddress)
            {
                if (!reg.IsMatch(item))
                {
                    FailedELst.Append(Environment.NewLine + item);
                }
            }
            if (FailedELst.Length > 0)
            {
                relustMessage = "以下收件地址未通过验证:" + FailedELst;
                return SendStatus.FromMailAddressIsUnmatch;
            }

            #endregion 验证基本信息

            SendStatus sendStatus = SendStatus.Normal;
            SmtpClient smtp = new SmtpClient(); //实例化一个Smtp
            smtp.EnableSsl = this.IsSSL; //smtp服务器是否启用SSL加密
            smtp.Host = this.SmtpAddress; //指定 Smtp 服务器地址
            smtp.Port = this.SmtpPort;  //指定 Smtp 服务器的端口，默认是465 ,587
            if (IsUseDefaultCredentials)   // SMTP服务器是否不需要身份认证
                smtp.UseDefaultCredentials = true;
            else
                smtp.Credentials = new NetworkCredential(this.SendMailAddress, this.PassWord); // NetworkCredential("邮箱名", "密码");

            MailMessage mm = new MailMessage(); //实例化一个邮件类
            mm.Priority = MailPriority.Normal; //邮件的优先级
            mm.From = new MailAddress(this.SendMailAddress, this.SendMailName, Encoding.UTF8);//收件方看到的邮件来源；第一个参数是发信人邮件地址第二参数是发信人显示的名称第三个参数是, Encoding.GetEncoding(936) 第二个参数所使用的编码，如果指定不正确，则对方收到后显示乱码，936是简体中文的codepage值
            mm.ReplyTo = new MailAddress(this.SendMailAddress, this.SendMailName, Encoding.UTF8);//ReplyTo 表示对方回复邮件时默认的接收地址，即：你用一个邮箱发信，但却用另一个来收信后两个参数的意义， 同 From 的意义

            StringBuilder fmLst = new StringBuilder();
            foreach (string item in this.FromMailAddress)
            {
                fmLst.Append(item + ",");
            }
            mm.To.Add(fmLst.Remove(fmLst.Length - 1, 1).ToString()); //邮件的接收者，支持群发，多个地址之间用 半角逗号 分开
            if (this.CarbonCopy != null && this.CarbonCopy.Length > 0 && this.IsCC) // 是否显示抄送
            {
                StringBuilder ccLst = new StringBuilder();
                foreach (string item in this.CarbonCopy)
                    ccLst.Append(item + ",");
                mm.CC.Add(ccLst.Remove(ccLst.Length - 1, 1).ToString()); //邮件的抄送者，支持群发，多个邮件地址之间用 半角逗号 分开
            }
            mm.Subject = this.Title; //邮件标题
            mm.SubjectEncoding = CharSet; // 这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。// 936是简体中文的pagecode，如果是英文标题，这句可以忽略不用
            mm.IsBodyHtml = this.IsBodyHtml; //邮件正文是否是HTML格式
            mm.BodyEncoding = CharSet; //邮件正文的编码， 设置不正确， 接收者会收到乱码
            mm.Body = this.Body;//邮件正文

            if (this.Attachments != null && this.Attachments.Length > 0 && this.IsSendAttachments)
            {
                foreach (string item in this.Attachments)
                    mm.Attachments.Add(new Attachment(item));  //添加附件
            }

            try
            {
                smtp.Send(mm);
                relustMessage = "邮件发送成功";
                sendStatus = SendStatus.Success;
            }
            catch (Exception ee)
            {
                sendStatus = SendStatus.Failed;
                relustMessage = ee.Message;
            }
            return sendStatus;
        }
    }
}