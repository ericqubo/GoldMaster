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
    public class MagazReserve
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public DateTime ReserveDate { get; set; }
        public int ReserveMagazID { get; set; }

        // 防注入检查
        static MagazReserve LimitExamine(MagazReserve item)
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
            return item;
        }

        // 返回所有
        public static List<MagazReserve> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM MagazReserve order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<MagazReserve> itemList = new List<MagazReserve>();
                foreach (DataRow row in dt.Rows)
                {
                    MagazReserve item = new MagazReserve();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.Mobile = Convert.ToString(row["Mobile"]);
                    item.Mail = Convert.ToString(row["Mail"]);
                    item.Address = Convert.ToString(row["Address"]);
                    item.ReserveDate = Convert.ToDateTime(row["ReserveDate"]);
                    item.ReserveMagazID = Convert.ToInt32(row["ReserveMagazID"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<MagazReserve> GetInfoByLimit(string order, string type, MagazReserve limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from MagazReserve where id not in (select top " + index + " ID from MagazReserve where 1=1";
            string countSql = "SELECT count(*) FROM MagazReserve where 1=1";
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
                if (limitItem.ReserveDate > Convert.ToDateTime("1984-07-28"))
                {
                    limitStr += " and ReserveDate='" + limitItem.ReserveDate + "'";
                }
                if (limitItem.ReserveMagazID > 0)
                {
                    limitStr += " and ReserveMagazID='" + limitItem.ReserveMagazID + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<MagazReserve> itemList = new List<MagazReserve>();
                foreach (DataRow row in dt.Rows)
                {
                    MagazReserve item = new MagazReserve();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.Mobile = Convert.ToString(row["Mobile"]);
                    item.Mail = Convert.ToString(row["Mail"]);
                    item.Address = Convert.ToString(row["Address"]);
                    item.ReserveDate = Convert.ToDateTime(row["ReserveDate"]);
                    item.ReserveMagazID = Convert.ToInt32(row["ReserveMagazID"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(MagazReserve item)
        {
            item = LimitExamine(item);

            string sql = "insert into MagazReserve(Name,Mobile,Mail,Address,ReserveDate,ReserveMagazID) values('" + item.Name + "','" + item.Mobile + "','" + item.Mail + "','" + item.Address + "','" + item.ReserveDate + "','" + item.ReserveMagazID + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(MagazReserve item)
        {
            item = LimitExamine(item);

            string sql = "update MagazReserve set Name='" + item.Name + "',Mobile='" + item.Mobile + "',Mail='" + item.Mail + "',Address='" + item.Address + "',ReserveDate='" + item.ReserveDate + "',ReserveMagazID='" + item.ReserveMagazID + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(MagazReserve item)
        {
            item = LimitExamine(item);

            string sql = "delete from MagazReserve where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM MagazReserve";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
    }
}