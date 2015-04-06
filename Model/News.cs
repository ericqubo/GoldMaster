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
    public class News
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AppTitle { get; set; }
        public DateTime CreateDate { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public string Content { get; set; }
        public int GroupId { get; set; }
        public string GripUrl { get; set; }
        public string ImgUrl { get; set; }
        public int Tag { get; set; }
        public string Discrption { get; set; }
        public int IsRecommend { get; set; }
        public int IsHot { get; set; }

        public int FavCount { get; set; }
        public int ShareCount { get; set; }
        public int? ToApp { get; set; }
        public int? AppJDT { get; set; }

        // 防注入检查
        static News LimitExamine(News item)
        {
            if (item == null)
                return null;
            try { item.Title = item.Title.Replace("'", ""); }
            catch { }
            try { item.Author = item.Author.Replace("'", ""); }
            catch { }
            try { item.Source = item.Source.Replace("'", ""); }
            catch { }
            try { item.Content = item.Content.Replace("'", ""); }
            catch { }
            try { item.GripUrl = item.GripUrl.Replace("'", ""); }
            catch { }
            try { item.ImgUrl = item.ImgUrl.Replace("'", ""); }
            catch { }
            try { item.Discrption = item.Discrption.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<News> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM News order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<News> itemList = new List<News>();
                foreach (DataRow row in dt.Rows)
                {
                    News item = new News();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Discrption = Convert.ToString(row["Discrption"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Author = Convert.ToString(row["Author"]);
                    item.Source = Convert.ToString(row["Source"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.GroupId = Convert.ToInt32(row["GroupId"]);
                    item.GripUrl = Convert.ToString(row["GripUrl"]);
                    item.ImgUrl = Convert.ToString(row["ImgUrl"]);
                    item.Tag = Convert.ToInt32(row["Tag"]);
                    item.IsRecommend = Convert.ToInt32(row["IsRecommend"]);
                    item.IsHot = Convert.ToInt32(row["IsHot"]);
                    item.FavCount = Convert.ToInt32(row["FavCount"]);
                    item.ShareCount = Convert.ToInt32(row["ShareCount"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<News> GetInfoByLimit(string order, string type, News limitItem, int index, int count, out int sumCount)
        {
            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from News where id not in (select top " + index + " ID from News where 1=1";
            string countSql = "SELECT count(*) FROM News where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Title))
                {
                    limitStr += " and Title like '%" + limitItem.Title + "%'";
                }
                if (!string.IsNullOrEmpty(limitItem.Discrption))
                {
                    limitStr += " and Discrption like '%" + limitItem.Discrption + "%'";
                }
                if (limitItem.CreateDate > Convert.ToDateTime("1984-07-28"))
                {
                    limitStr += " and CreateDate='" + limitItem.CreateDate + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Author))
                {
                    limitStr += " and Author='" + limitItem.Author + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Source))
                {
                    limitStr += " and Source='" + limitItem.Source + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Content))
                {
                    limitStr += " and Content='" + limitItem.Content + "'";
                }
                if (limitItem.GroupId > 0)
                {
                    limitStr += " and GroupId='" + limitItem.GroupId + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.GripUrl))
                {
                    limitStr += " and GripUrl='" + limitItem.GripUrl + "'";
                } if (!string.IsNullOrEmpty(limitItem.ImgUrl))
                {
                    limitStr += " and ImgUrl='" + limitItem.ImgUrl + "'";
                }
                if (limitItem.Tag > 0)
                {
                    limitStr += " and Tag='" + limitItem.Tag + "'";
                }
                if (limitItem.IsRecommend > 0)
                {
                    limitStr += " and IsRecommend='" + limitItem.IsRecommend + "'";
                }
                if (limitItem.IsHot > 0)
                {
                    limitStr += " and IsHot='" + limitItem.IsHot + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<News> itemList = new List<News>();
                foreach (DataRow row in dt.Rows)
                {
                    News item = new News();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Discrption = Convert.ToString(row["Discrption"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Author = Convert.ToString(row["Author"]);
                    item.Source = Convert.ToString(row["Source"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.GroupId = Convert.ToInt32(row["GroupId"]);
                    item.GripUrl = Convert.ToString(row["GripUrl"]);
                    item.ImgUrl = Convert.ToString(row["ImgUrl"]);
                    item.Tag = Convert.ToInt32(row["Tag"]);
                    item.IsRecommend = Convert.ToInt32(row["IsRecommend"]);
                    item.IsHot = Convert.ToInt32(row["IsHot"]);
                    item.FavCount = Convert.ToInt32(row["FavCount"]);
                    item.ShareCount = Convert.ToInt32(row["ShareCount"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(News item)
        {
            item = LimitExamine(item);

            string sql = "insert into News(Title,Discrption,CreateDate,Author,Source,Content,GroupId,GripUrl,ImgUrl,Tag,IsRecommend,IsHot) values('" + item.Title + "','" + item.Discrption + "','" + item.CreateDate + "','" + item.Author + "','" + item.Source + "','" + item.Content + "','" + item.GroupId + "','" + item.GripUrl + "','" + item.ImgUrl + "','" + item.Tag + "','" + item.IsRecommend + "','" + item.IsHot + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(News item)
        {
            item = LimitExamine(item);

            string sql = "update News set Title='" + item.Title + "',Discrption='" + item.Discrption + "',CreateDate='" + item.CreateDate + "',Author='" + item.Author + "',Source='" + item.Source + "',Content='" + item.Content + "',GroupId='" + item.GroupId + "',GripUrl='" + item.GripUrl + "',ImgUrl='" + item.ImgUrl + "',Tag='" + item.Tag + "',IsRecommend='" + item.IsRecommend + "',IsHot='" + item.IsHot + "',FavCount='" + item.FavCount + "',ShareCount='" + item.ShareCount + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool IncrementFavCount(int id, int count)
        {
            string sql = "update [News] set FavCount=FavCount+" + count
                + " where FavCount+" + count + " >= 0 and ID=" + id;
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool IncrementShareCount(int id, int count)
        {
            string sql = "update [News] set ShareCount=ShareCount+" + count
                + " where ShareCount+" + count + " >= 0 and ID=" + id;
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(News item)
        {
            item = LimitExamine(item);

            string sql = "delete from News where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM News";
            string c = DBCommon.GetConstr("SqlConnection");
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        // 正文修改
        public static bool UpdateContent(string ID, string content)
        {
            string sql = "update News set Content='" + content + "' where ID='" + ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 根据id返回一条新闻
        public static News GetSingle(string ID)
        {
            string sql = "SELECT * FROM News where ID = " + ID;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<News> itemList = new List<News>();
                foreach (DataRow row in dt.Rows)
                {
                    News item = new News();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Discrption = Convert.ToString(row["Discrption"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Author = Convert.ToString(row["Author"]);
                    item.Source = Convert.ToString(row["Source"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.GroupId = Convert.ToInt32(row["GroupId"]);
                    item.GripUrl = Convert.ToString(row["GripUrl"]);
                    item.ImgUrl = Convert.ToString(row["ImgUrl"]);
                    item.Tag = Convert.ToInt32(row["Tag"]);
                    item.IsRecommend = Convert.ToInt32(row["IsRecommend"]);
                    item.IsHot = Convert.ToInt32(row["IsHot"]);
                    item.FavCount = Convert.ToInt32(row["FavCount"]);
                    item.ShareCount = Convert.ToInt32(row["ShareCount"]);
                    itemList.Add(item);
                }
                return itemList[0];
            }
            return null;
        }

        // 根据一个父栏目ID，返回其所有子栏目的新闻
        public static List<News> GetNewsListByFGroupId(int fGroupId, int count)
        {
            List<NewsGroup> ngList = NewsGroup.GetNewsGroupListByFGroupId(fGroupId);
            if (ngList != null && ngList.Count > 0)
            {
                List<News> newsList = new List<News>();
                foreach (var ng in ngList)
                {
                    var sumCount = 0;
                    var limit = new News();
                    limit.GroupId = ng.ID;
                    try { newsList.AddRange(News.GetInfoByLimit(null, null, limit, 0, 999, out sumCount)); }
                    catch { }
                }
                newsList = newsList.OrderByDescending(t => t.ID).ToList();
                return newsList.Take(count).ToList();
            }
            return null;
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="limitItem"></param>
        /// <param name="pageId"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<News> GetList(News limitItem, int pageId, int pageSize)
        {
            limitItem = LimitExamine(limitItem);
            string idFilter = pageId > 0 ? " and ID < " + pageId : "";
            string sql = string.Format("SELECT top ({0}) * FROM News where GroupID={1} {2} order by ID desc"
                , pageSize, limitItem.GroupId, idFilter);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<News> itemList = new List<News>();
                foreach (DataRow row in dt.Rows)
                {
                    itemList.Add(GetModel(row));
                }
                return itemList;
            }
            return null;
        }

        /// <summary>
        /// 搜索列表
        /// </summary>
        /// <param name="keywork"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<News> SeachList(string keywork, int count)
        {
            keywork = keywork.Replace("'", "");
            string sql = string.Format("SELECT top ({0}) * FROM News where Title like '%{1}%' or AppTitle like '%{1}%' order by ID desc"
                , count, keywork);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<News> itemList = new List<News>();
                foreach (DataRow row in dt.Rows)
                {
                    itemList.Add(GetModel(row));
                }
                return itemList;
            }
            return new List<News>(); 
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<News> GetList(int pageId, int pageSize, bool? toApp = null, int? gid = null)
        {
            string idFilter = pageId > 0 ? " and ID < " + pageId : "";
            string sql = string.Format("SELECT top ({0}) * FROM News where 1=1 AND AppJDT!=1"
                , pageSize);

            if (toApp.HasValue && toApp.Value)
            {
                sql += " AND ToApp=1"; 
            }

            if (gid.HasValue)
            {
                sql += " AND GroupID=" + gid.Value;
            }

            sql += idFilter + " order by ID desc";

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<News> itemList = new List<News>();
                foreach (DataRow row in dt.Rows)
                {
                    itemList.Add(GetModel(row));
                }
                return itemList;
            }
            return null;
        }

        /// <summary>
        /// 焦点新闻
        /// </summary>
        /// <param name="isHot"></param>
        /// <param name="count"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static List<News> GetAppJDTList( int count, int? gid = null)
        {
            string sql = string.Format("SELECT top ({0}) * FROM News where 1=1 AND AppJDT=1"
                , count);

            if (gid.HasValue)
            {
                sql += " AND GroupID=" + gid.Value;
            }

            sql += " order by ID desc";

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<News> itemList = new List<News>();
                foreach (DataRow row in dt.Rows)
                {
                    itemList.Add(GetModel(row));
                }
                return itemList;
            }
            return null;
        }


        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="limitItem"></param>
        /// <param name="pageId"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<News> GetListByIds(IEnumerable<int> ids)
        {
            if (ids == null || ids.Count() == 0) return new List<News>();
            string idFilter = ids != null && ids.Count() > 0 ? " ID in(" + string.Join(",", ids) + ")" : "";
            string sql = string.Format("SELECT * FROM News where {0} order by ID desc"
                , idFilter);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<News> itemList = new List<News>();
                foreach (DataRow row in dt.Rows)
                {
                    itemList.Add(GetModel(row));
                }
                return itemList;
            }
            return new List<News>();
        }

        public static News GetModel(DataRow row)
        {
            News item = new News();
            item.ID = Convert.ToInt32(row["ID"]);
            item.Title = Convert.ToString(row["Title"]);
            item.AppTitle = Convert.ToString(row["AppTitle"]);
            item.Discrption = Convert.ToString(row["Discrption"]);
            item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
            item.Author = Convert.ToString(row["Author"]);
            item.Source = Convert.ToString(row["Source"]);
            item.Content = Convert.ToString(row["Content"]);
            item.GroupId = Convert.ToInt32(row["GroupId"]);
            item.GripUrl = Convert.ToString(row["GripUrl"]);
            item.ImgUrl = Convert.ToString(row["ImgUrl"]);
            item.Tag = Convert.ToInt32(row["Tag"]);
            item.IsRecommend = Convert.ToInt32(row["IsRecommend"]);
            item.IsHot = Convert.ToInt32(row["IsHot"]);
            item.FavCount = Convert.ToInt32(row["FavCount"]);
            item.ShareCount = Convert.ToInt32(row["ShareCount"]);
            if (row["ToApp"] != null)
            {
                item.ToApp = Convert.ToInt32(row["ToApp"]);
            }
            if (row["AppJDT"] != null)
            {
                item.AppJDT = Convert.ToInt32(row["AppJDT"]);
            }

            return item;
        }
    }
}