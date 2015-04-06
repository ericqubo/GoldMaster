using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KCommonLib.DBTools;

namespace Model
{
    /// <summary>
    /// NewsGroup 的摘要说明
    /// </summary>
    public class ActivityReserve
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Industry { get; set; }
        public DateTime ReserveDate { get; set; }
        public int ReserveActivityID { get; set; }

        // 防注入检查
        static ActivityReserve LimitExamine(ActivityReserve item)
        {
            if (item == null)
                return null;
            try { item.Name = item.Name.Replace("'", ""); }
            catch { }
            try { item.Mobile = item.Mobile.Replace("'", ""); }
            catch { }
            try { item.Mail = item.Mail.Replace("'", ""); }
            catch { }
            try { item.Address = item.Address.Replace("'", ""); }
            catch { }
            try { item.Industry = item.Industry.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<ActivityReserve> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM ActivityReserve order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<ActivityReserve> itemList = new List<ActivityReserve>();
                foreach (DataRow row in dt.Rows)
                {
                    ActivityReserve item = new ActivityReserve();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.Mobile = Convert.ToString(row["Mobile"]);
                    item.Mail = Convert.ToString(row["Mail"]);
                    item.Address = Convert.ToString(row["Address"]);
                    item.Industry = Convert.ToString(row["Industry"]);
                    item.ReserveDate = Convert.ToDateTime(row["ReserveDate"]);
                    item.ReserveActivityID = Convert.ToInt32(row["ReserveActivityID"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<ActivityReserve> GetInfoByLimit(string order, string type, ActivityReserve limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from ActivityReserve where id not in (select top " + index + " ID from ActivityReserve where 1=1";
            string countSql = "SELECT count(*) FROM ActivityReserve where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Name))
                {
                    limitStr += " and Name='" + limitItem.Name + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Mobile))
                {
                    limitStr += " and Mobile='" + limitItem.Mobile + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Mail))
                {
                    limitStr += " and Mail='" + limitItem.Mail + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Address))
                {
                    limitStr += " and Address='" + limitItem.Address + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Industry))
                {
                    limitStr += " and Industry='" + limitItem.Industry + "'";
                }
                if (limitItem.ReserveDate > Convert.ToDateTime("1984-07-28"))
                {
                    limitStr += " and ReserveDate='" + limitItem.ReserveDate + "'";
                }
                if (limitItem.ReserveActivityID > 0)
                {
                    limitStr += " and ReserveActivityID='" + limitItem.ReserveActivityID + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<ActivityReserve> itemList = new List<ActivityReserve>();
                foreach (DataRow row in dt.Rows)
                {
                    ActivityReserve item = new ActivityReserve();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.Mobile = Convert.ToString(row["Mobile"]);
                    item.Mail = Convert.ToString(row["Mail"]);
                    item.Address = Convert.ToString(row["Address"]);
                    item.Industry = Convert.ToString(row["Industry"]);
                    item.ReserveDate = Convert.ToDateTime(row["ReserveDate"]);
                    item.ReserveActivityID = Convert.ToInt32(row["ReserveActivityID"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(ActivityReserve item)
        {
            item = LimitExamine(item);

            string sql = "insert into ActivityReserve(Name,Mobile,Mail,Address,Industry,ReserveDate,ReserveActivityID) values('" + item.Name + "','" + item.Mobile + "','" + item.Mail + "','" + item.Address + "','" + item.Industry + "','" + item.ReserveDate + "','" + item.ReserveActivityID + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(ActivityReserve item)
        {
            item = LimitExamine(item);

            string sql = "update ActivityReserve set Name='" + item.Name + "',Mobile='" + item.Mobile + "',Mail='" + item.Mail + "',Address='" + item.Address + "',Industry='" + item.Industry + "',ReserveDate='" + item.ReserveDate + "',ReserveActivityID='" + item.ReserveActivityID + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(ActivityReserve item)
        {
            item = LimitExamine(item);

            string sql = "delete from ActivityReserve where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM ActivityReserve";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
    }
}