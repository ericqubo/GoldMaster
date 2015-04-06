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
    public class SiteInfo
    {
        public int ID { get; set; }
        public string SiteName { get; set; }
        public string KeyWords { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }

        // 防注入检查
        static SiteInfo LimitExamine(SiteInfo item)
        {
            if (item == null)
                return null;
            try { item.SiteName = item.SiteName.Replace("'", ""); }
            catch { }
            try { item.KeyWords = item.KeyWords.Replace("'", ""); }
            catch { }
            try { item.Description = item.Description.Replace("'", ""); }
            catch { }
            try { item.Contact = item.Contact.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<SiteInfo> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM SiteInfo order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<SiteInfo> itemList = new List<SiteInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    SiteInfo item = new SiteInfo();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.SiteName = Convert.ToString(row["SiteName"]);
                    item.KeyWords = Convert.ToString(row["KeyWords"]);
                    item.Description = Convert.ToString(row["Description"]);
                    item.Contact = Convert.ToString(row["Contact"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<SiteInfo> GetInfoByLimit(string order, string type, SiteInfo limitItem, int index, int count, out int sumCount)
        {
            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from SiteInfo where id not in (select top " + index + " ID from SiteInfo where 1=1";
            string countSql = "SELECT count(*) FROM SiteInfo where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.SiteName))
                {
                    limitStr += " and SiteName='" + limitItem.SiteName + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.KeyWords))
                {
                    limitStr += " and KeyWords='" + limitItem.KeyWords + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Description))
                {
                    limitStr += " and Description='" + limitItem.Description + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Contact))
                {
                    limitStr += " and Contact='" + limitItem.Contact + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<SiteInfo> itemList = new List<SiteInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    SiteInfo item = new SiteInfo();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.SiteName = Convert.ToString(row["SiteName"]);
                    item.KeyWords = Convert.ToString(row["KeyWords"]);
                    item.Description = Convert.ToString(row["Description"]);
                    item.Contact = Convert.ToString(row["Contact"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(SiteInfo item)
        {
            item = LimitExamine(item);

            string sql = "insert into SiteInfo(SiteName,KeyWords,Description,Contact) values('" + item.SiteName + "','" + item.KeyWords + "','" + item.Description + "','" + item.Contact + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(SiteInfo item)
        {
            item = LimitExamine(item);

            string sql = "update SiteInfo set SiteName='" + item.SiteName + "',KeyWords='" + item.KeyWords + "',Description='" + item.Description + "',Contact='" + item.Contact + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(SiteInfo item)
        {
            item = LimitExamine(item);

            string sql = "delete from SiteInfo where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }


        // 修改联系我们
        public static bool Update_Contact(SiteInfo item)
        {
            item = LimitExamine(item);

            string sql = "update SiteInfo set Contact='" + item.Contact + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改SEO
        public static bool Update_Seo(SiteInfo item)
        {
            item = LimitExamine(item);

            string sql = "update SiteInfo set SiteName='" + item.SiteName + "',KeyWords='" + item.KeyWords + "',Description='" + item.Description + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
    }
}