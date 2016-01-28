using Common.Utility;
using System;

namespace ConsoleTestCase
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var html = HttpHelper.HttpGet("http://china.huanqiu.com/article/2016-01/8461794.html?from=bdwz", "utf-8", "text/html");
            var res = StringHelper.RemoveHTML(html);

            var html1 = HttpHelper.HttpGet("http://www.caogen.com/blog/Infor_detail/77018.html", "GB2312", "text/html");
            var res1 = StringHelper.RemoveHTML(html1);

            CodeTimerHelper.Time("性能测试", 1000, () =>
            {
                StringHelper.RemoveHTML(html1);
            });
            Console.ReadKey();
        }
    }
}