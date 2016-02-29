using Common.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Common.Utility.Tests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var userInfo = new { Name = "Kt" };
            if (userInfo == null)
                throw new Exception("Object Is Null");
            PropertyInfo[] Properties = userInfo.GetType().GetProperties();
            string xml = "<xml>";
            foreach (PropertyInfo p in Properties)
            {
                string str = p.Name + " = " + p.GetValue(userInfo, null);
            }
            Console.ReadKey();

            var sqlConn = @"Data Source=.;Initial Catalog=test;User Id=sa;Password=123456;";
            var sqlCmd = "Select count(1) From [Test].[dbo].[UserInfo] Where [Id] >= @Id ";
            SqlParameter sp = new SqlParameter("@Id", SqlDbType.Int);
            sp.Value = 0;
            Object obj = SqlHelper.ExecuteScalar(sqlConn, CommandType.Text, sqlCmd, sp);
            var count = FunHelper.GetValue(obj, 0);

            sqlCmd = "Select * From [Test].[dbo].[UserInfo] Where [Id] >= @Id ";
            DataSet ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.Text, sqlCmd, sp);
            DataTable dt = (ds.Tables != null && ds.Tables.Count > 0) ? ds.Tables[0] : null;
            if (dt != null && dt.Rows.Count > 0)
            {
                var user = new { Name = (string)dt.Rows[0]["Name"] };   // DataTable To List
            }
            sqlCmd = " Update [Test].[dbo].[UserInfo]  Set [Name]='Updated' Where [Id] = @Id ";
            var updated = SqlHelper.ExecuteNonQuery(sqlConn, CommandType.Text, sqlCmd, sp) >= 1;

            sqlCmd = "if exists( Select count(1) From [Test].[dbo].[UserInfo] Where [Id] >= @Id  ) begin select '1' end ; else begin select '0' end ;";
            var exists = FunHelper.GetValue(SqlHelper.ExecuteScalar(sqlConn, CommandType.Text, sqlCmd, sp), 0) >= 1;

            Console.ReadKey();

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