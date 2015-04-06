using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Reflection;

namespace KCommonLib.Common
{
    /// <summary>
    /// WCF服务动态调用 
    /// </summary>
    public class WCFClient
    {
        /// <summary>
        /// 返回动态条用WCF实例
        /// </summary>
        /// <typeparam name="T">返回泛型类型</typeparam>
        /// <param name="pUrl">调用地址</param>
        /// <param name="pMethodName">名称</param>
        /// <param name="pParams">参数 </param>
        /// <returns></returns>
        public static object ExecuteMethod<T>(string pUrl, string pMethodName, params object[] pParams)
        {
            EndpointAddress address = new EndpointAddress(pUrl);
            Binding bindinginstance = null;
            NetTcpBinding ws = new NetTcpBinding();
            ws.MaxReceivedMessageSize = 20971520;
            ws.Security.Mode = SecurityMode.None;
            bindinginstance = ws;
            using (ChannelFactory<T> channel = new ChannelFactory<T>(bindinginstance, address))
            {
                T instance = channel.CreateChannel();
                using (instance as IDisposable)
                {
                    try
                    {
                        Type type = typeof(T);
                        MethodInfo mi = type.GetMethod(pMethodName);
                        return mi.Invoke(instance, pParams);
                    }
                    catch (TimeoutException)
                    {
                        (instance as ICommunicationObject).Abort();
                        throw;
                    }
                    catch (CommunicationException)
                    {
                        (instance as ICommunicationObject).Abort();
                        throw;
                    }
                    catch
                    {
                        (instance as ICommunicationObject).Abort();
                        throw;
                    }
                }
            }
        }
    }
}
