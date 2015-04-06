using System;
using System.Text;
using System.Net;
using System.IO;
using System.Data;


namespace KCommonLib.Common
{
    public class SendEsms
    {
        //static void Main(string[] args)
        //{
        //    string strContent = "中国短信网测试c#";
        //    //GET 方式
        //    String getReturn = doGetRequest("http://http.c123.com/tx/?uid=9999&pwd=fa246d0262c3925617b0c72bb20eeb1d&mobile=13585519197,13900008888&content=" + strContent);
        //    Console.WriteLine("Get response is: " + getReturn);
        //    StringBuilder sbTemp = new StringBuilder();

        //    //POST
        //    sbTemp.Append("uid=9999&pwd=fa246d0262c3925617b0c72bb20eeb1d&mobile=13585519197,13900008888&content=" + strContent);
        //    byte[] bTemp = System.Text.Encoding.GetEncoding("GBK").GetBytes(sbTemp.ToString());
        //    String postReturn = doPostRequest("http://http.c123.com/tx/", bTemp);
        //    Console.WriteLine("Post response is: " + postReturn);

        //}

        public static string SendMessage(string mobiles, string content)
        {
            StringBuilder sbTemp = new StringBuilder();
            sbTemp.Append("uid=194699&pwd=911283ddcfe12ba4ca8c799a10d2bdf1&mobile=" + mobiles + "&content=" + content);
            byte[] bTemp = System.Text.Encoding.GetEncoding("GBK").GetBytes(sbTemp.ToString());
            String postReturn = doPostRequest("http://http.c123.com/tx/", bTemp);
            //Console.WriteLine("Post response is: " + postReturn);

            return postReturn;
        }

        //POST方式发送得结果
        public static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }

        //GET方式发送得结果
        static String doGetRequest(string url)
        {
            HttpWebRequest hwRequest;
            HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "GET";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }

        static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }
    }
}
