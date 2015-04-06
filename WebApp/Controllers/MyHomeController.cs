using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [UserToken]
    public class MyHomeController : BaseController
    {

        /// <summary>
        /// 获取我的收藏列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTypeSum()
        {
            Dictionary<int, int> TypeNumDic = new Dictionary<int, int>();
            TypeNumDic = Model.Favorites.GetSumByType(base.UID);
            List<dynamic> returnList = new List<dynamic>();
            foreach(KeyValuePair<int,int> kvp in TypeNumDic)
            {
                returnList.Add(new { Type=kvp.Key,Count=kvp.Value });
            }
            return JsonResult(new
            {
                m = 1,
                d = returnList
            });
        }
      
    }
}
