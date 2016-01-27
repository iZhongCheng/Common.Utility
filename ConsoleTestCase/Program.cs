using Common.Utility;
using System;

namespace ConsoleTestCase
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var Html1 = HttpHelper.HttpGet("http://www.caogen.com/blog/Infor_detail/77018.html", "GB2312", "text/html");
            var result = StringHelper.RemoveHTML(Html1);
            Console.ReadKey();
        }
    }
}