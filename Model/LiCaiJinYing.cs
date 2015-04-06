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
    public class LiCaiJinYing
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompName { get; set; }
        public string IDCode { get; set; }
        public string LiCaiGeYan { get; set; }
        public string PhotoUrl { get; set; }
        public int inMainPage { get; set; }
        public string QuYu { get; set; }

        // 防注入检查
        static LiCaiJinYing LimitExamine(LiCaiJinYing item)
        {
            if (item == null)
                return null;
            try { item.Name = item.Name.Replace("'", ""); }
            catch { }
            try { item.CompName = item.CompName.Replace("'", ""); }
            catch { }
            try { item.IDCode = item.IDCode.Replace("'", ""); }
            catch { }
            try { item.LiCaiGeYan = item.LiCaiGeYan.Replace("'", ""); }
            catch { }
            try { item.PhotoUrl = item.PhotoUrl.Replace("'", ""); }
            catch { }
            try { item.QuYu = item.QuYu.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<LiCaiJinYing> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM LiCaiJinYing order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<LiCaiJinYing> itemList = new List<LiCaiJinYing>();
                foreach (DataRow row in dt.Rows)
                {
                    LiCaiJinYing item = new LiCaiJinYing();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.CompName = Convert.ToString(row["CompName"]);
                    item.IDCode = Convert.ToString(row["IDCode"]);
                    item.LiCaiGeYan = Convert.ToString(row["LiCaiGeYan"]);
                    item.PhotoUrl = Convert.ToString(row["PhotoUrl"]);
                    item.inMainPage = Convert.ToInt32(row["inMainPage"]);
                    item.QuYu = Convert.ToString(row["QuYu"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<LiCaiJinYing> GetInfoByLimit(string order, string type, LiCaiJinYing limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from LiCaiJinYing where id not in (select top " + index + " ID from LiCaiJinYing where 1=1";
            string countSql = "SELECT count(*) FROM LiCaiJinYing where 1=1";
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
                if (!string.IsNullOrEmpty(limitItem.CompName))
                {
                    limitStr += " and CompName='" + limitItem.CompName + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.IDCode))
                {
                    limitStr += " and IDCode='" + limitItem.IDCode + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.LiCaiGeYan))
                {
                    limitStr += " and LiCaiGeYan='" + limitItem.LiCaiGeYan + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.PhotoUrl))
                {
                    limitStr += " and PhotoUrl='" + limitItem.PhotoUrl + "'";
                }
                if (limitItem.inMainPage > 0)
                {
                    limitStr += " and inMainPage='" + limitItem.inMainPage + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.QuYu))
                {
                    limitStr += " and QuYu='" + limitItem.QuYu + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<LiCaiJinYing> itemList = new List<LiCaiJinYing>();
                foreach (DataRow row in dt.Rows)
                {
                    LiCaiJinYing item = new LiCaiJinYing();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.CompName = Convert.ToString(row["CompName"]);
                    item.IDCode = Convert.ToString(row["IDCode"]);
                    item.LiCaiGeYan = Convert.ToString(row["LiCaiGeYan"]);
                    item.PhotoUrl = Convert.ToString(row["PhotoUrl"]);
                    item.inMainPage = Convert.ToInt32(row["inMainPage"]);
                    item.QuYu = Convert.ToString(row["QuYu"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(LiCaiJinYing item)
        {
            item = LimitExamine(item);

            string sql = "insert into LiCaiJinYing(Name,CompName,IDCode,LiCaiGeYan,PhotoUrl,inMainPage,QuYu) values('" + item.Name + "','" + item.CompName + "','" + item.IDCode + "','" + item.LiCaiGeYan + "','" + item.PhotoUrl + "','" + item.inMainPage + "','" + item.QuYu + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(LiCaiJinYing item)
        {
            item = LimitExamine(item);

            string sql = "update LiCaiJinYing set Name='" + item.Name + "',CompName='" + item.CompName + "',IDCode='" + item.IDCode + "',LiCaiGeYan='" + item.LiCaiGeYan + "',PhotoUrl='" + item.PhotoUrl + "',inMainPage='" + item.inMainPage + "',QuYu='" + item.QuYu + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(LiCaiJinYing item)
        {
            item = LimitExamine(item);

            string sql = "delete from LiCaiJinYing where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM LiCaiJinYing";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        public static List<string> GetQuYuList()
        {
            string sql = "select QuYu from LiCaiJinYing group by QuYu";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<string> quyuList = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    quyuList.Add(row[0].ToString());
                }
                return quyuList;
            }
            else { return null; }
        }
    }
}