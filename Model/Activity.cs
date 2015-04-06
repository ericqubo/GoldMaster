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
    public class Activity
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public string Name { get; set; }
        public string ThisActivityInfo { get; set; }
        public DateTime ActivityDate { get; set; }
        public int SumPeopleCount { get; set; }
        public int ActivityPrice { get; set; }
        public int IsOver { get; set; }
        public string ImageUrl { get; set; }
        public int IsListTop { get; set; }
        public int? AppJDT { get; set; }
        // 防注入检查
        static Activity LimitExamine(Activity item)
        {
            if (item == null)
                return null;
            try { item.Name = item.Name.Replace("'", ""); }
            catch { }
            try { item.ThisActivityInfo = item.ThisActivityInfo.Replace("'", ""); }
            catch { }
            try { item.ImageUrl = item.ImageUrl.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<Activity> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM Activity order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Activity> itemList = new List<Activity>();
                foreach (DataRow row in dt.Rows)
                {
                    Activity item = new Activity();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.TypeID = Convert.ToInt32(row["TypeID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.ThisActivityInfo = Convert.ToString(row["ThisActivityInfo"]);
                    item.ActivityDate = Convert.ToDateTime(row["ActivityDate"]);
                    item.SumPeopleCount = Convert.ToInt32(row["SumPeopleCount"]);
                    item.ActivityPrice = Convert.ToInt32(row["ActivityPrice"]);
                    item.IsOver = Convert.ToInt32(row["IsOver"]);
                    item.IsListTop = Convert.ToInt32(row["IsListTop"]);
                    item.ImageUrl = Convert.ToString(row["ImageUrl"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<Activity> GetInfoByLimit(string order, string type, Activity limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from Activity where id not in (select top " + index + " ID from Activity where 1=1";
            string countSql = "SELECT count(*) FROM Activity where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (limitItem.TypeID > 0)
                {
                    limitStr += " and TypeID='" + limitItem.TypeID + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Name))
                {
                    limitStr += " and Name='" + limitItem.Name + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.ThisActivityInfo))
                {
                    limitStr += " and ThisActivityInfo='" + limitItem.ThisActivityInfo + "'";
                }
                if (limitItem.ActivityDate > Convert.ToDateTime("1984-07-28"))
                {
                    limitStr += " and ActivityDate='" + limitItem.ActivityDate + "'";
                }
                if (limitItem.SumPeopleCount > 0)
                {
                    limitStr += " and SumPeopleCount='" + limitItem.SumPeopleCount + "'";
                }
                if (limitItem.ActivityPrice > 0)
                {
                    limitStr += " and ActivityPrice='" + limitItem.ActivityPrice + "'";
                }
                if (limitItem.IsOver > 0)
                {
                    limitStr += " and IsOver='" + limitItem.IsOver + "'";
                }
                if (limitItem.IsListTop != 0)
                {
                    if (limitItem.IsListTop == 1)
                        limitStr += " and IsListTop='" + limitItem.IsListTop + "'";
                    else if (limitItem.IsListTop == -1)
                        limitStr += " and IsListTop != '1'";
                }
                if (!string.IsNullOrEmpty(limitItem.ImageUrl))
                {
                    limitStr += " and ImageUrl='" + limitItem.ImageUrl + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Activity> itemList = new List<Activity>();
                foreach (DataRow row in dt.Rows)
                {
                    Activity item = new Activity();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.TypeID = Convert.ToInt32(row["TypeID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.ThisActivityInfo = Convert.ToString(row["ThisActivityInfo"]);
                    item.ActivityDate = Convert.ToDateTime(row["ActivityDate"]);
                    item.SumPeopleCount = Convert.ToInt32(row["SumPeopleCount"]);
                    item.ActivityPrice = Convert.ToInt32(row["ActivityPrice"]);
                    item.IsOver = Convert.ToInt32(row["IsOver"]);
                    item.IsListTop = Convert.ToInt32(row["IsListTop"]);
                    item.ImageUrl = Convert.ToString(row["ImageUrl"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(Activity item)
        {
            item = LimitExamine(item);

            string sql = "insert into Activity(TypeID,Name,ThisActivityInfo,ActivityDate,SumPeopleCount,ActivityPrice,IsOver,ImageUrl,IsListTop) values('" + item.TypeID + "','" + item.Name + "','" + item.ThisActivityInfo + "','" + item.ActivityDate + "','" + item.SumPeopleCount + "','" + item.ActivityPrice + "','" + item.IsOver + "','" + item.ImageUrl + "','" + item.IsListTop + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(Activity item)
        {
            item = LimitExamine(item);

            string sql = "update Activity set TypeID='" + item.TypeID + "',Name='" + item.Name + "',ThisActivityInfo='" + item.ThisActivityInfo + "',ActivityDate='" + item.ActivityDate + "',SumPeopleCount='" + item.SumPeopleCount + "',ActivityPrice='" + item.ActivityPrice + "',IsOver='" + item.IsOver + "',ImageUrl='" + item.ImageUrl + "',IsListTop='" + item.IsListTop + "'where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(Activity item)
        {
            item = LimitExamine(item);

            string sql = "delete from Activity where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM Activity";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
        
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="pageId"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<Activity> GetList(int pageId, int pageSize, int? typeId = null)
        {
            string idFilter = pageId > 0 ? " and ID < " + pageId : "";
            string sql = string.Format("SELECT top ({0}) * FROM Activity where AppJDT!=1"//TypeID={1} {2} order by ID desc"
                , pageSize);

            if (typeId.HasValue)
            {
                sql += " AND TypeID=" + typeId.Value;
            }
            sql += idFilter + " order by ID desc";

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Activity> itemList = new List<Activity>();
                foreach (DataRow row in dt.Rows)
                {
                    itemList.Add(GetModel(row));
                }
                return itemList;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Activity GetSingle(int ID)
        {
            string sql = "SELECT top 1 * FROM Activity where ID = " + ID;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetModel(dt.Rows[0]);                
            }
            return null;
        }

        /// <summary>
        /// 活动
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="isListTop"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Activity> GetHotList(int count, int? typeId)
        {
            string sql = string.Format("SELECT top ({0}) * FROM Activity where 1=1"
                , count);

            if (typeId.HasValue)
            {
                sql += " AND TypeID=" + typeId.Value;
            }

            sql += " and AppJDT=1 order by ID desc";

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Activity> itemList = new List<Activity>();
                foreach (DataRow row in dt.Rows)
                {
                    itemList.Add(GetModel(row));
                }
                return itemList;
            }
            return null;
        }

        public static List<Activity> GetListByIds(IEnumerable<int> ids)
        {
            if (ids == null || ids.Count() == 0) return new List<Activity>();
            string idFilter = ids != null && ids.Count() > 0 ? " ID in(" + string.Join(",", ids) + ")" : "";
            string sql = string.Format("SELECT * FROM Activity where {0} order by ID desc"
                , idFilter);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Activity> itemList = new List<Activity>();
                foreach (DataRow row in dt.Rows)
                {
                    itemList.Add(GetModel(row));
                }
                return itemList;
            }
            return new List<Activity>();
        }

        public static Activity GetModel(DataRow row)
        {
            Activity item = new Activity();
            item.ID = Convert.ToInt32(row["ID"]);
            item.TypeID = Convert.ToInt32(row["TypeID"]);
            item.Name = Convert.ToString(row["Name"]);
            item.ThisActivityInfo = Convert.ToString(row["ThisActivityInfo"]);
            item.ActivityDate = Convert.ToDateTime(row["ActivityDate"]);
            item.SumPeopleCount = Convert.ToInt32(row["SumPeopleCount"]);
            item.ActivityPrice = Convert.ToInt32(row["ActivityPrice"]);
            item.IsOver = Convert.ToInt32(row["IsOver"]);
            item.IsListTop = Convert.ToInt32(row["IsListTop"]);
            item.ImageUrl = Convert.ToString(row["ImageUrl"]);

            if (row["AppJDT"] != null)
            {
                item.AppJDT = Convert.ToInt32(row["AppJDT"]);
            }

            return item;
        }
    }
}