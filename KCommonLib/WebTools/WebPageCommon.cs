using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Visitors;
using Winista.Text.HtmlParser.Tags;

namespace KCommonLib.WebTools
{
    /// <summary>
    /// 页面一般逻辑处理方法集
    /// </summary>
    public class WebPageCommon
    {
        #region 检测用户提交页面
        /// <summary>
        /// 检测用户提交页面
        /// </summary>
        /// <param name="rq"></param>
        public static void Check_Post_Url(HttpContext rq)
        {
            string WebHost = "";
            if (rq.Request.ServerVariables["SERVER_NAME"] != null)
            {
                WebHost = rq.Request.ServerVariables["SERVER_NAME"].ToString();
            }

            string From_Url = "";
            if (rq.Request.UrlReferrer != null)
            {
                From_Url = rq.Request.UrlReferrer.ToString();
            }

            if (From_Url == "" || WebHost == "")
            {
                rq.Response.Write("禁止外部提交资料!");
                rq.Response.End();
            }
            else
            {
                WebHost = "HTTP://" + WebHost.ToUpper();
                From_Url = From_Url.ToUpper();
                int a = From_Url.IndexOf(WebHost);
                if (From_Url.IndexOf(WebHost) < 0)
                {
                    rq.Response.Write("禁止外部提交资料!!");
                    rq.Response.End();
                }
            }

        }
        #endregion

        #region js弹框

        /// <summary>
        /// js信息提示框
        /// </summary>
        /// <param name="Message">提示信息文字</param>
        /// <param name="ReturnUrl">返回地址</param>
        /// <param name="rq"></param>
        public static void MessBox(string Message, string ReturnUrl, HttpContext rq)
        {
            System.Text.StringBuilder msgScript = new System.Text.StringBuilder();
            msgScript.Append("<script language=JavaScript>\n");
            msgScript.Append("alert(\"" + Message + "\");\n");
            msgScript.Append("parent.location.href='" + ReturnUrl + "';\n");
            msgScript.Append("</script>\n");
            rq.Response.Write(msgScript.ToString());
            rq.Response.End();
        }

        /// <summary>
        /// 弹出Alert信息窗
        /// </summary>
        /// <param name="Message">信息內容</param>
        public static void MessBox(string Message)
        {
            System.Text.StringBuilder msgScript = new System.Text.StringBuilder();
            msgScript.Append("<script language=JavaScript>\n");
            msgScript.Append("alert(\"" + Message + "\");\n");
            msgScript.Append("</script>\n");
            HttpContext.Current.Response.Write(msgScript.ToString());
        }

        #endregion

        #region "隐藏IP地址左后一位，用*替代"
        /// <summary>
        /// 隐藏IP地址左后一位，用*替代
        /// </summary>
        /// <param name="Ipaddress">IP地址:192.168.34.23</param>
        /// <returns></returns>
        public static string HidenLastIp(string Ipaddress)
        {
            return Ipaddress.Substring(0, Ipaddress.LastIndexOf(".")) + ".*";
        }
        #endregion

        #region "获取用户IP地址"
        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {

            string user_IP = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    user_IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
            }
            else
            {
                user_IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return user_IP;
        }
        #endregion

        #region "MD5加密"
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字元</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }

            if (code == 32)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }

            return strEncrypt;
        }
        public static string MD5_Java(string encypStr, string charset)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new
            System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = Encoding.GetEncoding(charset).GetBytes(encypStr);
            bs = md5.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }
            string password = s.ToString();
            return password;
        }
        #endregion

        #region 脚本提示信息，并且跳转到最上层框架
        /// <summary>
        /// 脚本提示信息，并且跳转到最上层框架
        /// </summary>
        /// <param name="Msg">信息内容，可以为空，为空表示不出现提示视窗</param>
        /// <param name="Url">跳转地址</param>
        public static string Hint(string Msg, string Url)
        {
            System.Text.StringBuilder rStr = new System.Text.StringBuilder();

            rStr.Append("<script language='javascript'>");
            if (Msg != "")
                rStr.Append(" alert('" + Msg + "');");

            if (Url != "")
                rStr.Append(" window.top.location.href = '" + Url + "';");

            rStr.Append("</script>");

            return rStr.ToString();
        }
        #endregion

        #region 脚本提示信息，并且跳转到当前框架内
        /// <summary>
        /// 脚本提示信息，并且跳转到当前框架内
        /// </summary>
        /// <param name="Msg">信息内容，可以为空，为空表示不出现提示视窗</param>
        /// <param name="Url">跳转地址，可以自己写入脚本</param>
        /// <returns></returns>
        public static string LocalHintJs(string Msg, string Url)
        {
            System.Text.StringBuilder rStr = new System.Text.StringBuilder();

            rStr.Append("<script language='JavaScript'>\n");
            if (Msg != "")
                rStr.AppendFormat(" alert('{0}');\n", Msg);

            if (Url != "")
                rStr.Append(Url + "\n");
            rStr.Append("</script>");

            return rStr.ToString();
        }

        #endregion

        #region 脚本提示信息，并且跳转到当前框架内，地址为空时，返回上页
        /// <summary>
        /// 脚本提示信息，并且跳转到当前框架内，地址为空时，返回上页
        /// </summary>
        /// <param name="Msg">信息内容，可以为空，为空表示不出现提示视窗</param>
        /// <param name="Url">跳转地址，为空时，返回上页</param>
        /// <returns></returns>
        public static string LocalHint(string Msg, string Url)
        {
            System.Text.StringBuilder rStr = new System.Text.StringBuilder();

            rStr.Append("<script language='JavaScript'>\n");
            if (Msg != "")
                rStr.AppendFormat(" alert('{0}');\n", Msg);

            if (Url != "")
                rStr.AppendFormat(" window.location.href = '" + Url + "';\n");
            else
                rStr.AppendFormat(" window.history.back();");

            rStr.Append("</script>\n");

            return rStr.ToString();
        }
        #endregion

        #region "检测是否为有效的邮件地址格式"
        /// <summary>
        /// 检测是否为有效的邮件地址格式
        /// </summary>
        /// <param name="strIn">邮件地址</param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="loadSrc"></param>
        /// <returns></returns>
        public static bool UpLoadFile(HtmlInputFile inputFile, string loadSrc)
        {
            if (inputFile == null)
                return false;
            try
            {
                inputFile.PostedFile.SaveAs(loadSrc);
                return true;
            }
            catch { return false; }
        }

        #endregion

        #region 从html代码中抽取某个节点的元素
        /// <summary>
        /// 从html代码中抽取某个节点的元素
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static string GetHodeValue(string htmlCode, string tagName)
        {
            //String htmlcode = "<HTML><HEAD><TITLE>AAA</TITLE></HEAD><BODY>"
            //        + "<a href='http://topic.csdn.net/u/20080522/14/0ff402ef-c382-499a-8213-ba6b2f550425.html'>连接1</a>"
            //        + "<a href='http://topic.csdn.net'>连接2</a></BODY></HTML>";

            // 创建Parser对象根据传给字符串和指定的编码
            Parser parser = Parser.CreateParser(htmlCode, "GBK");
            // 创建HtmlPage对象HtmlPage(Parser parser)
            HtmlPage page = new HtmlPage(parser);
            try
            {
                // HtmlPage extends visitor,Apply the given visitor to the current
                // page.
                parser.VisitAllNodesWith(page);
            }
            catch (ParserException e1)
            {
                //e1 = null;
            }
            // 所有的节点
            NodeList nodelist = page.Body;
            // 建立一个节点filter用于过滤节点
            NodeFilter filter = new TagNameFilter("li");
            // 得到所有过滤后，想要的节点
            nodelist = nodelist.ExtractAllNodesThatMatch(filter, true);
            if (nodelist != null && nodelist.Size() > 0)
            {
                // 获取一个节点
                LinkTag link = (LinkTag)nodelist.ElementAt(0);
                // 获取节点中的一个元素
                return link.GetAttribute(tagName);
            }
            else
            {
                return null;
            }

            //for (int i = 0; i < nodelist.Size(); i++)
            //{
            //    LinkTag link = (LinkTag)nodelist.ElementAt(i);
            //    // 链接地址
            //    return link.GetAttribute(tagName);
            //    // 链接名称
            //    //System.out.println(link.getStringText());
            //}
        }
        #endregion
    }
}
