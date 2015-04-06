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
    public class FriendLink
    {
        public int ID { get; set; }
        public string LinkName { get; set; }
        public string LinkSrc { get; set; }
        public string LinkImageUrl { get; set; }

        // 防注入检查
        static FriendLink LimitExamine(FriendLink item)
        {
            if (item == null)
                return null;
            try { item.LinkName = item.LinkName.Replace("'", ""); }
            catch { }
            try { item.LinkSrc = item.LinkSrc.Replace("'", ""); }
            catch { }
            try { item.LinkImageUrl = item.LinkImageUrl.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<FriendLink> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM FriendLink order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<FriendLink> itemList = new List<FriendLink>();
                foreach (DataRow row in dt.Rows)
                {
                    FriendLink item = new FriendLink();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.LinkName = Convert.ToString(row["LinkName"]);
                    item.LinkSrc = Convert.ToString(row["LinkSrc"]);
                    item.LinkImageUrl = Convert.ToString(row["LinkImageUrl"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<FriendLink> GetInfoByLimit(string order, string type, FriendLink limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from FriendLink where id not in (select top " + index + " ID from FriendLink where 1=1";
            string countSql = "SELECT count(*) FROM FriendLink where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.LinkName))
                {
                    limitStr += " and LinkName='" + limitItem.LinkName + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.LinkSrc))
                {
                    limitStr += " and LinkSrc='" + limitItem.LinkSrc + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.LinkImageUrl))
                {
                    limitStr += " and LinkImageUrl='" + limitItem.LinkImageUrl + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<FriendLink> itemList = new List<FriendLink>();
                foreach (DataRow row in dt.Rows)
                {
                    FriendLink item = new FriendLink();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.LinkName = Convert.ToString(row["LinkName"]);
                    item.LinkSrc = Convert.ToString(row["LinkSrc"]);
                    item.LinkImageUrl = Convert.ToString(row["LinkImageUrl"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(FriendLink item)
        {
            item = LimitExamine(item);

            string sql = "insert into FriendLink(LinkName,LinkSrc,LinkImageUrl) values('" + item.LinkName + "','" + item.LinkSrc + "','" + item.LinkImageUrl + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(FriendLink item)
        {
            item = LimitExamine(item);

            string sql = "update FriendLink set LinkName='" + item.LinkName + "',LinkSrc='" + item.LinkSrc + "',LinkImageUrl='" + item.LinkImageUrl + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(FriendLink item)
        {
            item = LimitExamine(item);

            string sql = "delete from FriendLink where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM FriendLink";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
    }
}