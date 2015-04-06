using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web;

namespace KCommonLib.Common
{
    public class MyWebRequest
    {
        /// <summary>
        /// 设置请求超时
        /// </summary>
        private static int _Timeout = 60;
        public static int Timeout
        {
            get
            {
                return _Timeout * 1000;
            }
            set
            {
                _Timeout = value;
            }
        }
        /// <summary>
        /// 下载文件到本地文件
        /// </summary>
        /// <param name="?"></param>
        public static void DownloadFile(string address, string fileName)
        {
            FileStream stream1 = null;
            bool flag1 = false;
            try
            {
                stream1 = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                WebRequest request1 = WebRequest.Create(address);
                request1.Timeout = Timeout;//加上超时时间
                //request1.Credentials = this.Credentials;
                //this.CopyHeadersTo(request1);
                WebResponse response1 = request1.GetResponse();
                //this.m_responseHeaders = response1.Headers;
                long num1 = response1.ContentLength;
                num1 = ((num1 == -1) || (num1 > 0x7fffffff)) ? ((long)0x7fffffff) : num1;
                byte[] buffer1 = new byte[Math.Min(0x2000, (int)num1)];
                using (Stream stream2 = response1.GetResponseStream())
                {
                    int num2;
                    do
                    {
                        num2 = stream2.Read(buffer1, 0, buffer1.Length);
                        stream1.Write(buffer1, 0, num2);
                    }
                    while (num2 != 0);
                }
                flag1 = true;
            }
            catch (Exception exception1)
            {
                throw new WebException("net_webclient", exception1);
            }
            finally
            {
                if (stream1 != null)
                {
                    stream1.Close();
                    stream1 = null;
                }
                if (!flag1)
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary> 
        /// 获取当前请求的IP地址 
        /// </summary> 
        /// <returns></returns> 
        public static string GetIP()
        {
            //获取IP地址 
            HttpRequest request = HttpContext.Current.Request;
            string ipAddress = string.Empty;
            if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null || request.ServerVariables["HTTP_X_FORWARDED_FOR"] == "")
            {
                ipAddress = request.ServerVariables["REMOTE_ADDR"];
            }
            else if (request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(",") >= 0)
            {
                int index = request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(",");
                ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(0, index - 1);
            }
            else if (request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(";") >= 0)
            {
                int index = request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(";");
                ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(0, index - 1);
            }
            else
            {
                ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            if (ipAddress == "127.0.0.1")
            {
                ipAddress = GetLocalhostIPAddress();
            }
            return ipAddress;
        }

        /// <summary> 
        /// 获取调用用户IP地址
        /// </summary> 
        /// <returns></returns> 
        public static string GetCustomerIP()
        {
            string CustomerIP = "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                CustomerIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                CustomerIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            return CustomerIP;
        }

        /// <summary> 
        /// 获取本机IP 
        /// </summary> 
        /// <returns></returns> 
        private static string GetLocalhostIPAddress()
        {
            string hostName = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry hostInfo = System.Net.Dns.GetHostByName(hostName);
            System.Net.IPAddress[] IpAddr = hostInfo.AddressList;
            string localIP = string.Empty;
            for (int i = 0; i < IpAddr.Length; i++)
            {
                localIP += IpAddr[i].ToString();
            }
            return localIP;
        }

        // 向指定地址POST内容
        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return ret;
        }

        #region 根据路径返回页面字符串
        /// <summary>
        /// 根据路径返回页面字符串
        /// </summary>
        /// <param name="?"></param>
        public static string DownloadFile(string address)
        {
            //FileStream stream1 = null;
            //bool flag1 = false;
            try
            {
                //stream1 = new FileStream(HttpContext.Current.Server.MapPath(fileName), FileMode.Create, FileAccess.Write);
                WebRequest request1 = WebRequest.Create(address);
                request1.Timeout = Timeout;//加上超时时间
                //request1.Credentials = this.Credentials;
                //this.CopyHeadersTo(request1);
                WebResponse response1 = request1.GetResponse();
                //this.m_responseHeaders = response1.Headers;
                long num1 = response1.ContentLength;
                num1 = ((num1 == -1) || (num1 > 0x7fffffff)) ? ((long)0x7fffffff) : num1;
                string result = string.Empty;
                byte[] buffer1 = new byte[Math.Min(0x2000, (int)num1)];
                using (Stream stream2 = response1.GetResponseStream())
                {
                    StreamReader sRead = new StreamReader(stream2, System.Text.Encoding.GetEncoding("GB2312"));
                    result = sRead.ReadToEnd();

                    //int num2;
                    //do
                    //{
                    //    num2 = stream2.Read(buffer1, 0, buffer1.Length);
                    //    //stream1.Write(buffer1, 0, num2);
                    //    result.Append(System.Text.Encoding.GetEncoding("GB2312").GetString(buffer1));
                    //    buffer1
                    //}
                    //while (num2 != 0);
                }
                //flag1 = true;
                return result;
            }
            catch (Exception exception1)
            {
                throw new WebException("net_webclient", exception1);
            }
            finally
            {
                //if (stream1 != null)
                //{
                //    stream1.Close();
                //    stream1 = null;
                //}
                //if (!flag1)
                //{
                //    try
                //    {
                //        //File.Delete(fileName);
                //    }
                //    catch
                //    {
                //    }
                //}
            }
        }
        #endregion

        // 简单格式化json字符串
        public static Dictionary<string, string> GetJsonValue(string jsonStr)
        {
            if (!string.IsNullOrEmpty(jsonStr))
            {
                jsonStr = jsonStr.Replace("{", "").Replace("}", "");
                string[] tmp1 = jsonStr.Split(',');
                if (tmp1 != null && tmp1.Length > 0)
                {
                    Dictionary<string, string> result = new Dictionary<string, string>();
                    for (int i = 0; i < tmp1.Length; i++)
                    {
                        try
                        {
                            tmp1[i] = tmp1[i].Replace("\"", "");
                            result.Add(tmp1[i].Substring(0, tmp1[i].IndexOf(":")),
                                tmp1[i].Substring(tmp1[i].IndexOf(":") + 1));
                        }
                        catch { }
                    }
                    return result;
                }
            }
            return null;
        }
    }
}
