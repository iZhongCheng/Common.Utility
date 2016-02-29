using System;
using System.Text.RegularExpressions;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：验证数据/正则-工具类
    /// </summary>
    public static class VerifyDataHelper
    {
        /// <summary>
        /// 验证数字
        /// </summary>
        /// <param name="num">数字</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        public static bool IsNum(string num)
        {
            return Regex.IsMatch(num + string.Empty, @"^[0-9]{1,}$");
        }

        /// <summary>
        /// 验证身份证
        /// </summary>
        /// <param name="idCard">身份证</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        public static bool IsIdCard(string idCard)
        {
            return Regex.IsMatch(idCard + string.Empty, @"(^\d{15}$)|(^\d{17}([0-9]|X)$)");
        }

        /// <summary>
        /// 验证(中国)邮政编码
        /// </summary>
        /// <param name="zipCode">(中国)邮政编码</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        public static bool IsZipCode(string zipCode)
        {
            return Regex.IsMatch(zipCode + string.Empty, @"(^\d{6}$");
        }

        /// <summary>
        /// 验证(中国)座机电话
        /// </summary>
        /// <param name="landline">(中国)座机电话</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        public static bool IsLandline(string landline)
        {
            return Regex.IsMatch(landline + string.Empty, @"(^(\(\d{3}\)|\d{3}-)?\d{8}|((\(\d{4}\)|\d{4}-)?\d{7})$");
        }

        /// <summary>
        /// 验证小数
        /// </summary>
        /// <param name="decimalNum">小数</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        public static bool IsDecimal(string decimalNum)
        {
            return Regex.IsMatch(decimalNum + string.Empty, @"^\d+(\.\d+)?$");
        }

        /// <summary>
        /// 验证汉字
        /// </summary>
        /// <param name="chineseCharacter">汉字</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        static public bool IsChineseCharacter(string chineseCharacter)
        {
            return Regex.IsMatch(chineseCharacter + string.Empty, @"[\u4e00-\u9fa5]{1,}[\u4e00-\u9fa5.·]{0,15}[\u4e00-\u9fa5]{1,}");
        }

        /// <summary>
        /// 验证时间
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        static public bool IsDateTime(string str)
        {
            DateTime dt;
            return DateTime.TryParse(str, out dt);
        }

        /// <summary>
        /// 验证时间
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="dt">时间</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        static public bool IsDateTime(string str, out DateTime dt)
        {
            return DateTime.TryParse(str, out dt);
        }

        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        static public bool IsEmail(string email)
        {
            //Todo : Old Reg String : @"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$"
            return Regex.IsMatch(email + string.Empty, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        static public bool IsMobile(string mobile)
        {
            return Regex.IsMatch(mobile + string.Empty, @"^(13[0-9]|14[0-9]|15[0-9]|17[0-9]|18[0-9])([0-9]{8})$");
        }

        /// <summary>
        /// 验证字符串空、 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        static public bool IsNullOrWhiteSpace(string str)
        {
            return string.IsNullOrWhiteSpace(RemoveSpace(str));
        }

        /// <summary>
        /// 去除空格字符
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static string RemoveSpace(string content)
        {
            return new Regex(@"\s").Replace(content + "", string.Empty);
        }

        /// <summary>
        /// 验证数字
        /// </summary>
        /// <param name="num">数字</param>
        /// <returns>验证成功返回ture 失败则返回false</returns>
        static public bool IsNumber(this string num)
        {
            return Regex.IsMatch(num + string.Empty, @"^[0-9]{1,}$");
        }
    }
}