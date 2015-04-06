using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Filters;
using WebApp.Lib;

namespace WebApp.Controllers
{
    //[UserToken]
    public class NewsController : BaseController
    {
        [UserToken]
        public JsonResult Get(int id)
        {
            var news = Model.News.GetSingle(id.ToString());

            if (news == null)
            {
                return JsonResult(new
                {
                    m = -1,
                    e = "新闻不存在"
                });
            }

            var fav = Model.Favorites.GetSingle(UID, id, 1);
            return JsonResult(new
            {
                m = 1,
                d = new
                {
                    isFav = fav != null,
                    news.ID,
                    news.GroupId,
                    news.Content,
                    Title = string.IsNullOrWhiteSpace(news.AppTitle) ? news.Title : news.AppTitle,
                    ImgUrl = FormatImgUrl(news.ImgUrl),
                    news.Source,
                    news.Author
                }
            });
        }

        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <returns></returns>
        public JsonResult GetList(int pi = 0, int ps = 10, int? gid = null)
        {
            var newsList = Model.News.GetList(pi, ps, true, gid);
            List<News> hotNewsList = null;

            if (pi == 0)
            {
                hotNewsList = Model.News.GetAppJDTList(3, null);
            }

            return JsonResult(new
            {
                m = 1,
                d = new
                {
                    nl = newsList == null ? null : newsList.Select(x => new
                    {
                        x.ID,
                        ImgUrl = FormatImgUrl(x.ImgUrl),
                        Title = string.IsNullOrWhiteSpace(x.AppTitle) ? x.Title : x.AppTitle,
                        x.Discrption,
                        CreateDate = x.CreateDate.ToString("yyyy/MM/dd HH:mm:ss"),
                        x.FavCount,
                        x.ShareCount
                    }),
                    hnl = hotNewsList == null ? null : hotNewsList.Select(x => new
                    {
                        Title = string.IsNullOrWhiteSpace(x.AppTitle) ? x.Title : x.AppTitle,
                        ImgUrl = FormatImgUrl(x.ImgUrl),
                        x.ID
                    }),
                    minId = newsList != null && newsList.Count > 0 ? newsList.Min(x => x.ID) : 0
                }
            });
        }

        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <returns></returns>
        public JsonResult SeachList(string keyword)
        {
            var newsList = Model.News.SeachList(keyword, 20);    

            return JsonResult(new
            {
                m = 1,
                d = new
                {
                    nl = newsList == null ? null : newsList.Select(x => new
                    {
                        x.ID,
                        ImgUrl = FormatImgUrl(x.ImgUrl),
                        Title = string.IsNullOrWhiteSpace(x.AppTitle) ? x.Title : x.AppTitle,
                        x.Discrption,
                        CreateDate = x.CreateDate.ToString("yyyy/MM/dd HH:mm:ss"),
                        x.FavCount,
                        x.ShareCount
                    }),
                    
                }
            });
        }
        

        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <returns></returns>
        [UserToken]
        public JsonResult GetFavList(int pi = 0, int ps = 10)
        {
            var favList = Model.Favorites.GetList(UID, 1, pi, ps).Select(x => x.RID).ToList();
            var newsList = Model.News.GetListByIds(favList);

            return JsonResult(new
            {
                m = 1,
                d = new
                {
                    nl = newsList == null ? null : newsList.Select(x => new
                    {
                        x.ID,
                        ImgUrl = FormatImgUrl(x.ImgUrl),
                        Title = string.IsNullOrWhiteSpace(x.AppTitle) ? x.Title : x.AppTitle,
                        x.Discrption,
                        CreateDate = x.CreateDate.ToString("yyyy/MM/dd HH:mm:ss"),
                        x.FavCount,
                        x.ShareCount
                    }),
                    minId = newsList != null && newsList.Count > 0 ? newsList.Min(x => x.ID) : 0
                }
            });
        }
    }
}
