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
    public class ActivityType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ImageInfo { get; set; }
        public string ActivityTypeInfo { get; set; }
        public string Tag { get; set; }

        // 防注入检查
        static ActivityType LimitExamine(ActivityType item)
        {
            if (item == null)
                return null;
            try { item.Name = item.Name.Replace("'", ""); }
            catch { }
            try { item.ImageUrl = item.ImageUrl.Replace("'", ""); }
            catch { }
            try { item.ImageInfo = item.ImageInfo.Replace("'", ""); }
            catch { }
            try { item.ActivityTypeInfo = item.ActivityTypeInfo.Replace("'", ""); }
            catch { }
            try { item.Tag = item.Tag.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<ActivityType> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM ActivityType order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<ActivityType> itemList = new List<ActivityType>();
                foreach (DataRow row in dt.Rows)
                {
                    ActivityType item = new ActivityType();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.ImageUrl = Convert.ToString(row["ImageUrl"]);
                    item.ImageInfo = Convert.ToString(row["ImageInfo"]);
                    item.ActivityTypeInfo = Convert.ToString(row["ActivityTypeInfo"]);
                    item.Tag = Convert.ToString(row["Tag"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<ActivityType> GetInfoByLimit(string order, string type, ActivityType limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from ActivityType where id not in (select top " + index + " ID from ActivityType where 1=1";
            string countSql = "SELECT count(*) FROM ActivityType where 1=1";
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
                if (!string.IsNullOrEmpty(limitItem.ImageUrl))
                {
                    limitStr += " and ImageUrl='" + limitItem.ImageUrl + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.ImageInfo))
                {
                    limitStr += " and ImageInfo='" + limitItem.ImageInfo + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.ActivityTypeInfo))
                {
                    limitStr += " and ActivityTypeInfo='" + limitItem.ActivityTypeInfo + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Tag))
                {
                    limitStr += " and Tag='" + limitItem.Tag + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<ActivityType> itemList = new List<ActivityType>();
                foreach (DataRow row in dt.Rows)
                {
                    ActivityType item = new ActivityType();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.ImageUrl = Convert.ToString(row["ImageUrl"]);
                    item.ImageInfo = Convert.ToString(row["ImageInfo"]);
                    item.ActivityTypeInfo = Convert.ToString(row["ActivityTypeInfo"]);
                    item.Tag = Convert.ToString(row["Tag"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(ActivityType item)
        {
            item = LimitExamine(item);

            string sql = "insert into ActivityType(Name,ImageUrl,ImageInfo,ActivityTypeInfo,Tag) values('" + item.Name + "','" + item.ImageUrl + "','" + item.ImageInfo + "','" + item.ActivityTypeInfo + "','" + item.Tag + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(ActivityType item)
        {
            item = LimitExamine(item);

            string sql = "update ActivityType set Name='" + item.Name + "',ImageUrl='" + item.ImageUrl + "',ImageInfo='" + item.ImageInfo + "',ActivityTypeInfo='" + item.ActivityTypeInfo + "',Tag='" + item.Tag + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(ActivityType item)
        {
            item = LimitExamine(item);

            string sql = "delete from ActivityType where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM ActivityType";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        //根据id返回名称
        public static string GetNameById(int id)
        {
            ActivityType limit = new ActivityType();
            limit.ID = id;
            int sumCount = 0;
            return ActivityType.GetInfoByLimit(null, null, limit, 0, 1, out sumCount).Single().Name;
        }
    }
}
