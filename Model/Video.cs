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
    public class Video
    {
        public int ID { get; set; }
        public string Src { get; set; }
        public string AppSrc { get; set; }
        public string IntroImage { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string Tag { get; set; }

        // 防注入检查
        static Video LimitExamine(Video item)
        {
            if (item == null)
                return null;
            try { item.Src = item.Src.Replace("'", ""); }
            catch { }
            try { item.IntroImage = item.IntroImage.Replace("'", ""); }
            catch { }
            try { item.Title = item.Title.Replace("'", ""); }
            catch { }
            try { item.Content = item.Content.Replace("'", ""); }
            catch { }
            try { item.Tag = item.Tag.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<Video> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM Video order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Video> itemList = new List<Video>();
                foreach (DataRow row in dt.Rows)
                {
                    Video item = new Video();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Src = Convert.ToString(row["Src"]);
                    item.IntroImage = Convert.ToString(row["IntroImage"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Tag = Convert.ToString(row["Tag"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<Video> GetInfoByLimit(string order, string type, Video limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from Video where id not in (select top " + index + " ID from Video where 1=1";
            string countSql = "SELECT count(*) FROM Video where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Src))
                {
                    limitStr += " and Src='" + limitItem.Src + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.IntroImage))
                {
                    limitStr += " and IntroImage='" + limitItem.IntroImage + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Title))
                {
                    limitStr += " and Title like '%" + limitItem.Title + "%'";
                }
                if (!string.IsNullOrEmpty(limitItem.Content))
                {
                    limitStr += " and Content='" + limitItem.Content + "'";
                }
                if (limitItem.CreateDate > Convert.ToDateTime("1984-07-28"))
                {
                    limitStr += " and CreateDate='" + limitItem.CreateDate + "'";
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
                List<Video> itemList = new List<Video>();
                foreach (DataRow row in dt.Rows)
                {
                    Video item = new Video();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Src = Convert.ToString(row["Src"]);
                    item.IntroImage = Convert.ToString(row["IntroImage"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Tag = Convert.ToString(row["Tag"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(Video item)
        {
            item = LimitExamine(item);

            string sql = "insert into Video(Src,IntroImage,Title,Content,CreateDate,Tag) values('" + item.Src + "','" + item.IntroImage + "','" + item.Title + "','" + item.Content + "','" + item.CreateDate + "','" + item.Tag + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(Video item)
        {
            item = LimitExamine(item);

            string sql = "update Video set Src='" + item.Src + "',IntroImage='" + item.IntroImage + "',Title='" + item.Title + "',Content='" + item.Content + "',CreateDate='" + item.CreateDate + "',Tag='" + item.Tag + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(Video item)
        {
            item = LimitExamine(item);

            string sql = "delete from Video where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM Video";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
        /// <summary>
        /// 翻页查询
        /// </summary>
        /// <returns></returns>
        public static List<Video> GetList(int pageId, int pageSize, bool? excludeSrcIsNull = null)
        {
            string idFilter = pageId > 0 ? " and ID < " + pageId : "";
            if (excludeSrcIsNull.HasValue)
            {
                idFilter = idFilter + " and src <> '' ";
            }
            string sql = string.Format("SELECT top ({0}) * FROM [Video] where 1=1 {1} order by [ID] desc"
                , pageSize, idFilter);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Video> itemList = new List<Video>();

                foreach (DataRow row in dt.Rows)
                {
                    Video item = new Video();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Src = Convert.ToString(row["Src"]);
                    item.AppSrc = Convert.ToString(row["AppSrc"]);
                    item.IntroImage = Convert.ToString(row["IntroImage"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Tag = Convert.ToString(row["Tag"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }
    }
}