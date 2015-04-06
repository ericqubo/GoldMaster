using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO.Compression;
using System.Collections;

namespace KCommonLib.Common
{
    /// <summary>
    /// 读写文件操作
    /// </summary>
    public class MyIO
    {
        /// <summary>
        /// 从一个目录将其内容移动到另一目录
        /// </summary>
        /// <param name="p">源目录</param>
        /// <param name="p_2">目的目录</param>
        public static void MoveFolderTo(string p, string p_2)
        {
            //检查是否存在目的目录
            //if (!Directory.Exists(p_2))
            //    Directory.CreateDirectory(p_2);
            //先来移动文件
            //DirectoryInfo info = new DirectoryInfo(p);
            //FileInfo[] files = info.GetFiles();
            //foreach (FileInfo file in files)
            //{
            File.Copy(p, p_2, true); //复制文件
            //}
        }

        public static void SaveExcel(DataTable table, string excelFile, List<string> colNameList)
        {
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFile + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2;\";";
                    string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + excelFile + ";" + "Extended Properties=Excel 8.0;";
                    OleDbConnection conn = new OleDbConnection(strConn);
                    StringBuilder strExcel = new StringBuilder();
                    strExcel.Append(" insert into [sheet1$] values (");
                    for (int i = 0; i < colNameList.Count; i++)
                    {
                        strExcel.Append("'" + row[colNameList[i]] + "'");
                        if (i < colNameList.Count - 1)
                            strExcel.Append(",");
                    }
                    //strExcel = strExcel.Substring(0, strExcel.Length - 1);
                    strExcel.Append("); ");
                    OleDbCommand cmd = new OleDbCommand(strExcel.ToString(), conn);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch { }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                        conn.Dispose();
                    }
                }
            }
        }

        #region 读取excel文件内容返回dataset
        /// <summary>
        /// 读取excel文件内容返回dataset(32位)
        /// </summary>
        /// <param name="filepath">路径</param>
        /// <returns></returns>
        public static DataSet ReadExcel(string filepath)
        {
            //ArrayList al = new ArrayList();
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2;\";";
            OleDbConnection conn = new OleDbConnection(strConn);
            string strExcel = "select * from [sheet1$]";
            OleDbDataAdapter da = new OleDbDataAdapter(strExcel, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        #endregion

        #region 写文件
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="sMsg">信息</param>
        /// <param name="src">路径</param>
        /// <param name="isCover">是否覆盖原文件</param>
        public static void WriteLog(string sMsg, string src, bool isCover)
        {
            if (sMsg != "")
            {
                string filename = src.Substring(src.LastIndexOf("\\") + 1, src.Length - src.LastIndexOf("\\") - 1);
                string path = src.Substring(0, src.LastIndexOf("\\") + 1);
                try
                {
                    FileInfo fi = new FileInfo(src);
                    if (!fi.Exists)
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }

                    if (isCover)
                    {
                        using (StreamWriter sw = new StreamWriter(src, false, Encoding.GetEncoding("utf-8")))
                        {
                            sw.Write(sMsg);
                            sw.Close();
                        }
                    }
                    else
                    {
                        string oldStr = "";
                        try
                        {
                            StreamReader sr = new StreamReader(src);
                            oldStr = sr.ReadToEnd();
                            sr.Close();
                        }
                        catch { }

                        using (StreamWriter sw = new StreamWriter(src, false, Encoding.GetEncoding("utf-8")))
                        {
                            sw.Write(oldStr + sMsg + "\r\n");
                            sw.Close();
                        }
                    }
                }
                catch(Exception ex)
                {
                    //throw new Exception(ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="sMsg">信息</param>
        /// <param name="src">路径</param>
        /// <param name="isCover">是否覆盖原文件</param>
        public static void WriteLog(string sMsg, string src, bool isCover, string coding)
        {
            if (sMsg != "")
            {
                string filename = src.Substring(src.LastIndexOf("\\") + 1, src.Length - src.LastIndexOf("\\") - 1);
                string path = src.Substring(0, src.LastIndexOf("\\") + 1);
                try
                {
                    FileInfo fi = new FileInfo(src);
                    if (!fi.Exists)
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }

                    if (isCover)
                    {
                        using (StreamWriter sw = new StreamWriter(src, false, Encoding.GetEncoding(coding)))
                        {
                            sw.Write(sMsg);
                            sw.Close();
                        }
                    }
                    else
                    {
                        string oldStr = "";
                        try
                        {
                            StreamReader sr = new StreamReader(src);
                            oldStr = sr.ReadToEnd();
                            sr.Close();
                        }
                        catch { }

                        using (StreamWriter sw = new StreamWriter(src, false, Encoding.GetEncoding(coding)))
                        {
                            sw.Write(oldStr + sMsg + "\r\n");
                            sw.Close();
                        }
                    }
                }
                catch
                {
                    //throw new Exception(ex.Message);
                }
            }
        }
        #endregion

        #region 获取路径文件内容
        /// <summary>
        /// 获取路径文件内容
        /// </summary>
        /// <param name="src">路径</param>
        /// <returns></returns>
        public static string ReadSrc(string src)
        {
            try
            {
                StreamReader sr = new StreamReader(src, Encoding.GetEncoding("gb2312"));
                string str = sr.ReadToEnd();
                sr.Close();
                return str;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 获取路径文件内容
        /// <summary>
        /// 获取路径文件内容
        /// </summary>
        /// <param name="src">路径</param>
        /// <returns></returns>
        public static string ReadSrc(string src, string encoding)
        {
            try
            {
                StreamReader sr = new StreamReader(src, Encoding.GetEncoding(encoding));
                string str = sr.ReadToEnd();
                sr.Close();
                return str;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 获取URL XML文件 Rss文件
        /// <summary>
        /// 获取URL XML文件 Rss文件 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="tableFlag"></param>
        /// <returns></returns>
        public static DataTable ReadRssXml(string src, int tableFlag)
        {
            WebClient client = new WebClient();
            //HttpWebRequest wq = new HttpWebRequest();
            //wq.UserAgent = "nobot";
            DataTable table = null;
            try
            {
                //client.Headers.Add(HttpRequestHeader.Trailer, "User-Agent nobot");
                using (Stream rss = client.OpenRead(src))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(rss);
                    table = ds.Tables[tableFlag];
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return table;
        }
        #endregion

        #region 读取远程xml文件
        /// <summary>
        /// 读取远程xml文件 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static DataSet ReadSrcXml(string src)
        {
            DataSet ds = new DataSet();
            try
            {
                using (XmlReader read = XmlReader.Create(src))
                {
                    ds.ReadXml(read);
                }
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 读取页面，返回字符串
        /// <summary>
        /// 读取页面，返回字符串
        /// </summary>
        /// <param name="webpath">页面地址</param>
        /// <returns></returns>
        public static bool LoadNetPage(string webpath, out string pageHtml)
        {
            WebClient myWebClient = new WebClient();
            try
            {
                myWebClient.Headers.Add("user-agent", "nobot");
                myWebClient.Credentials = CredentialCache.DefaultCredentials;

                byte[] pageData = myWebClient.DownloadData(webpath);
                pageHtml = Encoding.GetEncoding("gb2312").GetString(pageData);
                return true;
            }
            catch
            {
                pageHtml = null;
                return false;
            }
            finally { myWebClient.Dispose(); }
        }

        /// <summary>
        /// 读取页面，返回字符串
        /// </summary>
        /// <param name="webpath">页面地址</param>
        /// <returns></returns>
        public static string LoadNetPage(string webpath)
        {
            WebClient myWebClient = new WebClient();
            try
            {
                myWebClient.Headers.Add("user-agent", "nobot");
                myWebClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                myWebClient.Credentials = CredentialCache.DefaultCredentials;

                byte[] pageData = myWebClient.DownloadData(webpath);
                return Encoding.GetEncoding("gb2312").GetString(pageData);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally { myWebClient.Dispose(); }
        }

        /// <summary>
        /// 读取页面，返回字符串
        /// </summary>
        /// <param name="webpath">页面地址</param>
        /// <returns></returns>
        public static string LoadNetPage(string webpath, string enCoding)
        {
            WebClient myWebClient = new WebClient();
            try
            {
                myWebClient.Headers.Add("user-agent", "nobot");
                //myWebClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                myWebClient.Credentials = CredentialCache.DefaultCredentials;

                byte[] pageData = myWebClient.DownloadData(webpath);
                return Encoding.GetEncoding(enCoding).GetString(pageData);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally { myWebClient.Dispose(); }
        }
        #endregion

        #region 读取xml文件，按照类型返回
        /// <summary>
        /// 读取xml文件，按照类型返回
        /// </summary>
        /// <param name="path">xml文件路径</param>
        /// <param name="type">1. 返回dataset; 2. 返回datatable</param>
        /// <returns></returns>
        public static object ReadXml(string path, int type)
        {
            DataSet ds = null;
            switch (type)
            {
                case 1: //类型一：返回dataset数据类型
                    ds = new DataSet();
                    try
                    {
                        ds.ReadXml(path);
                        return ds;
                    }
                    catch { return null; }
                case 2:
                    ds = new DataSet();
                    try
                    {
                        ds.ReadXml(path);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            return ds.Tables[0];
                        else
                            return null;
                    }
                    catch { return null; }
                default: return null;
            }
        }
        #endregion

        #region 对象深拷贝
        /// <summary>
        /// 对象深拷贝
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object CloneObject(object obj)
        {
            // 创建内存流
            using (System.IO.MemoryStream ms = new MemoryStream(1000))
            {
                object CloneObject;
                // 创建序列化器（有的书称为串行器）
                // 创建一个新的序列化器对象总是比较慢。
                BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                // 将对象序列化至流
                bf.Serialize(ms, obj);
                // 将流指针指向第一个字符
                ms.Seek(0, SeekOrigin.Begin);

                // 反序列化至另一个对象（即创建了一个原对象的深表副本）
                CloneObject = bf.Deserialize(ms);
                // 关闭流
                ms.Close();
                return CloneObject;
            }
        }
        #endregion

        #region 返回路径下所有文件名
        /// <summary>
        /// 返回路径下所有文件名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetListFilesName(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileSystemInfo info = di;

            if (!info.Exists) return null;

            DirectoryInfo dir = info as DirectoryInfo;
            //不是目录 
            if (dir == null) return null;

            FileSystemInfo[] files = dir.GetFileSystemInfos();
            files = files.OrderByDescending(t => t.LastWriteTime).ToArray();
            List<string> fileNameList = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                //FileInfo file = files[i] as FileInfo;
                /*
                //是文件 
                if (file != null)
                    Console.WriteLine(file.FullName + "\t " + file.Length);
                //对于子目录，进行递归调用 
                else
                    ListFiles(files[i]);
                */
                fileNameList.Add(files[i].Name);
            }
            return fileNameList;
        }
        #endregion

        #region 将xml对象内容字符串转换为DataSet
        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        #endregion

        #region 获取开始和结束标签中内容名
        public static List<string> FindElementName(string content, string startStr, string endStr)
        {
            List<int> startIndexList = new List<int>();
            List<int> endIndexList = new List<int>();

            int tmpIndex = 0;
            while (true)
            {
                tmpIndex = content.IndexOf(startStr, tmpIndex + startStr.Length);
                if (tmpIndex < 0)
                    break;
                else
                    startIndexList.Add(tmpIndex);
            }
            tmpIndex = 0;
            while (true)
            {
                tmpIndex = content.IndexOf(endStr, tmpIndex + endStr.Length);
                if (tmpIndex < 0)
                    break;
                else
                    endIndexList.Add(tmpIndex);
            }

            if (startIndexList.Count > 0 && endIndexList.Count == startIndexList.Count)
            {
                List<string> ElementNameList = new List<string>();
                for (int i = 0; i < startIndexList.Count; i++)
                {
                    ElementNameList.Add(content.Substring(startIndexList[i] + startStr.Length, endIndexList[i] - startIndexList[i] - endStr.Length));
                }
                return ElementNameList;
            }
            return null;
        }
        #endregion

        #region 获取一段字符串中开始和结束为止之间的内容
        /// <summary>
        /// 获取一段字符串中开始和结束为止之间的内容
        /// </summary>
        /// <param name="panelStr">内容</param>
        /// <param name="startStr">开始字段</param>
        /// <param name="endStr">结束字段</param>
        /// <returns></returns>
        public static List<string> FindValueByPanel(string panelStr, string startStr, string endStr)
        {
            List<string> result = new List<string>();

            while (true)
            {
                if (panelStr.Contains(startStr))
                {
                    try
                    {
                        int startFlg = 0;
                        int endCount = 0;
                        startFlg = panelStr.IndexOf(startStr) + startStr.Length;
                        endCount = panelStr.Substring(startFlg).IndexOf(endStr);
                        result.Add(panelStr.Substring(startFlg, endCount));
                        panelStr = panelStr.Remove(0, endCount + panelStr.IndexOf(startStr) + startStr.Length + endStr.Length);
                    }
                    catch { break; }
                }
                else
                    break;
            }
            return result;
        }
        #endregion

        public static void CompressFile(string sourceFile, string destinationFile)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                //if (checkCount != buffer.Length) throw new ApplicationException();
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (GZipStream compressStream = new GZipStream(destinationStream, CompressionMode.Compress))
                    {
                        byte[] buffer = new byte[1024 * 64];
                        int checkCount = 0;
                        while ((checkCount = sourceStream.Read(buffer, 0, buffer.Length)) >= buffer.Length)
                        {
                            compressStream.Write(buffer, 0, buffer.Length);
                        }
                        compressStream.Write(buffer, 0, checkCount);
                    }
                }
            }
        }

        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="srcdir"></param>
        /// <param name="desdir"></param>
        public static void CopyDirectory(string srcdir, string desdir)
        {
            string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);

            string desfolderdir = desdir + "\\" + folderName;
            //string desfolderdir = desdir;

            if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
            {
                desfolderdir = desdir + folderName;
            }
            string[] filenames = Directory.GetFileSystemEntries(srcdir);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {

                    string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(currentdir))
                    {
                        Directory.CreateDirectory(currentdir);
                    }

                    CopyDirectory(file, desfolderdir);
                }

                else // 否则直接copy文件
                {
                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                    srcfileName = desfolderdir + "\\" + srcfileName;

                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }

                    File.Copy(file, srcfileName);
                }
            }
        }
    }
}
