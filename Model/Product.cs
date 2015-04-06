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
    public class Product
    {
        public int ID { get; set; }
        public double yqsyl { get; set; }
        public string name { get; set; }
        public int ishot { get; set; }
        public string fxjg { get; set; }
        public string qx { get; set; }
        public string Zhuangtai { get; set; }
        public string zjtx { get; set; }
        public string cplx { get; set; }
        public string tzqd { get; set; }
        public string dbqk { get; set; }
        public string dzyl { get; set; }
        public string fxgm { get; set; }
        public string sylsm { get; set; }
        public string zjyy { get; set; }
        public string fxkz { get; set; }
        public string xgxx { get; set; }

        // 防注入检查
        static Product LimitExamine(Product item)
        {
            if (item == null)
                return null;
            try { item.name = item.name.Replace("'", ""); }
            catch { }
            try { item.fxjg = item.fxjg.Replace("'", ""); }
            catch { }
            try { item.qx = item.qx.Replace("'", ""); }
            catch { }
            try { item.Zhuangtai = item.Zhuangtai.Replace("'", ""); }
            catch { }
            try { item.zjtx = item.zjtx.Replace("'", ""); }
            catch { }
            try { item.cplx = item.cplx.Replace("'", ""); }
            catch { }
            try { item.tzqd = item.tzqd.Replace("'", ""); }
            catch { }
            try { item.dbqk = item.dbqk.Replace("'", ""); }
            catch { }
            try { item.dzyl = item.dzyl.Replace("'", ""); }
            catch { }
            try { item.fxgm = item.fxgm.Replace("'", ""); }
            catch { }
            try { item.sylsm = item.sylsm.Replace("'", ""); }
            catch { }
            try { item.zjyy = item.zjyy.Replace("'", ""); }
            catch { }
            try { item.fxkz = item.fxkz.Replace("'", ""); }
            catch { }
            try { item.xgxx = item.xgxx.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<Product> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM Product order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Product> itemList = new List<Product>();
                foreach (DataRow row in dt.Rows)
                {
                    Product item = new Product();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.yqsyl = Convert.ToDouble(row["yqsyl"]);
                    item.name = Convert.ToString(row["name"]);
                    item.ishot = Convert.ToInt32(row["ishot"]);
                    item.fxjg = Convert.ToString(row["fxjg"]);
                    item.qx = Convert.ToString(row["qx"]);
                    item.Zhuangtai = Convert.ToString(row["Zhuangtai"]);
                    item.zjtx = Convert.ToString(row["zjtx"]);
                    item.cplx = Convert.ToString(row["cplx"]);
                    item.tzqd = Convert.ToString(row["tzqd"]);
                    item.dbqk = Convert.ToString(row["dbqk"]);
                    item.dzyl = Convert.ToString(row["dzyl"]);
                    item.fxgm = Convert.ToString(row["fxgm"]);
                    item.sylsm = Convert.ToString(row["sylsm"]);
                    item.zjyy = Convert.ToString(row["zjyy"]);
                    item.fxkz = Convert.ToString(row["fxkz"]);
                    item.xgxx = Convert.ToString(row["xgxx"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<Product> GetInfoByLimit(string order, string type, Product limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from Product where id not in (select top " + index + " ID from Product where 1=1";
            string countSql = "SELECT count(*) FROM Product where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (limitItem.yqsyl > 0)
                {
                    limitStr += " and yqsyl='" + limitItem.yqsyl + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.name))
                {
                    limitStr += " and name='" + limitItem.name + "'";
                }
                if (limitItem.ishot > 0)
                {
                    limitStr += " and ishot='" + limitItem.ishot + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.fxjg))
                {
                    limitStr += " and fxjg='" + limitItem.fxjg + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.qx))
                {
                    limitStr += " and qx='" + limitItem.qx + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.Zhuangtai))
                {
                    limitStr += " and Zhuangtai='" + limitItem.Zhuangtai + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.zjtx))
                {
                    limitStr += " and zjtx='" + limitItem.zjtx + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cplx))
                {
                    limitStr += " and cplx='" + limitItem.cplx + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.tzqd))
                {
                    limitStr += " and tzqd='" + limitItem.tzqd + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.dbqk))
                {
                    limitStr += " and dbqk='" + limitItem.dbqk + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.dzyl))
                {
                    limitStr += " and dzyl='" + limitItem.dzyl + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.fxgm))
                {
                    limitStr += " and fxgm='" + limitItem.fxgm + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.sylsm))
                {
                    limitStr += " and sylsm='" + limitItem.sylsm + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.zjyy))
                {
                    limitStr += " and zjyy='" + limitItem.zjyy + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.fxkz))
                {
                    limitStr += " and fxkz='" + limitItem.fxkz + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.xgxx))
                {
                    limitStr += " and xgxx='" + limitItem.xgxx + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Product> itemList = new List<Product>();
                foreach (DataRow row in dt.Rows)
                {
                    Product item = new Product();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.yqsyl = Convert.ToDouble(row["yqsyl"]);
                    item.name = Convert.ToString(row["name"]);
                    item.ishot = Convert.ToInt32(row["ishot"]);
                    item.fxjg = Convert.ToString(row["fxjg"]);
                    item.qx = Convert.ToString(row["qx"]);
                    item.Zhuangtai = Convert.ToString(row["Zhuangtai"]);
                    item.zjtx = Convert.ToString(row["zjtx"]);
                    item.cplx = Convert.ToString(row["cplx"]);
                    item.tzqd = Convert.ToString(row["tzqd"]);
                    item.dbqk = Convert.ToString(row["dbqk"]);
                    item.dzyl = Convert.ToString(row["dzyl"]);
                    item.fxgm = Convert.ToString(row["fxgm"]);
                    item.sylsm = Convert.ToString(row["sylsm"]);
                    item.zjyy = Convert.ToString(row["zjyy"]);
                    item.fxkz = Convert.ToString(row["fxkz"]);
                    item.xgxx = Convert.ToString(row["xgxx"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(Product item)
        {
            item = LimitExamine(item);

            string sql = "insert into Product(yqsyl,name,ishot,fxjg,qx,Zhuangtai,zjtx,cplx,tzqd,dbqk,dzyl,fxgm,sylsm,zjyy,fxkz,xgxx) values('" + item.yqsyl + "','" + item.name + "','" + item.ishot + "','" + item.fxjg + "','" + item.qx + "','" + item.Zhuangtai + "','" + item.zjtx + "','" + item.cplx + "','" + item.tzqd + "','" + item.dbqk + "','" + item.dzyl + "','" + item.fxgm + "','" + item.sylsm + "','" + item.zjyy + "','" + item.fxkz + "','" + item.xgxx + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(Product item)
        {
            item = LimitExamine(item);

            string sql = "update Product set yqsyl='" + item.yqsyl + "',name='" + item.name + "',ishot='" + item.ishot + "',fxjg='" + item.fxjg + "',qx='" + item.qx + "',Zhuangtai='" + item.Zhuangtai + "',zjtx='" + item.zjtx + "',cplx='" + item.cplx + "',tzqd='" + item.tzqd + "',dbqk='" + item.dbqk + "',dzyl='" + item.dzyl + "',fxgm='" + item.fxgm + "',sylsm='" + item.sylsm + "',zjyy='" + item.zjyy + "',fxkz='" + item.fxkz + "',xgxx='" + item.xgxx + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(Product item)
        {
            item = LimitExamine(item);

            string sql = "delete from Product where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM Product";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
    }
}