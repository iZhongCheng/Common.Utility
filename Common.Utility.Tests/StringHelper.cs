using System.Collections.Generic;
using System.Text;

namespace Common.Utility.Tests
{
    /// <summary>
    /// 运行示例
    /// </summary>
    internal partial class Program
    {
        private static void Main_StringHelper(string[] args)
        {
            var obj = new
            {
                Id = 1,
                mobile = StringHelper.ToFuzzyMobile("18657155720"),
                sign = ""
            };

            var dict = DictionaryHelper.ToDictionary(obj, new List<string> { "sign" });
            var link = StringHelper.SortedJoinString(dict, "=", "", false, false, new List<string> { "sign" });
            var md5 = EncryptHelper.Md5(link + "SecureKey", Encoding.UTF8).ToLower();
            var dict2 = DictionaryHelper.ToDictionary(link, new List<string> { });
            var parmsStr = StringHelper.SortedJoinString(dict2, "=", "&", false, false, new List<string> { });
        }
    }
}