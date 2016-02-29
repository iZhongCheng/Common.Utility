using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Controls;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：生成二维码-工具类
    /// </summary>
    public class Q2CodeHelper
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="filePath">保存文件路径</param>
        /// <param name="errorMessage">错误异常消息</param>
        /// <returns></returns>
        public static bool Generate(string content, string filePath, out string errorMessage)
        {
            errorMessage = string.Empty;
            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            try
            {
                QrCode qrCode;
                qrEncoder.TryEncode(content, out qrCode);
                var renderer = new Renderer(5, Brushes.Black, Brushes.White);

                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                else
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Exists)
                        fileInfo.Delete();
                }

                renderer.CreateImageFile(qrCode.Matrix, filePath, ImageFormat.Png);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="errorMessage">错误异常消息</param>
        /// <returns></returns>
        public static MemoryStream Generate(string content, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                QRCodeEncoder q2 = new QRCodeEncoder();
                q2.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                q2.QRCodeScale = 9;

                int count = Encoding.UTF8.GetBytes(content).Length;
                int version = 1;
                if (count <= 7)
                    version = 1;
                else if (count <= 14)
                    version = 2;
                else
                {
                    if (((count - 14) / 10.0) > ((count - 14) / 10))
                        version = ((count - 14) / 10) + 1 + 2;
                    else
                        version = ((count - 14) / 10) + 2;
                }

                q2.QRCodeVersion = version;
                q2.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

                Bitmap bmp = q2.Encode(content, Encoding.UTF8);
                var ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Jpeg);
                return ms;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }
    }
}