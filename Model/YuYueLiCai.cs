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
    public class YuYueLiCai
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string JiaoLiuShiJian { get; set; }
        public string LiaoJieZiXun { get; set; }
        public string QiTa { get; set; }
        public string YuanYiJieShouMail { get; set; }
        public DateTime YuYueDate { get; set; }

        // 防注入检查
        static YuYueLiCai LimitExamine(YuYueLiCai item)
        {
            if (item == null)
                return null;
            try { item.Name = item.Name.Replace("'", ""); }
            catch { }
            try { item.Phone = item.Phone.Replace("'", ""); }
            catch { }
            try { item.Mail = item.Mail.Replace("'", ""); }
            catch { }
            try { item.City = item.City.Replace("'", ""); }
            catch { }
            try { item.Address = item.Address.Replace("'", ""); }
            catch { }
            try { item.JiaoLiuShiJian = item.JiaoLiuShiJian.Replace("'", ""); }
            catch { }
            try { item.LiaoJieZiXun = item.LiaoJieZiXun.Replace("'", ""); }
            catch { }
            try { item.QiTa = item.QiTa.Replace("'", ""); }
            catch { }
            try { item.YuanYiJieShouMail = item.YuanYiJieShouMail.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<YuYueLiCai> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM YuYueLiCai order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<YuYueLiCai> itemList = new List<YuYueLiCai>();
                foreach (DataRow row in dt.Rows)
                {
                    YuYueLiCai item = new YuYueLiCai();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.Phone = Convert.ToString(row["Phone"]);
                    item.Mail = Convert.ToString(row["Mail"]);
                    item.City = Convert.ToString(row["City"]);
                    item.Address = Convert.ToString(row["Address"]);
                    item.JiaoLiuShiJian = Convert.ToString(row["JiaoLiuShiJian"]);
                    item.LiaoJieZiXun = Convert.ToString(row["LiaoJieZiXun"]);
                    item.QiTa = Convert.ToString(row["QiTa"]);
                    item.YuanYiJieShouMail = Convert.ToString(row["YuanYiJieShouMail"]);
                    item.YuYueDate = Convert.ToDateTime(row["YuYueDate"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<YuYueLiCai> GetInfoByLimit(string order, string type, YuYueLiCai limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from YuYueLiCai where id not in (select top " + index + " ID from YuYueLiCai where 1=1";
            string countSql = "SELECT count(*) FROM YuYueLiCai where 1=1";
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
                if (!string.IsNullOrEmpty(limitItem.Phone))
                {
                    limitStr += " and Phone='" + limitItem.Phone + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Mail))
                {
                    limitStr += " and Mail='" + limitItem.Mail + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.City))
                {
                    limitStr += " and City='" + limitItem.City + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Address))
                {
                    limitStr += " and Address='" + limitItem.Address + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.JiaoLiuShiJian))
                {
                    limitStr += " and JiaoLiuShiJian='" + limitItem.JiaoLiuShiJian + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.LiaoJieZiXun))
                {
                    limitStr += " and LiaoJieZiXun='" + limitItem.LiaoJieZiXun + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.QiTa))
                {
                    limitStr += " and QiTa='" + limitItem.QiTa + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.YuanYiJieShouMail))
                {
                    limitStr += " and YuanYiJieShouMail='" + limitItem.YuanYiJieShouMail + "'";
                }
                if (limitItem.YuYueDate > Convert.ToDateTime("1984-07-28"))
                {
                    limitStr += " and YuYueDate='" + limitItem.YuYueDate + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<YuYueLiCai> itemList = new List<YuYueLiCai>();
                foreach (DataRow row in dt.Rows)
                {
                    YuYueLiCai item = new YuYueLiCai();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.Phone = Convert.ToString(row["Phone"]);
                    item.Mail = Convert.ToString(row["Mail"]);
                    item.City = Convert.ToString(row["City"]);
                    item.Address = Convert.ToString(row["Address"]);
                    item.JiaoLiuShiJian = Convert.ToString(row["JiaoLiuShiJian"]);
                    item.LiaoJieZiXun = Convert.ToString(row["LiaoJieZiXun"]);
                    item.QiTa = Convert.ToString(row["QiTa"]);
                    item.YuanYiJieShouMail = Convert.ToString(row["YuanYiJieShouMail"]);
                    item.YuYueDate = Convert.ToDateTime(row["YuYueDate"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(YuYueLiCai item)
        {
            item = LimitExamine(item);

            string sql = "insert into YuYueLiCai(Name,Phone,Mail,City,Address,JiaoLiuShiJian,LiaoJieZiXun,QiTa,YuanYiJieShouMail,YuYueDate) values('" + item.Name + "','" + item.Phone + "','" + item.Mail + "','" + item.City + "','" + item.Address + "','" + item.JiaoLiuShiJian + "','" + item.LiaoJieZiXun + "','" + item.QiTa + "','" + item.YuanYiJieShouMail + "','" + item.YuYueDate + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(YuYueLiCai item)
        {
            item = LimitExamine(item);

            string sql = "update YuYueLiCai set Name='" + item.Name + "',Phone='" + item.Phone + "',Mail='" + item.Mail + "',City='" + item.City + "',Address='" + item.Address + "',JiaoLiuShiJian='" + item.JiaoLiuShiJian + "',LiaoJieZiXun='" + item.LiaoJieZiXun + "',QiTa='" + item.QiTa + "',YuanYiJieShouMail='" + item.YuanYiJieShouMail + "',YuYueDate='" + item.YuYueDate + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(YuYueLiCai item)
        {
            item = LimitExamine(item);

            string sql = "delete from YuYueLiCai where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM YuYueLiCai";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
    }
}