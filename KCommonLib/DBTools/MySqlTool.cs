using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace KCommonLib.DBTools.MySqlTool
{
    public class DBHelper
    {
        /// <summary>得到连接对象</summary> 
        /// <returns></returns> 
        public MySqlConnection GetConn()
        {
            //MySqlConnection mysqlconn = new MySqlConnection("server=222.73.129.143;user id=sq_minos01;password=wj840728;database=sq_minos01;CharSet=utf8");
            string msc = KCommonLib.DBTools.DBCommon.GetConstr("MySqlConnection");
            MySqlConnection mysqlconn = null;
            if (string.IsNullOrEmpty(msc))
                mysqlconn = new MySqlConnection("server=10.6.3.50;user id=root;password=wxjbxl080621;database=cbx_db;CharSet=utf8");
            else
                mysqlconn = new MySqlConnection(msc);

            return mysqlconn;
        }
    }



    public class SQLHelper : DBHelper
    {
        /// <summary>查询操作</summary> 
        /// <param name="sql"></param>        
        /// <returns></returns> 
        public DataTable Selectinfo(string sql)
        {
            
            MySqlConnection mysqlconn = null;
            MySqlDataAdapter sda = null;
            DataTable dt = null;
            try
            {
                mysqlconn = base.GetConn();
                sda = new MySqlDataAdapter(sql, mysqlconn);
                dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>查询操作</summary> 
        /// <param name="sql"></param>        
        /// <returns></returns> 
        public DataTable Selectinfo(string sql, MySqlConnection mySqlCon)
        {
            //MySqlConnection mysqlconn = null;
            MySqlDataAdapter sda = null;
            DataTable dt = null;
            try
            {
                //mysqlconn = base.GetConn();
                sda = new MySqlDataAdapter(sql, mySqlCon);
                dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>增删改操作</summary> 
        /// <param name="sql">sql语句</param>         
        /// <returns>执行后的条数</returns>         
        public int AddDelUpdate(string sql)
        {
            MySqlConnection conn = null; MySqlCommand cmd = null;
            try
            {
                conn = base.GetConn();
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>增删改操作</summary> 
        /// <param name="sql">sql语句</param>         
        /// <returns>执行后的条数</returns>         
        public int AddDelUpdate(string sql, MySqlConnection mySqlCon)
        {
            //MySqlConnection conn = null; 
            MySqlCommand cmd = null;
            try
            {
                //conn = base.GetConn();
                mySqlCon.Open();
                cmd = new MySqlCommand(sql, mySqlCon);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                mySqlCon.Close();
            }
        }

        /**/
        /// <summary>
        /// 分析用户请求是否正常
        /// </summary>
        /// <param name="Str">传入用户提交数据</param>
        /// <returns>返回是否含有SQL注入式攻击代码</returns>
        public string ProcessSqlStr(string Str)
        {
            string SqlStr = "exec|insert|select|delete|update|count|chr|mid|master|truncate|char|declare";
            string ReturnValue = Str;
            try
            {
                if (Str != "")
                {
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.ToLower().IndexOf(ss) >= 0)
                        {
                            ReturnValue = "";
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = "";
            }
            if (Str.Length > 20)
            {
                ReturnValue = "";
            }
            return ReturnValue;
        }
    }
}

