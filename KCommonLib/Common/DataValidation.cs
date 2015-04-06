using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace KCommonLib.Common
{
    /// <summary>
    /// 数据格式验证
    /// </summary>
    public class DataValidation
    {
        public static string GetSubString(object obj, int count)
        {
            string str = string.Empty;
            try { str = obj.ToString(); }
            catch { }

            if (!string.IsNullOrEmpty(str))
                return str.Length > count ? str.Substring(0, count) + "..." : str;
            return str;

        }

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');

            return ret;
        }

        public static string GetCNWeekday(object date)
        {
            DateTime da = DateTime.Now;
            try { DateTime.TryParse(date.ToString(), out da); }
            catch { }

            switch (da.DayOfWeek.ToString())
            {
                case "Monday": return "星期一";
                case "Tuesday": return "星期二";
                case "Wednesday": return "星期三";
                case "Thursday": return "星期四";
                case "Friday": return "星期五";
                case "Saturday": return "星期六";
                case "Sunday": return "星期日";
                default: return "星期一";
            }
        }

        /// <summary>
        /// 验证是否为日期格式
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool isDate(string content)
        {
            try
            {
                DateTime.Parse(content);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool isNum(string content)
        {
            try
            {
                int.Parse(content);
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool isDouble(string content)
        {
            try
            {
                Double.Parse(content);
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// 检查DataSet是否为空
        /// </summary>
        /// <returns></returns>
        public static bool isNullDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 返回格式化后的字段
        /// </summary>
        /// <param name="value">字段值</param>
        /// <param name="type">格式化类型 1.字符串 2.数字 3.日期 4.摘要</param>
        /// <param name="bdcount">小数点后保留几位</param>
        /// <param name="havebfh">是否百分号</param>
        /// <param name="dtFormatStr">日期格式化字符串</param>
        /// <returns></returns>
        public static string FormatField(object value, string type, int bdcount, bool havebfh, double chengNum, string dtFormatStr)
        {
            switch (type.ToUpper())
            {
                case "STRING":
                    string str = value.ToString();
                    if (str.Substring(0, 1) == "\"")
                        str = str.Remove(0, 1);
                    if (str.Substring(str.Length - 1, 1) == "\"")
                        str = str.Remove(str.Length - 1, 1);
                    if (str == "{}")
                        str = "-";
                    return str;
                case "NUM":
                    double doub = 0;
                    try { doub = Convert.ToDouble(value.ToString()); }
                    catch { return "-"; }
                    if (havebfh)
                        return (doub * chengNum).ToString("F" + bdcount.ToString()) + "%";
                    else
                        return (doub * chengNum).ToString("F" + bdcount.ToString());
                case "DATE":
                    string dtstr = value.ToString();
                    if (dtstr.Substring(0, 1) == "\"")
                        dtstr = dtstr.Remove(0, 1);
                    if (dtstr.Substring(dtstr.Length - 1, 1) == "\"")
                        dtstr = dtstr.Remove(dtstr.Length - 1, 1);
                    DateTime dt = DateTime.Now;
                    try { dt = Convert.ToDateTime(dtstr); }
                    catch { return "-"; }
                    return dt.ToString(dtFormatStr);
                case "CONTENT":
                    string substr = value.ToString();
                    if (substr.Substring(0, 1) == "\"")
                        substr = substr.Remove(0, 1);
                    if (substr.Substring(substr.Length - 1, 1) == "\"")
                        substr = substr.Remove(substr.Length - 1, 1);
                    substr = substr.Replace("  ", "&nbsp;&nbsp;");
                    substr = substr.Replace("\\n", "<br/>");
                    return substr;
                default: return "";
            }
        }

        #region 类型转换

        public static string ToString(object obj)
        {
            if (obj == null)
                return "";
            else
                return obj.ToString().Trim();
        }

        public static string ToString(object obj, int isDateTime)
        {
            if (obj == null)
                return "";
            else
            {
                try
                {
                    return Convert.ToDateTime(obj.ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                }
                catch { return ""; }
            }
        }

        public static int ToInt(object obj)
        {
            int i = 0;
            if (obj == null)
                return 0;
            else
            {
                if (int.TryParse(obj.ToString(), out i))
                {
                    return i;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static DateTime ToDateTime(object obj)
        {
            DateTime dt = new DateTime();
            if (obj != null)
            {
                if (obj.ToString().Length == 8 && !obj.ToString().Contains("-"))
                {
                    string tmpObj = obj.ToString().Insert(4, "-").Insert(7, "-");
                    DateTime.TryParse(tmpObj, out dt);
                }
                else
                {
                    DateTime.TryParse(obj.ToString(), out dt);
                }
                return dt;
            }
            return dt;
        }

        public static string ToDateTimeString(object obj, string format)
        {
            try
            {
                DateTime dt = ToDateTime(obj);
                switch (format)
                {
                    case "yyyy年MM月dd日 星期六 上午 hh时mm分":
                        string formatStr = dt.ToString("yyyy年MM月dd日");
                        formatStr += " " + GetCNWeekday(dt);
                        if (dt.Hour > 12)
                            formatStr += " " + "上午";
                        else
                            formatStr += " " + "下午";
                        formatStr += " " + dt.ToString("HH时mm分");
                        return formatStr;
                    default: return "";
                }
            }
            catch { return ""; }
        }

        public static decimal ToDecimal(object obj)
        {
            decimal i = 0;
            if (obj == null)
                return 0;
            else
            {
                if (decimal.TryParse(obj.ToString(), out i))
                {
                    return i;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static float ToFloat(object obj)
        {
            float i = 0;
            if (obj == null)
                return 0;
            else
            {
                if (float.TryParse(obj.ToString(), out i))
                {
                    return i;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region 过滤html字符串为纯文本
        /// <summary>
        /// 过滤html字符串为纯文本
        /// </summary>
        /// <param name="html">字符内容</param>
        /// <returns></returns>
        public static string FormatHtml(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            //html = html.Replace("　", "");
            return html;
        }
        /// <summary>
        /// 过滤html字符串为纯文本
        /// </summary>
        /// <param name="html">字符内容</param>
        /// <param name="count">返回字数</param>
        /// <returns></returns>
        public static string FormatHtml(string html,int count)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            //html = html.Replace("　", "");
            if (html.Length > count)
                return html.Substring(0, count);
            else
                return html;
        }
        /// <summary>
        /// 过滤html字符串为纯文本
        /// </summary>
        /// <param name="html">字符内容</param>
        /// <returns></returns>
        public static string FormatHtml(string html, string bk)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6;
            if (bk != "img")
                regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            else
                regex6 = new System.Text.RegularExpressions.Regex(@"\<aa[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            //html = html.Replace("　", "");
            return html;
        }
        #endregion

        /// <summary>
        /// 2012-11-13 新增功能，智能伪原创
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FmtNewsToNewNews(string content)
        {
            content = content.Replace("</p>", "\r\n");
            content = content.Replace("</P>", "\r\n");
            content = content.Replace("<br/>", "\r\n");
            content = KCommonLib.Common.DataValidation.FormatHtml(content);
            content = content.Replace("\r\n", "♪");
            content = content.Replace("\r", "♪");
            content = content.Replace("\n", "♪");

            //分段处理
            string[] tmpContentList = content.Split('♪');
            if (tmpContentList.Length > 1)
            {
                List<string> contentList = new List<string>();
                for (int i = 0; i < tmpContentList.Length; i++)
                {
                    if (!string.IsNullOrEmpty(tmpContentList[i]))
                        contentList.Add(tmpContentList[i]);
                }
                if (contentList.Count > 5)
                {
                    contentList.RemoveAt(0);
                    contentList.RemoveAt(contentList.Count - 1);
                }
                List<string> newContentList = new List<string>();
                newContentList.Add(contentList[0]);
                newContentList.Add(contentList[contentList.Count - 1]);
                contentList.RemoveAt(0);
                if (contentList.Count > 0)
                    contentList.RemoveAt(contentList.Count - 1);
                if (contentList.Count > 0 && contentList.Count < 2)
                {
                    newContentList.Add(contentList[0]);
                }
                if (contentList.Count >= 2)
                {
                    foreach (string n in contentList)
                    {
                        if (!string.IsNullOrEmpty(n))
                            newContentList.Add(n);
                    }
                }
                StringBuilder sb = new StringBuilder();
                foreach (string b in newContentList)
                {
                    sb.Append("　　" + b + "<br/>");
                }
                return sb.ToString();
            }

            return tmpContentList[0];
        }

        // 验证手机号码
        public static bool VaildatePhoneNumber(string code)
        {
            Regex reger = new Regex(@"(1[3,5,8][0-9])\d{8}$");
            return reger.IsMatch(code);
        }

        /// <summary>
        /// 替换内容单引号等
        /// </summary>
        /// <param name="InputTxt">Text string need to be escape with slashes</param>
        public static string AddSlashes(string InputTxt)
        {
            // List of characters handled:
            // \000 null
            // \010 backspace
            // \011 horizontal tab
            // \012 new line
            // \015 carriage return
            // \032 substitute
            // \042 double quote
            // \047 single quote
            // \134 backslash
            // \140 grave accent

            string Result = InputTxt;

            try
            {
                Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, @"[\000\010\011\012\015\032\042\047\134\140]", "\\$0");
            }
            catch (Exception Ex)
            {
                // handle any exception here
                Console.WriteLine(Ex.Message);
            }
            return Result;
        }
    }
}
