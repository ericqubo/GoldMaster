using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using KCommonLib.Common;


namespace KCommonLib.DBTools
{
    /// <summary>
    /// ���ݿ������
    /// </summary>
    public abstract class MySQLHelper
    {
        private static SqlConnection con;
        private static string LogSrc = System.Configuration.ConfigurationManager.AppSettings["LogSrc"] == null ?
            @"D:\SqlErrors\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" :
            System.Configuration.ConfigurationManager.AppSettings["LogSrc"] + DateTime.Now.ToString("yyyy-MM-dd") + "\\";

        /// <summary>
        /// �������ݿⷵ�� dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sqlStr, string conStr)
        {
            SqlDataAdapter da;
            SqlCommand cmd;
            con = new SqlConnection(conStr);
            cmd = new SqlCommand(sqlStr, con);
            cmd.CommandTimeout = 60 * 60 * 3;
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds, "Table");

            }
            catch (Exception ex)
            {
                try
                {
                    MyIO.WriteLog(DateTime.Now.ToString() + ex.Message, LogSrc + "Sqllog.txt", false);
                }
                catch { }
                Console.WriteLine(DateTime.Now + " " + cmd.CommandText);
                Console.WriteLine(DateTime.Now + " " + ex.Message);
                return null;
            }
            finally
            {
                //da.SelectCommand.Dispose();
                da.Dispose();
            }
            return ds;
        }

        /// <summary>
        /// �������ݿⷵ�� dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(SqlCommand cmd, string conStr)
        {
            SqlDataAdapter da;

            con = new SqlConnection(conStr);
            cmd.Connection = con;
            cmd.CommandTimeout = 60 * 60 * 3;
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds, "Table");

            }
            catch (Exception ex)
            {
                try
                {
                    MyIO.WriteLog(DateTime.Now.ToString() + ex.Message, LogSrc + "Sqllog.txt", false);
                }
                catch { }
                Console.WriteLine(DateTime.Now + " " + cmd.CommandText);
                Console.WriteLine(DateTime.Now + " " + ex.Message);
                return null;
            }
            finally { da.Dispose(); }
            return ds;
        }

        /// <summary>
        /// ���ݱ������ر��е��ֶ�
        /// exec sp_mshelpcolumns 'V_GRIP_YC_COMMENT '
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public static DataSet GetColumsByTable(string tableName, string conStr)
        {
            SqlDataAdapter da;
            SqlCommand cmd;
            con = new SqlConnection(conStr);
            cmd = new SqlCommand("exec sp_mshelpcolumns '" + tableName + "'", con);
            cmd.CommandTimeout = 60 * 60 * 3;
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds, "Table");
            }
            catch (Exception ex)
            {
                try
                {
                    MyIO.WriteLog(DateTime.Now.ToString() + ex.Message, LogSrc + "Sqllog.txt", false);
                }
                catch { }
                Console.WriteLine(DateTime.Now + " " + cmd.CommandText);
                Console.WriteLine(DateTime.Now + " " + ex.Message);
                return null;
            }
            finally { da.Dispose(); }
            return ds;
        }

        /// <summary>
        /// �������ݿⷵ���������ж���
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static object GetObject(string sqlStr, string conStr)
        {
            object obj = new object();
            con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            try
            {
                con.Open();
                obj = cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                try
                {
                    KCommonLib.Common.MyIO.WriteLog(DateTime.Now.ToString() + ex.Message, LogSrc + "Sqllog.txt", false);
                }
                catch { }
                Console.WriteLine(DateTime.Now + " " + cmd.CommandText);
                Console.WriteLine(DateTime.Now + " " + ex.Message);
                return null;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
            return obj;
        }

        /// <summary>
        /// �������ݿⷵ�ض���
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static object GetObject(SqlCommand cmd, string conStr)
        {
            object obj = new object();
            con = new SqlConnection(conStr);
            //SqlCommand cmd = new SqlCommand(sqlStr, con);
            cmd.Connection = con;
            try
            {
                con.Open();
                obj = cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                try
                {
                    KCommonLib.Common.MyIO.WriteLog(DateTime.Now.ToString() + ex.Message, LogSrc + "Sqllog.txt", false);
                }
                catch { }
                Console.WriteLine(DateTime.Now + " " + cmd.CommandText);
                Console.WriteLine(DateTime.Now + " " + ex.Message);
                return null;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
            return obj;
        }

        /// <summary>
        /// �������ݿⷵ��Ӱ������
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int GetInt(string sqlStr, string conStr)
        {
            int cnt = -1;
            con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            cmd.CommandTimeout = 60 * 60 * 3;
            try
            {
                con.Open();
                cnt = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                try
                {
                    KCommonLib.Common.MyIO.WriteLog(DateTime.Now.ToString() + ex.Message, LogSrc + "Sqllog.txt", false);
                }
                catch { }
                Console.WriteLine(DateTime.Now + " " + cmd.CommandText);
                Console.WriteLine(DateTime.Now + " " + ex.Message);
                return -1;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
            return cnt;
        }

        /// <summary>
        /// �������ݿⷵ��Ӱ������
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int GetInt(string sqlStr, string conStr, out string exceptionMsg)
        {
            exceptionMsg = "����ɹ�";
            int cnt = -1;
            con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            cmd.CommandTimeout = 60 * 60 * 3;
            try
            {
                con.Open();
                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                exceptionMsg = ex.Message;
                return -1;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
            return cnt;
        }

        /// <summary>
        /// �������ݿⷵ��Ӱ������
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int GetInt(SqlCommand cmd, string conStr)
        {
            int cnt = -1;
            con = new SqlConnection(conStr);
            cmd.Connection = con;
            //SqlCommand cmd = new SqlCommand(sqlStr, con);
            try
            {
                con.Open();
                cnt = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                try
                {
                    KCommonLib.Common.MyIO.WriteLog(DateTime.Now.ToString() + ex.Message, LogSrc + "Sqllog.txt", false);
                }
                catch { }
                Console.WriteLine(DateTime.Now + " " + cmd.CommandText);
                Console.WriteLine(DateTime.Now + " " + ex.Message);
                return -1;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
            return cnt;
        }
    }
}



