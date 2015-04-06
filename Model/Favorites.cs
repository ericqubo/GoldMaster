using KCommonLib.DBTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Model
{
    public class Favorites
    {
        public int ID { get; set; }
        public int UID { get; set; }
        public int RID { get; set; }
        /// <summary>
        /// 1.新闻，2.杂志, 3.活动
        /// </summary>
        public int Type { get; set; }
        public DateTime CreateDate { get; set; }

        public Favorites()
        {
            CreateDate = DateTime.Now;
        }
        public static int Set(Favorites model)
        {
            string sql = "insert into [Favorites]([UID],[RID],[Type]) values(" + model.UID + "," + model.RID + "," + model.Type + "); select @@IDENTITY";

            var obj = MySQLHelper.GetObject(sql, DBCommon.GetConstr("SqlConnection"));
            try
            {
                model.ID = Convert.ToInt32(obj);
            }
            catch
            {
                model.ID = 0;
            }

            return model.ID;

        }

        // 删除
        public static bool Delete(int uid, int rid, int type)
        {
            string sql = string.Format("delete from Favorites where UID={0} and RID={1} and [Type]={2}", uid, rid, type);
            if (MySQLHelper.GetInt(sql, DBCommon.GetConstr("SqlConnection")) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <param name="type">1.新闻，2.杂志</param>
        /// <returns></returns>
        public static Favorites GetSingle(int uid, int rid, int type)
        {
            string sql = string.Format("SELECT top 1 * FROM Favorites where UID={0} and RID={1} and [Type]={2}", uid, rid, type);
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return GetModel(dt.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// 翻页查询
        /// </summary>
        /// <returns></returns>
        public static List<Favorites> GetList(int uid, int favType, int pageId, int pageSize)
        {
            string idFilter = pageId > 0 ? " and ID < " + pageId : "";
            string sql = string.Format("SELECT top ({0}) * FROM Favorites where  UID={1} and [Type]={2} {3} order by ID desc"
                , pageSize, uid, favType, idFilter);

            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Favorites> itemList = new List<Favorites>();

                foreach (DataRow row in dt.Rows)
                {
                    itemList.Add(GetModel(row));
                }
                return itemList;
            }
            return new List<Favorites>();
        }
        public static Dictionary<int, int> GetSumByType(long uid)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            string sql = "select count(*) as Count,[Type] from [dbo].[Favorites] where uid=" + uid + " group by [Type]";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dic.Add(Convert.ToInt32(row["Type"]), Convert.ToInt32(row["Count"]));
                }
            }
            return dic;
        }

        public static Favorites GetModel(DataRow row)
        {
            Favorites item = new Favorites();
            item.ID = Convert.ToInt32(row["ID"]);
            item.UID = Convert.ToInt32(row["UID"]);
            item.RID = Convert.ToInt32(row["RID"]);
            item.Type = Convert.ToInt32(row["Type"]);
            item.CreateDate = Convert.ToDateTime(row["CreateDate"]);

            return item;
        }
    }

}
