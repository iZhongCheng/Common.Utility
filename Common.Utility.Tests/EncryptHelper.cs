using System;
using System.Text;

namespace Common.Utility.Tests
{
    /// <summary>
    /// 运行示例
    /// </summary>
    internal partial class Program
    {
        private static void Main_EncryptHelper(string[] args)
        {
            string str = "需要加密的字符串12345678";
            string md5_1 = EncryptHelper.Md5(str);
            string md5_2 = EncryptHelper.Md5(str, Encoding.UTF8);

            str = "10086";
            string secretKey_AES = Guid.NewGuid().ToString("N");
            string JiaMi = EncryptHelper.AESEncrypt(str, secretKey_AES);
            string JieMi = EncryptHelper.AESDecrypt(JiaMi, secretKey_AES);

            string secretKey_DES = "fsT7ObM1nEnrRAGO1djI2YBi";
            string ivs_DES = "GUGlYE1g";
            JiaMi = EncryptHelper.DESEncrypt(str, secretKey_DES, ivs_DES);
            JieMi = EncryptHelper.DESDecrypt(JiaMi, secretKey_DES, ivs_DES);
        }
    }
}