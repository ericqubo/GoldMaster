using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using WebApp.Filters;

namespace WebApp.Controllers
{
    public class ServiceController : BaseController
    {

        /// <summary>
        /// 基础数据
        /// </summary>
        /// <returns></returns>
        public JsonResult BaseData()
        {
            List<NewsGroup> ngList = Model.NewsGroup.GetList(5, 1);
            List<ActivityType> atList = Model.ActivityType.Get(null, null);

            return JsonResult(new
            {
                m = 1,
                d = new
                {
                    ngList = ngList.Select(x => new { x.ID, x.GroupName }).ToList(),
                    atList = atList.Select(x => new { x.ID, x.Name }).ToList()
                }
            });
        }

        /// <summary>
        /// 获取app版本号
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAppVersion()
        {
            return JsonResult(new {
                m = 1,
                v = ConfigHelper.AppVersion,
                mv = 0,
                c = "有新版本了，为了获得更好的体验获取最新版本",
                vn = "0.0.1",
                u = ConfigHelper.WebDomain + "/apk/GoldMaster.apk",
                n = "GoldMaster.apk"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult AppJson()
        {
            var path = VirtualPathUtility.RemoveTrailingSlash(ConfigHelper.WebDomain);
            var data = new
            {
                css = new List<object>() { 
                    new {
                        path = path + Styles.Url("~/source/bundles/style").ToHtmlString()
                    }//,
                    //new {
                    //    path = path + Styles.Url("~/source/social.umeng/css/bundles/style").ToHtmlString()
                    //}
                },
                js = new List<object>() { 
                    //new {
                    //    path = path + Scripts.Url("~/bundles/lib_1").ToHtmlString()
                    //},
                    new {
                        path = path + Scripts.Url("~/bundles/app").ToHtmlString()                 
                    },
                }
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新闻收藏
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="action">1,收藏，2取消</param>
        /// <returns></returns>
        [UserToken]
        public JsonResult Fav(int id, int type, int action)
        {
            if (type == 1)
                return NewsFav(id, action);
            else if (type == 3)
                return EventFav(id, action);
            else
                return null;
        }

        /// <summary>
        /// 新闻收藏
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="action">1,收藏，2取消</param>
        /// <returns></returns>
        private JsonResult NewsFav(int id, int action)
        {
            var news = Model.News.GetSingle(id.ToString());

            if (news == null)
            {
                return JsonResult(new { m = -1, e = "所收藏的新闻不存在:" + id.ToString() });
            }

            var fav = Model.Favorites.GetSingle(UID, id, 1);
            if (action == 1)
            {
                if (fav == null)
                {
                    fav = new Favorites();
                    fav.RID = news.ID;
                    fav.UID = UID;
                    fav.Type = 1;
                    Model.Favorites.Set(fav);
                    Model.News.IncrementFavCount(news.ID, 1);
                }
            }
            else
            {
                if (fav != null)
                {
                    if (Model.Favorites.Delete(UID, id, 1))
                    {
                        Model.News.IncrementFavCount(news.ID, -1);
                    }
                }
            }

            return JsonResult(new { m = 1 });
        }

        private JsonResult EventFav(int id, int action)
        {
            var activity = Model.Activity.GetSingle(id);

            if (activity == null)
            {
                return JsonResult(new { m = -1, e = "所收藏的活动不存在:" + id.ToString() });
            }

            var fav = Model.Favorites.GetSingle(UID, id, 3);
            if (action == 1)
            {
                if (fav == null)
                {
                    fav = new Favorites();
                    fav.RID = activity.ID;
                    fav.UID = UID;
                    fav.Type = 3;
                    Model.Favorites.Set(fav);
                }
            }
            else
            {
                if (fav != null)
                {
                    if (Model.Favorites.Delete(UID, id, 3))
                    {
                        
                    }
                }
            }

            return JsonResult(new { m = 1 });
        }
    }
}
