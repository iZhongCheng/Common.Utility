namespace Common.Utility.Tests
{
    /// <summary>
    /// 运行示例
    /// </summary>
    internal partial class Program
    {
        private static void Main_ConfigHelper(string[] args)
        {
            var appValue = ConfigHelper.GetValue("");
            var sqlconn = ConfigHelper.GetConnection("");
        }
    }
}