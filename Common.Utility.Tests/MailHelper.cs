namespace Common.Utility.Tests
{
    /// <summary>
    /// 运行示例
    /// </summary>
    internal partial class Program
    {
        private static void Main_MailHelper(string[] args)
        {
            var msg = "";
            var sendMail = new MailHelper("smtp.sina.com", 25, "hihitch@sina.com", "123456", new string[] { "你自己的邮箱@qq.com" }, "title is hitch send", "body is xxxx", false);
            sendMail.Send(out msg);
        }
    }
}