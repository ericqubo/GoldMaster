using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Drawing.Imaging;
using System.Configuration;
using System.Drawing;

namespace KCommonLib.Common
{
    /// <summary>
    /// 上传文件类
    /// </summary>
    public class FileUpload
    {
        public enum File //定义一个人用于存放玩家上传文件信息的一个数组
        {
            FILE_SIZE, //大小
            FILE_POSTNAME, //类型（文件后缀名）
            FILE_SYSNAME, //系统名
            FILE_ORGINNAME, //原来的名字
            FILE_PATH //文件路径
        }
        private static Random rnd = new Random(); //获取一个随机数

        public static string[] UploadFile(HtmlInputFile file, string Upload_Dir) //实现玩家文件上传功能的主函数
        {
            string[] arr = new String[5];
            string FileName = GetUniquelyString(); //获取一个不重复的文件名
            string FileOrginName = file.PostedFile.FileName.Substring

            (file.PostedFile.FileName.LastIndexOf("\\") + 1);//获取文件的原始名
            if (file.PostedFile.ContentLength <= 0)
            { return null; }
            string postFileName;
            string FilePath = Upload_Dir.ToString();
            string path = FilePath + "\\";
            try
            {
                int pos = file.PostedFile.FileName.LastIndexOf(".") + 1;
                postFileName = file.PostedFile.FileName.Substring(pos, file.PostedFile.FileName.Length - pos);
                file.PostedFile.SaveAs(path + FileName + "." + postFileName); //存储指定的文件到指定的目录
            }
            catch (Exception exec)
            {
                throw (exec);
            }

            double unit = 1024;
            double size = Math.Round(file.PostedFile.ContentLength / unit, 2);
            arr[(int)File.FILE_SIZE] = size.ToString(); //文件大小
            arr[(int)File.FILE_POSTNAME] = postFileName; //文件类型（文件后缀名）
            arr[(int)File.FILE_SYSNAME] = FileName; //文件系统名
            arr[(int)File.FILE_ORGINNAME] = FileOrginName; //文件原来的名字
            arr[(int)File.FILE_PATH] = path + FileName + "." + postFileName; //文件路径
            return arr;
        }

        //public static bool OperateDB(string sqlstr) //建立一个和数据库的关联
        //{
        //    if (sqlstr == String.Empty)
        //        return false;

        //    SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["connstring"]);
        //    SqlCommand myCommand = new SqlCommand(sqlstr, myConnection);

        //    myConnection.Open();
        //    myCommand.ExecuteNonQuery();
        //    myConnection.Close();
        //    return true;
        //}

        public static string GetUniquelyString() //获取一个不重复的文件名
        {
            const int RANDOM_MAX_VALUE = 1000;
            string strTemp, strYear, strMonth, strDay, strHour, strMinute, strSecond, strMillisecond;

            DateTime dt = DateTime.Now;
            int rndNumber = rnd.Next(RANDOM_MAX_VALUE);
            strYear = dt.Year.ToString();
            strMonth = (dt.Month > 9) ? dt.Month.ToString() : "0" + dt.Month.ToString();
            strDay = (dt.Day > 9) ? dt.Day.ToString() : "0" + dt.Day.ToString();
            strHour = (dt.Hour > 9) ? dt.Hour.ToString() : "0" + dt.Hour.ToString();
            strMinute = (dt.Minute > 9) ? dt.Minute.ToString() : "0" + dt.Minute.ToString();
            strSecond = (dt.Second > 9) ? dt.Second.ToString() : "0" + dt.Second.ToString();
            strMillisecond = dt.Millisecond.ToString();

            strTemp = strYear + strMonth + strDay + "_" + strHour + strMinute + strSecond + "_" + strMillisecond + "_" + rndNumber.ToString();

            return strTemp;
        }
    }
}
