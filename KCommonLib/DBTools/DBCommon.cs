using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCommonLib.DBTools
{
    public class DBCommon
    {
        #region 格式化字符串，符合SQL语句
        /// <summary>
        /// 格式化字符串，符合SQL语句
        /// </summary>
        /// <param name="formatStr">需要格式化的字串</param>
        /// <returns>字串</returns>
        public static string inSQL(string formatStr)
        {
            string rStr = formatStr;
            if (formatStr != null && formatStr != string.Empty)
            {
                rStr = rStr.Replace("'", "''");
                //rStr = rStr.Replace("\"", "\"\"");
            }
            return rStr;
        }

        /// <summary>
        /// 格式化字串,是inSQL的反向
        /// </summary>
        /// <param name="formatStr">需要格式化的字串</param>
        /// <returns></returns>
        public static string outSQL(string formatStr)
        {
            string rStr = formatStr;
            if (rStr != null)
            {
                rStr = rStr.Replace("''", "'");
                rStr = rStr.Replace("\"\"", "\"");
            }
            return rStr;
        }

        /// <summary>
        /// 查询SQL语句，删除一些SQL注入问题
        /// </summary>
        /// <param name="formatStr">需要格式化的字串</param>
        /// <returns></returns>
        public static string querySQL(string formatStr)
        {
            string rStr = formatStr;
            if (rStr != null && rStr != "")
            {
                rStr = rStr.Replace("'", "");
            }
            return rStr;
        }
        #endregion

        #region 返回数据库连接字符串
        /// <summary>
        /// 返回数据库连接字符串
        /// </summary>
        /// <param name="name">config配置文件名称</param>
        /// <returns></returns>
        public static string GetConstr(string name)
        {
            return System.Configuration.ConfigurationManager.AppSettings[name];
        }
        #endregion

    }
}
