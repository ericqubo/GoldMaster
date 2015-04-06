//===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//===============================================================================

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections;


namespace KCommonLib.DBTools
{
    //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\somepath\myDb.mdb 

    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class SQLHelper_ac
    {
        private static OleDbConnection con;

        /// <summary>
        /// 驱动数据库返回dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sqlStr, string conStr)
        {

            OleDbDataAdapter da;
            con = new OleDbConnection(conStr);
            da = new OleDbDataAdapter(sqlStr, con);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds, "Table");
            }
            catch
            {
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 驱动数据库返回对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static object GetObject(string sqlStr, string conStr)
        {
            object obj = new object();
            con = new OleDbConnection(conStr);
            OleDbCommand cmd = new OleDbCommand(sqlStr, con);
            try
            {
                con.Open();
                obj = cmd.ExecuteScalar();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
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
        public static int GetInt(string sqlStr, string conStr)
        {
            int cnt = -1;
            con = new OleDbConnection(conStr);
            OleDbCommand cmd = new OleDbCommand(sqlStr, con);
            try
            {
                con.Open();
                cnt = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally
            {
                con.Close();
            }
            return cnt;
        }

    }
}



