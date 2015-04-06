using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace KCommonLib.DBTools
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class SQLHelperNoStatic
    {
        private SqlConnection con;
        public SQLHelperNoStatic(string conStr)
        {
            con = new SqlConnection(conStr);
        }

        /// <summary>
        /// 驱动数据库返回dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sqlStr)
        {
            SqlDataAdapter da;
            //con = new SqlConnection(conStr);
            da = new SqlDataAdapter(sqlStr, con);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds, "Table");
            }
            catch (Exception ex)
            {
                try
                {
                    KCommonLib.Common.MyIO.WriteLog(ex.Message + "(" + sqlStr + ")", @"D:\SqlLog\" + DateTime.Now.ToShortDateString() + " Error.txt", false);
                }
                catch { }
                return null;
            }
            finally 
            {
                da.Dispose();
            }
            return ds;
        }

        /// <summary>
        /// 驱动数据库返回对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public object GetObject(string sqlStr)
        {
            object obj = new object();
            //con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            try
            {
                try { con.Close(); }
                catch { }
                con.Open();
                obj = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                try
                {
                    KCommonLib.Common.MyIO.WriteLog(ex.Message + "(" + sqlStr + ")", @"D:\SqlLog\" + DateTime.Now.ToShortDateString() + " Error.txt", false);
                }
                catch { }
                return null;
            }
            finally
            {
                con.Close();
            }
            return obj;
        }

        /// <summary>
        /// 驱动数据库返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int GetInt(string sqlStr)
        {
            int cnt = -1;
            //con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            try
            {
                try { con.Close(); }
                catch { }
                con.Open();
                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                try
                {
                    KCommonLib.Common.MyIO.WriteLog(ex.Message + "(" + sqlStr + ")", @"D:\SqlLog\" + DateTime.Now.ToShortDateString() + " Error.txt", false);
                }
                catch { }
                return -1;
            }
            finally
            {
                con.Close();
            }
            return cnt;
        }

        /// <summary>
        /// 驱动数据库返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int GetInt(SqlCommand cmd)
        {
            int cnt = -1;
            //con = new SqlConnection(conStr);
            cmd.Connection = con;
            //SqlCommand cmd = new SqlCommand(sqlStr, con);
            try
            {
                try { con.Close(); }
                catch { }
                con.Open();
                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                try
                {
                    KCommonLib.Common.MyIO.WriteLog(ex.Message + "(" + cmd.CommandText + ")", @"D:\SqlLog\" + DateTime.Now.ToShortDateString() + " Error.txt", false);
                }
                catch { }
                return -1;
            }
            finally
            {
                con.Close();
            }
            return cnt;
        }

    }
}



