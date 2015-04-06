using KCommonLib.DBTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Model
{
    public class User
    {
        public int ID { get; set; }
        public string Identity { get; set; }
        public DateTime CreateDate { get; set; }

        public User()
        {
            CreateDate = DateTime.Now;
        }


        // 防注入检查
        static User LimitExamine(User item)
        {
            if (item == null)
                return null;
            try { item.Identity = item.Identity.Replace("'", ""); }
            catch { }
            return item;
        }

        public static int Set(User user)
        {
            user = LimitExamine(user);
            string sql = "insert into [User]([Identity]) values('" + user.Identity + "'); select @@IDENTITY";

            var obj = MySQLHelper.GetObject(sql, DBCommon.GetConstr("SqlConnection"));
            try
            {
                user.ID = Convert.ToInt32(obj);
            }
            catch
            {
                user.ID = 0;
            }

            return user.ID;

        }

        public static User GetByIdentity(string identity)
        {
            identity = identity.Replace("'", "");
            string sql = "SELECT * FROM [User] where [Identity] = '" + identity + "'";
            DataTable dt = MySQLHelper.GetDataSet(sql, DBCommon.GetConstr("SqlConnection")).Tables[0];
            User user = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                user = GetModel(dt.Rows[0]);
            }

            return user;

        }


        private static User GetModel(DataRow row)
        {
            User item = new User();
            item.ID = Convert.ToInt32(row["ID"]);
            item.Identity = row["Identity"].ToString();
            item.CreateDate = Convert.ToDateTime(row["CreateDate"]);

            return item;
        }
    }

}
