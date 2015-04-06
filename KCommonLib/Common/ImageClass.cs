using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using KCommonLib.DBTools;
using System.Net;
/// <summary>
/// 图片处理类
/// 1、生成缩略图片或按照比例改变图片的大小和画质
/// 2、将生成的缩略图放到指定的目录下
/// </summary>
public class ImageClass
{
    public Image ResourceImage;
    private int ImageWidth;
    private int ImageHeight;
    public string ErrMessage;
    /// <summary>
    /// 类的构造函数
    /// </summary>
    /// <param name="ImageFileName">图片文件的全路径名称</param>
    public ImageClass(string ImageFileName)
    {
        ResourceImage = Image.FromFile(ImageFileName);
        ErrMessage = "";
    }
    public bool ThumbnailCallback()
    {
        return false;
    }
    /// <summary>
    /// 生成缩略图重载方法1，返回缩略图的Image对象
    /// </summary>
    /// <param name="Width">缩略图的宽度</param>
    /// <param name="Height">缩略图的高度</param>
    /// <returns>缩略图的Image对象</returns>
    public Image GetReducedImage(int Width, int Height)
    {
        try
        {
            Image ReducedImage;
            Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            ReducedImage = ResourceImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);

            return ReducedImage;
        }
        catch (Exception e)
        {
            ErrMessage = e.Message;
            return null;
        }
    }
    /// <summary>
    /// 生成缩略图重载方法2，将缩略图文件保存到指定的路径
    /// </summary>
    /// <param name="Width">缩略图的宽度</param>
    /// <param name="Height">缩略图的高度</param>
    /// <param name="targetFilePath">缩略图保存的全文件名，(带路径)，参数格式：D:\Images\filename.jpg</param>
    /// <returns>成功返回true，否则返回false</returns>
    public bool GetReducedImage(int Width, int Height, string targetFilePath)
    {
        try
        {
            Image ReducedImage;
            Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            ReducedImage = ResourceImage.GetThumbnailImage(Width, Height, callb, IntPtr.Zero);
            ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);
            ReducedImage.Dispose();

            return true;
        }
        catch (Exception e)
        {
            ErrMessage = e.Message;
            return false;
        }
    }

    /// <summary>
    /// 生成缩略图重载方法3，返回缩略图的Image对象
    /// </summary>
    /// <param name="Percent">缩略图的宽度百分比 如：需要百分之80，就填0.8</param>  
    /// <returns>缩略图的Image对象</returns>
    public Image GetReducedImage(double Percent)
    {
        try
        {
            Image ReducedImage;
            Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            ImageWidth = Convert.ToInt32(ResourceImage.Width * Percent);
            ImageHeight = Convert.ToInt32(ResourceImage.Width * Percent);

            ReducedImage = ResourceImage.GetThumbnailImage(ImageWidth, ImageHeight, callb, IntPtr.Zero);

            return ReducedImage;
        }
        catch (Exception e)
        {
            ErrMessage = e.Message;
            return null;
        }
    }
    /// <summary>
    /// 生成缩略图重载方法4，返回缩略图的Image对象
    /// </summary>
    /// <param name="Percent">缩略图的宽度百分比 如：需要百分之80，就填0.8</param>  
    /// <param name="targetFilePath">缩略图保存的全文件名，(带路径)，参数格式：D:\Images\filename.jpg</param>
    /// <returns>成功返回true,否则返回false</returns>
    public bool GetReducedImage(double Percent, string targetFilePath)
    {
        try
        {
            Image ReducedImage;
            Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            ImageWidth = Convert.ToInt32(ResourceImage.Width * Percent);
            ImageHeight = Convert.ToInt32(ResourceImage.Width * Percent);

            ReducedImage = ResourceImage.GetThumbnailImage(ImageWidth, ImageHeight, callb, IntPtr.Zero);
            ReducedImage.Save(@targetFilePath, ImageFormat.Jpeg);

            ReducedImage.Dispose();

            return true;
        }
        catch (Exception e)
        {
            ErrMessage = e.Message;
            return false;
        }
    }

    public static int getJpgSize(string FileName, out Size JpgSize, out float Wpx, out float Hpx)
    {
        //C#快速获取JPG图片大小及英寸分辨率
        JpgSize = new Size(0, 0);

        Wpx = 0; Hpx = 0;
        int rx = 0;
        if (!File.Exists(FileName)) return rx;
        FileStream F_Stream = File.OpenRead(FileName);
        int ff = F_Stream.ReadByte();
        int type = F_Stream.ReadByte();
        if (ff != 0xff || type != 0xd8)
        {//非JPG文件
            F_Stream.Close();
            return rx;
        }

        long ps = 0;
        do
        {
            do
            {
                ff = F_Stream.ReadByte();
                if (ff < 0)
                //文件结束
                {
                    F_Stream.Close();
                    return rx;
                }
            } while (ff != 0xff);
            do
            {
                type = F_Stream.ReadByte();
            } while (type == 0xff);
            //MessageBox.Show(ff.ToString() + "," + type.ToString(), F_Stream.Position.ToString());
            ps = F_Stream.Position;
            switch (type)
            {
                case 0x00:
                case 0x01:
                case 0xD0:
                case 0xD1:
                case 0xD2:
                case 0xD3:
                case 0xD4:
                case 0xD5:
                case 0xD6:
                case 0xD7: break;
                case 0xc0:
                    //SOF0段
                    ps = F_Stream.ReadByte() * 256;
                    ps = F_Stream.Position + ps + F_Stream.ReadByte() - 2; //加段长度
                    F_Stream.ReadByte();
                    //丢弃精度数据
                    //高度
                    JpgSize.Height = F_Stream.ReadByte() * 256;
                    JpgSize.Height = JpgSize.Height + F_Stream.ReadByte();
                    //宽度
                    JpgSize.Width = F_Stream.ReadByte() * 256;
                    JpgSize.Width = JpgSize.Width + F_Stream.ReadByte();
                    //后面信息忽略
                    if (rx != 1 && rx < 3) rx = rx + 1;
                    break;
                case 0xe0:
                    //APP0段
                    ps = F_Stream.ReadByte() * 256;
                    ps = F_Stream.Position + ps + F_Stream.ReadByte() - 2; //加段长度
                    F_Stream.Seek(5, SeekOrigin.Current);
                    //丢弃APP0标记(5bytes)
                    F_Stream.Seek(2, SeekOrigin.Current);
                    //丢弃主版本号(1bytes)及次版本号(1bytes)
                    int units = F_Stream.ReadByte();
                    //X和Y的密度单位,units=0：无单位,units=1：点数/英寸,units=2：点数/厘米
                    //水平方向(像素/英寸)分辨率
                    Wpx = F_Stream.ReadByte() * 256;
                    Wpx = Wpx + F_Stream.ReadByte();
                    if (units == 2)
                        Wpx = (float)(Wpx * 2.54); //厘米变为英寸
                    //垂直方向(像素/英寸)分辨率
                    Hpx = F_Stream.ReadByte() * 256;
                    Hpx = Hpx + F_Stream.ReadByte();
                    if (units == 2) Hpx = (float)(Hpx * 2.54); //厘米变为英寸
                    //后面信息忽略
                    if (rx != 2 && rx < 3) rx = rx + 2;
                    break;
                default:
                    //别的段都跳过////////////////
                    ps = F_Stream.ReadByte() * 256;
                    ps = F_Stream.Position + ps + F_Stream.ReadByte() - 2; //加段长度
                    break;
            }
            if (ps + 1 >= F_Stream.Length)
            //文件结束
            {
                F_Stream.Close();
                return rx;
            }
            F_Stream.Position = ps; //移动指针
        } while (type != 0xda); // 扫描行开始
        F_Stream.Close();
        return rx;
    }

    #region 等比缩放
    /// <summary> 
    /// 图片等比缩放 
    /// </summary> 
    /// <param name="postedFile">原图HttpPostedFile对象</param> 
    /// <param name="savePath">缩略图存放地址</param> 
    /// <param name="targetWidth">指定的最大宽度</param> 
    /// <param name="targetHeight">指定的最大高度</param> 
    /// <param name="watermarkText">水印文字(为""表示不使用水印)</param> 
    /// <param name="watermarkImage">水印图片路径(为""表示不使用水印)</param> 
    public static void ZoomAuto(System.Web.HttpPostedFile postedFile, string savePath, System.Double targetWidth, System.Double targetHeight, string watermarkText, string watermarkImage)
    {
        //创建目录 
        string dir = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息） 
        System.Drawing.Image initImage = System.Drawing.Image.FromStream(postedFile.InputStream, true);

        //原图宽高均小于模版，不作处理，直接保存 
        if (initImage.Width <= targetWidth && initImage.Height <= targetHeight)
        {
            //文字水印 
            if (watermarkText != "")
            {
                using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(initImage))
                {
                    System.Drawing.Font fontWater = new Font("黑体", 10);
                    System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                    gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                    gWater.Dispose();
                }
            }

            //透明图片水印 
            if (watermarkImage != "")
            {
                if (File.Exists(watermarkImage))
                {
                    //获取水印图片 
                    using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                    {
                        //水印绘制条件：原始图片宽高均大于或等于水印图片 
                        if (initImage.Width >= wrImage.Width && initImage.Height >= wrImage.Height)
                        {
                            Graphics gWater = Graphics.FromImage(initImage);

                            //透明属性 
                            ImageAttributes imgAttributes = new ImageAttributes();
                            ColorMap colorMap = new ColorMap();
                            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                            ColorMap[] remapTable = { colorMap };
                            imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                            float[][] colorMatrixElements = {  
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5 
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f} 
                                };

                            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                            imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                            gWater.DrawImage(wrImage, new Rectangle(initImage.Width - wrImage.Width, initImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);

                            gWater.Dispose();
                        }
                        wrImage.Dispose();
                    }
                }
            }

            //保存 
            initImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        else
        {
            //缩略图宽、高计算 
            double newWidth = initImage.Width;
            double newHeight = initImage.Height;

            //宽大于高或宽等于高（横图或正方） 
            if (initImage.Width > initImage.Height || initImage.Width == initImage.Height)
            {
                //如果宽大于模版 
                if (initImage.Width > targetWidth)
                {
                    //宽按模版，高按比例缩放 
                    newWidth = targetWidth;
                    newHeight = initImage.Height * (targetWidth / initImage.Width);
                }
            }
            //高大于宽（竖图） 
            else
            {
                //如果高大于模版 
                if (initImage.Height > targetHeight)
                {
                    //高按模版，宽按比例缩放 
                    newHeight = targetHeight;
                    newWidth = initImage.Width * (targetHeight / initImage.Height);
                }
            }

            //生成新图 
            //新建一个bmp图片 
            System.Drawing.Image newImage = new System.Drawing.Bitmap((int)newWidth, (int)newHeight);
            //新建一个画板 
            System.Drawing.Graphics newG = System.Drawing.Graphics.FromImage(newImage);

            //设置质量 
            newG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            newG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //置背景色 
            newG.Clear(Color.White);
            //画图 
            newG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);

            //文字水印 
            if (watermarkText != "")
            {
                using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(newImage))
                {
                    System.Drawing.Font fontWater = new Font("宋体", 10);
                    System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                    gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                    gWater.Dispose();
                }
            }

            //透明图片水印 
            if (watermarkImage != "")
            {
                if (File.Exists(watermarkImage))
                {
                    //获取水印图片 
                    using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                    {
                        //水印绘制条件：原始图片宽高均大于或等于水印图片 
                        if (newImage.Width >= wrImage.Width && newImage.Height >= wrImage.Height)
                        {
                            Graphics gWater = Graphics.FromImage(newImage);

                            //透明属性 
                            ImageAttributes imgAttributes = new ImageAttributes();
                            ColorMap colorMap = new ColorMap();
                            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                            ColorMap[] remapTable = { colorMap };
                            imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                            float[][] colorMatrixElements = {  
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5 
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f} 
                                };

                            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                            imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                            gWater.DrawImage(wrImage, new Rectangle(newImage.Width - wrImage.Width, newImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);
                            gWater.Dispose();
                        }
                        wrImage.Dispose();
                    }
                }
            }

            //保存缩略图 
            newImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            //释放资源 
            newG.Dispose();
            newImage.Dispose();
            initImage.Dispose();
        }
    }

    #endregion

    /// <summary>
    /// 按比例缩小图片，自动计算高度
    /// </summary>
    /// <param name="strOldPic">源图文件名(包括路径)</param>
    /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param>
    /// <param name="intWidth">缩小至宽度</param>
    public static void SmallPic(string strOldPic, string strNewPic, float intWidth, out int intHeight)
    {

        System.Drawing.Bitmap objPic, objNewPic;
        try
        {
            objPic = new System.Drawing.Bitmap(strOldPic);
            intHeight = (int)((intWidth / (float)objPic.Width) * objPic.Height);
            objNewPic = new System.Drawing.Bitmap(objPic, (int)intWidth, intHeight);
            objNewPic.Save(strNewPic, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        catch (Exception exp) { throw exp; }
        finally
        {
            objPic = null;
            objNewPic = null;
        }
    }

    /// <summary>
    /// 下载图片并生成310宽度略缩图
    /// </summary>
    /// <param name="src"></param>
    /// <param name="imagePath"></param>
    public static bool DownLoadImage(string src, string imagePath, out int newHeight)
    {
        newHeight = 0;
        string imgName = DateTime.Now.ToString("ddhhmmssfff") + src.Substring(src.LastIndexOf("."));
        //string path = DBCommon.GetConstr("imagePath") + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + imgName;
        FileInfo fi = new FileInfo(imagePath);
        if (!fi.Exists)
        {
            System.IO.Directory.CreateDirectory(imagePath.Substring(0, imagePath.LastIndexOf("\\") + 1));
        }

        try
        {
            WebRequest request = WebRequest.Create(src);
            WebResponse response = request.GetResponse();
            Stream reader = response.GetResponseStream();
            FileStream writer = new FileStream(imagePath, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buff = new byte[512];
            int c = 0; //实际读取的字节数
            while ((c = reader.Read(buff, 0, buff.Length)) > 0)
            {
                writer.Write(buff, 0, c);
            }
            writer.Close();
            writer.Dispose();

            reader.Close();
            reader.Dispose();
            response.Close();
            Console.Write("下载图片成功，");

        }
        catch (Exception ex)
        {
            Console.Write("下载图片失败：" + ex.Message);
            return false;
        }

        try
        {

            ImageClass.SmallPic(imagePath, imagePath.Replace(".", "s."), 310, out newHeight);
            Console.Write("缩放图片成功。\r\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine("缩放图片失败：" + ex.Message + "\r\n");
            return false;
        }

        return true;
    }

    public static void ConvertTextFileToImage(String textFile,string path)
    {
        //设置画布字体
        System.Drawing.Font drawFont = new System.Drawing.Font("宋体", 12);
        //实例一个画布起始位置为1.1
        System.Drawing.Bitmap image = new System.Drawing.Bitmap(1, 1);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
        //读取文本内容
        //String text = "1-3输入一个数字，在原数字的基础上加２ program Yu_1_3; var a:integer; begin readln(a); writeln(a+=2); end.";
        String text = textFile;//得到转来的文本代码
        //String text = Regular.FiltrationHtml(Regular.htmlInputText("&lt;P&gt;1-3输入一个数字，在原数字的基础上加２&lt;/P&gt;<br>&lt;P&gt;program&nbsp;Yu_1_3;&nbsp;&lt;/P&gt;<br>&lt;P&gt;var&nbsp;a:integer;&lt;/P&gt;<br>&lt;P&gt;begin&lt;/P&gt;<br>&lt;P&gt;readln(a);&nbsp;&lt;/P&gt;<br>&lt;P&gt;writeln(a&lt;&gt;&lt;=+2);&nbsp;&lt;/P&gt;<br>&lt;P&gt;end.&lt;/P&gt;",1));
        System.Drawing.SizeF sf = g.MeasureString(text, drawFont, 640); //设置一个显示的宽度 宽度为640px
        image = new System.Drawing.Bitmap(image, new System.Drawing.Size(Convert.ToInt32(sf.Width), Convert.ToInt32(sf.Height)));
        g = System.Drawing.Graphics.FromImage(image);
        g.Clear(System.Drawing.Color.White);
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        g.DrawString(text, drawFont, System.Drawing.Brushes.Black, new System.Drawing.RectangleF(new System.Drawing.PointF(0, 0), sf));
        //image.Save(imageFile, System.Drawing.Imaging.ImageFormat.Png);

        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        //Response.ClearContent();
        //Response.ContentType = "image/Png";
        //Response.BinaryWrite(ms.ToArray());
        //Response.End();

        g.Dispose();
        image.Dispose();
    }

}
