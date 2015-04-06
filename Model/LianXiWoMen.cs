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
    public class LianXiWoMen
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string PostCode { get; set; }
        public string AllCitePhone { get; set; }
        public string ZuoBiao { get; set; }

        // 防注入检查
        static LianXiWoMen LimitExamine(LianXiWoMen item)
        {
            if (item == null)
                return null;
            try { item.Name = item.Name.Replace("'", ""); }
            catch { }
            try { item.Address = item.Address.Replace("'", ""); }
            catch { }
            try { item.Phone = item.Phone.Replace("'", ""); }
            catch { }
            try { item.Fax = item.Fax.Replace("'", ""); }
            catch { }
            try { item.Mail = item.Mail.Replace("'", ""); }
            catch { }
            try { item.PostCode = item.PostCode.Replace("'", ""); }
            catch { }
            try { item.AllCitePhone = item.AllCitePhone.Replace("'", ""); }
            catch { }
            try { item.ZuoBiao = item.ZuoBiao.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<LianXiWoMen> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM LianXiWoMen order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<LianXiWoMen> itemList = new List<LianXiWoMen>();
                foreach (DataRow row in dt.Rows)
                {
                    LianXiWoMen item = new LianXiWoMen();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.Address = Convert.ToString(row["Address"]);
                    item.Phone = Convert.ToString(row["Phone"]);
                    item.Fax = Convert.ToString(row["Fax"]);
                    item.Mail = Convert.ToString(row["Mail"]);
                    item.PostCode = Convert.ToString(row["PostCode"]);
                    item.AllCitePhone = Convert.ToString(row["AllCitePhone"]);
                    item.ZuoBiao = Convert.ToString(row["ZuoBiao"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<LianXiWoMen> GetInfoByLimit(string order, string type, LianXiWoMen limitItem, int index, int count, out int sumCount)
        {
            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from LianXiWoMen where id not in (select top " + index + " ID from LianXiWoMen where 1=1";
            string countSql = "SELECT count(*) FROM LianXiWoMen where 1=1";
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
                if (!string.IsNullOrEmpty(limitItem.Address))
                {
                    limitStr += " and Address='" + limitItem.Address + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Phone))
                {
                    limitStr += " and Phone='" + limitItem.Phone + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Fax))
                {
                    limitStr += " and Fax='" + limitItem.Fax + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Mail))
                {
                    limitStr += " and Mail='" + limitItem.Mail + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.PostCode))
                {
                    limitStr += " and PostCode='" + limitItem.PostCode + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.AllCitePhone))
                {
                    limitStr += " and AllCitePhone='" + limitItem.AllCitePhone + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.ZuoBiao))
                {
                    limitStr += " and ZuoBiao='" + limitItem.ZuoBiao + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<LianXiWoMen> itemList = new List<LianXiWoMen>();
                foreach (DataRow row in dt.Rows)
                {
                    LianXiWoMen item = new LianXiWoMen();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.Name = Convert.ToString(row["Name"]);
                    item.Address = Convert.ToString(row["Address"]);
                    item.Phone = Convert.ToString(row["Phone"]);
                    item.Fax = Convert.ToString(row["Fax"]);
                    item.Mail = Convert.ToString(row["Mail"]);
                    item.PostCode = Convert.ToString(row["PostCode"]);
                    item.AllCitePhone = Convert.ToString(row["AllCitePhone"]);
                    item.ZuoBiao = Convert.ToString(row["ZuoBiao"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(LianXiWoMen item)
        {
            item = LimitExamine(item);

            string sql = "insert into LianXiWoMen(Name,Address,Phone,Fax,Mail,PostCode,AllCitePhone,ZuoBiao) values('" + item.Name + "','" + item.Address + "','" + item.Phone + "','" + item.Fax + "','" + item.Mail + "','" + item.PostCode + "','" + item.AllCitePhone + "','" + item.ZuoBiao + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(LianXiWoMen item)
        {
            item = LimitExamine(item);

            string sql = "update LianXiWoMen set Name='" + item.Name + "',Address='" + item.Address + "',Phone='" + item.Phone + "',Fax='" + item.Fax + "',Mail='" + item.Mail + "',PostCode='" + item.PostCode + "',AllCitePhone='" + item.AllCitePhone + "',ZuoBiao='" + item.ZuoBiao + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(LianXiWoMen item)
        {
            item = LimitExamine(item);

            string sql = "delete from LianXiWoMen where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
    }
}