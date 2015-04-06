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
    public class AdminUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string UserLimits { get; set; }

        // 防注入检查
        static AdminUser LimitExamine(AdminUser item)
        {
            if (item == null)
                return null;
            try { item.UserName = item.UserName.Replace("'", ""); }
            catch { }
            try { item.UserPwd = item.UserPwd.Replace("'", ""); }
            catch { }
            try { item.UserLimits = item.UserLimits.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<AdminUser> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM AdminUser order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<AdminUser> itemList = new List<AdminUser>();
                foreach (DataRow row in dt.Rows)
                {
                    AdminUser item = new AdminUser();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.UserName = Convert.ToString(row["UserName"]);
                    item.UserPwd = Convert.ToString(row["UserPwd"]);
                    item.UserLimits = Convert.ToString(row["UserLimits"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<AdminUser> GetInfoByLimit(string order, string type, AdminUser limitItem, int index, int count, out int sumCount)
        {
            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from AdminUser where id not in (select top " + index + " ID from AdminUser where 1=1";
            string countSql = "SELECT count(*) FROM AdminUser where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.UserName))
                {
                    limitStr += " and UserName='" + limitItem.UserName + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.UserPwd))
                {
                    limitStr += " and UserPwd='" + limitItem.UserPwd + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.UserLimits))
                {
                    limitStr += " and UserLimits='" + limitItem.UserLimits + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<AdminUser> itemList = new List<AdminUser>();
                foreach (DataRow row in dt.Rows)
                {
                    AdminUser item = new AdminUser();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.UserName = Convert.ToString(row["UserName"]);
                    item.UserPwd = Convert.ToString(row["UserPwd"]);
                    item.UserLimits = Convert.ToString(row["UserLimits"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(AdminUser item)
        {
            item = LimitExamine(item);

            string sql = "insert into AdminUser(UserName,UserPwd,UserLimits) values('" + item.UserName + "','" + item.UserPwd + "','" + item.UserLimits + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(AdminUser item)
        {
            item = LimitExamine(item);

            string sql = "update AdminUser set UserName='" + item.UserName + "',UserPwd='" + item.UserPwd + "',UserLimits='" + item.UserLimits + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(AdminUser item)
        {
            item = LimitExamine(item);

            string sql = "delete from AdminUser where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 返回管理员的权限字段
        public static string GetLimitsByUserId(string userId)
        {
            string sql = "SELECT UserLimits FROM AdminUser where ID='" + userId + "'";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["UserLimits"].ToString();
            }
            return null;
        }
    }
}