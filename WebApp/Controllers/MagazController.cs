using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    /// <summary>
    /// 杂志页
    /// </summary>
    //[UserToken]
    public class MagazController : BaseController
    {
        //
        // GET: /Magaz/
        public JsonResult Get(int id)
        {
            Magaz magaz = Model.Magaz.GetSingle(id);
            if(magaz == null)
            {
                return JsonResult(new
                {
                    m = -1,
                    e = "杂志不存在"
                });
            }
            var pages = magaz.magazPages != null ? magaz.magazPages.Split('|') : null;
            if (pages != null)
            {
                pages = pages.Select(x => FormatImgUrl(x)).ToArray();
            }
            return JsonResult(new
            {
                m = 1,
                d = new 
                {
                    pages = pages,
                    pagesInfo = magaz.magazPagesInfo != null ? magaz.magazPagesInfo.Split('|') : null
                }

            });
        }

        /// <summary>
        /// 获取杂志列表
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public JsonResult GetList(int pi = 0, int ps = 6)
        {
            var magazList = Model.Magaz.GetList(pi, ps);
            if (magazList == null)
            {
                magazList = new List<Magaz>();
            }
            return JsonResult(new
            {
                m = 1,
                d = new
                {
                    l = magazList.Select(x => new
                    {
                        ID = x.ID,
                        Title = x.AppTitle,
                        //Content = x.Content,
                        ImgUrl = FormatImgUrl(x.magazPages.IndexOf('|') > 0 ? x.magazPages.Substring(0, x.magazPages.IndexOf('|')) : x.magazPages),
                        //Desc = x.magazPagesInfo.IndexOf('|') > 0 ? x.magazPagesInfo.Substring(0, x.magazPagesInfo.IndexOf('|')) : x.magazPagesInfo
                    }),
                    minId = magazList != null && magazList.Count > 0 ? magazList.Min(x => x.ID) : 0
                }

            });
        }
        /// <summary>
        /// 获取收藏列表详情
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="pi"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public JsonResult GetListFav(int pi = 0, int ps = 6)
        {
            var favoriteList = Model.Favorites.GetList(base.UID, 2, pi, ps);

            var list = new List<dynamic>();

            if (favoriteList != null)
            {
                var magazIDS = favoriteList.Select(t => t.RID).ToList();
                var magazList = Model.Magaz.GetInfoByIDS(magazIDS);
                return JsonResult(new
                {
                    m = 1,
                    d = new
                    {
                        l = magazList.Select(x => new
                        {
                            Title = x.AppTitle,
                            Content = x.Content,
                            ImgUrl = FormatImgUrl(x.magazPages.IndexOf('|') > 0 ? x.magazPages.Substring(0, x.magazPages.IndexOf('|')) : x.magazPages),
                            Desc = x.magazPagesInfo.IndexOf('|') > 0 ? x.magazPagesInfo.Substring(0, x.magazPagesInfo.IndexOf('|')) : x.magazPagesInfo
                        }),
                        minId = magazList != null && magazList.Count > 0 ? magazList.Min(x => x.ID) : 0

                    },
                    minId = magazList != null && favoriteList.Count > 0 ? favoriteList.Min(x => x.ID) : 0

                });
            }
            return JsonResult(new
            {
                m = 1,
                d = new { l = list, minId = 0 },

            });
        }


    }
}
