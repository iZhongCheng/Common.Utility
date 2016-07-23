using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：加密对象-工具类
    /// </summary>
    public class EncryptHelper
    {
        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="secretKey">私钥（长度： 16、24、32字节）</param>
        /// <returns></returns>
        public static string AESDecrypt(string content, string secretKey)
        {
            if (string.IsNullOrEmpty(content)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(content);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(secretKey),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// AES 加密
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="secretKey">私钥（长度： 16、24、32字节）</param>
        /// <returns></returns>
        public static string AESEncrypt(string content, string secretKey)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(secretKey);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(content);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string Md5(string content)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(content, "MD5");
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="encode">编码</param>
        /// <returns>签名字符串</returns>
        public static string Md5(string content, Encoding encode)
        {
            var result = new StringBuilder();
            var md5 = new MD5CryptoServiceProvider();
            var bytes = md5.ComputeHash(encode.GetBytes(content));
            foreach (var item in bytes)
                result.Append(item.ToString("x").PadLeft(2, '0'));

            return result.ToString();
        }

        /// <summary>
        /// 3DES 解密
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="secretKey">私钥</param>
        /// <param name="ivs">向量</param>
        /// <returns></returns>
        public static string DESDecrypt(string content, string secretKey, string ivs)
        {
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
            mCSP.Mode = CipherMode.CBC;
            mCSP.Padding = PaddingMode.PKCS7;
            mCSP.Key = Encoding.UTF8.GetBytes(secretKey);
            mCSP.IV = Encoding.UTF8.GetBytes(ivs);
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);
            byt = Convert.FromBase64String(content);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        /// 3DES 加密
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="secretKey">私钥</param>
        /// <param name="ivs">向量</param>
        /// <returns></returns>
        public static string DESEncrypt(string content, string secretKey, string ivs)
        {
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
            mCSP.Mode = CipherMode.CBC;
            mCSP.Padding = PaddingMode.PKCS7;
            mCSP.Key = Encoding.UTF8.GetBytes(secretKey);
            mCSP.IV = Encoding.UTF8.GetBytes(ivs);
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);
            byt = Encoding.UTF8.GetBytes(content);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}