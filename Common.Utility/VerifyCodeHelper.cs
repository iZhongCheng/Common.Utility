using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Generateed：2011-04-01
    /// Description：验证码图片-工具类
    /// </summary>
    public class VerifyCodeHelper
    {
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="drawLine">画干扰线</param>
        /// <param name="verifyCode">验证码的值</param>
        /// <returns></returns>
        public MemoryStream GenerateGifPictureVerification(int drawLine, out string verifyCode)
        {
            verifyCode = string.Empty;

            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Gray, Color.DarkBlue };  //颜色列表，用于验证码、噪线、噪点

            string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };    //字体列表，用于验证码

            char[] character = { '0', '1', '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y', 'Z' };   //验证码的字符集，去掉了一些容易混淆的字符
            var rnd = new Random();
            for (var i = 0; i < 4; i++)
                verifyCode += character[rnd.Next(character.Length)];

            var bmp = new Bitmap(90, 32);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            for (var i = 0; i < drawLine; i++)   //画噪线
            {
                var x1 = rnd.Next(100);
                var y1 = rnd.Next(40);
                var x2 = rnd.Next(100);
                var y2 = rnd.Next(40);
                var clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            for (var i = 0; i < verifyCode.Length; i++) //画验证码字符串
            {
                var fnt = font[rnd.Next(font.Length)];
                var ft = new Font(fnt, 18);
                var clr = color[rnd.Next(color.Length)];
                g.DrawString(verifyCode[i].ToString(CultureInfo.InvariantCulture), ft, new SolidBrush(clr), (float)i * 20 + 4, 2);
            }

            for (var i = 0; i < 100; i++) //画噪点
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }

            var ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                return ms;
            }
            finally
            {
                bmp.Dispose();
                g.Dispose();
            }
        }
    }
}