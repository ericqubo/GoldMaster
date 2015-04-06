using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KCommonLib.DBTools;
using System.Text;

namespace Model
{
    /// <summary>
    /// NewsGroup 的摘要说明
    /// </summary>
    public class NewsGroup
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public string GroupDName { get; set; }
        public int FGroupID { get; set; }

        // 防注入检查
        static NewsGroup LimitExamine(NewsGroup item)
        {
            if (item == null)
                return null;
            try { item.GroupName = item.GroupName.Replace("'", ""); }
            catch { }
            try { item.GroupDName = item.GroupDName.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<NewsGroup> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM NewsGroup order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<NewsGroup> itemList = new List<NewsGroup>();
                foreach (DataRow row in dt.Rows)
                {
                    NewsGroup item = new NewsGroup();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.GroupName = Convert.ToString(row["GroupName"]);
                    item.GroupDName = Convert.ToString(row["GroupDName"]);
                    item.FGroupID = Convert.ToInt32(row["FGroupID"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }
        
        // 条件查询
        public static List<NewsGroup> GetInfoByLimit(string order, string type, NewsGroup limitItem, int index, int count, out int sumCount)
        {
            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from NewsGroup where id not in (select top " + index + " ID from NewsGroup where 1=1";
            string countSql = "SELECT count(*) FROM NewsGroup where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.GroupName))
                {
                    limitStr += " and GroupName='" + limitItem.GroupName + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.GroupDName))
                {
                    limitStr += " and GroupDName='" + limitItem.GroupDName + "'";
                }
                if (limitItem.FGroupID > 0)
                {
                    limitStr += " and FGroupID='" + limitItem.FGroupID + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<NewsGroup> itemList = new List<NewsGroup>();
                foreach (DataRow row in dt.Rows)
                {
                    NewsGroup item = new NewsGroup();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.GroupName = Convert.ToString(row["GroupName"]);
                    item.GroupDName = Convert.ToString(row["GroupDName"]);
                    item.FGroupID = Convert.ToInt32(row["FGroupID"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(NewsGroup item)
        {
            item = LimitExamine(item);

            string sql = "insert into NewsGroup(GroupName,GroupDName,FGroupID) values('" + item.GroupName + "','" + item.GroupDName + "','" + item.FGroupID + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(NewsGroup item)
        {
            item = LimitExamine(item);

            string sql = "update NewsGroup set GroupName='" + item.GroupName + "',GroupDName='" + item.GroupDName + "',FGroupID='" + item.FGroupID + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(NewsGroup item)
        {
            item = LimitExamine(item);

            string sql = "delete from NewsGroup where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 查看栏目名称是否已存在
        public static int GetHaveCountByName(string name)
        {
            string sql = "SELECT count(*) FROM NewsGroup where GroupName='" + name + "'";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count;
        }

        /// 根据栏目Id返回栏目名称
        public static string GetGroupNameById(int groupId)
        {
            int sumCount = 0;
            NewsGroup limitItem = new NewsGroup();
            limitItem.ID = groupId;
            NewsGroup newsGroup = GetInfoByLimit(null, null, limitItem, 0, 1, out sumCount)[0];
            return newsGroup.GroupName;
        }

        /// 根据栏目Id返回栏目信息
        public static string GetGroupFNameById(int groupId)
        {
            int sumCount = 0;
            NewsGroup limitItem = new NewsGroup();
            limitItem.ID = groupId;
            NewsGroup newsGroup = GetInfoByLimit(null, null, limitItem, 0, 1, out sumCount)[0];
            limitItem.ID = newsGroup.FGroupID;
            NewsGroup newsFGroup = GetInfoByLimit(null, null, limitItem, 0, 1, out sumCount)[0];
            return newsFGroup.GroupName;
        }

        public static string GetGroupListStrByGroupId(int groupId)
        {
            int sumCount = 0;
            NewsGroup limitItem = new NewsGroup();
            limitItem.ID = groupId;
            NewsGroup newsGroup = GetInfoByLimit(null, null, limitItem, 0, 1, out sumCount)[0];
            limitItem.ID = 0;
            limitItem.FGroupID = newsGroup.FGroupID;
            List<NewsGroup> newsGroupList = GetInfoByLimit("ID", "ASC", limitItem, 0, 999, out sumCount).ToList();
            if (newsGroupList != null && newsGroupList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var ng in newsGroupList)
                {
                    sb.AppendLine("<a href=\"newslist.aspx?id=" + ng.ID + "\">" + ng.GroupName + "</a>");
                }
                return sb.ToString();
            }
            return string.Empty;
        }

        public static string GetGroupListStrByGroupId_gd(int groupId)
        {
            int sumCount = 0;
            NewsGroup limitItem = new NewsGroup();
            limitItem.ID = groupId;
            NewsGroup newsGroup = GetInfoByLimit(null, null, limitItem, 0, 1, out sumCount)[0];
            limitItem.ID = 0;
            limitItem.FGroupID = newsGroup.FGroupID;
            List<NewsGroup> newsGroupList = GetInfoByLimit("ID", "ASC", limitItem, 0, 999, out sumCount).ToList();
            if (newsGroupList != null && newsGroupList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var ng in newsGroupList)
                {
                    if (ng.ID == 34)
                        sb.AppendLine("<a href=\"risktest.aspx\">风险测试</a>");
                    else if (ng.ID == 35)
                        sb.AppendLine("<a href=\"assets.aspx\">资产配置</a>");
                    else if (ng.ID == 36)
                        sb.AppendLine("<a href=\"reserve.aspx\">预约理财</a>");
                    else if (ng.ID == 43)
                        sb.AppendLine("<a href=\"userlogin.aspx\">会员专区</a>");
                    else if (ng.ID == 30)
                        sb.AppendLine("<a href=\"Friend.aspx\">合作伙伴</a>");
                    else if (ng.ID == 31)
                        sb.AppendLine("<a href=\"Contact.aspx\">联系我们</a>");
                    else
                    {
                        News newslimit = new News();
                        newslimit.GroupId = ng.ID;
                        News _news = News.GetInfoByLimit("ID", "DESC", newslimit, 0, 1, out sumCount)[0];
                        sb.AppendLine("<a href=\"newsgd.aspx?id=" + _news.ID + "\">" + ng.GroupName + "</a>");
                    }
                }
                return sb.ToString();
            }
            return string.Empty;
        }

        public static List<NewsGroup> GetNewsGroupListByFGroupId(int fGroupId)
        {
            NewsGroup limit = new NewsGroup();
            limit.FGroupID = fGroupId;
            int sumCount = 0;
            List<NewsGroup> ngList = NewsGroup.GetInfoByLimit(null, null, limit, 0, 999, out sumCount);
            return ngList;
        }

        /// <summary>
        /// 获取新闻类别列表
        /// </summary>
        /// <returns></returns>
        public static List<NewsGroup> GetList(int count, int fGroupID)
        {
            string sql = string.Format("select top {0} * from NewsGroup Where FGroupID={1}", count, fGroupID);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<NewsGroup> itemList = new List<NewsGroup>();
                foreach (DataRow row in dt.Rows)
                {
                    itemList.Add(GetModel(row));
                }
                return itemList;
            }
            return null;
        }

        public static NewsGroup GetModel(DataRow row)
        {
            NewsGroup item = new NewsGroup();
            item.ID = Convert.ToInt32(row["ID"]);
            item.GroupName = Convert.ToString(row["GroupName"]);
            item.GroupDName = Convert.ToString(row["GroupDName"]);
            item.FGroupID = Convert.ToInt32(row["FGroupID"]);

            return item;
        }
    }
}