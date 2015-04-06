using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    public class EventController : BaseController
    {
        [UserToken]
        public JsonResult Get(int id)
        {
            var news = Model.Activity.GetSingle(id);
            if (news == null)
            {
                return JsonResult(new
                {
                    m = -1,
                    e = "活动不存在"
                });
            }

            var fav = Model.Favorites.GetSingle(UID, id, 3);

            return JsonResult(new
            {
                m = 1,
                d = new
                {
                    isFav = fav != null,
                    news.ID,
                    news.TypeID,
                    ImageUrl = FormatImgUrl(news.ImageUrl),
                    news.ThisActivityInfo,
                    news.Name
                }
            });
        }

        public JsonResult GetList(int? tid = null, int pi = 0, int ps = 10)
        {
            var activityList = Model.Activity.GetList(pi, ps, tid);
            List<Activity> hotActivityList = null;

            if (pi == 0)
            {
                hotActivityList = Model.Activity.GetHotList(3, null);
            }

            return JsonResult(new
            {
                m = 1,
                d = new
                {
                    nl = activityList == null ? null : activityList.Select(x => new
                    {
                        x.ID,
                        ImageUrl = FormatImgUrl(x.ImageUrl),
                        x.Name,
                        ActivityDate = x.ActivityDate.ToString("yyyy/MM/dd HH:mm:ss")
                    }),
                    hnl = hotActivityList == null ? null : hotActivityList.Select(x => new
                    {
                        x.Name,
                        ImageUrl = FormatImgUrl(x.ImageUrl),
                        x.ID
                    }),
                    minId = activityList != null && activityList.Count > 0 ? activityList.Min(x => x.ID) : 0
                }
            });
        }


        [UserToken]
        public JsonResult GetFavList(int pi = 0, int ps = 10)
        {
            var favList = Model.Favorites.GetList(UID, 3, pi, ps).Select(x => x.RID).ToList();
            var eventList = Model.Activity.GetListByIds(favList);

            return JsonResult(new
            {
                m = 1,
                d = new
                {
                    nl = eventList == null ? null : eventList.Select(x => new
                    {
                        x.ID,
                        ImageUrl = FormatImgUrl(x.ImageUrl),
                        x.Name,
                        ActivityDate = x.ActivityDate.ToString("yyyy/MM/dd HH:mm:ss")
                    }),
                    minId = eventList != null && eventList.Count > 0 ? eventList.Min(x => x.ID) : 0
                }
            });
        }

        /// <summary>
        /// 预约
        /// </summary>
        /// <returns></returns>
        public JsonResult Reserve(int id, string name, string mobile, string mail, string industry, string address)
        {
            Model.ActivityReserve ar = new ActivityReserve();
            ar.Name = name;
            ar.Mobile = mobile;
            ar.Mail = mail;
            ar.Industry = industry;
            ar.Address = address;
            ar.ReserveActivityID = id;
            ar.ReserveDate = DateTime.Now;

            if (Model.ActivityReserve.Set(ar))
            {
                return JsonResult(new
                {
                    m = 1
                });
            }
            else
            {
                return JsonResult(new
                {
                    m = -1,
                    e = "预约失败"
                });
            }

        }
    }
}
