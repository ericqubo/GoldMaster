using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;

namespace KCommonLib.Common
{
    /// <summary>
    /// TCP/IP 信息传递 
    /// </summary>
    public static class TcpLogManager
    {
        //static TcpClient client = null;
        //static NetworkStream ns = null;
        static TcpListener lis = null;
        static Dictionary<TcpClient,NetworkStream> list;
        static List<TcpClient> errorList;

        static TcpLogManager()
        {
            IPHostEntry ip = Dns.GetHostEntry(Environment.MachineName);
            list = new Dictionary<TcpClient,NetworkStream>();
            lis = new TcpListener(ip.AddressList[0], 6666);
            lis.Start();
            lis.BeginAcceptTcpClient(new AsyncCallback(GetClient), null);
            errorList = new List<TcpClient>();
        }
        public static void Prepare()
        {

        }

        static void GetClient(IAsyncResult result)
        {
            TcpClient client = lis.EndAcceptTcpClient(result);
            NetworkStream ns = client.GetStream();
            list.Add(client,ns);
            lis.BeginAcceptTcpClient(new AsyncCallback(GetClient), null);
            //ns = client.GetStream();
        }


        /// <summary>
        /// 编写日志
        /// </summary>
        /// <param name="logMessage">日志内容</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteLog(string logMessage)
        {
            string testWords = logMessage;
            
            foreach (KeyValuePair<TcpClient,NetworkStream> pair in list)
            {
                try
                {
                    byte[] bytes = Encoding.Default.GetBytes(testWords + "\r\n");
                    pair.Value.Write(bytes, 0, bytes.Length);
                }
                catch
                {
                    if (pair.Key != null)
                    {
                        pair.Key.Client.Close();
                    } 
                    if (pair.Value != null)
                    {
                        pair.Value.Dispose();
                    }
                    //list.Remove(pair.Key);
                    errorList.Add(pair.Key);
                }
            }
            foreach (TcpClient cli in errorList)
            {
                list.Remove(cli);
            }
            errorList.Clear();
        }

        /// <summary>
        /// 编写日志
        /// </summary>
        /// <param name="logMessage">日志内容</param>
        public static void WriteLog(NetworkStream ns, string logMessage)
        {
            string testWords = logMessage;
            byte[] bytes = Encoding.Default.GetBytes(testWords + "\r\n");
            ns.Write(bytes, 0, bytes.Length);  
        }
    }
}
