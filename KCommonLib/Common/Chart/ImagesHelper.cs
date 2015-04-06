using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace KCommonLib.Common.Chart
{
    /// <summary>
    /// 图片操作方法
    /// </summary>
    public class ImageHelper
    {
        /// <summary><SUMMARY></SUMMARY>   
        /// 获取图片中的各帧   
        /// </summary>   
        /// <PARAM name="pPath" />图片路径</param>   
        /// <PARAM name="pSavePath" />保存路径</param>   
        public void GetFrames(string pPath, string pSavedPath)
        {
            Image gif = Image.FromFile(pPath);
            FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);

            //获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)   
            int count = gif.GetFrameCount(fd);

            //以Jpeg格式保存各帧   
            for (int i = 0; i < count; i++)
            {
                gif.SelectActiveFrame(fd, i);
                gif.Save(pSavedPath + "\\frame_" + i + ".jpg", ImageFormat.Jpeg);
            }
        }

        /// <summary><SUMMARY></SUMMARY>   
        /// 获取图片缩略图   
        /// </summary>   
        /// <PARAM name="pPath" />图片路径</param>   
        /// <PARAM name="pSavePath" />保存路径</param>   
        /// <PARAM name="pWidth" />缩略图宽度</param>   
        /// <PARAM name="pHeight" />缩略图高度</param>   
        /// <PARAM name="pFormat" />保存格式，通常可以是jpeg</param>   
        public void GetSmaller(string pPath, string pSavedPath, int pWidth, int pHeight)
        {
            try
            {
                Image smallerImg;
                Image originalImg = Image.FromFile(pPath);
                Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                smallerImg = originalImg.GetThumbnailImage(pWidth, pHeight, callback, IntPtr.Zero);
                smallerImg.Save(pSavedPath + "\\smaller.jpg", ImageFormat.Jpeg);
            }
            catch 
            {
                //   
            }
        }

        /// <summary><SUMMARY></SUMMARY>   
        /// 获取图片指定部分   
        /// </summary>   
        /// <PARAM name="pPath" />图片路径</param>   
        /// <PARAM name="pSavePath" />保存路径</param>   
        /// <PARAM name="pPartStartPointX" />目标图片开始绘制处的坐标X值(通常为)</param>   
        /// <PARAM name="pPartStartPointY" />目标图片开始绘制处的坐标Y值(通常为)</param>   
        /// <PARAM name="pPartWidth" />目标图片的宽度</param>   
        /// <PARAM name="pPartHeight" />目标图片的高度</param>   
        /// <PARAM name="pOrigStartPointX" />原始图片开始截取处的坐标X值</param>   
        /// <PARAM name="pOrigStartPointY" />原始图片开始截取处的坐标Y值</param>   
        /// <PARAM name="pFormat" />保存格式，通常可以是jpeg</param>   
        public void GetPart(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY)
        {
            Image originalImg = Image.FromFile(pPath);

            Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);
            Graphics graphics = Graphics.FromImage(partImg);
            Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置   
            Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）   

            graphics.DrawImage(originalImg, destRect, origRect, GraphicsUnit.Pixel);
            partImg.Save(pSavedPath + "\\part.jpg", ImageFormat.Jpeg);
        }

        #region 绘制验证码图片
        /// <summary>
        /// 绘制验证码图片
        /// </summary>
        /// <param name="outBmp"></param>
        /// <returns></returns>
        public static string DrawRandomImg(out Bitmap outBmp)
        {
            // 创建一个包含随机内容的验证码文本 
            System.Random rand = new Random();
            int len = rand.Next(4, 6);
            char[] chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            for (int iCount = 0; iCount < len; iCount++)
            {
                myStr.Append(chars[rand.Next(chars.Length)]);
            }
            //获取随机值字符串
            string text = myStr.ToString();
            // 保存验证码到 session 中以便其他模块使用 
            //this.Session["checkcode"] = text;
            Size ImageSize = Size.Empty;
            Font myFont = new Font("宋体", 27);
            // 计算验证码图片大小 
            using (Bitmap bmp = new Bitmap(10, 10))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    SizeF size = g.MeasureString(text, myFont, 10000);
                    ImageSize.Width = (int)size.Width + 8;
                    ImageSize.Height = (int)size.Height + 8;
                }
            }
            // 创建验证码图片 
            using (outBmp = new Bitmap(ImageSize.Width, ImageSize.Height))
            {
                // 绘制验证码文本 
                using (Graphics g = Graphics.FromImage(outBmp))
                {
                    g.Clear(Color.White);
                    using (StringFormat f = new StringFormat())
                    {
                        f.Alignment = StringAlignment.Near;
                        f.LineAlignment = StringAlignment.Center;
                        f.FormatFlags = StringFormatFlags.NoWrap;
                        g.DrawString(text, myFont, Brushes.Black, new RectangleF(0, 0, ImageSize.Width, ImageSize.Height), f);
                    }
                }
                // 制造噪声 杂点面积占图片面积的 30% 
                int num = ImageSize.Width * ImageSize.Height * 30 / 100;
                for (int iCount = 0; iCount < num; iCount++)
                {
                    // 在随机的位置使用随机的颜色设置图片的像素 
                    int x = rand.Next(ImageSize.Width);
                    int y = rand.Next(ImageSize.Height);
                    int r = rand.Next(255);
                    int g = rand.Next(255);
                    int b = rand.Next(255);
                    Color c = Color.FromArgb(r, g, b);
                    outBmp.SetPixel(x, y, c);
                }
                myFont.Dispose();
                return text;
                // 输出图片 
                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //this.Response.ContentType = "image/png";
                //ms.WriteTo(this.Response.OutputStream);
                //ms.Close();
            }
        }
        #endregion        

        public bool ThumbnailCallback()
        {
            return false;
        }
    }
}
