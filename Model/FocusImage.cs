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
    public class FocusImage
    {
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        public string ImageTitle { get; set; }
        public string NewsSrc { get; set; }
        public int OrderIndex { get; set; }

        // 防注入检查
        static FocusImage LimitExamine(FocusImage item)
        {
            if (item == null)
                return null;
            try { item.ImageUrl = item.ImageUrl.Replace("'", ""); }
            catch { }
            try { item.ImageTitle = item.ImageTitle.Replace("'", ""); }
            catch { }
            try { item.NewsSrc = item.NewsSrc.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<FocusImage> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM FocusImage order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<FocusImage> itemList = new List<FocusImage>();
                foreach (DataRow row in dt.Rows)
                {
                    FocusImage item = new FocusImage();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.ImageUrl = Convert.ToString(row["ImageUrl"]);
                    item.ImageTitle = Convert.ToString(row["ImageTitle"]);
                    item.NewsSrc = Convert.ToString(row["NewsSrc"]);
                    item.OrderIndex = Convert.ToInt32(row["OrderIndex"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<FocusImage> GetInfoByLimit(string order, string type, FocusImage limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from FocusImage where id not in (select top " + index + " ID from FocusImage where 1=1";
            string countSql = "SELECT count(*) FROM FocusImage where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.ImageUrl))
                {
                    limitStr += " and ImageUrl='" + limitItem.ImageUrl + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.ImageTitle))
                {
                    limitStr += " and ImageTitle='" + limitItem.ImageTitle + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.NewsSrc))
                {
                    limitStr += " and NewsSrc='" + limitItem.NewsSrc + "'";
                }
                if (limitItem.OrderIndex > 0)
                {
                    limitStr += " and OrderIndex='" + limitItem.OrderIndex + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<FocusImage> itemList = new List<FocusImage>();
                foreach (DataRow row in dt.Rows)
                {
                    FocusImage item = new FocusImage();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.ImageUrl = Convert.ToString(row["ImageUrl"]);
                    item.ImageTitle = Convert.ToString(row["ImageTitle"]);
                    item.NewsSrc = Convert.ToString(row["NewsSrc"]);
                    item.OrderIndex = Convert.ToInt32(row["OrderIndex"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(FocusImage item)
        {
            item = LimitExamine(item);

            string sql = "insert into FocusImage(ImageUrl,ImageTitle,NewsSrc,OrderIndex) values('" + item.ImageUrl + "','" + item.ImageTitle + "','" + item.NewsSrc + "','" + item.OrderIndex + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(FocusImage item)
        {
            item = LimitExamine(item);

            string sql = "update FocusImage set ImageUrl='" + item.ImageUrl + "',ImageTitle='" + item.ImageTitle + "',NewsSrc='" + item.NewsSrc + "',OrderIndex='" + item.OrderIndex + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(FocusImage item)
        {
            item = LimitExamine(item);

            string sql = "delete from FocusImage where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM FocusImage";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
    }
}