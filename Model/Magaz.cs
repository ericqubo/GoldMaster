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
    public class Magaz
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string magazPages { get; set; }
        public string magazPagesInfo { get; set; }
        public int InMainTop { get; set; }
        public int InMainList { get; set; }
        public int IsListTop { get; set; }
        public string AppTitle { get; set; }

        // 防注入检查
        static Magaz LimitExamine(Magaz item)
        {
            if (item == null)
                return null;
            try { item.Title = item.Title.Replace("'", ""); }
            catch { }
            try { item.Content = item.Content.Replace("'", ""); }
            catch { }
            try { item.magazPages = item.magazPages.Replace("'", ""); }
            catch { }
            try { item.magazPagesInfo = item.magazPagesInfo.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<Magaz> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM Magaz order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Magaz> itemList = new List<Magaz>();
                foreach (DataRow row in dt.Rows)
                {
                    Magaz item = new Magaz();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.magazPages = Convert.ToString(row["magazPages"]);
                    item.magazPagesInfo = Convert.ToString(row["magazPagesInfo"]);
                    item.InMainTop = Convert.ToInt32(row["InMainTop"]);
                    item.InMainList = Convert.ToInt32(row["InMainList"]);
                    item.IsListTop = Convert.ToInt32(row["IsListTop"]);
                    item.AppTitle = Convert.ToString(row["AppTitle"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        public static List<Magaz> GetInfoByIDS(List<int> idList)
        {
            List<Magaz> itemList = new List<Magaz>();
            string ids = "";
            if (idList != null)
            {
                foreach (int id in idList)
                {
                    ids = ids + id.ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(ids))
            {
                ids = ids.Substring(0, ids.LastIndexOf(","));
            }
            else
            {
                return itemList;
            }
            string sql = "select * from Magaz where ID in (" + ids + ")";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Magaz item = new Magaz();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.magazPages = Convert.ToString(row["magazPages"]);
                    item.magazPagesInfo = Convert.ToString(row["magazPagesInfo"]);
                    item.InMainTop = Convert.ToInt32(row["InMainTop"]);
                    item.InMainList = Convert.ToInt32(row["InMainList"]);
                    item.IsListTop = Convert.ToInt32(row["IsListTop"]);
                    item.AppTitle = Convert.ToString(row["AppTitle"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return itemList;
        }

        // 条件查询
        public static List<Magaz> GetInfoByLimit(string order, string type, Magaz limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from Magaz where id not in (select top " + index + " ID from Magaz where 1=1";
            string countSql = "SELECT count(*) FROM Magaz where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (limitItem.CreateDate > Convert.ToDateTime("1984-07-28"))
                {
                    limitStr += " and CreateDate='" + limitItem.CreateDate + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Title))
                {
                    limitStr += " and Title='" + limitItem.Title + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Content))
                {
                    limitStr += " and Content='" + limitItem.Content + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.magazPages))
                {
                    limitStr += " and magazPages='" + limitItem.magazPages + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.magazPagesInfo))
                {
                    limitStr += " and magazPagesInfo='" + limitItem.magazPagesInfo + "'";
                }
                if (limitItem.InMainTop > 0)
                {
                    limitStr += " and InMainTop='" + limitItem.InMainTop + "'";
                }
                if (limitItem.InMainList > 0)
                {
                    limitStr += " and InMainList='" + limitItem.InMainList + "'";
                }
                if (limitItem.IsListTop != 0)
                {
                    if (limitItem.IsListTop == 1)
                        limitStr += " and IsListTop='" + limitItem.IsListTop + "'";
                    else if (limitItem.IsListTop == -1)
                        limitStr += " and IsListTop != '1'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Magaz> itemList = new List<Magaz>();
                foreach (DataRow row in dt.Rows)
                {
                    Magaz item = new Magaz();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.magazPages = Convert.ToString(row["magazPages"]);
                    item.magazPagesInfo = Convert.ToString(row["magazPagesInfo"]);
                    item.InMainTop = Convert.ToInt32(row["InMainTop"]);
                    item.InMainList = Convert.ToInt32(row["InMainList"]);
                    item.IsListTop = Convert.ToInt32(row["IsListTop"]);
                    item.AppTitle = Convert.ToString(row["AppTitle"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }
        /// <summary>
        /// 翻页查询
        /// </summary>
        /// <returns></returns>
        public static List<Magaz> GetList(int pageId, int pageSize)
        {
            string idFilter = pageId > 0 ? " where  ID < " + pageId : "";
            string sql = string.Format("SELECT top ({0}) * FROM [Magaz] {1} order by [ID] desc"
                , pageSize, idFilter);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Magaz> itemList = new List<Magaz>();

                foreach (DataRow row in dt.Rows)
                {
                    Magaz item = new Magaz();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.magazPages = Convert.ToString(row["magazPages"]);
                    item.magazPagesInfo = Convert.ToString(row["magazPagesInfo"]);
                    item.InMainTop = Convert.ToInt32(row["InMainTop"]);
                    item.InMainList = Convert.ToInt32(row["InMainList"]);
                    item.IsListTop = Convert.ToInt32(row["IsListTop"]);
                    item.AppTitle = Convert.ToString(row["AppTitle"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }


        // 添加
        public static bool Set(Magaz item)
        {
            item = LimitExamine(item);

            string sql = "insert into Magaz(CreateDate,Title,Content,magazPages,magazPagesInfo,InMainTop,InMainList,IsListTop) values('" + item.CreateDate + "','" + item.Title + "','" + item.Content + "','" + item.magazPages + "','" + item.magazPagesInfo + "','" + item.InMainTop + "','" + item.InMainList + "','" + item.IsListTop + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(Magaz item)
        {
            item = LimitExamine(item);

            string sql = "update Magaz set CreateDate='" + item.CreateDate + "',Title='" + item.Title + "',Content='" + item.Content + "',magazPages='" + item.magazPages + "',magazPagesInfo='" + item.magazPagesInfo + "',InMainTop='" + item.InMainTop + "',InMainList='" + item.InMainList + "',IsListTop='" + item.IsListTop + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(Magaz item)
        {
            item = LimitExamine(item);

            string sql = "delete from Magaz where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM Magaz";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        // 返回随机行
        public static List<Magaz> GetRadom(int count)
        {
            string sql = "SELECT top " + count + " * FROM Magaz";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Magaz> itemList = new List<Magaz>();
                foreach (DataRow row in dt.Rows)
                {
                    Magaz item = new Magaz();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                    item.Title = Convert.ToString(row["Title"]);
                    item.Content = Convert.ToString(row["Content"]);
                    item.magazPages = Convert.ToString(row["magazPages"]);
                    item.magazPagesInfo = Convert.ToString(row["magazPagesInfo"]);
                    item.InMainTop = Convert.ToInt32(row["InMainTop"]);
                    item.InMainList = Convert.ToInt32(row["InMainList"]);
                    item.IsListTop = Convert.ToInt32(row["IsListTop"]);
                    item.AppTitle = Convert.ToString(row["AppTitle"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }


        public static Magaz GetSingle(int ID)
        {
            string sql = "SELECT * FROM Magaz where ID = " + ID;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                Magaz item = new Magaz();
                item.ID = Convert.ToInt32(row["ID"]);
                item.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                item.Title = Convert.ToString(row["Title"]);
                item.Content = Convert.ToString(row["Content"]);
                item.magazPages = Convert.ToString(row["magazPages"]);
                item.magazPagesInfo = Convert.ToString(row["magazPagesInfo"]);
                item.InMainTop = Convert.ToInt32(row["InMainTop"]);
                item.InMainList = Convert.ToInt32(row["InMainList"]);
                item.IsListTop = Convert.ToInt32(row["IsListTop"]);
                item.AppTitle = Convert.ToString(row["AppTitle"]);
                return item;
            }
            return null;
        }
    }
}
