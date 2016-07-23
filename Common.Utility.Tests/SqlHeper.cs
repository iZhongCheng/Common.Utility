using System;
using System.Data;
using System.Data.SqlClient;

namespace Common.Utility.Tests
{
    /// <summary>
    /// 运行示例
    /// </summary>
    internal partial class Program
    {
        private static void Main_SqlHelper(string[] args)
        {
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
        }
    }
}