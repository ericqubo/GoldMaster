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
    public class ZCPZ
    {
        public int ID { get; set; }
        public string jbxx_zsxm { get; set; }
        public string jbxx_sjhm { get; set; }
        public string jbxx_jkzk { get; set; }
        public string jbxx_xfph { get; set; }
        public string jbxx_cshy { get; set; }
        public string jbxx_yyqy { get; set; }
        public string jbxx_srly { get; set; }
        public string tzjl_tzfx { get; set; }
        public string tzjl_tzcp { get; set; }
        public string tzjl_gzwt { get; set; }
        public string tzjl_zxly { get; set; }
        public string cwxx_nsr_brsr { get; set; }
        public string cwxx_nsr_posr { get; set; }
        public string cwxx_nsr_jjsr { get; set; }
        public string cwxx_nsr_lxsr { get; set; }
        public string cwxx_nsr_zjsr { get; set; }
        public string cwxx_nsr_qtsr { get; set; }
        public string cwxx_nsr_zzl { get; set; }
        public string cwxx_nzc_shkx { get; set; }
        public string cwxx_nzc_ylfy { get; set; }
        public string cwxx_nzc_jyfy { get; set; }
        public string cwxx_nzc_bxzc { get; set; }
        public string cwxx_nzc_syf { get; set; }
        public string cwxx_nzc_qt { get; set; }
        public string cwxx_nzc_zzl { get; set; }
        public string cwxx_zc_xjl { get; set; }
        public string cwxx_zc_gdsyl { get; set; }
        public string cwxx_zc_fdsyl { get; set; }
        public string cwxx_zc_bdctz { get; set; }
        public string cwxx_zc_bdczy { get; set; }
        public string cwxx_zc_bx { get; set; }
        public string cwxx_zc_qt { get; set; }
        public string cwxx_zc_ktzgm { get; set; }
        public string cwxx_fzzk_gjj { get; set; }
        public string cwxx_fzzk_grjd { get; set; }
        public string cwxx_fzzk_syzf { get; set; }
        public string cwxx_fzzk_qt { get; set; }
        public string lcmb_mbtzje { get; set; }
        public string lcmb_mbjzrq { get; set; }
        public string lcmb_mbnsyl { get; set; }
        public string lcmb_lcmbms { get; set; }
        public DateTime addDate { get; set; }

        // 防注入检查
        static ZCPZ LimitExamine(ZCPZ item)
        {
            if (item == null)
                return null;
            try { item.jbxx_zsxm = item.jbxx_zsxm.Replace("'", ""); }
            catch { }
            try { item.jbxx_sjhm = item.jbxx_sjhm.Replace("'", ""); }
            catch { }
            try { item.jbxx_jkzk = item.jbxx_jkzk.Replace("'", ""); }
            catch { }
            try { item.jbxx_xfph = item.jbxx_xfph.Replace("'", ""); }
            catch { }
            try { item.jbxx_cshy = item.jbxx_cshy.Replace("'", ""); }
            catch { }
            try { item.jbxx_yyqy = item.jbxx_yyqy.Replace("'", ""); }
            catch { }
            try { item.jbxx_srly = item.jbxx_srly.Replace("'", ""); }
            catch { }
            try { item.tzjl_tzfx = item.tzjl_tzfx.Replace("'", ""); }
            catch { }
            try { item.tzjl_tzcp = item.tzjl_tzcp.Replace("'", ""); }
            catch { }
            try { item.tzjl_gzwt = item.tzjl_gzwt.Replace("'", ""); }
            catch { }
            try { item.tzjl_zxly = item.tzjl_zxly.Replace("'", ""); }
            catch { }
            try { item.cwxx_nsr_brsr = item.cwxx_nsr_brsr.Replace("'", ""); }
            catch { }
            try { item.cwxx_nsr_posr = item.cwxx_nsr_posr.Replace("'", ""); }
            catch { }
            try { item.cwxx_nsr_jjsr = item.cwxx_nsr_jjsr.Replace("'", ""); }
            catch { }
            try { item.cwxx_nsr_lxsr = item.cwxx_nsr_lxsr.Replace("'", ""); }
            catch { }
            try { item.cwxx_nsr_zjsr = item.cwxx_nsr_zjsr.Replace("'", ""); }
            catch { }
            try { item.cwxx_nsr_qtsr = item.cwxx_nsr_qtsr.Replace("'", ""); }
            catch { }
            try { item.cwxx_nsr_zzl = item.cwxx_nsr_zzl.Replace("'", ""); }
            catch { }
            try { item.cwxx_nzc_shkx = item.cwxx_nzc_shkx.Replace("'", ""); }
            catch { }
            try { item.cwxx_nzc_ylfy = item.cwxx_nzc_ylfy.Replace("'", ""); }
            catch { }
            try { item.cwxx_nzc_jyfy = item.cwxx_nzc_jyfy.Replace("'", ""); }
            catch { }
            try { item.cwxx_nzc_bxzc = item.cwxx_nzc_bxzc.Replace("'", ""); }
            catch { }
            try { item.cwxx_nzc_syf = item.cwxx_nzc_syf.Replace("'", ""); }
            catch { }
            try { item.cwxx_nzc_qt = item.cwxx_nzc_qt.Replace("'", ""); }
            catch { }
            try { item.cwxx_nzc_zzl = item.cwxx_nzc_zzl.Replace("'", ""); }
            catch { }
            try { item.cwxx_zc_xjl = item.cwxx_zc_xjl.Replace("'", ""); }
            catch { }
            try { item.cwxx_zc_gdsyl = item.cwxx_zc_gdsyl.Replace("'", ""); }
            catch { }
            try { item.cwxx_zc_fdsyl = item.cwxx_zc_fdsyl.Replace("'", ""); }
            catch { }
            try { item.cwxx_zc_bdctz = item.cwxx_zc_bdctz.Replace("'", ""); }
            catch { }
            try { item.cwxx_zc_bdczy = item.cwxx_zc_bdczy.Replace("'", ""); }
            catch { }
            try { item.cwxx_zc_bx = item.cwxx_zc_bx.Replace("'", ""); }
            catch { }
            try { item.cwxx_zc_qt = item.cwxx_zc_qt.Replace("'", ""); }
            catch { }
            try { item.cwxx_zc_ktzgm = item.cwxx_zc_ktzgm.Replace("'", ""); }
            catch { }
            try { item.cwxx_fzzk_gjj = item.cwxx_fzzk_gjj.Replace("'", ""); }
            catch { }
            try { item.cwxx_fzzk_grjd = item.cwxx_fzzk_grjd.Replace("'", ""); }
            catch { }
            try { item.cwxx_fzzk_syzf = item.cwxx_fzzk_syzf.Replace("'", ""); }
            catch { }
            try { item.cwxx_fzzk_qt = item.cwxx_fzzk_qt.Replace("'", ""); }
            catch { }
            try { item.lcmb_mbtzje = item.lcmb_mbtzje.Replace("'", ""); }
            catch { }
            try { item.lcmb_mbjzrq = item.lcmb_mbjzrq.Replace("'", ""); }
            catch { }
            try { item.lcmb_mbnsyl = item.lcmb_mbnsyl.Replace("'", ""); }
            catch { }
            try { item.lcmb_lcmbms = item.lcmb_lcmbms.Replace("'", ""); }
            catch { }
            return item;
        }

        // 返回所有
        public static List<ZCPZ> Get(string order, string type)
        {
            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";
            string sql = "SELECT * FROM ZCPZ order by " + order + " " + type;
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<ZCPZ> itemList = new List<ZCPZ>();
                foreach (DataRow row in dt.Rows)
                {
                    ZCPZ item = new ZCPZ();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.jbxx_zsxm = Convert.ToString(row["jbxx_zsxm"]);
                    item.jbxx_sjhm = Convert.ToString(row["jbxx_sjhm"]);
                    item.jbxx_jkzk = Convert.ToString(row["jbxx_jkzk"]);
                    item.jbxx_xfph = Convert.ToString(row["jbxx_xfph"]);
                    item.jbxx_cshy = Convert.ToString(row["jbxx_cshy"]);
                    item.jbxx_yyqy = Convert.ToString(row["jbxx_yyqy"]);
                    item.jbxx_srly = Convert.ToString(row["jbxx_srly"]);
                    item.tzjl_tzfx = Convert.ToString(row["tzjl_tzfx"]);
                    item.tzjl_tzcp = Convert.ToString(row["tzjl_tzcp"]);
                    item.tzjl_gzwt = Convert.ToString(row["tzjl_gzwt"]);
                    item.tzjl_zxly = Convert.ToString(row["tzjl_zxly"]);
                    item.cwxx_nsr_brsr = Convert.ToString(row["cwxx_nsr_brsr"]);
                    item.cwxx_nsr_posr = Convert.ToString(row["cwxx_nsr_posr"]);
                    item.cwxx_nsr_jjsr = Convert.ToString(row["cwxx_nsr_jjsr"]);
                    item.cwxx_nsr_lxsr = Convert.ToString(row["cwxx_nsr_lxsr"]);
                    item.cwxx_nsr_zjsr = Convert.ToString(row["cwxx_nsr_zjsr"]);
                    item.cwxx_nsr_qtsr = Convert.ToString(row["cwxx_nsr_qtsr"]);
                    item.cwxx_nsr_zzl = Convert.ToString(row["cwxx_nsr_zzl"]);
                    item.cwxx_nzc_shkx = Convert.ToString(row["cwxx_nzc_shkx"]);
                    item.cwxx_nzc_ylfy = Convert.ToString(row["cwxx_nzc_ylfy"]);
                    item.cwxx_nzc_jyfy = Convert.ToString(row["cwxx_nzc_jyfy"]);
                    item.cwxx_nzc_bxzc = Convert.ToString(row["cwxx_nzc_bxzc"]);
                    item.cwxx_nzc_syf = Convert.ToString(row["cwxx_nzc_syf"]);
                    item.cwxx_nzc_qt = Convert.ToString(row["cwxx_nzc_qt"]);
                    item.cwxx_nzc_zzl = Convert.ToString(row["cwxx_nzc_zzl"]);
                    item.cwxx_zc_xjl = Convert.ToString(row["cwxx_zc_xjl"]);
                    item.cwxx_zc_gdsyl = Convert.ToString(row["cwxx_zc_gdsyl"]);
                    item.cwxx_zc_fdsyl = Convert.ToString(row["cwxx_zc_fdsyl"]);
                    item.cwxx_zc_bdctz = Convert.ToString(row["cwxx_zc_bdctz"]);
                    item.cwxx_zc_bdczy = Convert.ToString(row["cwxx_zc_bdczy"]);
                    item.cwxx_zc_bx = Convert.ToString(row["cwxx_zc_bx"]);
                    item.cwxx_zc_qt = Convert.ToString(row["cwxx_zc_qt"]);
                    item.cwxx_zc_ktzgm = Convert.ToString(row["cwxx_zc_ktzgm"]);
                    item.cwxx_fzzk_gjj = Convert.ToString(row["cwxx_fzzk_gjj"]);
                    item.cwxx_fzzk_grjd = Convert.ToString(row["cwxx_fzzk_grjd"]);
                    item.cwxx_fzzk_syzf = Convert.ToString(row["cwxx_fzzk_syzf"]);
                    item.cwxx_fzzk_qt = Convert.ToString(row["cwxx_fzzk_qt"]);
                    item.lcmb_mbtzje = Convert.ToString(row["lcmb_mbtzje"]);
                    item.lcmb_mbjzrq = Convert.ToString(row["lcmb_mbjzrq"]);
                    item.lcmb_mbnsyl = Convert.ToString(row["lcmb_mbnsyl"]);
                    item.lcmb_lcmbms = Convert.ToString(row["lcmb_lcmbms"]);
                    item.addDate = Convert.ToDateTime(row["addDate"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 条件查询
        public static List<ZCPZ> GetInfoByLimit(string order, string type, ZCPZ limitItem, int index, int count, out int sumCount)
        {

            limitItem = LimitExamine(limitItem);

            if (order == null)
                order = "ID";
            if (type == null)
                type = "DESC";

            string sql = "select top " + count + " * from ZCPZ where id not in (select top " + index + " ID from ZCPZ where 1=1";
            string countSql = "SELECT count(*) FROM ZCPZ where 1=1";
            string limitStr = "";
            if (limitItem != null)
            {
                if (limitItem.ID > 0)
                {
                    limitStr += " and ID='" + limitItem.ID + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.jbxx_zsxm))
                {
                    limitStr += " and jbxx_zsxm='" + limitItem.jbxx_zsxm + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.jbxx_sjhm))
                {
                    limitStr += " and jbxx_sjhm='" + limitItem.jbxx_sjhm + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.jbxx_jkzk))
                {
                    limitStr += " and jbxx_jkzk='" + limitItem.jbxx_jkzk + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.jbxx_xfph))
                {
                    limitStr += " and jbxx_xfph='" + limitItem.jbxx_xfph + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.jbxx_cshy))
                {
                    limitStr += " and jbxx_cshy='" + limitItem.jbxx_cshy + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.jbxx_yyqy))
                {
                    limitStr += " and jbxx_yyqy='" + limitItem.jbxx_yyqy + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.jbxx_srly))
                {
                    limitStr += " and jbxx_srly='" + limitItem.jbxx_srly + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.tzjl_tzfx))
                {
                    limitStr += " and tzjl_tzfx='" + limitItem.tzjl_tzfx + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.tzjl_tzcp))
                {
                    limitStr += " and tzjl_tzcp='" + limitItem.tzjl_tzcp + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.tzjl_gzwt))
                {
                    limitStr += " and tzjl_gzwt='" + limitItem.tzjl_gzwt + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.tzjl_zxly))
                {
                    limitStr += " and tzjl_zxly='" + limitItem.tzjl_zxly + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nsr_brsr))
                {
                    limitStr += " and cwxx_nsr_brsr='" + limitItem.cwxx_nsr_brsr + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nsr_posr))
                {
                    limitStr += " and cwxx_nsr_posr='" + limitItem.cwxx_nsr_posr + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nsr_jjsr))
                {
                    limitStr += " and cwxx_nsr_jjsr='" + limitItem.cwxx_nsr_jjsr + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nsr_lxsr))
                {
                    limitStr += " and cwxx_nsr_lxsr='" + limitItem.cwxx_nsr_lxsr + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nsr_zjsr))
                {
                    limitStr += " and cwxx_nsr_zjsr='" + limitItem.cwxx_nsr_zjsr + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nsr_qtsr))
                {
                    limitStr += " and cwxx_nsr_qtsr='" + limitItem.cwxx_nsr_qtsr + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nsr_zzl))
                {
                    limitStr += " and cwxx_nsr_zzl='" + limitItem.cwxx_nsr_zzl + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nzc_shkx))
                {
                    limitStr += " and cwxx_nzc_shkx='" + limitItem.cwxx_nzc_shkx + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nzc_ylfy))
                {
                    limitStr += " and cwxx_nzc_ylfy='" + limitItem.cwxx_nzc_ylfy + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nzc_jyfy))
                {
                    limitStr += " and cwxx_nzc_jyfy='" + limitItem.cwxx_nzc_jyfy + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nzc_bxzc))
                {
                    limitStr += " and cwxx_nzc_bxzc='" + limitItem.cwxx_nzc_bxzc + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nzc_syf))
                {
                    limitStr += " and cwxx_nzc_syf='" + limitItem.cwxx_nzc_syf + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nzc_qt))
                {
                    limitStr += " and cwxx_nzc_qt='" + limitItem.cwxx_nzc_qt + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_nzc_zzl))
                {
                    limitStr += " and cwxx_nzc_zzl='" + limitItem.cwxx_nzc_zzl + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_zc_xjl))
                {
                    limitStr += " and cwxx_zc_xjl='" + limitItem.cwxx_zc_xjl + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_zc_gdsyl))
                {
                    limitStr += " and cwxx_zc_gdsyl='" + limitItem.cwxx_zc_gdsyl + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_zc_fdsyl))
                {
                    limitStr += " and cwxx_zc_fdsyl='" + limitItem.cwxx_zc_fdsyl + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_zc_bdctz))
                {
                    limitStr += " and cwxx_zc_bdctz='" + limitItem.cwxx_zc_bdctz + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_zc_bdczy))
                {
                    limitStr += " and cwxx_zc_bdczy='" + limitItem.cwxx_zc_bdczy + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_zc_bx))
                {
                    limitStr += " and cwxx_zc_bx='" + limitItem.cwxx_zc_bx + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_zc_qt))
                {
                    limitStr += " and cwxx_zc_qt='" + limitItem.cwxx_zc_qt + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_zc_ktzgm))
                {
                    limitStr += " and cwxx_zc_ktzgm='" + limitItem.cwxx_zc_ktzgm + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_fzzk_gjj))
                {
                    limitStr += " and cwxx_fzzk_gjj='" + limitItem.cwxx_fzzk_gjj + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_fzzk_grjd))
                {
                    limitStr += " and cwxx_fzzk_grjd='" + limitItem.cwxx_fzzk_grjd + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_fzzk_syzf))
                {
                    limitStr += " and cwxx_fzzk_syzf='" + limitItem.cwxx_fzzk_syzf + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.cwxx_fzzk_qt))
                {
                    limitStr += " and cwxx_fzzk_qt='" + limitItem.cwxx_fzzk_qt + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.lcmb_mbtzje))
                {
                    limitStr += " and lcmb_mbtzje='" + limitItem.lcmb_mbtzje + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.lcmb_mbjzrq))
                {
                    limitStr += " and lcmb_mbjzrq='" + limitItem.lcmb_mbjzrq + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.lcmb_mbnsyl))
                {
                    limitStr += " and lcmb_mbnsyl='" + limitItem.lcmb_mbnsyl + "'";
                }
                if (!string.IsNullOrEmpty(limitItem.lcmb_lcmbms))
                {
                    limitStr += " and lcmb_lcmbms='" + limitItem.lcmb_lcmbms + "'";
                }
                if (limitItem.addDate > Convert.ToDateTime("1984-07-28"))
                {
                    limitStr += " and addDate='" + limitItem.addDate + "'";
                }
            }
            countSql += limitStr;
            sql += limitStr + " order by " + order + " " + type + ")" + limitStr + " order by " + order + " " + type + "";

            DataTable dtc = MySQLHelper.GetDataSet(countSql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            sumCount = Convert.ToInt32(dtc.Rows[0][0]);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<ZCPZ> itemList = new List<ZCPZ>();
                foreach (DataRow row in dt.Rows)
                {
                    ZCPZ item = new ZCPZ();
                    item.ID = Convert.ToInt32(row["ID"]);
                    item.jbxx_zsxm = Convert.ToString(row["jbxx_zsxm"]);
                    item.jbxx_sjhm = Convert.ToString(row["jbxx_sjhm"]);
                    item.jbxx_jkzk = Convert.ToString(row["jbxx_jkzk"]);
                    item.jbxx_xfph = Convert.ToString(row["jbxx_xfph"]);
                    item.jbxx_cshy = Convert.ToString(row["jbxx_cshy"]);
                    item.jbxx_yyqy = Convert.ToString(row["jbxx_yyqy"]);
                    item.jbxx_srly = Convert.ToString(row["jbxx_srly"]);
                    item.tzjl_tzfx = Convert.ToString(row["tzjl_tzfx"]);
                    item.tzjl_tzcp = Convert.ToString(row["tzjl_tzcp"]);
                    item.tzjl_gzwt = Convert.ToString(row["tzjl_gzwt"]);
                    item.tzjl_zxly = Convert.ToString(row["tzjl_zxly"]);
                    item.cwxx_nsr_brsr = Convert.ToString(row["cwxx_nsr_brsr"]);
                    item.cwxx_nsr_posr = Convert.ToString(row["cwxx_nsr_posr"]);
                    item.cwxx_nsr_jjsr = Convert.ToString(row["cwxx_nsr_jjsr"]);
                    item.cwxx_nsr_lxsr = Convert.ToString(row["cwxx_nsr_lxsr"]);
                    item.cwxx_nsr_zjsr = Convert.ToString(row["cwxx_nsr_zjsr"]);
                    item.cwxx_nsr_qtsr = Convert.ToString(row["cwxx_nsr_qtsr"]);
                    item.cwxx_nsr_zzl = Convert.ToString(row["cwxx_nsr_zzl"]);
                    item.cwxx_nzc_shkx = Convert.ToString(row["cwxx_nzc_shkx"]);
                    item.cwxx_nzc_ylfy = Convert.ToString(row["cwxx_nzc_ylfy"]);
                    item.cwxx_nzc_jyfy = Convert.ToString(row["cwxx_nzc_jyfy"]);
                    item.cwxx_nzc_bxzc = Convert.ToString(row["cwxx_nzc_bxzc"]);
                    item.cwxx_nzc_syf = Convert.ToString(row["cwxx_nzc_syf"]);
                    item.cwxx_nzc_qt = Convert.ToString(row["cwxx_nzc_qt"]);
                    item.cwxx_nzc_zzl = Convert.ToString(row["cwxx_nzc_zzl"]);
                    item.cwxx_zc_xjl = Convert.ToString(row["cwxx_zc_xjl"]);
                    item.cwxx_zc_gdsyl = Convert.ToString(row["cwxx_zc_gdsyl"]);
                    item.cwxx_zc_fdsyl = Convert.ToString(row["cwxx_zc_fdsyl"]);
                    item.cwxx_zc_bdctz = Convert.ToString(row["cwxx_zc_bdctz"]);
                    item.cwxx_zc_bdczy = Convert.ToString(row["cwxx_zc_bdczy"]);
                    item.cwxx_zc_bx = Convert.ToString(row["cwxx_zc_bx"]);
                    item.cwxx_zc_qt = Convert.ToString(row["cwxx_zc_qt"]);
                    item.cwxx_zc_ktzgm = Convert.ToString(row["cwxx_zc_ktzgm"]);
                    item.cwxx_fzzk_gjj = Convert.ToString(row["cwxx_fzzk_gjj"]);
                    item.cwxx_fzzk_grjd = Convert.ToString(row["cwxx_fzzk_grjd"]);
                    item.cwxx_fzzk_syzf = Convert.ToString(row["cwxx_fzzk_syzf"]);
                    item.cwxx_fzzk_qt = Convert.ToString(row["cwxx_fzzk_qt"]);
                    item.lcmb_mbtzje = Convert.ToString(row["lcmb_mbtzje"]);
                    item.lcmb_mbjzrq = Convert.ToString(row["lcmb_mbjzrq"]);
                    item.lcmb_mbnsyl = Convert.ToString(row["lcmb_mbnsyl"]);
                    item.lcmb_lcmbms = Convert.ToString(row["lcmb_lcmbms"]);
                    item.addDate = Convert.ToDateTime(row["addDate"]);
                    itemList.Add(item);
                }
                return itemList;
            }
            return null;
        }

        // 添加
        public static bool Set(ZCPZ item)
        {
            item = LimitExamine(item);

            string sql = "insert into ZCPZ(jbxx_zsxm,jbxx_sjhm,jbxx_jkzk,jbxx_xfph,jbxx_cshy,jbxx_yyqy,jbxx_srly,tzjl_tzfx,tzjl_tzcp,tzjl_gzwt,tzjl_zxly,cwxx_nsr_brsr,cwxx_nsr_posr,cwxx_nsr_jjsr,cwxx_nsr_lxsr,cwxx_nsr_zjsr,cwxx_nsr_qtsr,cwxx_nsr_zzl,cwxx_nzc_shkx,cwxx_nzc_ylfy,cwxx_nzc_jyfy,cwxx_nzc_bxzc,cwxx_nzc_syf,cwxx_nzc_qt,cwxx_nzc_zzl,cwxx_zc_xjl,cwxx_zc_gdsyl,cwxx_zc_fdsyl,cwxx_zc_bdctz,cwxx_zc_bdczy,cwxx_zc_bx,cwxx_zc_qt,cwxx_zc_ktzgm,cwxx_fzzk_gjj,cwxx_fzzk_grjd,cwxx_fzzk_syzf,cwxx_fzzk_qt,lcmb_mbtzje,lcmb_mbjzrq,lcmb_mbnsyl,lcmb_lcmbms,addDate) values('" + item.jbxx_zsxm + "','" + item.jbxx_sjhm + "','" + item.jbxx_jkzk + "','" + item.jbxx_xfph + "','" + item.jbxx_cshy + "','" + item.jbxx_yyqy + "','" + item.jbxx_srly + "','" + item.tzjl_tzfx + "','" + item.tzjl_tzcp + "','" + item.tzjl_gzwt + "','" + item.tzjl_zxly + "','" + item.cwxx_nsr_brsr + "','" + item.cwxx_nsr_posr + "','" + item.cwxx_nsr_jjsr + "','" + item.cwxx_nsr_lxsr + "','" + item.cwxx_nsr_zjsr + "','" + item.cwxx_nsr_qtsr + "','" + item.cwxx_nsr_zzl + "','" + item.cwxx_nzc_shkx + "','" + item.cwxx_nzc_ylfy + "','" + item.cwxx_nzc_jyfy + "','" + item.cwxx_nzc_bxzc + "','" + item.cwxx_nzc_syf + "','" + item.cwxx_nzc_qt + "','" + item.cwxx_nzc_zzl + "','" + item.cwxx_zc_xjl + "','" + item.cwxx_zc_gdsyl + "','" + item.cwxx_zc_fdsyl + "','" + item.cwxx_zc_bdctz + "','" + item.cwxx_zc_bdczy + "','" + item.cwxx_zc_bx + "','" + item.cwxx_zc_qt + "','" + item.cwxx_zc_ktzgm + "','" + item.cwxx_fzzk_gjj + "','" + item.cwxx_fzzk_grjd + "','" + item.cwxx_fzzk_syzf + "','" + item.cwxx_fzzk_qt + "','" + item.lcmb_mbtzje + "','" + item.lcmb_mbjzrq + "','" + item.lcmb_mbnsyl + "','" + item.lcmb_lcmbms + "','" + item.addDate + "')";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 修改
        public static bool Update(ZCPZ item)
        {
            item = LimitExamine(item);

            string sql = "update ZCPZ set jbxx_zsxm='" + item.jbxx_zsxm + "',jbxx_sjhm='" + item.jbxx_sjhm + "',jbxx_jkzk='" + item.jbxx_jkzk + "',jbxx_xfph='" + item.jbxx_xfph + "',jbxx_cshy='" + item.jbxx_cshy + "',jbxx_yyqy='" + item.jbxx_yyqy + "',jbxx_srly='" + item.jbxx_srly + "',tzjl_tzfx='" + item.tzjl_tzfx + "',tzjl_tzcp='" + item.tzjl_tzcp + "',tzjl_gzwt='" + item.tzjl_gzwt + "',tzjl_zxly='" + item.tzjl_zxly + "',cwxx_nsr_brsr='" + item.cwxx_nsr_brsr + "',cwxx_nsr_posr='" + item.cwxx_nsr_posr + "',cwxx_nsr_jjsr='" + item.cwxx_nsr_jjsr + "',cwxx_nsr_lxsr='" + item.cwxx_nsr_lxsr + "',cwxx_nsr_zjsr='" + item.cwxx_nsr_zjsr + "',cwxx_nsr_qtsr='" + item.cwxx_nsr_qtsr + "',cwxx_nsr_zzl='" + item.cwxx_nsr_zzl + "',cwxx_nzc_shkx='" + item.cwxx_nzc_shkx + "',cwxx_nzc_ylfy='" + item.cwxx_nzc_ylfy + "',cwxx_nzc_jyfy='" + item.cwxx_nzc_jyfy + "',cwxx_nzc_bxzc='" + item.cwxx_nzc_bxzc + "',cwxx_nzc_syf='" + item.cwxx_nzc_syf + "',cwxx_nzc_qt='" + item.cwxx_nzc_qt + "',cwxx_nzc_zzl='" + item.cwxx_nzc_zzl + "',cwxx_zc_xjl='" + item.cwxx_zc_xjl + "',cwxx_zc_gdsyl='" + item.cwxx_zc_gdsyl + "',cwxx_zc_fdsyl='" + item.cwxx_zc_fdsyl + "',cwxx_zc_bdctz='" + item.cwxx_zc_bdctz + "',cwxx_zc_bdczy='" + item.cwxx_zc_bdczy + "',cwxx_zc_bx='" + item.cwxx_zc_bx + "',cwxx_zc_qt='" + item.cwxx_zc_qt + "',cwxx_zc_ktzgm='" + item.cwxx_zc_ktzgm + "',cwxx_fzzk_gjj='" + item.cwxx_fzzk_gjj + "',cwxx_fzzk_grjd='" + item.cwxx_fzzk_grjd + "',cwxx_fzzk_syzf='" + item.cwxx_fzzk_syzf + "',cwxx_fzzk_qt='" + item.cwxx_fzzk_qt + "',lcmb_mbtzje='" + item.lcmb_mbtzje + "',lcmb_mbjzrq='" + item.lcmb_mbjzrq + "',lcmb_mbnsyl='" + item.lcmb_mbnsyl + "',lcmb_lcmbms='" + item.lcmb_lcmbms + "',addDate='" + item.addDate + "' where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        // 删除
        public static bool Delete(ZCPZ item)
        {
            item = LimitExamine(item);

            string sql = "delete from ZCPZ where ID='" + item.ID + "'";
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }
        // 返回总条数
        public static int GetSumCount()
        {
            string sql = "SELECT count(*) FROM ZCPZ";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
    }
}