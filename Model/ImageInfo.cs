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
    public class ImageInfo
    {
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
        public string Src { get; set; }

        // 防注入检查
        static ImageInfo LimitExamine(ImageInfo item)
        {
            if (item == null)
                return null;
            try { item.ImageUrl = item.ImageUrl.Replace("'", ""); }
            catch { }
            try { item.Tag = item.Tag.Replace("'", ""); }
            catch { }
            try { item.Title = item.Title.Replace("'", ""); }
            catch { }
            try { item.Src = item.Src.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<ImageInfo> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM ImageInfo order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<ImageInfo> itemList = new List<ImageInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    ImageInfo item = new ImageInfo();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.ImageUrl = Convert.ToString(row["ImageUrl"]);
                    item.Tag = Convert.ToString(row["Tag"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Src = Convert.ToString(row["Src"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<ImageInfo> GetInfoByLimit(string order, string type, ImageInfo limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from ImageInfo where id not in (select top " + index + " ID from ImageInfo where 1=1";
            string countSql = "SELECT count(*) FROM ImageInfo where 1=1";
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
                if (!string.IsNullOrEmpty(limitItem.Tag))
                {
                    limitStr += " and Tag='" + limitItem.Tag + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Title))
                {
                    limitStr += " and Title='" + limitItem.Title + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Src))
                {
                    limitStr += " and Src='" + limitItem.Src + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<ImageInfo> itemList = new List<ImageInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    ImageInfo item = new ImageInfo();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.ImageUrl = Convert.ToString(row["ImageUrl"]);
                    item.Tag = Convert.ToString(row["Tag"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Src = Convert.ToString(row["Src"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(ImageInfo item)
        {
            item = LimitExamine(item);

            string sql = "insert into ImageInfo(ImageUrl,Tag,Title,Src) values('" + item.ImageUrl + "','" + item.Tag + "','" + item.Title + "','" + item.Src + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(ImageInfo item)
        {
            item = LimitExamine(item);

            string sql = "update ImageInfo set ImageUrl='" + item.ImageUrl + "',Title='" + item.Title + "',Src='" + item.Src + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(ImageInfo item)
        {
            item = LimitExamine(item);

            string sql = "delete from ImageInfo where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM ImageInfo";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        // 根据tag返回单条记录
        public static ImageInfo GetSingel(string tag)
        {
            string sql = "SELECT * FROM ImageInfo where tag='" + tag + "'";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                ImageInfo item = new ImageInfo();
                item.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                item.ImageUrl = Convert.ToString(dt.Rows[0]["ImageUrl"]);
                item.Tag = Convert.ToString(dt.Rows[0]["Tag"]);
                item.Title = Convert.ToString(dt.Rows[0]["Title"]);
                item.Src = Convert.ToString(dt.Rows[0]["Src"]);
                return item;
            }
            return null;
        }
    }
}
